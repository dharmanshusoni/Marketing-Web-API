using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CompanyClientDTO
    {
        public int CompanyClientId { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [Required]
        public string Email { get; set; } = null!;

        [MaxLength(50)]
        [Required]
        public string ContactPerson { get; set; } = null!;

        [MaxLength(12)]
        [Required]
        public string ContactNo { get; set; } = null!;

        [MaxLength(50)]
        [Required]
        public string Address { get; set; } = null!;

        [MaxLength(10)]
        [Required]
        public string Zipcode { get; set; } = null!;

        [MaxLength(500)]
        public string CompanyDetail { get; set; } = null!;

        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        public string? ProfileImageUrl { get; set; }
    }
}
