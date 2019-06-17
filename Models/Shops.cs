using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
namespace projectTest.Models
{
    public class Shops
    {
        [Key]
        public int ShopId{get;set;}
        public string Name { get; set; }
        public DbGeography Location { get; set; }
    }
}