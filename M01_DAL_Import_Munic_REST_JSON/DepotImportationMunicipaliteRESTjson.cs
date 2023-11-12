using M01_Srv_Municipalite;
using Newtonsoft.Json;
using System.Net.Http.Headers;

using Municipalite = M01_Srv_Municipalite.Municipalite;

namespace M01_DAL_Import_Munic_REST_JSON
{
    public class DepotImportationMunicipaliteRESTjson : IDepotImportationMunicipalites
    {
        private string m_baseAddress;
        private static HttpClient m_httpClient;
        private string m_url;

        public DepotImportationMunicipaliteRESTjson(string p_baseAddress, string p_url)
        {
            m_baseAddress = p_baseAddress;                      //dans le main passer https://www.donneesquebec.ca
            m_httpClient = new HttpClient();
            m_url = p_url;                              //dans le main passer https://www.donneesquebec.ca/recherche/api/action/datastore_search?resource_id=19385b4e-5503-4330-9e59-f998f5918363&limit=3000
        }

        public IEnumerable<Municipalite> LireMunicipalites()
        {
            List<Municipalite> municipalites = Requete().Result;

            return municipalites;
        }

        private async Task<List<Municipalite>> Requete()
        {
            List<Municipalite> municipalites = new List<Municipalite>();

            m_httpClient.BaseAddress = new Uri(m_baseAddress);
            m_httpClient.DefaultRequestHeaders.Accept.Clear();
            m_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await m_httpClient.GetAsync(m_url);

            if (response.IsSuccessStatusCode)
            {
                string contenuJSON = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(contenuJSON);

                var records = jsonObject["result"]["records"];

                foreach (var record in records)
                {
                    int codeGeographique = record.mcode;
                    string nomMunicipalite = record.munnom;
                    string adresseCourriel = record.mcourriel;
                    string adresseWeb = record.mweb;
                    DateTime? dateProchaineElection = null;

                    DateTime parsedDate = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(record.datelec?.ToString()) && DateTime.TryParse(record.datelec.ToString(), out parsedDate))
                    {
                        dateProchaineElection = parsedDate;
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

            return municipalites;
        }

    }
}

