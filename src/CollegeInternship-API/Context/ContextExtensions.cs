using CollegeInternship_API.Context;
using CollegeInternship_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace CollegeInternship_API.Contexts
{
    public static class ContextExtensions
    {

        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.SeedDb();
            }
            return host;
        }

        public static void SeedDb(this ApplicationDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                if (!context.Skills.Any())
                {
                    var skills = new List<Skill>()
                    {
                        new Skill(){Name=".Net",Description="Microsoft .Net Framework",IsActive=true},
                        new Skill(){Name="Angular",Description="Angular Framework",IsActive=true},
                        new Skill(){Name="Html",Description="Hyper Text Markup Language",IsActive=true}
                    };

                    context.AddRange(skills);
                    context.SaveChanges();

                    var candidates = new List<Candidate>()
                    {
                        new Candidate(){FirstName="Jignesh",LastName="Patel",MiddleName="S",Address1="123 My Street",City="Ajax",Province="ON",Country="Canada",PostalCode="A1B 2C3", Status=1,Description="Angular .Net html"},
                        new Candidate(){FirstName="Aashish",LastName="Jassal",MiddleName="S",Address1="345 His Street",City="Brampton",Province="ON",Country="Canada",PostalCode="D2E 3F4", Status=1,Description="Angular html"},
                        new Candidate(){FirstName="Paras",LastName="Parikh",MiddleName="S",Address1="567 Main Street",City="Toronto",Province="ON",Country="Canada",PostalCode="G5H 6I7", Status=1,Description=".Net html"},
                    };

                    context.AddRange(candidates);
                    context.SaveChanges();

                    var jobs = new List<Job>()
                    {
                        new Job(){Title="Fullstack developer",Description="Angular .NET html", Address1="789 Nowhere Drive", City="Toronto", Province="ON",Country="CAN",PostalCode="D2E 3F4", Status=1},
                        new Job(){Title="API Devoper",Description=".NET html", Address1="890 Everywhere Drive", City="Markham", Province="ON",Country="CAN",PostalCode="U3V 3R4", Status=1},
                        new Job(){Title="Frontoend developer",Description="Angular html", Address1="901 Earth Drive", City="Brampton", Province="ON",Country="CAN",PostalCode="P8H 4L2", Status=1},
                    };
                    context.AddRange(jobs);
                    context.SaveChanges();

                    var jobskills = new List<JobSkill>(){
                        new JobSkill() { JobId = 1,SkillId=1},
                        new JobSkill() { JobId = 1,SkillId=2},
                        new JobSkill() { JobId = 1,SkillId=3},

                        new JobSkill() { JobId = 2,SkillId=2},
                        new JobSkill() { JobId = 2,SkillId=3},

                        new JobSkill() { JobId = 3,SkillId=1},
                        new JobSkill() { JobId = 3,SkillId=3}
                    };

                    context.AddRange(jobskills);
                    context.SaveChanges();



                    var candidateSkills = new List<CandidateSkill>(){
                        new CandidateSkill() { CandidateId = 1,SkillId=1},
                        new CandidateSkill() { CandidateId = 1,SkillId=2},
                        new CandidateSkill() { CandidateId = 1,SkillId=3},

                        new CandidateSkill() { CandidateId = 2,SkillId=2},
                        new CandidateSkill() { CandidateId = 2,SkillId=3},

                        new CandidateSkill() { CandidateId = 3,SkillId=1},
                        new CandidateSkill() { CandidateId = 3,SkillId=3}
                    };

                    context.AddRange(candidateSkills);
                    context.SaveChanges();
                }
            }
        }
    }
}
