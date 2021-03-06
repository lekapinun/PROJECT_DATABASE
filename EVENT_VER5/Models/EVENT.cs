//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EVENT_VER5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EVENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENT()
        {
            this.TICKET = new HashSet<TICKET>();
            this.MEMBER = new HashSet<MEMBER>();
            this.MEMBER1 = new HashSet<MEMBER>();
        }
    
        public short EVENT_ID { get; set; }
        [Required(ErrorMessage = "please enter event name")]
        public string EVENT_NAME { get; set; }
        [Required(ErrorMessage = "please enter event category")]
        public string CATEGORY { get; set; }
        public string DETAIL { get; set; }
        public string PICTURE { get; set; }
        public string VIDEO { get; set; }
        //[Required(ErrorMessage = "please enter start event time")]
        public Nullable<System.DateTime> TIME_START_E { get; set; }
        //[Required(ErrorMessage = "please enter end event time")]
        public Nullable<System.DateTime> TIME_END_E { get; set; }
        public Nullable<byte> CONDITION_MIN_AGE { get; set; }
        public Nullable<byte> CONDITION_MAX_AGE { get; set; }
        public string CONDITION_SEX { get; set; }
        public Nullable<short> SOLD_OUT_SEAT { get; set; }
        [Required(ErrorMessage = "please enter max seat")]
        public Nullable<short> MAX_SEAT { get; set; }
        [Required(ErrorMessage = "please enter ticket price")]
        public Nullable<short> PRICE { get; set; }
        public Nullable<short> PROMOTE_E_ID { get; set; }
        [Required(ErrorMessage = "please enter location")]
        public string LOCATION { get; set; }

        public string Owner_member { get; set; }
        public DateTime S_DATE { get; set; }
        public DateTime E_DATE { get; set; }
        public TimeSpan S_TIME { get; set; }
        public TimeSpan E_TIME { get; set; }

        public virtual PROMOTE_E PROMOTE_E { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TICKET> TICKET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBER> MEMBER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBER> MEMBER1 { get; set; }
    }
}
