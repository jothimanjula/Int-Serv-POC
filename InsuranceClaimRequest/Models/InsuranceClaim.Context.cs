﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InsuranceClaimEntites : DbContext
    {
        public InsuranceClaimEntites()
            : base("name=InsuranceClaimEntites")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<InsuranceLineItem> InsuranceLineItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
