using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BazinamSite2.Models
{
    public class SystemDbContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigUsersModel(modelBuilder);
            ConfigPicturesModel(modelBuilder);
            //modelBuilder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(20).IsRequired();
            //modelBuilder.Entity<Order>().ToTable("SalesOrders");
        }

        private void ConfigUsersModel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(c => c.nameAndFamily).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(c => c.email).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(c => c.password).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(c => c.address).IsOptional().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(c => c.phone).IsOptional().HasMaxLength(50);
        }

        private void ConfigPicturesModel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Picture>().Property(c => c.PicSourceBytes).IsOptional();
            modelBuilder.Entity<Picture>().Property(c => c.PicUrl).IsOptional();
        }
    }
}