using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeInternship_API.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100)]
        [Required]
        public string MiddleName { get; set; }

        [MaxLength(2000)]
        [Required]
        public string Description { get; set; }

        [MaxLength(255)]
        [Required]
        public string Address1 { get; set; }

        [MaxLength(255)]
        public string Address2 { get; set; }

        [MaxLength(100)]
        [Required]
        public string City { get; set; }

        [MaxLength(50)]
        [Required]
        public string Province { get; set; }

        [MaxLength(25)]
        [Required]
        public string Country { get; set; }

        [MaxLength(15)]
        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int Status { get; set; }

        public ICollection<CandidateSkill> CandidateSkills{ get; set; }
    }
}
