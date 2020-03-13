using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeronaTour.DAL.Entites;

namespace VeronaTour.DAL.EF
{
    public class VeronaTourDbInitializer : DropCreateDatabaseAlways<VeronaTourDbContext>
    {
        protected override void Seed(VeronaTourDbContext db)
        {
            
        }
    }
}
