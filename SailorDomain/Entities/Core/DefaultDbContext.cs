using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SailorDomain.Entities
{
    public class DefaultDbContext:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //在数据库中生成的表名为单数
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vessel>().HasRequired(v => v.Shipowner).WithMany(s => s.Vessels).HasForeignKey(v => v.ShipownerID).WillCascadeOnDelete(false);
        }

        public DbSet<Sailor> Sailors { get; set; }
        public DbSet<Shipowner> Shipowners { get; set; }
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<CertificateType> CertificateTypes { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamItem> ExamItems { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        public DbSet<TrainingClass> TrainingClasses { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<LaborSupply> LaborSupplies { get; set; }
        public DbSet<LaborSupplyPut> LaborSupplyPuts { get; set; }
        public DbSet<LaborSupplyTake> LaborSupplyTakes { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<VesselBalance> VesselBalances { get; set; }
        public DbSet<VesselAccount> VesselAccounts { get; set; }
        public DbSet<UploadFile> UploadFiles { get; set; }
        public DbSet<VesselCostPayment> VesselCostPayments { get; set; }
        public DbSet<VesselCertificate> VesselCertificates { get; set; }
        public DbSet<SysCompany> SysCompanies { get; set; }
        public DbSet<Wage> Wages { get; set; } 
    }
}