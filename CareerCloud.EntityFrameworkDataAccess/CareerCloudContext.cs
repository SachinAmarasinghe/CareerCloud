using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; } = null!;
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; } = null!;
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; } = null!;
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; } = null!;
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; } = null!;
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; } = null!;
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; } = null!;
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; } = null!;
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; } = null!;
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; } = null!;
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; } = null!;
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; } = null!;
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; } = null!;
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; } = null!;
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; } = null!;
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; } = null!;
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; } = null!;
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; } = null!;
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "your_connection_string_here";

                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddConsole()
                        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
                });

                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>(entity =>
            {
                entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantEducations)
                    .HasForeignKey(d => d.Applicant) // Use only 'Applicant' as the foreign key
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Educations_Applicant_Profiles");
            });

            modelBuilder.Entity<ApplicantJobApplicationPoco>(entity =>
            {
                entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantJobApplications)
                    .HasForeignKey(d => d.Applicant)  // Include all parts of the composite key
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Job_Applications_Applicant_Profiles");
            });

            modelBuilder.Entity<ApplicantProfilePoco>(entity =>
            {
                entity.HasKey(e => e.Id); // Corrected to have only one primary key

                entity.HasOne(d => d.SystemCountryCode) // Assuming 'Code' is the correct navigation property
                    .WithMany(p => p.ApplicantProfiles)
                    .HasForeignKey(d => d.Country) // 'Country' is the foreign key in ApplicantProfilePoco
                    .HasPrincipalKey(p => p.Code) // 'Code' is the primary key in SystemCountryCodePoco
                    .HasConstraintName("FK_Applicant_Profiles_System_Country_Codes");

                entity.HasOne(d => d.Logins)
                    .WithMany(p => p.ApplicantProfiles)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Profiles_Security_Logins");

            });

            modelBuilder.Entity<ApplicantResumePoco>(entity =>
            {

                entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantResumes)
                    .HasForeignKey(d => d.Applicant) // Use both Applicant and Id as foreign keys
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Resumes_Applicant_Profiles");

            });

            modelBuilder.Entity<ApplicantSkillPoco>(entity =>
            {
                entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantSkills)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Skills_Applicant_Profiles");

            });

            modelBuilder.Entity<ApplicantWorkHistoryPoco>(entity =>
            {
                entity.HasOne(d => d.ApplicantProfile)
                    .WithMany(p => p.ApplicantWorkHistorys)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Work_Experiences_Applicant_Profiles");

            });

            modelBuilder.Entity<CompanyDescriptionPoco>(entity =>
            {

                entity.HasKey(e => new { e.Id, e.Company,e.LanguageId });

                entity.HasOne(d => d.CompanyProfile)
                    .WithMany(p => p.CompanyDescriptions)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Descriptions_Company_Profiles");

                entity.HasOne(d => d.SystemLanguageCode)
                    .WithMany(p => p.CompanyDescriptions)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Descriptions_System_Language_Codes");
            });

            modelBuilder.Entity<CompanyJobPoco>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(d => d.CompanyProfile)
                    .WithMany(p => p.CompanyJobs)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Jobs_Company_Profiles");
            });

            modelBuilder.Entity<CompanyJobEducationPoco>(entity =>
            {
                entity.HasKey(e => e.Id); 

                entity.HasOne(d => d.CompanyJob)
                    .WithMany(p => p.CompanyJobEducations) 
                    .HasForeignKey(d => d.Job) 
                    .OnDelete(DeleteBehavior.ClientSetNull) 
                    .HasConstraintName("FK_Company_Job_Educations_Company_Jobs");
            });

            modelBuilder.Entity<CompanyJobSkillPoco>(entity =>
            {
                entity.HasKey(e => e.Id); // Id is the primary key

                entity.HasOne(d => d.CompanyJob) // Configuring the relationship to CompanyJobPoco
                    .WithMany(p => p.CompanyJobSkills) // CompanyJob has many CompanyJobSkills
                    .HasForeignKey(d => d.Job) // Job is the foreign key in CompanyJobSkillPoco
                    .OnDelete(DeleteBehavior.ClientSetNull) // Set null on delete behavior
                    .HasConstraintName("FK_Company_Job_Skills_Company_Jobs"); // Constraint name
            });

            modelBuilder.Entity<CompanyJobDescriptionPoco>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Job });
            });

            modelBuilder.Entity<CompanyLocationPoco>(entity =>
            {

                entity.HasKey(e => new { e.Id, e.Company });
                entity.HasOne(d => d.CompanyProfile)
                    .WithMany(p => p.CompanyLocations)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Locations_Company_Profiles");
            });


            modelBuilder.Entity<SecurityLoginsLogPoco>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Login });
                entity.HasOne(d => d.SecurityLogin)
                    .WithMany(p => p.SecurityLoginsLogs)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Security_Logins_Log_Security_Logins");
            });

            modelBuilder.Entity<SecurityLoginsRolePoco>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Login,e.Role });
                entity.HasOne(d => d.SecurityLogin)
                    .WithMany(p => p.SecurityLoginsRoles)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Security_Logins_Roles_Security_Logins");

                entity.HasOne(d => d.SecurityRole)
                    .WithMany(p => p.SecurityLoginsRoles)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Security_Logins_Roles_Security_Roles");
            });
            
            modelBuilder.Entity<SystemCountryCodePoco>(entity =>
            {
                entity.HasKey(e => new { e.Code });
            });

        }

    }
}