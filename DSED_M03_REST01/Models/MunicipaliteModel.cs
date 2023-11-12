using System.ComponentModel.DataAnnotations;
using srvm = M01_Srv_Municipalite;

namespace DSED_M03_REST01.Models
{
    public class MunicipaliteModel
    {
        [Required]
        public int MunicipaliteID { get; set; }
        [Required]
        public string NomMunicipalite { get; set; }
        public string? AdresseCourriel { get; set; }
        public string? AdresseWeb { get; set; }
        public DateTime? DateProchaineElection { get; set; }
        public bool Actif { get; set; }

        public MunicipaliteModel()
        {
            ;
        }

        public MunicipaliteModel(srvm.Municipalite p_municipalite)
        {
            MunicipaliteID = p_municipalite.CodeGeographique;
            NomMunicipalite = p_municipalite.NomMunicipalite;
            AdresseCourriel = p_municipalite.AdresseCourriel;
            AdresseWeb = p_municipalite.AdresseWeb;
            DateProchaineElection = p_municipalite.DateProchaineElection;
            Actif = false;
        }

        public srvm.Municipalite VersEntite()
        {
            return new srvm.Municipalite(this.MunicipaliteID, this.NomMunicipalite, this.AdresseCourriel, this.AdresseWeb, this.DateProchaineElection);
        }
    }
}
