using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_DAL_Municipalite_SQLServer
{
    public class MunicipaliteContextMYSQL : DbContext
    {
        public DbSet<Municipalite>? MUNICIPALITES { get; set; }

        public MunicipaliteContextMYSQL(DbContextOptions<MunicipaliteContextMYSQL> options)
            : base(options)
        {
            if (MUNICIPALITES == null)
            {
                MUNICIPALITES = Set<Municipalite>();
            }
        }
    }
}
