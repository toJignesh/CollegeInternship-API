using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeInternship_API.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(2000)]
        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public ICollection<CandidateSkill> CandidateSkills { get; set; }
        public ICollection<JobSkill> JobSkills { get; set; }
    }
}
