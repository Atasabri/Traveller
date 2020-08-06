//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Place
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Place()
        {
            this.Place_Comments = new HashSet<Place_Comments>();
            this.Place_Likes = new HashSet<Place_Likes>();
            this.Places_Photos = new HashSet<Places_Photos>();
            this.Places_Videos = new HashSet<Places_Videos>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Name_en { get; set; }
        public string Description { get; set; }
        public string Description_en { get; set; }
        public double Log { get; set; }
        public double Lat { get; set; }
        public int City_ID { get; set; }
        public string NameInCountry { get; set; }
    
        public virtual City City { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Place_Comments> Place_Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Place_Likes> Place_Likes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Places_Photos> Places_Photos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Places_Videos> Places_Videos { get; set; }
    }
}
