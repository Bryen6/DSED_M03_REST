using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Srv_Municipalite
{
    public class TraitementImporterDonneesMunicipalite
    {
        private IDepotImportationMunicipalites m_depotImportationMunicipalites;
        private IDepotMunicipalites m_depotMunicipalites;

        public TraitementImporterDonneesMunicipalite(IDepotImportationMunicipalites p_depotImportationMunicipalites, IDepotMunicipalites p_depotMunicipalites)
        {
            m_depotImportationMunicipalites = p_depotImportationMunicipalites;
            m_depotMunicipalites = p_depotMunicipalites;
        }


        public StatistiquesImportationDonnees Executer()
        {
            StatistiquesImportationDonnees statistiques = new StatistiquesImportationDonnees();

            IEnumerable<Municipalite> municipalitesImport = m_depotImportationMunicipalites.LireMunicipalites().ToList();

            foreach (Municipalite municipaliteSRVM in municipalitesImport)
            {
                Municipalite? municipaliteDAL = m_depotMunicipalites.ChercherMunicipaliteParCodeGeographique(municipaliteSRVM.CodeGeographique);

                if (municipaliteDAL is null)
                {
                    m_depotMunicipalites.AjouterMunicipalite(municipaliteSRVM);
                    m_depotMunicipalites.MAJMunicipalite(municipaliteSRVM);
                    statistiques.NombreEnregistrementsAjoutes++;
                }
                else
                {
                    if (municipaliteDAL.Equals(municipaliteSRVM))
                    {
                        IEnumerable<Municipalite> municipaliteDALActives = m_depotMunicipalites.ListerMunicipalitesActives();
                        bool estActive = false;

                        foreach (Municipalite municipalite in municipaliteDALActives)
                        {
                            if (municipalite.CodeGeographique == municipaliteSRVM.CodeGeographique)
                            {
                                estActive = true;
                            }
                        }

                        if (estActive)
                        {
                            statistiques.NombreEnregistrementsNonModifies++;
                        }
                        else
                        {
                            m_depotMunicipalites.MAJMunicipalite(municipaliteSRVM);
                            statistiques.NombreEnregistrementsModifies++;
                        }
                    }
                }
            }
            IEnumerable<Municipalite> municipaliteDALADesactiver = m_depotMunicipalites.ListerMunicipalitesActives();

            municipaliteDALADesactiver = municipaliteDALADesactiver.Where(m => !municipalitesImport.Contains(m)).ToList();

            foreach (Municipalite municipalite in municipaliteDALADesactiver)
            {
                m_depotMunicipalites.DesactiverMunicipalite(municipalite);
                statistiques.NombreEnregistrementsDesactives++;
            }

            return statistiques;
        }
    }
}
