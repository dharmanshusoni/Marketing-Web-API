using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models;

public partial class MarketingToolContext : DbContext
{
    public MarketingToolContext()
    {
    }

    public MarketingToolContext(DbContextOptions<MarketingToolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateDocument> CandidateDocuments { get; set; }

    public virtual DbSet<CityMaster> CityMasters { get; set; }

    public virtual DbSet<ClientDocument> ClientDocuments { get; set; }

    public virtual DbSet<CompanyClient> CompanyClients { get; set; }

    public virtual DbSet<CountryMaster> CountryMasters { get; set; }

    public virtual DbSet<GenderMaster> GenderMasters { get; set; }

    public virtual DbSet<IndustryMaster> IndustryMasters { get; set; }

    public virtual DbSet<LanguageMaster> LanguageMasters { get; set; }

    public virtual DbSet<QualificationMaster> QualificationMasters { get; set; }

    public virtual DbSet<SkillMaster> SkillMasters { get; set; }

    public virtual DbSet<StateMaster> StateMasters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MarketingToolV1;Trusted_Connection=True;TrustServerCertificate=true;");
    //=> optionsBuilder.UseSqlServer("Server=SQL5108.site4now.net;Initial Catalog=db_a8da31_marketing;User Id=db_a8da31_marketing_admin;Password=Market@123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.ToTable("Agent");

            entity.Property(e => e.AgentId).HasColumnName("agentId");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.AgentDetail)
                .HasMaxLength(500)
                .HasColumnName("agentDetail");
            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(13)
                .HasColumnName("contactNo");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(50)
                .HasColumnName("contactPerson");
            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(500)
                .HasColumnName("profileImageURL");
            entity.Property(e => e.StateId).HasColumnName("stateId");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(10)
                .HasColumnName("zipcode");
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.ToTable("Candidate");

            entity.Property(e => e.CandidateId).HasColumnName("candidateId");
            entity.Property(e => e.ContactNo).HasMaxLength(15);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Experience).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LanguagesIds).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SkillsIds)
                .HasMaxLength(50)
                .HasColumnName("skillsIds");
            entity.Property(e => e.Zipcode).HasMaxLength(10);
        });

        modelBuilder.Entity<CandidateDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId);

            entity.ToTable("CandidateDocument");

            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.CandidateId).HasColumnName("candidateId");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");
            entity.Property(e => e.UploadDate)
                .HasColumnType("datetime")
                .HasColumnName("uploadDate");
        });

        modelBuilder.Entity<CityMaster>(entity =>
        {
            entity.ToTable("CityMaster");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.State).WithMany(p => p.CityMasters)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_CityMaster_StateMaster");
        });

        modelBuilder.Entity<ClientDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId);

            entity.ToTable("ClientDocument");

            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");
            entity.Property(e => e.UploadDate)
                .HasColumnType("datetime")
                .HasColumnName("uploadDate");
        });

        modelBuilder.Entity<CompanyClient>(entity =>
        {
            entity.ToTable("CompanyClient");

            entity.Property(e => e.CompanyClientId).HasColumnName("companyClientId");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.CompanyDetail)
                .HasMaxLength(500)
                .HasColumnName("companyDetail");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(13)
                .HasColumnName("contactNo");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(50)
                .HasColumnName("contactPerson");
            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(500)
                .HasColumnName("profileImageURL");
            entity.Property(e => e.StateId).HasColumnName("stateId");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(10)
                .HasColumnName("zipcode");

            entity.HasOne(d => d.City).WithMany(p => p.CompanyClients)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City");

            entity.HasOne(d => d.Country).WithMany(p => p.CompanyClients)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country");

            entity.HasOne(d => d.State).WithMany(p => p.CompanyClients)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_State");
        });

        modelBuilder.Entity<CountryMaster>(entity =>
        {
            entity.ToTable("CountryMaster");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<GenderMaster>(entity =>
        {
            entity.ToTable("GenderMaster");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<IndustryMaster>(entity =>
        {
            entity.ToTable("IndustryMaster");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<LanguageMaster>(entity =>
        {
            entity.ToTable("LanguageMaster");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<QualificationMaster>(entity =>
        {
            entity.ToTable("QualificationMaster");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SkillMaster>(entity =>
        {
            entity.ToTable("SkillMaster");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<StateMaster>(entity =>
        {
            entity.ToTable("StateMaster");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.StateMasters)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_StateMaster_CountryMaster");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserEmailId)
                .HasMaxLength(50)
                .HasColumnName("User_Email_Id");
            entity.Property(e => e.UserFirstName)
                .HasMaxLength(50)
                .HasColumnName("User_First_Name");
            entity.Property(e => e.UserImage)
                .HasMaxLength(500)
                .HasColumnName("User_Image");
            entity.Property(e => e.UserLastName)
                .HasMaxLength(50)
                .HasColumnName("User_Last_Name");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(12)
                .HasColumnName("User_Phone");
            entity.Property(e => e.UserTypeId).HasColumnName("UserType_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
