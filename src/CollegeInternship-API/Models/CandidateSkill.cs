using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeInternship_API.Models
{
    public class CandidateSkill
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        // temp solution for the skill name
        [MaxLength(100)]
        [Required]
        public string SkillName { get; set; }

    }
}
