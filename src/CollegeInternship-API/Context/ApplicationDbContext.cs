using CollegeInternship_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeInternship_API.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Candidate>().Property(c => c.Longitude).HasColumnType("double(15,7)");
            //modelBuilder.Entity<Candidate>().Property(c => c.Latitude).HasColumnType("double(15,7)");

            //modelBuilder.Entity<Job>().Property(j => j.Longitude).HasColumnType("double(15,7)");
            //modelBuilder.Entity<Job>().Property(j => j.Latitude).HasColumnType("double(15,7)");

            modelBuilder.Entity<CandidateSkill>()
                .HasKey(cs => new { cs.CandidateId, cs.SkillId });

            modelBuilder.Entity<CandidateSkill>()
                .HasOne(m => m.Candidate)
                .WithMany(m => m.CandidateSkills)
                .HasForeignKey(m => m.CandidateId);

            modelBuilder.Entity<CandidateSkill>()
                .HasOne(m => m.Skill)
                .WithMany(m => m.CandidateSkills)
                .HasForeignKey(m => m.SkillId);


            modelBuilder.Entity<JobSkill>()
                .HasKey(js => new { js.JobId, js.SkillId });

            modelBuilder.Entity<JobSkill>()
                .HasOne(m => m.Job)
                .WithMany(m => m.JobSkills)
                .HasForeignKey(m => m.JobId);

            modelBuilder.Entity<JobSkill>()
                .HasOne(m => m.Skill)
                .WithMany(m => m.JobSkills)
                .HasForeignKey(m => m.SkillId);
        }
    }
    
}
