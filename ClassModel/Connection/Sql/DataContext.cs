using System;
using System.Collections.Generic;
using System.Text;
using ClassModel.Model.Fiber;
using Microsoft.EntityFrameworkCore;

namespace ClassModel.Connection.Sql
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        //Tên dbset phải giống hệt trong sqlserver
        public DbSet<FiberPTM> FiberPTM { get; set; }
        public DbSet<PTMFiber> PTMFiber { get; set; }
        public DbSet<ThuLaoFiber> ThuLaoFiber { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PTMFiber>().HasKey(i => new { i.MA_TB, i.HDTB_ID });
            builder.Entity<ThuLaoFiber>().HasKey(i => new { i.MA_TB, i.HDTB_ID });
            builder.Entity<FiberPTM>().HasKey(i => new { i.hdtb_id, i.hdkh_id });
        }
       
    }
}
