using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ship.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Data
{
    public class DefaultDbContext: DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vessel>().HasOne(v => v.Shipowner).WithMany(s => s.Vessels).HasForeignKey(v => v.ShipownerID).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Contract>().HasOne(c => c.Vessel).WithMany(v => v.Contracts).HasForeignKey(c => c.VesselID).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<ServiceRecord>().HasOne(c => c.Vessel).WithMany(v => v.ServiceRecords).HasForeignKey(c => c.VesselID).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<VesselBalance>().HasOne(b => b.Vessel).WithOne(s => s.VesselBalance).HasForeignKey("VesselBalance", "VesselID").OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Wage>().HasOne(w => w.Sailor).WithMany(s => s.Wages).HasForeignKey(w => w.SailorID).OnDelete(DeleteBehavior.ClientSetNull);
        }

        public DbSet<Sailor> Sailor { get; set; }
        public DbSet<Shipowner> Shipowner { get; set; }
        public DbSet<Vessel> Vessel { get; set; }
        public DbSet<Title> Title { get; set; }
        public DbSet<CertificateType> CertificateType { get; set; }
        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamItem> ExamItem { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<Interview> Interview { get; set; }
        public DbSet<ServiceRecord> ServiceRecord { get; set; }
        public DbSet<TrainingClass> TrainingClass { get; set; }
        public DbSet<Trainee> Traine { get; set; }
        public DbSet<LaborSupply> LaborSupply { get; set; }
        public DbSet<LaborSupplyPut> LaborSupplyPut { get; set; }
        public DbSet<LaborSupplyTake> LaborSupplyTake { get; set; }
        public DbSet<Notice> Notice { get; set; }
        public DbSet<VesselBalance> VesselBalance { get; set; }
        public DbSet<VesselAccount> VesselAccount { get; set; }
        public DbSet<UploadFile> UploadFile { get; set; }
        public DbSet<VesselCostPayment> VesselCostPayment { get; set; }
        public DbSet<VesselCertificate> VesselCertificate { get; set; }
        public DbSet<SysCompany> SysCompany { get; set; }
        public DbSet<Wage> Wage { get; set; }
    }
}
