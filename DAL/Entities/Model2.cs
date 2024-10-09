using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entities
{
    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Major)
                .WithRequired(e => e.Faculty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Major>()
                .HasMany(e => e.Student)
                .WithOptional(e => e.Major)
                .HasForeignKey(e => new { e.FacultyID, e.MajorID });
        }
    }
}
