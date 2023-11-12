using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using srvm = M01_Srv_Municipalite;

namespace M01_DAL_Municipalite_SQLServer
{
    public class Municipalite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MunicipaliteID { get; set; }

        public string NomMunicipalite { get; set; }
        public string? AdresseCourriel { get; set; }
        public string? AdresseWeb { get; set; }
        public DateTime? DateProchaineElection { get; set; }
        public bool Actif { get; set; }

        public Municipalite()
        {
            ;
        }

        public Municipalite(srvm.Municipalite p_municipalite)
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
