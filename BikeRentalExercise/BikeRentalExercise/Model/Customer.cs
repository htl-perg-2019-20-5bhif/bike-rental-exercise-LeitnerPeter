using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRentalExercise.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public Gender gender { get; set; }

        [Required]
        [MaxLength(50)]
        public String Firstname { get; set; }

        [Required]
        [MaxLength(75)]
        public String Lastname { get; set; }

        [Required]
        public DateTime Birthday { get { return Birthday; } set { Birthday = value.Date; } }

        [Required]
        [MaxLength(75)]
        public String  Street { get; set; }

        [MaxLength(10)]
        public int Housenumber { get; set; }

        [Required]
        [MaxLength(10)]
        public int Zipcode { get; set; }

        [Required]
        [MaxLength(75)]
        public String Town { get; set; }
    }
}
