using BikeRentalExercise.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRentalExercise.Data
{
    public class BikeDataContext : DbContext
    {
        public BikeDataContext(DbContextOptions<BikeDataContext> options)
        : base(options)
        { }

        public DbSet<Bike> Bikes { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Rental> Rentals { get; set; }
    }
}
