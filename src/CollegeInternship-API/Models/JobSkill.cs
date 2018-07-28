using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeInternship_API.Models
{
    public class JobSkill
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public Job  Job { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        // temp solution for the skill name
        [MaxLength(100)]
        [Required]
        public string SkillName { get; set; }
    }
}