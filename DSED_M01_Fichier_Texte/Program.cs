using M01_DAL_Import_Munic_CSV;
using M01_DAL_Import_Munic_JSON;
using M01_DAL_Import_Munic_REST_JSON;
using M01_DAL_Municipalite_SQLServer;
using M01_Srv_Municipalite;
using Microsoft.EntityFrameworkCore;

namespace DSED_M01_Fichier_Texte
{
    public class Program
    {
        static void Main(string[] args)
        {
            //DepotImportationMunicipaliteCSV depotCSV = new DepotImportationMunicipaliteCSV("MUN");
            //DepotImportationMunicipaliteJson depotJSON = new DepotImportationMunicipaliteJson("datastore_search");
            DepotImportationMunicipaliteRESTjson depotRestJSON = new DepotImportationMunicipaliteRESTjson("https://www.donneesquebec.ca", "/recherche/api/action/datastore_search?resource_id=19385b4e-5503-4330-9e59-f998f5918363&limit=3000");

            using (MunicipaliteContextMYSQL contextBD = GenerationContextDB.ObtenirMunicipaliteContext())
            {
                contextBD.Database.OpenConnection();
                contextBD.Database.ExecuteSqlRaw("SET IDENTITY_INSERT municipalites ON");

                //TraitementImporterDonneesMunicipalite traitement = new TraitementImporterDonneesMunicipalite(depotCSV, new DepotMunicipalitesMySQL(contextBD));
                //TraitementImporterDonneesMunicipalite traitement = new TraitementImporterDonneesMunicipalite(depotJSON, new DepotMunicipalitesMySQL(contextBD));
                TraitementImporterDonneesMunicipalite traitement = new TraitementImporterDonneesMunicipalite(depotRestJSON, new DepotMunicipalitesMySQL(contextBD));
                StatistiquesImportationDonnees statistiques = traitement.Executer();

                contextBD.SaveChanges();

                contextBD.Database.ExecuteSqlRaw("SET IDENTITY_INSERT municipalites OFF");
                contextBD.Database.CloseConnection();

                Console.WriteLine(statistiques.ToString());
            }
        }
    }
}