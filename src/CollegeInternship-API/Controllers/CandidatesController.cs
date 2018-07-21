using CollegeInternship_API.Context;
using CollegeInternship_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using CollegeInternship_API.ViewModels;
using System.Device.Location;
using System;
using RestSharp;
using Newtonsoft.Json;

namespace CollegeInternship_API.Controllers
{
    [Produces("application/json")]
    [Route("api/Candidates")]
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext context;

        public CandidatesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await this.context.Candidates.Include(c => c.CandidateSkills).ToListAsync());

        
        [HttpGet]
        [Route("for-job")]
        public async Task<IActionResult> Get([FromQuery] int JobId)
        {
            Job job = await this.context.Jobs.Include(j=>j.JobSkills).FirstOrDefaultAsync(j => j.Id == JobId);

            if (job == null)
            {
                return NotFound();
            }

            int[] jobSkills = job.JobSkills.Select(s => s.SkillId).ToArray();

            #region linq-tries
            ////var q = this.context.Candidates
            ////            .Include(c=>c.CandidateSkills)
            ////            .Join(
            ////                this.context.Skills,
            ////                c=>c.CandidateSkills.Select(cs=>cs.SkillId),
            ////                s=>s.Id,
            ////                (c,s )=>new { c, s.Name});

            ////var a = from c in this.context.Candidates
            ////        from cs in c.CandidateSkills
            ////        join s in this.context.Skills
            ////            on c.CandidateSkills.SelectMany(cs=>cs.SkillId) equals s.Id
            ////            into candSkills
            ////        where c.Id == 2
            ////        select new
            ////        {
            ////            name = c.FirstName + ' ' + c.LastName,
            ////            skills = candSkills
            ////        };

            //var x = this.context.Candidates
            //        .SelectMany(c => c.CandidateSkills)
            //        .GroupJoin(this.context.Skills,
            //        g => g.SkillId,
            //        s => s.Id,
            //        (g, s) => new { g, s })
            //        .Where(c => c.g.CandidateId == 1);


            //return Ok(await x.ToListAsync());
            #endregion linq-tries

            List<Candidate> result = await this.context.Candidates
                .Include(c => c.CandidateSkills)
                //.ThenInclude(cs => cs.Skill)
                .Where(c => c.CandidateSkills.Any(cs => jobSkills.Contains(cs.SkillId)))
                .ToListAsync();
            List<CandidateViewModel> output = new List<CandidateViewModel>();
            foreach(Candidate c in result)
            {
                CandidateViewModel cvm = new CandidateViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PostalCode = c.PostalCode,
                    City = c.City,
                    CandidateSkills = c.CandidateSkills,
                    DistanceFromJob = DistanceBetweenPlaces(job.Latitude, job.Longitude, c.Latitude, c.Longitude)
                };

                output.Add(cvm);
            }

            if (output.Count > 0)
            {
                GoogleDistanceMatrixResponse googleDistances = FindDistanceFromJob(job.PostalCode, output.Select(o => o.PostalCode).ToArray());



                if (googleDistances.status == "OK")
                {
                    for (int index = 0; index < output.Count; index++)
                    {
                        if (googleDistances.rows[0].elements[index].status == "OK")
                        {
                            output[index].DistanceFromJob = Convert.ToDouble(googleDistances.rows[0]
                                                                                            .elements[index]
                                                                                            .distance
                                                                                            .text
                                                                                            .Replace(" km", ""));
                        }
                    }
                }
            }

            return Ok(output);


        }


        [HttpGet]
        [Route("for-job-desc")]
        public async Task<IActionResult> GetByJobDesc([FromQuery] int JobId)
        {
            Job job = await this.context.Jobs.Include(j => j.JobSkills).FirstOrDefaultAsync(j => j.Id == JobId);

            if (job == null)
            {
                return NotFound();
            }

            string[] jobSkills = job.JobSkills.Select(s => s.SkillName).ToArray();

            #region linq-tries

            ////var q = this.context.Candidates
            ////            .Include(c=>c.CandidateSkills)
            ////            .Join(
            ////                this.context.Skills,
            ////                c=>c.CandidateSkills.Select(cs=>cs.SkillId),
            ////                s=>s.Id,
            ////                (c,s )=>new { c, s.Name});

            ////var a = from c in this.context.Candidates
            ////        from cs in c.CandidateSkills
            ////        join s in this.context.Skills
            ////            on c.CandidateSkills.SelectMany(cs=>cs.SkillId) equals s.Id
            ////            into candSkills
            ////        where c.Id == 2
            ////        select new
            ////        {
            ////            name = c.FirstName + ' ' + c.LastName,
            ////            skills = candSkills
            ////        };

            //var x = this.context.Candidates
            //        .SelectMany(c => c.CandidateSkills)
            //        .GroupJoin(this.context.Skills,
            //        g => g.SkillId,
            //        s => s.Id,
            //        (g, s) => new { g, s })
            //        .Where(c => c.g.CandidateId == 1);


            //return Ok(await x.ToListAsync());

            #endregion linq-tries

            List<Candidate> result = await this.context.Candidates
                //.ThenInclude(cs => cs.Skill)
                .Where(c => jobSkills.Any(js=>  c.Description.ToLower().Contains(js.ToLower())))
                .Include(c => c.CandidateSkills)
                .ToListAsync();
            List<CandidateViewModel> output = new List<CandidateViewModel>();
            foreach (Candidate c in result)
            {
                CandidateViewModel cvm = new CandidateViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PostalCode = c.PostalCode,
                    City = c.City,
                    CandidateSkills = c.CandidateSkills
                    //DistanceFromJob = DistanceBetweenPlaces(job.Latitude, job.Longitude, c.Latitude, c.Longitude)
                };

                output.Add(cvm);
            }

            if (output.Count > 0)
            {
                GoogleDistanceMatrixResponse googleDistances = FindDistanceFromJob(job.PostalCode, output.Select(o => o.PostalCode).ToArray());



                if (googleDistances.status == "OK")
                {
                    for (int index = 0; index < output.Count; index++)
                    {
                        if (googleDistances.rows[0].elements[index].status == "OK")
                        {
                            output[index].DistanceFromJob = Convert.ToDouble(googleDistances.rows[0]
                                                                                            .elements[index]
                                                                                            .distance
                                                                                            .text
                                                                                            .Replace(" km", ""));
                        }
                    }
                }
            }
            return Ok(output);


        }

        private GoogleDistanceMatrixResponse FindDistanceFromJob(string origin, string[] destinations)
        {
            string dest = String.Join("|", destinations);

            var client = new RestClient("https://maps.googleapis.com");

            var request = new RestRequest($"maps/api/distancematrix/json?units=metric&origins={origin}&destinations={dest}&key=AIzaSyBwAzTe6aotBybr98zmCqIxBIvhQ12e6rk", Method.GET);

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            return JsonConvert.DeserializeObject<GoogleDistanceMatrixResponse>(content);
        }

        private double DistanceBetweenPlaces(double lat1, double lon1, double lat2, double lon2)
        {
            var sCoord = new GeoCoordinate(lat1, lon1);
            var eCoord = new GeoCoordinate(lat2, lon2);

            return sCoord.GetDistanceTo(eCoord) / 1000;
        }
    }
}