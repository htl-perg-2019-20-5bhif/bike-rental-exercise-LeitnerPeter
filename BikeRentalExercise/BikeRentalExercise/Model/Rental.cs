using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRentalExercise.Model
{
    public class Rental
    {
        public int RentalId { get; set; }

        [Required]
        public int CustomerId;

        public Customer Customer;

        [Required]
        public int BikeId;

        public Bike Bike;

        [Required]
        public DateTime RentalBegin;

        public DateTime RentalEnd
        {
            get { return RentalEnd; }
            set
            {
                if (RentalBegin >= value)
                {
                    throw new ArgumentException("Rental end must be after rental start!");
                }
                RentalEnd = value;
            }
        }

        public double Total
        {
            get { return Total; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Rental price must be greater than 0!");
                }
                Total = Math.Round(value, 2);
            }
        }

        [Required]
        public Boolean Paid
        {
            get { return Paid; }
            set
            {
                if ((RentalEnd == null) && value)
                {
                    throw new ArgumentException("Ride must have ended before you can pay!");
                }
                Paid = value;
            }
        }
    }
}
