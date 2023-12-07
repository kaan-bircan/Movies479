using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // new { e.UserId, e.ResourceId }: anonymous type used for using more then one property with the delegate
            modelBuilder.Entity<MovieGenre>().HasKey(e => new { e.MovieId, e.GenreId });
        }

    }
}
