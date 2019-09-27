using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcore.Model
{
    [Table("user")]
    public class User
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public DateTime? createAt { get; set; }

    }
    
    public class Db:DbContext
    {
        public DbSet<User> users { get; set; }
        public Db(DbContextOptions options) : base(options) { }
    }
}
