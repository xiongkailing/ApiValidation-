using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TokenManager.Models;

namespace TokenManager.TokenDAL
{
    public class TokenDbContext:DbContext
    {
        public TokenDbContext()
            : base("name=TokenDbContext")
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>().ToTable("Tokens");
            modelBuilder.Entity<Token>().HasKey(e => e.Id);
            modelBuilder.Entity<Token>().Property(t => t.ThirdPartName).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Token>().Property(t => t.OpenToken).IsRequired();
            modelBuilder.Entity<Token>().Property(t => t.SecretToken).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}