using CollegeInternship_API.Context;
using CollegeInternship_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        

    }
}