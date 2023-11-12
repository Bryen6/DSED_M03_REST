using M01_DAL_Import_Munic_CSV;
using M01_DAL_Import_Munic_JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_DSED_M01
{
    public class Test_Import_Munic_JSON
    {
        [Fact]
        public void LireMunicipalites_fichierJSONExistant_retourneListeDeMunicipalite()
        {
            // Arrange
            DepotImportationMunicipaliteJson depotJSON = new DepotImportationMunicipaliteJson("datastore_search");

            // Act
            var municipalites = depotJSON.LireMunicipalites();

            // Assert
            Assert.NotEmpty(municipalites);
        }

        [Fact]
        public void LireMunicipalites_fichierJSONNonExistant_FileNotFoundException()
        {
            // Arrange
            DepotImportationMunicipaliteJson depotJSON = new DepotImportationMunicipaliteJson("FauxNomDeFichier");

            // Act
            var exception = Assert.Throws<FileNotFoundException>(() => depotJSON.LireMunicipalites());

            // Assert
            Assert.Contains("Le fichier JSON n'existe pas.", exception.Message);
        }
    }
}
