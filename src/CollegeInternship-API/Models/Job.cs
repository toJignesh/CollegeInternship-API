﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeInternship_API.Models
{
    public class Job
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

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
        public string City{ get; set; }

        [MaxLength(50)]
        [Required]
        public string Province{ get; set; }

        [MaxLength(25)]
        [Required]
        public string Country{ get; set; }

        [MaxLength(15)]
        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        public ICollection<JobSkill> JobSkills{ get; set; }

    }
}
