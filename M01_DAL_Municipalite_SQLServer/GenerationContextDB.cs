using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;

namespace M01_DAL_Municipalite_SQLServer
{
    public class GenerationContextDB
    {
        private static DbContextOptions<MunicipaliteContextMYSQL> _dbContextOptions;

        static GenerationContextDB()
        {
            IConfigurationRoot configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsetting.json")                 // C:\Session 4\DEVELOPEMENT\DSED_M01_Fichiers_Texte\DSED_M01_Fichiers_Texte\bin\Debug\net6.0\appsetting.json
                    .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MunicipaliteContextMYSQL>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBConnection"));
            _dbContextOptions = optionsBuilder.Options;

        }

        public static MunicipaliteContextMYSQL ObtenirMunicipaliteContext()
        {
            return new MunicipaliteContextMYSQL(_dbContextOptions);
        }
    }
}
