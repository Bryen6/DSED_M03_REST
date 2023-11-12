using M01_Srv_Municipalite;
using Newtonsoft.Json;

using Municipalite = M01_Srv_Municipalite.Municipalite;

namespace M01_DAL_Import_Munic_JSON
{
    public class DepotImportationMunicipaliteJson : IDepotImportationMunicipalites
    {
        
        private string NomFichierAImporter { get; set; }

        public DepotImportationMunicipaliteJson(string p_nomFichierAImporter)
        {
            NomFichierAImporter = p_nomFichierAImporter;
        }

        public IEnumerable<Municipalite> LireMunicipalites()
        {
            List<Municipalite> municipalites = new List<Municipalite>();
            string pathFichierJSON = $"C:\\{NomFichierAImporter}.json";

            try
            {
                if (File.Exists(pathFichierJSON))
                {
                    using (StreamReader reader = new StreamReader(pathFichierJSON))
                    {
                        string json = reader.ReadToEnd();

                        dynamic data = JsonConvert.DeserializeObject(json);

                        if (data is not null && data.result is not null && data.result.records is not null)
                        {
                            var records = data.result.records;

                            foreach (var record in records)
                            {
                                int codeGeographique = record.mcode;
                                string nomMunicipalite = record.munnom;
                                string adresseCourriel = record.mcourriel;
                                string adresseWeb = record.mweb;
                                DateTime? dateProchaineElection = null;

                                DateTime parsedDate = DateTime.MinValue;

                                if (!string.IsNullOrEmpty(record.datelec.ToString()) && DateTime.TryParse(record.datelec.ToString(), out parsedDate))
                                {
                                    dateProchaineElection = parsedDate;
                                }
                                else
                                {
                                    dateProchaineElection = (DateTime?)null;
                                }

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
                        else
                        {
                            if (data is null)
                            {
                                throw new Exception("Le fichier JSON est vide.");
                            }
                            else if (data.result is null)
                            {
                                throw new Exception("Le fichier JSON ne contient pas de résultat.");
                            }
                            else if (data.result.records is null)
                            {
                                throw new Exception("Le fichier JSON ne contient pas de records.");
                            }
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException("Le fichier JSON n'existe pas.");
                }
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'importation du JSON.", ex);
            }
            return municipalites;
        }
        
    }
}
