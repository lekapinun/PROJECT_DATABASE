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

    public partial class MEMBER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MEMBER()
        {
            this.FOLLOWING = new HashSet<FOLLOWING>();
            this.FOLLOWING1 = new HashSet<FOLLOWING>();
            this.PROMOTE_E = new HashSet<PROMOTE_E>();
            this.PROMOTE_L = new HashSet<PROMOTE_L>();
            this.TICKET = new HashSet<TICKET>();
            this.EVENT = new HashSet<EVENT>();
            this.EVENT1 = new HashSet<EVENT>();
            this.LOCATION = new HashSet<LOCATION>();
        }
    
        public short MEMBER_ID { get; set; }
        [Required(ErrorMessage = "please enter nation id")]
        public long NATIONAL_ID { get; set; }
        [Required(ErrorMessage = "please enter user name")]
        public string USERNAME { get; set; }
        [Required(ErrorMessage = "please enter password")]
        public string PASSWORD { get; set; }
        [Required(ErrorMessage = "please enter name")]
        public string FNAME { get; set; }
        [Required(ErrorMessage = "please enter surname")]
        public string LNAME { get; set; }
        [Required(ErrorMessage = "please enter sex")]
        public string SEX { get; set; }
        [Required(ErrorMessage = "please enter birth day")]
        public Nullable<System.DateTime> BIRTH_DATE { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "please enter address")]
        public string ADDRESS { get; set; } 
        [Required(ErrorMessage = "please enter e-mail")]
        public string E_MAIL { get; set; }
        [Required(ErrorMessage = "please enter phone")]
        public string PHONE { get; set; }
        [Required(ErrorMessage = "please enter credit card")]
        public string CREDIT_CARD { get; set; }
        public string URL_IMG { get; set; }

        [Required(ErrorMessage = "please enter birth day")]
        public string B_DATE { get; set; } = "0";
        [Required(ErrorMessage = "please enter re-password")]
        public string RE_ENTER { get; set; } = "0";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FOLLOWING> FOLLOWING { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FOLLOWING> FOLLOWING1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROMOTE_E> PROMOTE_E { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROMOTE_L> PROMOTE_L { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TICKET> TICKET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EVENT> EVENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EVENT> EVENT1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOCATION> LOCATION { get; set; }
    }
}
