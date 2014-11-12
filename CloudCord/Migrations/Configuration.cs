using CloudCord.Models;

namespace CloudCord.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CloudCord.Models.CloudCord_DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CloudCord.Models.CloudCord_DB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
                context.ENcalculation.AddOrUpdate(
                  r => r.izracunId,
                  new ENconversion { tagIz = "prvi", Eistok = 12345, Nsjever = 5678, xp = 333333, yp = 2222222, createdIz = DateTime.Today },
                  new ENconversion { tagIz = "drugi", Nsjever = 98765, Eistok = 667788, xp = 999999, yp = 6786586, createdIz = DateTime.Today.AddMonths(1) },
                  new ENconversion { tagIz = "drugi", Nsjever = 98765, Eistok = 6788.324 , xp = 999999, yp = 6786586, createdIz = DateTime.Today.AddMonths(2) },
                  new ENconversion { tagIz = "drugi", Nsjever = 934565, Eistok = 611128.56, xp = 9111212.546, yp = 678324.890, createdIz = DateTime.Today.AddMonths(5) },
                  new ENconversion { tagIz = "treci", Nsjever = 98225, Eistok = 661243.222, xp = 9992321.11, yp = 67865008.56, createdIz = DateTime.Today.AddYears(1) }

                );
                //context.XYcalculation.AddOrUpdate(
                //  r => r.izracunId,
                //  new ENconversion { tagIz = "prvi",  Eistok = 12345, Nsjever = 5678, xp = 333333, yp = 2222222, createdIz = DateTime.Today },
                //  new ENconversion { tagIz = "drugi", Nsjever = 98765, Eistok = 667788, xp = 999999, yp = 6786586, createdIz = DateTime.Today.AddMonths(1) },
                //  new ENconversion { tagIz = "drugi", Nsjever = 98765, Eistok = 6788.324, xp = 999999, yp = 6786586, createdIz = DateTime.Today.AddMonths(2) },
                //  new ENconversion { tagIz = "drugi", Nsjever = 934565, Eistok = 611128.56, xp = 9111212.546, yp = 678324.890, createdIz = DateTime.Today.AddMonths(5) },
                //  new ENconversion { tagIz = "treci", Nsjever = 98225, Eistok = 661243.222, xp = 9992321.11, yp = 67865008.56, createdIz = DateTime.Today.AddYears(1) }
                //
                //);
            
        }
    }
}
