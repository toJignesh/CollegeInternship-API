using CollegeInternship_API.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using CollegeInternship_API.Models;

namespace CollegeInternship_API.Controllers
{
    [Produces("application/json")]
    [Route("api/Jobs")]
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext context;

        public JobsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var a = await context.Jobs.ToListAsync();
            return Ok(a);
        }

        [HttpGet]
        [Route("{id}/skills")]
        public async Task<IActionResult> GetJobSkills(int id)
        {
            Job job = await this.context.Jobs.Include(j => j.JobSkills).FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            int[] jobSkills = job.JobSkills.Select(s => s.SkillId).ToArray();
            return Ok(await this.context.Skills.Where(s => jobSkills.Contains(s.Id)).Select(s=> new { id= s.Id, name=s.Name}).ToListAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<string> Delete(int id)
        {
            Job job = await this.context.Jobs.Include(j => j.JobSkills).FirstOrDefaultAsync(j => j.Id == id);
            if(job != null)
            {
                this.context.Jobs.Remove(job);
                this.context.SaveChanges();
                return "";
            }
            else
            {
                return "Could not delete job";
            }
        }
    }
}