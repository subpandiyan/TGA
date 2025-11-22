using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Writers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace TGA.Model
{
    public class UserDbContext :DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }  
        public DbSet<Timesheet> Timesheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .Navigation(e => e.Timesheets)
            //    .UsePropertyAccessMode(PropertyAccessMode.Property);

            //modelBuilder.Entity<Timesheet>()
            //    .Navigation(e => e.User)
            //    .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
    [Table("users")]
    public class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public DateTime createdDate { get; set; }

        //public virtual ICollection<Timesheet> Timesheets { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();

    }

    [Table("Timesheet")]
    public class Timesheet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime WorkingDate { get; set; }
        public string Project { get; set; }
        public decimal RegularHrs { get; set; }
        public decimal OverTimeHrs { get; set; }
       
        public string Task { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public int UserId { get; set; }
        public DateTime createdDate { get; set; }

       // public virtual User User { get; set; }  
    }



}
