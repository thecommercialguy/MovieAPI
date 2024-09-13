
using WebApplication4.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WebApplication4.Data
{
    public class LiteDbContext : DbContext
    {
        public LiteDbContext(DbContextOptions<LiteDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite(@"Data Source=C:\Users\willi\source\repos\WebApplication4\WebApplication4\LiteDb.db");
        //  => optionsBuilder.UseSqlite(@"DataSource=C:\Users\willi\source\repos\WebApplication4\WebApplication4\LiteDb.db");
        // Data Source, not DataSource (what I had previously)
        // needed a connectio string added on, that's when we do the DataSource thing, but
        // yeah that's likely where we tack on a connection string, or when the program.cs file is
        // like alerted or whatever to the existence of one, anyways, gonna need to add a migration
        // after as well..
        //    => optionsBuilder.UseSqlite(@"C:\Users\willi\source\repos\WebApplication4\WebApplication4\LiteDb.db");
        //{
        //    optionsBuilder.UseSqlite("FileName=sqlitedb1", option => {
        //        option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        //    });
        //    base.OnConfiguring(optionsBuilder);
        //}//C:\Users\willi\source\repos\WebApplication4\WebApplication4\WebApplication4.csproj


        //C:\Users\willi\source\repos\WebApplication4\WebApplication4\mydatabase.db


        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set;}
        // public DbSet<MovieDirector> MovieDirectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////////////////////////////////////
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId }) ;

            modelBuilder.Entity<MovieGenre>()
                .HasOne(m => m.Movie)
                .WithMany(mg => mg.MovieGenres)
                .HasForeignKey(g => g.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(g => g.Genre)
                .WithMany(mg => mg.MovieGenres)
                .HasForeignKey(m => m.GenreId);
            //////////////////////////////////////
            //modelBuilder.Entity<MovieDirector>()
            //    .HasKey(md => new { md.MovieId, md.DirectorId }) ;

            //modelBuilder.Entity<MovieDirector>()
            //    .HasOne(m => m.Movie)
            //    .WithMany(md => md.MovieDirectors)
            //    .HasForeignKey(g => g.MovieId);

            //modelBuilder.Entity<MovieDirector>()
            //    .HasOne(g => g.Director)
            //    .WithMany(md => md.MovieDirectors)
            //    .HasForeignKey(m => m.DirectorId);
            ////////////////////////////////////////



        }

        
    }
}
