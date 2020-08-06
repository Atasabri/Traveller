﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Traveller.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AboutUsPhoto> AboutUsPhotos { get; set; }
        public virtual DbSet<AboutUsVideo> AboutUsVideos { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Calls_Companies> Calls_Companies { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Cars_Companies> Cars_Companies { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<City_Photos> City_Photos { get; set; }
        public virtual DbSet<City_Rates> City_Rates { get; set; }
        public virtual DbSet<City_Reports> City_Reports { get; set; }
        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Country_Photo_Comments> Country_Photo_Comments { get; set; }
        public virtual DbSet<Country_Photos> Country_Photos { get; set; }
        public virtual DbSet<Country_Video_Comments> Country_Video_Comments { get; set; }
        public virtual DbSet<Country_Videos> Country_Videos { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Fly> Flys { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Place_Comments> Place_Comments { get; set; }
        public virtual DbSet<Place_Likes> Place_Likes { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Places_Photos> Places_Photos { get; set; }
        public virtual DbSet<Places_Videos> Places_Videos { get; set; }
        public virtual DbSet<Report_Comments> Report_Comments { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User_City> User_City { get; set; }
        public virtual DbSet<User_Photos> User_Photos { get; set; }
        public virtual DbSet<User_Rates> User_Rates { get; set; }
        public virtual DbSet<User_Videos> User_Videos { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual int City_Rate(Nullable<double> rate, Nullable<int> city_ID, Nullable<int> user_ID, ObjectParameter returnValue)
        {
            var rateParameter = rate.HasValue ?
                new ObjectParameter("Rate", rate) :
                new ObjectParameter("Rate", typeof(double));
    
            var city_IDParameter = city_ID.HasValue ?
                new ObjectParameter("City_ID", city_ID) :
                new ObjectParameter("City_ID", typeof(int));
    
            var user_IDParameter = user_ID.HasValue ?
                new ObjectParameter("User_ID", user_ID) :
                new ObjectParameter("User_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("City_Rate", rateParameter, city_IDParameter, user_IDParameter, returnValue);
        }
    
        public virtual int EditFCM(Nullable<int> user_id, string fCM)
        {
            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));
    
            var fCMParameter = fCM != null ?
                new ObjectParameter("FCM", fCM) :
                new ObjectParameter("FCM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditFCM", user_idParameter, fCMParameter);
        }
    
        public virtual int Like_Place(Nullable<int> user_ID, Nullable<int> place_ID, ObjectParameter returnValue)
        {
            var user_IDParameter = user_ID.HasValue ?
                new ObjectParameter("User_ID", user_ID) :
                new ObjectParameter("User_ID", typeof(int));
    
            var place_IDParameter = place_ID.HasValue ?
                new ObjectParameter("Place_ID", place_ID) :
                new ObjectParameter("Place_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Like_Place", user_IDParameter, place_IDParameter, returnValue);
        }
    
        public virtual int User_Rate(Nullable<double> rate, Nullable<int> fUser_ID, Nullable<int> tUser_ID, ObjectParameter returnValue)
        {
            var rateParameter = rate.HasValue ?
                new ObjectParameter("Rate", rate) :
                new ObjectParameter("Rate", typeof(double));
    
            var fUser_IDParameter = fUser_ID.HasValue ?
                new ObjectParameter("FUser_ID", fUser_ID) :
                new ObjectParameter("FUser_ID", typeof(int));
    
            var tUser_IDParameter = tUser_ID.HasValue ?
                new ObjectParameter("TUser_ID", tUser_ID) :
                new ObjectParameter("TUser_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("User_Rate", rateParameter, fUser_IDParameter, tUser_IDParameter, returnValue);
        }
    }
}
