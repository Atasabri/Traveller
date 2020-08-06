using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Web.Script.Serialization;
using Traveller.Models;
using System.Drawing;

namespace Traveller
{

    public class DEL
    {
        private static string CipherKey = ConfigurationManager.AppSettings["Cipher"].ToString();
        public static string Domain = ConfigurationManager.AppSettings["Domain"].ToString();
        
        /// <summary>
        /// Encrypt A string And Retuen Encryption String
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string encrypt(string encryptString)
        {
            string EncryptionKey = CipherKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        /// <summary>
        /// Return A string After Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = CipherKey;
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
           });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        /// Thsi Function For Translate Text
        /// </summary>
        /// <param name="input"></param>
        /// <param name="languagePair"></param>
        /// <returns></returns>
        public static string TranslateText(string input, string languagePair)
        {
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
            WebClient webClient = new WebClient();
            if(languagePair=="ar|en")
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
            }
            else
            {
                webClient.Encoding = System.Text.Encoding.GetEncoding(1256);
            }
            string result = webClient.DownloadString(url);
            result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
            result = result.Substring(result.IndexOf("TRANSLATED_TEXT='") + 17);
            result = result.Substring(0, result.IndexOf("';"));
            return result.Trim();
        }

        /// <summary>
        /// Send Html E-Mail To Muli-Users
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="file"></param>
        /// <param name="To"></param>
        public static void Send_Mail(string Subject, HttpPostedFileBase file, List<string> To)
        {
            string From = ConfigurationManager.AppSettings["Email"].ToString();
            string Pass = ConfigurationManager.AppSettings["Password"].ToString();
            string Host = ConfigurationManager.AppSettings["Host"].ToString();
            int Port = int.Parse(ConfigurationManager.AppSettings["Port"].ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(From);
            foreach (var item in To)
            {
                if (item.Contains("@"))
                {
                    mail.To.Add(item);
                }
            }
            mail.Subject = Subject;
            StreamReader read = new StreamReader(file.InputStream);
            mail.Body = read.ReadToEnd();
            mail.IsBodyHtml = true;
            ///-------------------------------------------------------------------------//
            SmtpClient smtpMail = new SmtpClient();
            smtpMail.EnableSsl = false;
            smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpMail.Host = Host;
            smtpMail.Port = Port;

            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = new NetworkCredential(From, Pass);
            ///-------------------------------------------------------------------------//
            smtpMail.Send(mail);
        }
       
        /// <summary>
        /// Send Password To E-Mail
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="file"></param>
        /// <param name="To"></param>
        public static void Send_Password(string UserPass,string To)
        {
            string From = ConfigurationManager.AppSettings["Email"].ToString();
            string Pass = ConfigurationManager.AppSettings["Password"].ToString();
            string Host = ConfigurationManager.AppSettings["Host"].ToString();
            int Port = int.Parse(ConfigurationManager.AppSettings["Port"].ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(From);
            mail.To.Add(To);
            mail.Subject = "Traveller Confirmation Password";

            mail.Body = "Hi , Your Password Is : "+UserPass;
            ///-------------------------------------------------------------------------//
            SmtpClient smtpMail = new SmtpClient();
            smtpMail.EnableSsl = false;
            smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpMail.Host = Host;
            smtpMail.Port = Port;

            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = new NetworkCredential(From, Pass);
            ///-------------------------------------------------------------------------//
            smtpMail.Send(mail);
        }
        
        /// <summary>
        /// This Function For Send Notifications For FCM Mobile Users
        /// </summary>
        /// <param name="devices"></param>
        /// <param name="body"></param>
        /// <param name="title"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="city"></param>
        public static void Notify(string[] devices,string body,string title,int id,string type, object city=null)
        {
            string SERVER_API_KEY = ConfigurationManager.AppSettings["SERVER_API_KEY"];
            var SENDER_ID = ConfigurationManager.AppSettings["SENDER_ID"];
            try
            {
                var applicationID = SERVER_API_KEY;
                var senderId = SENDER_ID;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    registration_ids = devices,
                    notification = new
                    {
                        body = body,
                        title = title,
                        icon = "http://traveler-apps.com/images/favicon.png"
                    },
                    data =new
                    {
                        id=id,
                        city=city,
                        type=type
                    },
                    priority = "high"

                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
       
        /// <summary>
        /// This Function For Compress Photo Size
        /// </summary>
        /// <param name="path"></param>
        /// <param name="photo"></param>
        public static void PhotoCompress(string path,HttpPostedFileBase photo)
        {
            using (var image = System.Drawing.Image.FromStream(photo.InputStream))
            {
                var newWidth = (int)(image.Width * .3);
                var newHeight = (int)(image.Height * .3);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(path);
            }
        }
    }
}