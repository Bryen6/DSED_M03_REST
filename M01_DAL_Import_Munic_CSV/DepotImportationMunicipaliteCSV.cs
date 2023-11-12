using M01_Srv_Municipalite;
using System.Globalization;
using CsvHelper;

using Municipalite = M01_Srv_Municipalite.Municipalite;

namespace M01_DAL_Import_Munic_CSV
{
    public class DepotImportationMunicipaliteCSV : IDepotImportationMunicipalites
    {
        private string NomFichierAImporter { get; set; }

        public DepotImportationMunicipaliteCSV(string p_nomFichierAImporter)
        {
            NomFichierAImporter = p_nomFichierAImporter;
        }

        public IEnumerable<Municipalite> LireMunicipalites()
        {
            List<Municipalite> municipalites = new List<Municipalite>();
            string pathFichierCSV = $"C:\\{NomFichierAImporter}.csv";

            try
            {
                if (File.Exists(pathFichierCSV))
                {
                    using (var reader = new StreamReader(pathFichierCSV))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Read();
                        csv.ReadHeader();

                        while (csv.Read())
                        {
                            int codeGeographique = csv.GetField<int>("mcode");
                            string nomMunicipalite = csv.GetField("munnom");
                            string adresseCourriel = csv.GetField("mcourriel");
                            string adresseWeb = csv.GetField("mweb");

                            DateTime? dateProchaineElection = csv.GetField<DateTime?>("datelec");

                            Municipalite municipalite = new Municipalite
                            {
                                CodeGeographique = codeGeographique,
                                NomMunicipalite = nomMunicipalite,
                                AdresseCourriel = adresseCourriel,
                                AdresseWeb = adresseWeb,
                                DateProchaineElection = dateProchaineElection
                            };

                            municipalites.Add(municipalite);
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException("Le fichier CSV n'existe pas.");
                }
            }
            catch (FileNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'importation du CSV.", ex);
            }

            return municipalites;
        }
    }
}

