using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using projectTest.ViewModels;

namespace projectTest.Models
{
    public class ShopsNearByDbContext:DbContext
    {
        public DbSet<RegisterModel> register { get; set; }
        public DbSet<Shops> shops { get; set; }
        public DbSet<ShopOutputModel> shop { get; set; }
    }
}