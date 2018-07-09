using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeInternship_API.Models
{
    public class CandidateSkill
    {
        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

    }
}
