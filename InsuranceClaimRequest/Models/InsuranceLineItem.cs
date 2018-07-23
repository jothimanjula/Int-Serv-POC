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
    
    
    public partial class InsuranceLineItem
    {
        public int InsurerLineItemId { get; set; }
        public string InsurerId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BillDate { get; set; }

        public string ClaimItemDescription { get; set; }
        public decimal AmountClaimed { get; set; }
        public bool BenefitEmergency { get; set; }
        public Nullable<int> BenefitId { get; set; }
        public decimal BenefitAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
    
        public virtual Benefit Benefit { get; set; }
        public virtual Insurance Insurance { get; set; }
    }
}
