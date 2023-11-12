using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Srv_Municipalite
{
    public interface IDepotImportationMunicipalites
    {
        IEnumerable<Municipalite> LireMunicipalites();
    }
}
