using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Traveller.Models;
using System.Web;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Device.Location;

#region Author
/*
 *[Auth : Ata Sabri Ata Ahmed
 * Version : V_001
 * Info : Api Controller For Manage All Requests & Responses From FreeHands APPS 
 * E-Mail : ata.sabry@rooyadev.com]
 */
#endregion

namespace Traveller.Controllers
{
    public class ManagerController : ApiController
    {
        DB db = new DB();

        //Home _________________________________________________________________________
        [HttpGet]
        public IHttpActionResult GetCountries()
        {
            return Ok(db.Countries.Select(x=>new {x.ID
                ,x.Name,
                x.Name_en,
                x.Log,
                x.Lat,
                Photo= DEL.Domain + "/Uploads/Countries/" + x.ID + ".jpg"
            }));
        }

        [HttpGet]
        public IHttpActionResult GetCities(int Country_ID)
        {
            var Cities = db.Cities.Where(x => x.Country_ID == Country_ID);
            var Data = Cities.Select(x => new
            {
                x.ID,
                x.Name,
                x.Name_en,
                x.Country_ID,
                x.Log,
                x.Lat,
                Photo=DEL.Domain+"/Uploads/Cities/"+x.City_Photos.FirstOrDefault().ID+".jpg",
                BackgroundPhoto = DEL.Domain + "/Uploads/Cities_Background/" + x.ID + ".jpg"
            });
            return Ok(Data);
                
        }

