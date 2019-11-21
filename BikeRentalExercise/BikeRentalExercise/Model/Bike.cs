using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRentalExercise.Model
{
    public class Bike
    {
        public int BikeId { get; set; }

        [Required]
        [MaxLength(25)]
        public String Brand { get; set; }

        [Required]
        public DateTime PuchaseDate { get { return PuchaseDate; } set { PuchaseDate = value.Date; } }

        [MaxLength(1000)]
        public String Notes { get; set; }

        public DateTime LastServiceDate { get { return LastServiceDate; } set { PuchaseDate = value.Date; } }

        [Required]
        public double RentalPriceFirstHour { 
            get { return RentalPriceFirstHour; }
            set {
                if (value <= 0)
                {
                    throw new ArgumentException("Rental price must be greater than 0!");
                }
                RentalPriceFirstHour = Math.Round(value, 2); } 
        }

        [Required]
        public double RentalPrice
        {
            get { return RentalPrice; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Rental price must be greater than 0!");
                }
                RentalPrice = Math.Round(value, 2);
            }
        }

        [Required]
        public BikeCategory BikeCategory { get; set; }
    }
}
