using M01_Srv_Municipalite;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_DSED_M01
{
    public class Test_TraitementImporterDonnees
    {
        [Fact]
        public void Executer_SiAucuneDonneesImportees_RetourneStatisticsTousA_0()
        {
            // Arrange
            var mockDepotImportation = new Mock<IDepotImportationMunicipalites>();
            mockDepotImportation.Setup(dep => dep.LireMunicipalites()).Returns(new List<Municipalite>());

            var mockDepotMunicipalites = new Mock<IDepotMunicipalites>();

            var traitement = new TraitementImporterDonneesMunicipalite(mockDepotImportation.Object, mockDepotMunicipalites.Object);

            // Act
            var result = traitement.Executer();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.NombreEnregistrementsAjoutes);
            Assert.Equal(0, result.NombreEnregistrementsModifies);
            Assert.Equal(0, result.NombreEnregistrementsDesactives);
            Assert.Equal(0, result.NombreEnregistrementsNonModifies);
        }

        [Fact]
        public void Executer_AvecBdVide_NombreEnregistrementsAjoutes_5()
        {
            // Arrange
            var municipalites = new List<Municipalite>
            {
                new Municipalite { CodeGeographique = 1, NomMunicipalite = "Ville A", AdresseCourriel = "villea@test.com",
                                    AdresseWeb = "www.villea.com", DateProchaineElection = new DateTime(2023, 10, 31) },
                new Municipalite { CodeGeographique = 2, NomMunicipalite = "Ville B", AdresseCourriel = "villeb@test.com",
                                    AdresseWeb = "www.villeb.com", DateProchaineElection = new DateTime(2023, 11, 15) },
                new Municipalite { CodeGeographique = 3, NomMunicipalite = "Ville C", AdresseCourriel = "villec@test.com",
                                    AdresseWeb = "www.villec.com", DateProchaineElection = new DateTime(2023, 12, 5) },
                new Municipalite { CodeGeographique = 4, NomMunicipalite = "Ville D", AdresseCourriel = "villed@test.com",
                                    AdresseWeb = "www.villed.com", DateProchaineElection = new DateTime(2023, 12, 20) },
                new Municipalite { CodeGeographique = 5, NomMunicipalite = "Ville E", AdresseCourriel = "villee@test.com",
                                    AdresseWeb = "www.villee.com", DateProchaineElection = new DateTime(2024, 1, 5) }
            };

            var mockDepotImportation = new Mock<IDepotImportationMunicipalites>();
            mockDepotImportation.Setup(dep => dep.LireMunicipalites()).Returns(municipalites);

            var mockDepotMunicipalites = new Mock<IDepotMunicipalites>();

            var traitement = new TraitementImporterDonneesMunicipalite(mockDepotImportation.Object, mockDepotMunicipalites.Object);

            // Act
            StatistiquesImportationDonnees result = traitement.Executer();

            // Assert
            mockDepotMunicipalites.Verify(mock => mock.AjouterMunicipalite(It.IsAny<Municipalite>()), Times.Exactly(5));

            Assert.Equal(5, result.NombreEnregistrementsAjoutes);
            Assert.Equal(0, result.NombreEnregistrementsModifies);
            Assert.Equal(0, result.NombreEnregistrementsDesactives);
            Assert.Equal(0, result.NombreEnregistrementsNonModifies);
        }

        [Fact]
        public void Executer_Avec1DejaPresentDansBdInactif_NombreEnregistrementsAjoutes_4_modifies_1()
        {
            // Arrange
            var municipalites = new List<Municipalite>
            {
                new Municipalite { CodeGeographique = 1, NomMunicipalite = "Ville A", AdresseCourriel = "villea@test.com",
                                    AdresseWeb = "www.villea.com", DateProchaineElection = new DateTime(2023, 10, 31) },
                new Municipalite { CodeGeographique = 2, NomMunicipalite = "Ville B", AdresseCourriel = "villeb@test.com",
                                    AdresseWeb = "www.villeb.com", DateProchaineElection = new DateTime(2023, 11, 15) },
                new Municipalite { CodeGeographique = 3, NomMunicipalite = "Ville C", AdresseCourriel = "villec@test.com",
                                    AdresseWeb = "www.villec.com", DateProchaineElection = new DateTime(2023, 12, 5) },
                new Municipalite { CodeGeographique = 4, NomMunicipalite = "Ville D", AdresseCourriel = "villed@test.com",
                                    AdresseWeb = "www.villed.com", DateProchaineElection = new DateTime(2023, 12, 20) },
                new Municipalite { CodeGeographique = 5, NomMunicipalite = "Ville E", AdresseCourriel = "villee@test.com",
                                    AdresseWeb = "www.villee.com", DateProchaineElection = new DateTime(2024, 1, 5) }
            };

            var mockDepotImportation = new Mock<IDepotImportationMunicipalites>();
            mockDepotImportation.Setup(dep => dep.LireMunicipalites()).Returns(municipalites);

            var mockDepotMunicipalites = new Mock<IDepotMunicipalites>();

            mockDepotMunicipalites.Setup(dep => dep.ChercherMunicipaliteParCodeGeographique(1)).Returns(
                new Municipalite
                {
                    CodeGeographique = 1,
                    NomMunicipalite = "Ville A",
                    AdresseCourriel = "villea@test.com",
                    AdresseWeb = "www.villea.com",
                    DateProchaineElection = new DateTime(2023, 10, 31)
                }
            );


            var traitement = new TraitementImporterDonneesMunicipalite(mockDepotImportation.Object, mockDepotMunicipalites.Object);

            // Act
            StatistiquesImportationDonnees result = traitement.Executer();

            // Assert
            mockDepotMunicipalites.Verify(mock => mock.AjouterMunicipalite(It.IsAny<Municipalite>()), Times.Exactly(4));
            mockDepotMunicipalites.Verify(mock => mock.MAJMunicipalite(It.IsAny<Municipalite>()), Times.Exactly(5));

            Assert.Equal(4, result.NombreEnregistrementsAjoutes);
            Assert.Equal(1, result.NombreEnregistrementsModifies);
            Assert.Equal(0, result.NombreEnregistrementsDesactives);
            Assert.Equal(0, result.NombreEnregistrementsNonModifies);
        }
    }
}
