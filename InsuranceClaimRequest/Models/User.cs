//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsuranceClaimRequest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter Login Id.")]
        [Display(Name = "Login Id")]
        [StringLength(30)]
        public string LoginId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public Nullable<System.DateTime> Orgl_Stamp { get; set; }
        public string Orgl_User { get; set; }
        public Nullable<System.DateTime> Updt_Stamp { get; set; }
        public string Updt_User { get; set; }
        public string Expired { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(20)]
        public string password { get; set; }
    }
}
