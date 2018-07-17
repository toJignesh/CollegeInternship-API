using CollegeInternship_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeInternship_API.ViewModels
{
    public class CandidateViewModel
    {
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public string Description { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string City { get; set; }

    public string Province { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }

    public int Status { get; set; }

    public int RankBySkill { get; set; }

        public double DistanceFromJob { get; set; }
        public IEnumerable<CandidateSkill> CandidateSkills { get; set; }
    }
}
