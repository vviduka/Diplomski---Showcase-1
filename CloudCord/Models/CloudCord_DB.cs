using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudCord.Models
{
    public class CloudCord_DB : DbContext  
    {
        public CloudCord_DB() : base("name=DefaultConnection")
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ENconversion> ENcalculation { get; set; }
        public DbSet<XYconversion> XYcalculation { get; set; }
    }
}