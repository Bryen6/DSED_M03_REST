using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;
using M01_DAL_Import_Munic_CSV;
using M01_Srv_Municipalite;
using Moq;
using Xunit;

namespace Test_DSED_M01
{
    public class Test_Import_Munic_CSV
    {
        [Fact]
        public void LireMunicipalites_fichierCSVExistant_retourneListeDeMunicipalite()
        {
            // Arrange
            DepotImportationMunicipaliteCSV depot = new DepotImportationMunicipaliteCSV("MUN");

            // Act
            var municipalites = depot.LireMunicipalites();

            // Assert
            Assert.NotEmpty(municipalites);
        }

        [Fact]
        public void LireMunicipalites_fichierCSVNonExistant_FileNotFoundException()
        {
            // Arrange
            DepotImportationMunicipaliteCSV depot = new DepotImportationMunicipaliteCSV("FauxNomDeFichier");

            // Act
            var exception = Assert.Throws<FileNotFoundException>(() => depot.LireMunicipalites());

            // Assert
            Assert.Contains("Le fichier CSV n'existe pas.", exception.Message);
        }
    }
}