        //Authentication ________________________________________________________________
        [HttpPost]
        public IHttpActionResult Login(string Lan)
        {
            string Name = HttpContext.Current.Request.Form["Name"];
            string Password = HttpContext.Current.Request.Form["Password"];
            string FCM = HttpContext.Current.Request.Form["FCM"];

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            var user = db.Users.Where(x => x.Name == Name && x.Password == Password).Select
                (x => new
                {
                    x.ID,
                    x.Name,
                    x.Token,
                    x.Password,
                    x.Email,
                    x.Phone,
                    x.Country_ID,
                    x.Is_Traveller,
                    x.FaceBook_Link,
                    x.Twitter_Link,
                    x.Google_Link,
                    x.Snap_Link,
                    x.Insta_Link,
                    x.Country.Currency,
                    Photo= DEL.Domain + "/Uploads/Users/" + x.Name + ".jpg"
                }).FirstOrDefault();
                    
            if (user==null)
            {
                return BadRequest(ApiResource.InvalidLogin);
            }
            else
            {
                db.EditFCM(user.ID, FCM);
                db.SaveChanges();
                return Ok(user);
            }
        }
        [HttpPost]
        public IHttpActionResult Login(string Lan,string S)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string FCM = HttpContext.Current.Request.Form["FCM"];
            if (S=="FB")
            {
                #region FB Login
                string Facebook_ID = HttpContext.Current.Request.Form["Facebook_ID"];
                var user = db.Users.Where(x => x.Facebook_ID==Facebook_ID).Select
                (x => new
                {
                 x.ID,
                 x.Name,
                 x.Token,
                 x.Email,
                 x.Phone,
                 x.Facebook_ID,
                 x.Country_ID,
                 x.Is_Traveller,
                 x.FaceBook_Link,
                 x.Twitter_Link,
                 x.Google_Link,
                 x.Snap_Link,
                 x.Insta_Link,
                 x.Country.Currency,
                 Photo= DEL.Domain + "/Uploads/Users/" + x.Name + ".jpg"
                }).FirstOrDefault();

                if (user == null)
                {
                    return BadRequest(ApiResource.Error);
                }
                else
                {
                    db.EditFCM(user.ID, FCM);
                    db.SaveChanges();
                    return Ok(user);
                }
                #endregion
            }
            else if(S=="T")
            {
                #region TWitter Login
                string Twitter_ID = HttpContext.Current.Request.Form["Twitter_ID"];
                var user = db.Users.Where(x => x.Twitter_ID == Twitter_ID).Select
                (x => new
                      {
                        x.ID,
                        x.Name,
                        x.Token,
                        x.Email,
                        x.Phone,
                        x.Twitter_ID,
                        x.Country_ID,
                        x.Is_Traveller,
                    x.FaceBook_Link,
                    x.Twitter_Link,
                    x.Google_Link,
                    x.Snap_Link,
                    x.Insta_Link,
                    x.Country.Currency,
                    Photo= DEL.Domain + "/Uploads/Users/" + x.Name + ".jpg"
                }).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest(ApiResource.Error);
                }
                else
                {
                    db.EditFCM(user.ID, FCM);
                    db.SaveChanges();
                    return Ok(user);
                }
                #endregion
            }else if(S=="G")
            {
                #region Google Login
                string Google_ID = HttpContext.Current.Request.Form["Google_ID"];               
                var user = db.Users.Where(x => x.Google_ID == Google_ID).Select
                         (x => new
                         {
                             x.ID,
                             x.Name,
                             x.Token,
                             x.Email,
                             x.Phone,
                             x.Google_ID,
                             x.Country_ID,
                             x.Is_Traveller,
                             x.FaceBook_Link,
                             x.Twitter_Link,
                             x.Google_Link,
                             x.Snap_Link,
                             x.Insta_Link,
                             x.Country.Currency,
                             Photo= DEL.Domain + "/Uploads/Users/" + x.Name + ".jpg"
                         }).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest(ApiResource.Error);
                }
                else
                {
                    db.EditFCM(user.ID, FCM);
                    db.SaveChanges();
                    return Ok(user);
                }
                #endregion
            }
            return BadRequest(ApiResource.Error);
        }

        //Another Activites
        [HttpPost]
        public IHttpActionResult Register(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            try
            {
                #region Params
                string Name = HttpContext.Current.Request.Form["Name"];
                string Email = HttpContext.Current.Request.Form["Email"];
                string Password = HttpContext.Current.Request.Form["Password"];
                string Phone = HttpContext.Current.Request.Form["Phone"];
                string FCM = HttpContext.Current.Request.Form["FCM"];
                string Code = HttpContext.Current.Request.Form["Code"];
                string FaceBook_ID = HttpContext.Current.Request.Form["FaceBook_ID"];
                string Twitter_ID = HttpContext.Current.Request.Form["Twitter_ID"];
                string Google_ID = HttpContext.Current.Request.Form["Google_ID"];
                int Country_ID = int.Parse(HttpContext.Current.Request.Form["Country_ID"]);
                string Photo =HttpContext.Current.Request.Form["Photo"];
                #endregion                
                if (!Email.Contains("@"))
                {
                    return BadRequest(ApiResource.InvalidEmail);
                }
                if (db.Users.Any(x => x.Name == Name))
                {
                    return BadRequest(ApiResource.UserNameUsed);
                }
                if (db.Users.Any(x => x.Email == Email))
                {
                    return BadRequest(ApiResource.UserEmailUsed);
                }

                User user = new User();
                user.Name = Name;
                string Token = DEL.encrypt(Name);
                user.Token = Token;
                user.Email = Email;
                user.Phone = Phone;
                user.FCM = FCM;
                user.Country_ID = Country_ID;
                user.Currency = db.Countries.Find(Country_ID).Currency;

                #region Check Traveller Code
                if (!string.IsNullOrEmpty(Code))
                {
                    try
                    {
                        int c_id = int.Parse(DEL.Decrypt(Code));
                        Code c = db.Codes.Single(x => x.ID == c_id);
                        if (c == null)
                        {
                            return BadRequest(ApiResource.InValidCode);
                        }
                        else
                        {
                            db.Codes.Remove(c);
                            db.SaveChanges();
                            user.Is_Traveller = true;
                        }
                    }
                    catch
                    {
                        return BadRequest(ApiResource.InValidCode);
                    }                  
                }
                else
                {
                    user.Is_Traveller = false;
                }
                #endregion

                if (FaceBook_ID!=null)
                {
                    user.Facebook_ID = FaceBook_ID;                    
                }
                else if (Twitter_ID!=null)
                {
                    user.Twitter_ID = Twitter_ID;
                }
                else if (Google_ID!=null)
                {
                    user.Google_ID = Google_ID;
                }
                else
                {
                    user.Password = Password;                   
                }
                if (string.IsNullOrEmpty(Photo))
                {
                    return BadRequest(ApiResource.RequiredPhoto);
                }
                else
                {
                    byte[] Base64Photo = Convert.FromBase64String(Photo);
                    File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/Users/" + user.Name + ".jpg"), Base64Photo);
                    user.Photo = DEL.Domain + "/Uploads/Users/" + user.Name + ".jpg";
                }
                db.Users.Add(user);
                db.SaveChanges();
                var Data = new
                {
                    user.ID,
                    user.Name,
                    user.Token,
                    user.Password,
                    user.Email,
                    user.Phone,
                    user.Country_ID,
                    user.Is_Traveller,
                    user.FaceBook_Link,
                    user.Twitter_Link,
                    user.Google_Link,
                    user.Snap_Link,
                    user.Insta_Link,
                    user.Currency,
                    user.Photo
                };
                return Ok(Data);
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }

        [HttpGet]
        public IHttpActionResult City(int City_ID,int User_ID)
        {
            var City = db.Cities.Where(x => x.ID == City_ID);
            var Data=City.Select(x => new
            {
                x.ID,
                x.Name,
                x.Name_en,
                x.Log,
                x.Lat,
                x.Country.Currency,
                x.Country.CurrencyName,
                IsRate=x.City_Rates.Any(r=>r.User_ID==User_ID),
                Background=DEL.Domain+ "/Uploads/Cities_Background/"+x.ID+".jpg",
                Rating = x.City_Rates.Count > 0 ? x.City_Rates.Select(r=>r.Rate).Average() : 0,
                Photos = x.City_Photos.Select(p => new
                {
                    Photo=DEL.Domain+"/Uploads/Cities/"+p.ID+".jpg"
                }),
                Travellers = x.User_City.Select(m => new
                {
                    m.User_ID,
                    m.User.Name,
                    CountryName = m.User.Country.Name,
                    CountryName_en = m.User.Country.Name_en,
                    m.User.Twitter_ID,
                    m.User.Facebook_ID,
                    m.User.Google_ID,
                    Photo = DEL.Domain + "/Uploads/Users/"+m.User.Name+".jpg",
                    Rating=m.User.User_Rates1.Count>0 ?m.User.User_Rates1.Select(r=>r.Rate).Average():0
                }).Distinct().OrderByDescending(o=>o.Rating)
            }).FirstOrDefault();

            if(Data==null)
            {
                return BadRequest();
            }
            return Ok(Data);
        }
        [HttpPost]
        public IHttpActionResult AddReport(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            try
            {
                string Report = HttpContext.Current.Request.Form["Report"];
                int City_ID = int.Parse(HttpContext.Current.Request.Form["City_ID"]);
                City_Reports city_Reports = new City_Reports
                {
                    City_ID = City_ID
                };
                db.City_Reports.Add(city_Reports);
                db.SaveChanges();
                byte[] Base64Report = Convert.FromBase64String(Report);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/City_Reports/" + city_Reports.ID + ".jpg"),Base64Report);
              
                return Ok(new {Message= ApiResource.ReportAdded});
            }
            catch { return BadRequest(ApiResource.Error); }
        }
        [HttpPut]
        public IHttpActionResult Rate_User(int Traveller_ID,string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            double Rate = Convert.ToDouble(HttpContext.Current.Request.Form["Rate"]);
            User user = db.Users.SingleOrDefault(x => x.Token == Token);
            try
            {
                System.Data.Entity.Core.Objects.ObjectParameter ReturnValue = new System.Data.Entity.Core.Objects.ObjectParameter("ReturnValue", typeof(int));
                db.User_Rate(Rate, user.ID ,Traveller_ID,ReturnValue);
                db.SaveChanges();
                if (ReturnValue.Value.ToString() =="0")
                {
                    return BadRequest(ApiResource.Error); ;
                }
                else
                {
                    return Ok(new {Message=ApiResource.RateTraveller });
                }
               
            }
            catch { return BadRequest(ApiResource.Error); }
        }
        [HttpPut]
        public IHttpActionResult Rate_City(int City_ID, string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            double Rate = Convert.ToDouble(HttpContext.Current.Request.Form["Rate"]);
            User user = db.Users.SingleOrDefault(x => x.Token == Token);
            try
            {
                System.Data.Entity.Core.Objects.ObjectParameter ReturnValue = new System.Data.Entity.Core.Objects.ObjectParameter("ReturnValue", typeof(int));
                db.City_Rate(Rate,City_ID,user.ID,ReturnValue);
                db.SaveChanges();
                if (ReturnValue.Value.ToString() == "0")
                {
                    return BadRequest(ApiResource.Error); ;
                }
                else
                {
                    return Ok(new { Message = ApiResource.RateCity });
                }
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }
        [HttpPut]
        public IHttpActionResult Like_Place(int Place_ID, string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            User user = db.Users.SingleOrDefault(x => x.Token == Token);
            try
            {
                System.Data.Entity.Core.Objects.ObjectParameter ReturnValue = new System.Data.Entity.Core.Objects.ObjectParameter("ReturnValue", typeof(int));
                db.Like_Place(user.ID,Place_ID, ReturnValue);
                db.SaveChanges();
                if (ReturnValue.Value.ToString() == "0")
                {
                    return BadRequest(ApiResource.Error); ;
                }
                else
                {
                    return Ok(new { Message = ApiResource.LikePlace });
                }
            }
            catch { return BadRequest(ApiResource.Error); }
        }
        [HttpPut]
        public IHttpActionResult UnLike_Place(int Place_ID, string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                User user = db.Users.SingleOrDefault(x => x.Token == Token);
                Place_Likes Like =db.Place_Likes.SingleOrDefault(x=>x.Place_ID == Place_ID && x.User_ID == user.ID );
                db.Place_Likes.Remove(Like);
                db.SaveChanges();
                return Ok(new { Message = ApiResource.UnLikePlace });
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }


        [HttpPost]
        public IHttpActionResult Search(string Type)
        {
            string Key = HttpContext.Current.Request.Form["Key"];

            if (Type == "User")
            {
                var Data = db.Users.Where(x => x.Name.Contains(Key)).Select(x => new
                {
                    x.ID,
                    x.Name,
                    Photo =DEL.Domain + "/Uploads/Users/" + x.Name + ".jpg",
                    x.Twitter_ID,
                    x.Facebook_ID,
                    x.Google_ID,
                    x.Is_Traveller,
                    CountryName = x.Country.Name,
                });
                return Ok(Data);
            }
            else if (Type == "City")
            {
                var Data = db.Cities.Where(x => x.Name.Contains(Key) || x.Name_en.Contains(Key)).Select(x => new
                {
                    x.ID,
                    x.Name,
                    x.Name_en,
                    x.Lat,
                    x.Log,
                    x.Country_ID,
                    CountryName = x.Country.Name,
                    Photo = DEL.Domain + "/Uploads/Cities/" + x.City_Photos.FirstOrDefault().ID + ".jpg",
                    BackgroundPhoto = DEL.Domain + "/Uploads/Cities_Background/" + x.ID + ".jpg"
                });
                return Ok(Data);
            }
            else if(Type=="Country")
            {
                var Data = db.Countries.Where(x => x.Name.Contains(Key) || x.Name_en.Contains(Key)).Select(x => new
                {
                    x.ID,
                    x.Name,
                    x.Name_en,
                    x.Lat,
                    x.Log,
                    Photo = DEL.Domain + "/Uploads/Countries/" + x.ID + ".jpg"
                });
                return Ok(Data);
            }
            else if (Type == "Place")
            {
                var Data = db.Places.Where(x => x.Name.Contains(Key) || x.Name_en.Contains(Key)).Select(x => new
                {
                    x.ID,
                    x.Name,
                    x.Name_en,
                    x.Lat,
                    x.Log,
                    CityName = x.City.Name,
                    Photo = DEL.Domain + "/Uploads/Places_Photos/" + x.Places_Photos.FirstOrDefault().ID + ".jpg"
                });
                return Ok(Data);
            }
            return BadRequest();
        }
        [HttpPost]
        public IHttpActionResult ConfirmPassword(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Name = HttpContext.Current.Request.Form["Name"];
            User user = db.Users.Where(x => x.Name == Name).FirstOrDefault();
            if (user == null)
            {
                return BadRequest(ApiResource.UserNameNotFound);
            }
            try
            {
                DEL.Send_Password(user.Password, user.Email);
                return Ok(new { Message = ApiResource.CheckEmail });
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }
        [HttpPost]
        public IHttpActionResult Add_Place(string Lan, string NameLan, string DescriptionLan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);

            #region Params
            string Name = HttpContext.Current.Request.Form["Name"];
            double Lat = Convert.ToDouble(HttpContext.Current.Request.Form["Lat"]);
            double Log = Convert.ToDouble(HttpContext.Current.Request.Form["Log"]);
            string Description = HttpContext.Current.Request.Form["Description"];
            string NameInCountry = HttpContext.Current.Request.Form["NameInCountry"];
            string Photo = HttpContext.Current.Request.Form["Photo"];
            string Video = HttpContext.Current.Request.Form["Video"];
            int City_ID = int.Parse(HttpContext.Current.Request.Form["City_ID"]);
            #endregion
            Place place = new Place();
            place.City_ID = City_ID;
            place.Log = Log;
            place.Lat = Lat;
            place.NameInCountry = NameInCountry;
            if (NameLan == "ar")
            {
                place.Name = Name;
                place.Name_en = DEL.TranslateText(Name, "ar|en");
            }
            else
            {
                place.Name_en = DEL.TranslateText(Name, "en");
                place.Name = DEL.TranslateText(place.Name_en, "en|ar");
            }
            if (DescriptionLan == "ar")
            {
                place.Description = Description;
                place.Description_en = DEL.TranslateText(Description, "ar|en");
            }
            else
            {
                place.Description_en = DEL.TranslateText(Description, "en");
                place.Description = DEL.TranslateText(place.Description_en, "en|ar");
            }

            if (!string.IsNullOrEmpty(Photo))
            {
                place.Places_Photos.Add(new Places_Photos { });
            }
            if (!string.IsNullOrEmpty(Video))
            {
                place.Places_Videos.Add(new Places_Videos { });
            }
            db.Places.Add(place);
            db.SaveChanges();
            if (!string.IsNullOrEmpty(Photo))
            {
                byte[] Base64Photo = Convert.FromBase64String(Photo);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/Places_Photos/" + place.Places_Photos.FirstOrDefault().ID + ".jpg"),Base64Photo);
            }
            if (!string.IsNullOrEmpty(Video))
            {
                byte[] Base64Video = Convert.FromBase64String(Video);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/Places_Videos/" + place.Places_Videos.FirstOrDefault().ID + ".mp4"),Base64Video);
            }
            return Ok(new { Message = ApiResource.PlaceAdded });
        }
        [HttpPost]
        public IHttpActionResult Arrange()
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            var REHotels = db.Reservations.Where(x => x.User.Token == Token && x.Type == "Hotel"&&x.Date.CompareTo(DateTime.Now)>0).
                Select(x => new { Lat = x.Lat, Log = x.Log, Days = x.Days, x.Name });

            List<object> List = new List<object>();
            var radiusInKM = 40;

            foreach (var item in REHotels)
            {
                var myLat = item.Lat;
                var myLon = item.Log;
                var Location = new GeoCoordinate(myLat, myLon);

                var data = db.Places.Select(x => new {
                    x.ID,
                    x.Name,
                    x.Name_en,
                    CityName = x.City.Name,
                    CityName_en = x.City.Name_en,
                    x.Lat,
                    Likes=x.Place_Likes.Count,
                    x.Log,
                    Photo = DEL.Domain + "/Uploads/Places_Photos/" + x.Places_Photos.FirstOrDefault().ID + ".jpg",
                })
                   .AsEnumerable().Select(x => new {
                       x.ID,
                       x.Name,
                       x.Name_en,
                       x.CityName,
                       x.CityName_en,
                       x.Photo,
                       x.Likes,
                       x.Lat,
                       x.Log,
                       Hotel = item.Name,
                       Distance = Location.GetDistanceTo(new GeoCoordinate(x.Lat, x.Log)) * 0.001
                   })
                   .Where(p => p.Distance <= radiusInKM).OrderBy(x => x.Distance);
                List.AddRange(
                    data.Take(item.Days).OrderByDescending(x => x.Likes)
                    .Select((x, y) =>new {Data=x,Day=y+1 })
                 );
            }
            return Ok(List);
        }
        [HttpGet]
        public IHttpActionResult Place_Map(int Place_ID)
        {
            Place place = db.Places.Find(Place_ID);
            var Location = new GeoCoordinate(place.Lat, place.Log);
            var Places = db.Places.Select(x=>new {
                x.ID,
                x.Name,
                x.Name_en,
                CityName = x.City.Name,
                CityName_en = x.City.Name_en,
                x.Lat,
                x.Log,
                Photo = DEL.Domain + "/Uploads/Places_Photos/" + x.Places_Photos.FirstOrDefault().ID + ".jpg"
            }).AsEnumerable().Select(x => new
            {
                x.ID,
                x.Name,
                x.Name_en,
                x.CityName,
                x.CityName_en,
                x.Lat,
                x.Log,
                x.Photo,
                Distance = Location.GetDistanceTo(new GeoCoordinate(x.Lat, x.Log)) * 0.001
            }).Where(x => x.Distance < 100).OrderBy(x => x.Distance);

            return Ok(Places);
        }

        //Traveller _________________________________________________________________________
        [HttpGet]
        public  IHttpActionResult Traveller(int ID,int User_ID)
        {
            var user = db.Users.Where(x => x.ID == ID);
                
             var Data=user.Select(x => new
            {
                x.ID,
                x.Name,
                CountryName = x.Country.Name,
                CountryName_en = x.Country.Name_en,
                Photo =DEL.Domain+"/Uploads/Users/"+x.Name+".jpg",
                x.Email,
                x.Phone,
                x.FaceBook_Link,
                x.Twitter_Link,
                x.Google_Link,
                x.Snap_Link,
                x.Insta_Link,
                x.Is_Traveller,
                IsRate=x.User_Rates1.Any(r=>r.FUser_ID==User_ID),
                Rating =x.User_Rates1.Count>0? x.User_Rates1.Select(r=>r.Rate).Average() : 0,
                PhotoCountries = x.User_Photos.Select(p =>new { p.Country_ID, p.Country.Name,p.Country.Name_en }).Distinct(),
                VideoCountries = x.User_Videos.Select(v => new { v.Country_ID, v.Country.Name,v.Country.Name_en }).Distinct()
             }).FirstOrDefault();
            if(Data==null)
            {
                return BadRequest();
            }
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult Traveller_Photos(int ID,int Country_ID)
        {
            var Photos = db.User_Photos.Where(x => x.User_ID == ID&&x.Country_ID==Country_ID);
            var Data = Photos.Select(x => new
            {
                x.ID,
                Photo = DEL.Domain + "/Uploads/User_Photos/" + x.ID + ".jpg",
            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult Traveller_Photo(int ID)
        {
            User_Photos Photo = db.User_Photos.Find(ID);
            if(Photo!=null)
            {
                return Ok(new
                {
                    ID = Photo.ID,
                    Photo = DEL.Domain + "/Uploads/User_Photos/" + ID + ".jpg",
                    Description = Photo.Description,
                    Log = Photo.Log,
                    Lat = Photo.Lat,
                    Location = Photo.Location,
                    Date=Photo.date.ToShortDateString()
                });
            }
            return BadRequest();
        }
        [HttpGet]
        public IHttpActionResult Traveller_Videos(int ID,int Country_ID)
        {
            var Videos = db.User_Videos.Where(x => x.User_ID == ID && x.Country_ID == Country_ID);
            var Data = Videos.Select(x => new
            {
                x.ID,
                Video = DEL.Domain + "/Uploads/User_Videos/" + x.ID + ".mp4",
            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult Traveller_Video(int ID)
        {
            User_Videos Video = db.User_Videos.Find(ID);
            if (Video != null)
            {
                return Ok(new
                {
                    ID = Video.ID,
                    Video = DEL.Domain + "/Uploads/User_Videos/" + ID + ".mp4",
                    Description = Video.Description,
                    Log = Video.Log,
                    Lat = Video.Lat,
                    Location = Video.Location,
                    Date=Video.date.ToShortDateString()
                });
            }
            return BadRequest();
        }
        [HttpGet]
        public IHttpActionResult Traveller_Cities(int ID)
        {
            var Cities = db.User_City.Where(x => x.User_ID == ID);
            var Data = Cities.Select(x => new
            {
                x.ID,
                CityID=x.City.ID,
                x.City.Name,
                x.City.Name_en,
                Resturant = x.Resturant,
                Hotel = x.Hotel,
                Place = x.Place,
            });
            return Ok(Data);
        }

        //Profile ______________________________________________________________________________
        [HttpPost]
        public IHttpActionResult Profile()
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            var user = db.Users.Where(x => x.Token == Token);
                
            var Data=user.Select(x => new
            {
                x.ID,
                x.Name,
                x.Country_ID,
                CountryName = x.Country.Name,
                CountryName_en = x.Country.Name_en,
                Photo =  DEL.Domain + "/Uploads/Users/" + x.Name + ".jpg",
                x.Email,
                x.Phone,
                x.Is_Traveller,
                Rating = x.User_Rates1.Count > 0 ? x.User_Rates1.Select(r => r.Rate).Average() : 0,
                x.FaceBook_Link,
                x.Twitter_Link,
                x.Google_Link, 
                x.Snap_Link,
                x.Insta_Link,
                PhotoCountries=x.User_Photos.Select(p=> new { p.Country_ID, p.Country.Name,p.Country.Name_en }).Distinct(),
                VideoCountries = x.User_Videos.Select(v => new { v.Country_ID, v.Country.Name,v.Country.Name_en }).Distinct()
            }).FirstOrDefault();
            if (Data == null)
            {
                return BadRequest();
            }
            return Ok(Data);
        }
        [HttpPost]
        public IHttpActionResult Profile_Photos(int Country_ID)
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            var Photos = db.User_Photos.Where(x => x.User.Token == Token&&x.Country_ID==Country_ID);
            var Data = Photos.Select(x => new
            {
                x.ID,
                Photo = DEL.Domain + "/Uploads/User_Photos/" + x.ID + ".jpg",
            });
            return Ok(Data);
        }
        [HttpPost]
        public IHttpActionResult Profile_Photo(int ID)
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            var Photo = db.User_Photos.Where(x=>x.ID==ID&&x.User.Token==Token).Select(x=>new {
                x.ID,
                Photo = DEL.Domain + "/Uploads/User_Photos/" + x.ID + ".jpg",
                x.Description,
                x.Lat,               
                x.Log,
                x.Location,
                Date=x.date.Month+"/"+x.date.Day+"/"+x.date.Year
            }).FirstOrDefault();
            if (Photo != null)
            {
                return Ok(Photo);
            }
            return BadRequest();
        }
        [HttpPost]
        public IHttpActionResult Profile_Videos(int Country_ID )
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            var Videos = db.User_Videos.Where(x => x.User.Token == Token && x.Country_ID == Country_ID);
            var Data = Videos.Select(x => new
            {
                x.ID,
                Video = DEL.Domain + "/Uploads/User_Videos/" + x.ID + ".mp4",
            });
            return Ok(Data);
        }
        [HttpPost]
        public IHttpActionResult Profile_Video(int ID)
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            var Video = db.User_Videos.Where(x => x.ID == ID && x.User.Token == Token).Select(x => new {
                x.ID,
                Video = DEL.Domain + "/Uploads/User_Videos/" + x.ID + ".mp4",
                x.Description,
                x.Lat,
                x.Log,
                x.Location,
                Date = x.date.Month + "/" + x.date.Day + "/" + x.date.Year
            }).FirstOrDefault();
            if (Video != null)
            {
                return Ok(Video);
            }
            return BadRequest();
        }
        [HttpPost]
        public IHttpActionResult Profile_Cities()
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            var Cities = db.User_City.Where(x => x.User.Token == Token);
            var Data = Cities.Select(x => new
            {
                x.ID,
                x.City_ID,
                x.City.Name,
                x.City.Name_en,
                Resturant = x.Resturant,
                Hotel = x.Hotel,
                Place = x.Place,
            });
            return Ok(Data);
        }
        [HttpPost]
        public IHttpActionResult AddFavorite(int Country_ID,string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                User user = db.Users.SingleOrDefault(x => x.Token == Token);
                if(user.Favorites.Any(x=>x.Country_ID==Country_ID))
                {
                    return BadRequest(ApiResource.UserAddFavorites);
                }
                Favorite favorite = new Favorite
                {
                    Country_ID = Country_ID,
                    User_ID = user.ID
                };
                db.Favorites.Add(favorite);
                db.SaveChanges();
                return Ok(new {Message=ApiResource.AddedFavorite, ID = favorite.ID });
            }catch
            {
                return BadRequest(ApiResource.Error);
            }
        }
        [HttpPost]
        public IHttpActionResult DeleteFavorite(int ID,string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                Favorite favorite = db.Favorites.SingleOrDefault(x => x.ID == ID && x.User.Token == Token);
                db.Favorites.Remove(favorite);
                db.SaveChanges();
                return Ok(new {Message=ApiResource.DeleteFavorite });
            }catch
            {
                return BadRequest(ApiResource.Error);
            }
        }
        [HttpGet]
        public IHttpActionResult Favorites(int ID)
        {
            var Data = db.Favorites.Where(x => x.User_ID == ID).Select(x => new
            {
                x.ID,
                x.Country.Name,
                x.Country.Name_en,
                x.Country_ID
            });
            return Ok(Data);
        }

        [HttpPut]
        public IHttpActionResult EditProfile(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            try
            {
                #region Params
                string Token = HttpContext.Current.Request.Form["Token"];
                string Email = HttpContext.Current.Request.Form["Email"];
                string Facebook_Link = HttpContext.Current.Request.Form["Facebook_Link"];
                string Twitter_Link = HttpContext.Current.Request.Form["Twitter_Link"];
                string Google_Link = HttpContext.Current.Request.Form["Google_Link"];
                string Snap_Link = HttpContext.Current.Request.Form["Snap_Link"];
                string Insta_Link = HttpContext.Current.Request.Form["Insta_Link"];
                string Password = HttpContext.Current.Request.Form["Password"];
                string Phone = HttpContext.Current.Request.Form["Phone"];
                string Country_ID = HttpContext.Current.Request.Form["Country_ID"];
                string Photo = HttpContext.Current.Request.Form["Photo"];
                #endregion

                var users = db.Users.Where(x => x.Token == Token);
                User user = users.FirstOrDefault();
                if (user == null)
                {
                    return BadRequest();
                }
                if (!string.IsNullOrEmpty(Email))
                    user.Email = Email;
                if (Facebook_Link!=null)
                    user.FaceBook_Link = Facebook_Link;
                if (Twitter_Link!=null)
                    user.Twitter_Link = Twitter_Link;
                if (Google_Link!=null)
                    user.Google_Link = Google_Link;
                if (Snap_Link!=null)
                    user.Snap_Link = Snap_Link;
                if (Insta_Link!=null)
                    user.Insta_Link = Insta_Link;
                if (!string.IsNullOrEmpty(Phone))
                    user.Phone = Phone;
                if (!string.IsNullOrEmpty(Password))
                {
                    if (string.IsNullOrEmpty(user.Facebook_ID) && string.IsNullOrEmpty(user.Google_ID) && string.IsNullOrEmpty(user.Twitter_ID))
                    {
                        user.Password = Password;
                    }
                }
                    
                if (!string.IsNullOrEmpty(Country_ID))
                    user.Country_ID = int.Parse(Country_ID);

                if (Photo!=null)
                {
                        byte[] Base64Photo = Convert.FromBase64String(Photo);
                        File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/Users/" + user.Name + ".jpg"), Base64Photo);
                }
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var Data = users.Select(x => new
                {
                    x.ID,
                    x.Name,
                    x.Country.Currency,
                    x.Token,
                    x.Password,
                    x.Email,
                    x.Phone,
                    x.Country_ID,
                    x.Is_Traveller,
                    x.FaceBook_Link,
                    x.Twitter_Link,
                    x.Google_Link,
                    Photo = DEL.Domain + "/Uploads/Users/" + x.Name + ".jpg"
                }).FirstOrDefault();
                return Ok(Data);
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }

        [HttpPost]
        public IHttpActionResult AddPhoto(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            string Description = HttpContext.Current.Request.Form["Description"];
            double Log = Convert.ToDouble(HttpContext.Current.Request.Form["Log"]);
            double Lat =Convert.ToDouble(HttpContext.Current.Request.Form["Lat"]);
            string Location = HttpContext.Current.Request.Form["Location"];
            DateTime Date =Convert.ToDateTime(HttpContext.Current.Request.Form["Date"]);
            int Country_ID = int.Parse(HttpContext.Current.Request.Form["Country_ID"]);
            string Photo = HttpContext.Current.Request.Form["Photo"];
            User user = db.Users.SingleOrDefault(x => x.Token == Token);
            User_Photos User_Photo = new User_Photos
            {
                User_ID=user.ID,
                Description= Description,
                Location=Location,
                Log=Log,
                Lat=Lat,
                date=Date,
                Country_ID=Country_ID
            };
            if(!string.IsNullOrEmpty(Photo))
            {
                db.User_Photos.Add(User_Photo);
                db.SaveChanges();
                byte[] Base64Photo = Convert.FromBase64String(Photo);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/User_Photos/"+ User_Photo.ID+ ".jpg"),Base64Photo);
                return Ok(new {Message=ApiResource.AddPhoto, Photo_ID = User_Photo.ID });
            }
            return BadRequest(ApiResource.Error);
        }
        [HttpPost]
        public IHttpActionResult AddVideo(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            string Description = HttpContext.Current.Request.Form["Description"];
            double Log = Convert.ToDouble(HttpContext.Current.Request.Form["Log"]);
            double Lat = Convert.ToDouble(HttpContext.Current.Request.Form["Lat"]);
            string Location = HttpContext.Current.Request.Form["Location"];
            DateTime Date = Convert.ToDateTime(HttpContext.Current.Request.Form["Date"]);
            int Country_ID = int.Parse(HttpContext.Current.Request.Form["Country_ID"]);
            string Video = HttpContext.Current.Request.Form["Video"];
            User user = db.Users.SingleOrDefault(x => x.Token == Token);
            User_Videos User_Video = new User_Videos
            {
                User_ID = user.ID,
                Description = Description,
                Location = Location,
                Log = Log,
                Lat = Lat,
                date=Date,
                Country_ID=Country_ID
            };
            if (!string.IsNullOrEmpty(Video))
            {
                db.User_Videos.Add(User_Video);
                db.SaveChanges();
                byte[] Base64Video = Convert.FromBase64String(Video);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/User_Videos/" + User_Video.ID + ".mp4"),Base64Video);
                return Ok(new { Message = ApiResource.AddVideo, Video_ID = User_Video.ID });
            }
            return BadRequest(ApiResource.Error);
        }

        [HttpPut]
        public IHttpActionResult EditPhoto(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            string Description = HttpContext.Current.Request.Form["Description"];
            string Log = HttpContext.Current.Request.Form["Log"];
            string Lat =HttpContext.Current.Request.Form["Lat"];
            string Location = HttpContext.Current.Request.Form["Location"];
            string Date = HttpContext.Current.Request.Form["Date"];
            string Country_ID = HttpContext.Current.Request.Form["Country_ID"];
            string Photo = HttpContext.Current.Request.Form["Photo"];
            int Photo_ID = int.Parse(HttpContext.Current.Request.Form["Photo_ID"]);
            User_Photos User_Photo = db.User_Photos.SingleOrDefault(x=>x.ID==Photo_ID&&x.User.Token==Token);
            if(User_Photo!=null)
            {
                if(!string.IsNullOrEmpty(Description))
                User_Photo.Description = Description;
                if (!string.IsNullOrEmpty(Log))
                    User_Photo.Log =Convert.ToDouble(Log);
                if (!string.IsNullOrEmpty(Lat))
                    User_Photo.Lat =Convert.ToDouble(Lat);
                if (!string.IsNullOrEmpty(Location))
                    User_Photo.Location = Location;
                if (!string.IsNullOrEmpty(Date))
                    User_Photo.date = Convert.ToDateTime(Date);
                if (!string.IsNullOrEmpty(Country_ID))
                    User_Photo.Country_ID = int.Parse(Country_ID);

                db.Entry(User_Photo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                if(!string.IsNullOrEmpty(Photo))
                {
                    byte[] Base64Photo = Convert.FromBase64String(Photo);
                    File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/User_Photos/" + User_Photo.ID + ".jpg"),Base64Photo);
                }
                return Ok(new { Message = ApiResource.EditPhoto });
            }
            return BadRequest();
        }
        [HttpPut]
        public IHttpActionResult EditVideo(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            string Description = HttpContext.Current.Request.Form["Description"];
            string Log = HttpContext.Current.Request.Form["Log"];
            string Lat = HttpContext.Current.Request.Form["Lat"];
            string Location = HttpContext.Current.Request.Form["Location"];
            string Date = HttpContext.Current.Request.Form["Date"];
            string Country_ID = HttpContext.Current.Request.Form["Country_ID"];
            string Video = HttpContext.Current.Request.Form["Video"];
            int Video_ID = int.Parse(HttpContext.Current.Request.Form["Video_ID"]);
            User_Videos User_Video = db.User_Videos.SingleOrDefault(x=>x.ID==Video_ID&&x.User.Token==Token);
            if (User_Video != null)
            {
                if (!string.IsNullOrEmpty(Description))
                    User_Video.Description = Description;
                if (!string.IsNullOrEmpty(Log))
                    User_Video.Log = Convert.ToDouble(Log);
                if (!string.IsNullOrEmpty(Lat))
                    User_Video.Lat = Convert.ToDouble(Lat);
                if (!string.IsNullOrEmpty(Location))
                    User_Video.Location = Location;
                if (!string.IsNullOrEmpty(Date))
                    User_Video.date = Convert.ToDateTime(Date);
                if (!string.IsNullOrEmpty(Country_ID))
                    User_Video.Country_ID = int.Parse(Country_ID);

                db.Entry(User_Video).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                if (!string.IsNullOrEmpty(Video))
                {
                    byte[] Base64Video = Convert.FromBase64String(Video);
                    File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/User_Videos/" + User_Video.ID + ".mp4"),Base64Video);
                }
                return Ok(new { Message = ApiResource.EditVideo });
            }
            return BadRequest(ApiResource.Error);
        }

        [HttpPost]
        public IHttpActionResult DeletePhoto(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            int Photo_ID = int.Parse(HttpContext.Current.Request.Form["Photo_ID"]);
            User_Photos User_Photo = db.User_Photos.SingleOrDefault(x=>x.ID==Photo_ID&&x.User.Token==Token);
            if(User_Photo!=null)
            {
                db.User_Photos.Remove(User_Photo);
                db.SaveChanges();
                FileInfo F = new FileInfo(HttpContext.Current.Server.MapPath("~/Uploads/User_Photos/"+ Photo_ID + ".jpg"));
                if(F.Exists)
                {
                    F.Delete();
                }
                return Ok(new { Message = ApiResource.DeletePhoto });
            }
            return BadRequest(ApiResource.Error);
        }
        [HttpPost]
        public IHttpActionResult DeleteVideo(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            int Video_ID = int.Parse(HttpContext.Current.Request.Form["Video_ID"]);
            User_Videos User_Video = db.User_Videos.SingleOrDefault(x=>x.ID==Video_ID&&x.User.Token==Token);
            if (User_Video != null)
            {
                db.User_Videos.Remove(User_Video);
                db.SaveChanges();
                FileInfo F = new FileInfo(HttpContext.Current.Server.MapPath("~/Uploads/User_Videos/" + Video_ID + ".mp4"));
                if (F.Exists)
                {
                    F.Delete();
                }
                return Ok(new { Message = ApiResource.EditVideo });
            }
            return BadRequest(ApiResource.Error);
        }

        //User Visits _____________________________________________________________________
        [HttpPost]
        public IHttpActionResult AddTravellerCity()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string Hotel = HttpContext.Current.Request.Form["Hotel"];
                string Resturant = HttpContext.Current.Request.Form["Resturant"];
                string Place = HttpContext.Current.Request.Form["Place"];
                int City_ID = int.Parse(HttpContext.Current.Request.Form["City_ID"]);
                

                User user = db.Users.Single(x => x.Token == Token);
                User_City User_City = new User_City();
                User_City.User_ID = user.ID;
                User_City.City_ID = City_ID;
                User_City.Place = Place;
                User_City.Resturant = Resturant;
                User_City.Hotel = Hotel;
                db.User_City.Add(User_City);
                db.SaveChanges();
                var Data = new
                {
                    ID = User_City.ID,
                    Hotel = User_City.Hotel,
                    Place = User_City.Place,
                    Resturant = User_City.Resturant,
                };
                return Ok(Data);
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }
        [HttpPost]
        public IHttpActionResult DeleteVisit(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            int Visit_ID = int.Parse(HttpContext.Current.Request.Form["Visit_ID"]);
            User_City User_City = db.User_City.SingleOrDefault(x => x.ID == Visit_ID && x.User.Token == Token);
            if (User_City != null)
            {
                db.User_City.Remove(User_City);
                db.SaveChanges();
                return Ok(new {Message=ApiResource.DeleteVisit });
            }
            return BadRequest(ApiResource.Error);
        }
        [HttpPut]
        public IHttpActionResult EditVisit(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);

            string Token = HttpContext.Current.Request.Form["Token"];
            string Hotel = HttpContext.Current.Request.Form["Hotel"];
            string Place = HttpContext.Current.Request.Form["Place"];
            string  Resturant = HttpContext.Current.Request.Form["Resturant"];
            string City_ID= HttpContext.Current.Request.Form["City_ID"];
            int Visit_ID = int.Parse(HttpContext.Current.Request.Form["Visit_ID"]);
            User_City User_City = db.User_City.SingleOrDefault(x => x.ID == Visit_ID && x.User.Token == Token);

            if (User_City != null)
            {
                if(!string.IsNullOrEmpty(Hotel))
                    User_City.Hotel = Hotel;
                if (!string.IsNullOrEmpty(Place))
                    User_City.Place = Place;
                if (!string.IsNullOrEmpty(Resturant))
                    User_City.Resturant = Resturant;
                if (!string.IsNullOrEmpty(City_ID))
                    User_City.City_ID =int.Parse(City_ID);

                db.Entry(User_City).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok(new {Message=ApiResource.EditVisit });
            }
            return BadRequest(ApiResource.Error);
        }



        //RE ________________________________________________________________________________
        [HttpPost]
        public IHttpActionResult AddRE(string Type, string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            #region Params
            string Token = HttpContext.Current.Request.Form["Token"];
            string Name = HttpContext.Current.Request.Form["Name"];
            int Days = int.Parse(HttpContext.Current.Request.Form["Days"]);
            DateTime Date = Convert.ToDateTime(HttpContext.Current.Request.Form["Date"]);
            TimeSpan Time = TimeSpan.Parse(HttpContext.Current.Request.Form["Time"]);
            double Log = Convert.ToDouble(HttpContext.Current.Request.Form["Log"]);
            double Lat = Convert.ToDouble(HttpContext.Current.Request.Form["Lat"]);
            #endregion

            User user = db.Users.Single(x => x.Token == Token);
            Reservation RE = new Reservation
            {
                Name=Name,
                Date = Date,
                Days = Days,
                Lat = Lat,
                Log = Log,
                Type = Type,
                Time = Time,
                User_ID = user.ID
            };
            db.Reservations.Add(RE);
            db.SaveChanges();
            return Ok(new { Message = ApiResource.ReSuccess });
        }
        [HttpPost]
        public IHttpActionResult Reservations()
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            var RE = db.Reservations.Where(x => x.User.Token == Token);
            var Data = RE.AsEnumerable().Select(x => new
            {
                x.ID,
                x.Name,
                x.Days,
                x.Type,
                x.Lat,
                x.Log,
                x.Time,
                Date=x.Date.ToShortDateString(),
            });
            return Ok(Data);
        }
        [HttpPut]
        public IHttpActionResult EditRE(string Type, string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            #region Params
            string Token = HttpContext.Current.Request.Form["Token"];
            string Name = HttpContext.Current.Request.Form["Name"];
            int RE_ID = int.Parse(HttpContext.Current.Request.Form["RE_ID"]);
            string Days = HttpContext.Current.Request.Form["Days"];
            string Date = HttpContext.Current.Request.Form["Date"];
            string Time = HttpContext.Current.Request.Form["Time"];
            string Log = HttpContext.Current.Request.Form["Log"];
            string Lat = HttpContext.Current.Request.Form["Lat"];
            #endregion

            var RE = db.Reservations.Where(x => x.User.Token == Token && x.ID == RE_ID).FirstOrDefault();
            if (RE == null)
            {
                return BadRequest(ApiResource.Error);
            }
            if(!string.IsNullOrEmpty(Date))
            RE.Date =Convert.ToDateTime(Date);
            if (!string.IsNullOrEmpty(Days))
                RE.Days =int.Parse(Days);
            if (!string.IsNullOrEmpty(Time))
                RE.Time =TimeSpan.Parse(Time);
            if (!string.IsNullOrEmpty(Log))
                RE.Log =Convert.ToDouble(Log);
            if (!string.IsNullOrEmpty(Lat))
                RE.Lat = Convert.ToDouble(Lat);
            if (!string.IsNullOrEmpty(Type))
                RE.Type = Type;

            db.Entry(RE).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok(new { Messsage = ApiResource.EditRE });
        }
        [HttpPost]
        public IHttpActionResult DeleteRE(string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            int RE_ID = int.Parse(HttpContext.Current.Request.Form["RE_ID"]);
            var RE = db.Reservations.Where(x => x.User.Token == Token && x.ID == RE_ID).FirstOrDefault();
            if (RE == null)
            {
                return BadRequest(ApiResource.Error);
            }
            db.Reservations.Remove(RE);
            db.SaveChanges();
            return Ok(new { Message = ApiResource.DeleteRE });
        }


        //Country Data ________________________________________________________________________
        [HttpGet]
        public IHttpActionResult CountryNumbers(int Country_ID)
        {
            var Country = db.Countries.Where(x => x.ID == Country_ID);
                
             var Data= Country.Select(x => new
            {
               x.Ambulance_Number,
               x.Police_Number,
               x.Fire_Number

            }).FirstOrDefault();
            if(Data!=null)
            {
                return Ok(Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult CountryCalls(int Country_ID)
        {
            var Companies = db.Calls_Companies.Where(x => x.Country_ID == Country_ID);

            var Data = Companies.Select(x => new
            {
                x.ID,
                x.Name,
                x.Name_en,
                x.Help_Number,
                x.Link,
                x.Code,
                Photo = DEL.Domain + "/Uploads/Calls_Companies/" + x.ID + ".jpg"
            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult CountryHistory(int Country_ID)
        {
            var Country = db.Countries.Where(x => x.ID == Country_ID);

            var Data = Country.Select(x => new
            {
                x.History,
                x.History_en,
                HistoryPDF = DEL.Domain + "/Uploads/Countries/HistoryPDF/" + x.ID + ".pdf",

            }).FirstOrDefault();
            if (Data != null)
            {
                return Ok(Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult CountryRoles(int Country_ID)
        {
            var Country = db.Countries.Where(x => x.ID == Country_ID);

            var Data = Country.Select(x => new
            {
                x.Roles,
                x.Roles_en,
                RolesPDF = DEL.Domain + "/Uploads/Countries/RolesPDF/" + x.ID + ".pdf",

            }).FirstOrDefault();
            if (Data != null)
            {
                return Ok(Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult CountryOffers(int Country_ID)
        {
            var Offers = db.Offers.Where(x => 
                            x.Country_ID == Country_ID
                            && DateTime.Now.CompareTo(x.FromDate)>0
                            && x.ToDate.CompareTo(DateTime.Now)>0);

            var Data = Offers.AsEnumerable().Select(x => new
            {
                x.ID,
                FromDate=x.FromDate.ToString("MM/dd/yyyy"),
                ToDate=x.ToDate.ToString("MM/dd/yyyy"),
                x.Days_Number,
                x.Visitors_Number,
                x.Currency,
                x.Price,
                x.Description,
                x.Description_en,
                Photo = DEL.Domain + "/Uploads/Offers/" + x.ID + ".jpg"
            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult Offer(int Offer_ID)
        {
            var offer = db.Offers.Where(x => x.ID == Offer_ID).Select(x => new
            {
                x.ID,
                FromDate=x.FromDate.Month+"/"+x.FromDate.Day+"/"+x.FromDate.Year,
                ToDate=x.ToDate.Month+"/"+x.ToDate.Day+"/"+x.ToDate.Year,
                x.Days_Number,
                x.Visitors_Number,
                x.Currency,
                x.Price,
                x.Description,
                x.Description_en,
                Photo = DEL.Domain + "/Uploads/Offers/" + x.ID + ".jpg"
            }).FirstOrDefault();
            return Ok(offer);
        }
        [HttpGet]
        public IHttpActionResult AllOffers()
        {
            var Offers = db.Offers.Where(x =>DateTime.Now.CompareTo(x.FromDate) > 0
                            && x.ToDate.CompareTo(DateTime.Now) > 0);

            var Data = Offers.AsEnumerable().Select(x => new
            {
                x.ID,
                x.Days_Number,
                FromDate = x.FromDate.ToString("MM/dd/yyyy"),
                ToDate = x.ToDate.ToString("MM/dd/yyyy"),
                x.Visitors_Number,
                x.Currency,
                x.Price,
                x.Description,
                x.Description_en,
                x.Country.Name,
                x.Country.Name_en,
                Photo = DEL.Domain + "/Uploads/Offers/" + x.ID + ".jpg"
            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult CountryCars(int Country_ID)
        {
            var Cars = db.Cars_Companies.Where(x => x.Country_ID == Country_ID);

            var Data = Cars.Select(x => new
            {
                x.ID,
                x.Name,
                x.Name_en,
                x.Link,
                Photo = DEL.Domain + "/Uploads/Car_Companies/" + x.ID + ".jpg"
            });
            return Ok(Data);
        }

        //City Data ___________________________________________________________________________
        [HttpGet]
        public IHttpActionResult CityLagLat(int City_ID)
        {
            var CityData = db.Cities.Where(x => x.ID == City_ID);
                
             var Data= CityData.Select(x => new
            {
                x.ID,
                x.Log,
                x.Lat,              

            }).FirstOrDefault();
            if(Data!=null)
            {
                return Ok(Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult CityPlaces(int City_ID,int User_ID)
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            User user = db.Users.Where(x => x.Token == Token).FirstOrDefault();

            var Places = db.Places.Where(x => x.City_ID == City_ID);

            var Data = Places.Select(x => new
            {
                x.ID,
                x.Lat,
                x.Log,
                x.Name,
                x.Name_en,
                x.NameInCountry,
                IsLike=x.Place_Likes.Any(r=>r.User_ID==User_ID),
                Likes=x.Place_Likes.Count,
                CityName = x.City.Name,
                CityName_en = x.City.Name_en,
                Photos = x.Places_Photos.Select(PH => new
                {
                    PH.ID,
                    Photo = DEL.Domain + "/Uploads/Places_Photos/" + PH.ID + ".jpg"
                }),
                Videos = x.Places_Videos.Select(V => new
                {
                    V.ID,
                    Video = DEL.Domain + "/Uploads/Places_Videos/" + V.ID + ".mp4"
                })

            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult CityCarsHotelFlys(int City_ID)
        {
            var CityData = db.Cities.Where(x => x.ID == City_ID);

            var Data = CityData.Select(x => new
            {              
                    Hotels = x.Hotels.Select(H => new
                    {
                        H.ID,
                        H.Name,
                        H.Name_en,
                        H.Link,
                        Photo = DEL.Domain + "/Uploads/Hotels/" + H.ID + ".jpg"
                    }),
                    Flys = x.Flys.Select(H => new
                    {
                        H.ID,
                        H.Name,
                        H.Name_en,
                        H.Link,
                        Photo = DEL.Domain + "/Uploads/Flys/" + H.ID + ".jpg"
                    }),
                    Cars = x.Cars.Select(H => new
                    {
                        H.ID,
                        H.Name,
                        H.Name_en,
                        H.Link,
                        Photo = DEL.Domain + "/Uploads/Cars/" + H.ID + ".jpg"
                    }),

            }).FirstOrDefault();
            if (Data != null)
            {
                return Ok(Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult CityReports(int City_ID)
        {
            var Reports = db.City_Reports.Where(x => x.City_ID == City_ID);
            var Data = Reports.Select(x => new
            {
                x.ID,
                Photo=DEL.Domain+"/Uploads/City_Reports/"+x.ID+".jpg",
            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult Report(int Report_ID)
        {
            var Reports = db.City_Reports.Where(x => x.ID == Report_ID);
            var Data = Reports.Select(x => new
            {
                x.ID,
                Photo = DEL.Domain + "/Uploads/City_Reports/" + x.ID + ".jpg",
                Comments = x.Report_Comments.Select(c => new
                {
                    c.ID,
                    c.Comment,
                    c.User_ID,
                    c.User.Name,
                    User_Photo = DEL.Domain + "/Uploads/Users/" + c.User.Name + ".jpg",
                    c.User.Facebook_ID,
                    c.User.Google_ID,
                    c.User.Twitter_ID
                })
            }).FirstOrDefault();
            if(Data!=null)
            {
                return Ok(Data);
            }
            return BadRequest();
        }
        [HttpPost]
        public IHttpActionResult PlaceData(int Place_ID,int User_ID)
        {
            string Token = HttpContext.Current.Request.Form["Token"];
            User user = db.Users.Where(x => x.Token == Token).FirstOrDefault();

            var Places = db.Places.Where(x=>x.ID==Place_ID);
            var Data = Places.Select(x => new
            {
                x.ID,
                x.Lat,
                x.Log,
                x.Name,
                x.Name_en,
                x.NameInCountry,
                IsLike = x.Place_Likes.Any(r => r.User_ID == User_ID),
                Likes =x.Place_Likes.Count,
                x.Description,
                x.Description_en,
                isLike = x.Place_Likes.Any(y => y.User_ID == user.ID),
                CityName = x.City.Name,
                CityName_en = x.City.Name_en,
                Photos = x.Places_Photos.Select(PH => new
                {
                    PH.ID,
                    Photo = DEL.Domain + "/Uploads/Places_Photos/" + PH.ID + ".jpg"
                }),
                Videos = x.Places_Videos.Select(V => new
                {
                    V.ID,
                    Video = DEL.Domain + "/Uploads/Places_Videos/" + V.ID + ".mp4"
                }),
                Comments = x.Place_Comments.Select(c => new
                {
                    c.ID,
                    c.Comment,
                    c.User_ID,
                    c.User.Name,
                    User_Photo=DEL.Domain + "/Uploads/Users/" + c.User.Name + ".jpg",
                    c.User.Facebook_ID,
                    c.User.Twitter_ID,
                    c.User.Google_ID
                })
            }).FirstOrDefault();
            if(Data!=null)
            {
                return Ok(Data);
            }
            return BadRequest();
        }
        [HttpPost]
        public IHttpActionResult CommentReport(int Report_ID)
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string Comment = HttpContext.Current.Request.Form["Comment"];
                User user = db.Users.Single(x => x.Token == Token);
                Report_Comments Report_Comment = new Report_Comments
                {
                    Report_ID = Report_ID,
                    User_ID = user.ID,
                    Comment = Comment
                };
                db.Report_Comments.Add(Report_Comment);
                db.SaveChanges();
                return Ok(new { Comment_ID= Report_Comment.ID });
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }
        [HttpPost]
        public IHttpActionResult CommentPlace(int Place_ID)
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string Comment = HttpContext.Current.Request.Form["Comment"];
                User user = db.Users.Single(x => x.Token == Token);
                Place_Comments Place_Comment = new Place_Comments
                {
                    Place_ID = Place_ID,
                    User_ID = user.ID,
                    Comment = Comment
                };
                db.Place_Comments.Add(Place_Comment);
                db.SaveChanges();
                return Ok(new { Comment_ID = Place_Comment.ID });
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }
        [HttpPost]
        public IHttpActionResult DeleteCommentReport(int Report_ID,string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            int Comment_ID =int.Parse(HttpContext.Current.Request.Form["Comment_ID"]);

            var Comment = db.Report_Comments.FirstOrDefault(x => x.User.Token == Token && 
            x.ID == Comment_ID&&x.Report_ID==Report_ID);

            if(Comment==null)
            {
                return BadRequest();
            }
            db.Report_Comments.Remove(Comment);
            db.SaveChanges();
            return Ok(new {Message=ApiResource.DeleteComment });
        }
        [HttpPost]
        public IHttpActionResult DeleteCommentPlace(int Place_ID, string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            string Token = HttpContext.Current.Request.Form["Token"];
            int Comment_ID = int.Parse(HttpContext.Current.Request.Form["Comment_ID"]);
            var Comment = db.Place_Comments.FirstOrDefault(x => x.User.Token == Token &&
            x.ID == Comment_ID && x.Place_ID == Place_ID);

            if (Comment == null)
            {
                return BadRequest();
            }
            db.Place_Comments.Remove(Comment);
            db.SaveChanges();
            return Ok(new { Message = ApiResource.DeleteComment });
        }


        //About Us Photos && Videos ______________________________________________________________

        [HttpGet]
        public IHttpActionResult AboutUsPhotos()
        {
            var Data = db.AboutUsPhotos.Select(x => new
            {
                x.ID,
                x.Description,
                x.Description_en,
                Photo=DEL.Domain+"/Uploads/AboutUsPhotos/"+x.ID+".jpg"
            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult AboutUsVideos()
        {
            var Data = db.AboutUsVideos.Select(x => new
            {
                x.ID,
                x.Description,
                x.Description_en,
                Video = DEL.Domain + "/Uploads/AboutUsVideos/" + x.ID + ".mp4"
            });
            return Ok(Data);
        }


        //Travel Services Photos && Videos ________________________________________________________

        [HttpGet]
        public IHttpActionResult Services_Photos(int Country_ID)
        {
            var Data = db.Country_Photos.Where(x => x.Country_ID == Country_ID).Select(x => new
            {
                x.ID,
                x.Description,
                x.Description_en,
                Photo=DEL.Domain+ "/Uploads/Country_Photos/"+x.ID+".jpg"
            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult Services_Photo_Comments(int Photo_ID)
        {
            var Data = db.Country_Photo_Comments.Where(x => x.Country_Photo_ID == Photo_ID).Select(x => new
            {
                x.ID,
                x.Comment,
                x.User.Name,
                Photo=DEL.Domain+"/Uploads/Users/"+ x.User.Name + ".jpg"
            });
            return Ok(Data);
        }
        [HttpPost]
        public IHttpActionResult Add_ServicePhoto_Comment(int Photo_ID)
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string Comment = HttpContext.Current.Request.Form["Comment"];
                User user = db.Users.SingleOrDefault(x => x.Token == Token);
                Country_Photo_Comments com = new Country_Photo_Comments()
                {
                    User_ID = user.ID,
                    Country_Photo_ID = Photo_ID,
                    Comment = Comment
                };
                db.Country_Photo_Comments.Add(com);
                db.SaveChanges();
                return Ok(new { ID = com.ID });
            }catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult Delete_ServicePhoto_Comment(int Comment_ID,string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string Comment = HttpContext.Current.Request.Form["Comment"];
                Country_Photo_Comments com = db.Country_Photo_Comments.Where(x => x.User.Token == Token && x.ID == Comment_ID).FirstOrDefault();
                db.Country_Photo_Comments.Remove(com);
                db.SaveChanges();
                return Ok(new { Message=ApiResource.DeleteComment });
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }

        [HttpGet]
        public IHttpActionResult Services_Videos(int Country_ID)
        {
            var Data = db.Country_Videos.Where(x => x.Country_ID == Country_ID).Select(x => new
            {
                x.ID,
                x.Description,
                x.Description_en,
                Video = DEL.Domain + "/Uploads/Country_Videos/" + x.ID + ".mp4"
            });
            return Ok(Data);
        }
        [HttpGet]
        public IHttpActionResult Services_Video_Comments(int Video_ID)
        {
            var Data = db.Country_Video_Comments.Where(x => x.Country_Video_ID == Video_ID).Select(x => new
            {
                x.ID,
                x.Comment,
                x.User.Name,
                Photo = DEL.Domain + "/Uploads/Users/" + x.User.Name + ".jpg"
            });
            return Ok(Data);
        }
        [HttpPost]
        public IHttpActionResult Add_ServiceVideo_Comment(int Video_ID)
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string Comment = HttpContext.Current.Request.Form["Comment"];
                User user = db.Users.SingleOrDefault(x => x.Token == Token);
                Country_Video_Comments com = new Country_Video_Comments()
                {
                    User_ID = user.ID,
                     Country_Video_ID= Video_ID,
                    Comment = Comment
                };
                db.Country_Video_Comments.Add(com);
                db.SaveChanges();
                return Ok(new { ID = com.ID });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult Delete_ServiceVideo_Comment(int Comment_ID, string Lan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lan);
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string Comment = HttpContext.Current.Request.Form["Comment"];
                Country_Video_Comments com = db.Country_Video_Comments.Where(x => x.User.Token == Token && x.ID == Comment_ID).FirstOrDefault();
                db.Country_Video_Comments.Remove(com);
                db.SaveChanges();
                return Ok(new { Message = ApiResource.DeleteComment });
            }
            catch
            {
                return BadRequest(ApiResource.Error);
            }
        }




        //Transalte
        [HttpPost]
        public IHttpActionResult Transalte(string pairs)
        {
            string Text = HttpContext.Current.Request.Form["Text"];

            return Ok(DEL.TranslateText(Text, pairs));
        }

    }
}
