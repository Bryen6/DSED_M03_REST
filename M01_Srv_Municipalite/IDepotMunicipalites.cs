using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Srv_Municipalite
{
    public interface IDepotMunicipalites
    {
        Municipalite ChercherMunicipaliteParCodeGeographique(int p_codeGeographique);
        IEnumerable<Municipalite> ListerMunicipalitesActives();
        void DesactiverMunicipalite(Municipalite p_municipalite);
        void AjouterMunicipalite(Municipalite p_municipalite);
        void MAJMunicipalite(Municipalite p_municipalite);
    }
}
