using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Srv_Municipalite
{
    public class ManipulationMunicipalites
    {
        private IDepotMunicipalites m_depot;


        public ManipulationMunicipalites(IDepotMunicipalites p_depot)
        {
            this.m_depot = p_depot;
        }

        public void AjouterMunicipalite(Municipalite p_municipalite)
        {
            this.m_depot.AjouterMunicipalite(p_municipalite);
        }

        public void DesactiverMunicipalite(Municipalite p_municipalite)
        {
            this.m_depot.DesactiverMunicipalite(p_municipalite);
        }

        public Municipalite ChercherMunicipaliteParCodeGeographique(int p_codeGeographique)
        {
            return this.m_depot.ChercherMunicipaliteParCodeGeographique(p_codeGeographique);
        }

        public IEnumerable<Municipalite> ListerMunicipalitesActives()
        {
            return this.m_depot.ListerMunicipalitesActives();
        }

        public void MAJMunicipalite(Municipalite p_municipalite)
        {
            this.m_depot.MAJMunicipalite(p_municipalite);
        }
    }
}
