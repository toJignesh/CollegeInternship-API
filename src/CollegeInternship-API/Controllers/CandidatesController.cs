using CollegeInternship_API.Context;
using CollegeInternship_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

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

            return Ok(await this.context.Candidates
                .Include(c => c.CandidateSkills)
                //.ThenInclude(cs=>cs.Skill)
                .Where(c=>c.CandidateSkills.Any(cs=>jobSkills.Contains(cs.SkillId)))
                .ToListAsync());
        }


    }
}