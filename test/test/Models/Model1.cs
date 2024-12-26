using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace test.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=MyDatabase")
        {
        }

        public virtual DbSet<Nhanvien> Nhanviens { get; set; }
        public virtual DbSet<Phongban> Phongbans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.matkhau)
                .IsUnicode(false);
        }
    }
}
