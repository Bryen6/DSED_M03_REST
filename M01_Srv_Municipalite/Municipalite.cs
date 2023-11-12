using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace M01_Srv_Municipalite
{
    public class Municipalite
    {
        [Name("mcode")]
        public int CodeGeographique { get; set; }
        [Name("munnom")]
        public string NomMunicipalite { get; set; }
        [Name("mcourriel")]
        public string AdresseCourriel { get; set; }
        [Name("mweb")]
        public string AdresseWeb { get; set; }
        [Name("datelec")]
        public DateTime? DateProchaineElection { get; set; }

        public Municipalite()
        {
            ;
        }

        public Municipalite(int p_codeGeographique, string p_nomMunicipalite, string p_adresseCourriel,
                            string p_adresseWeb, DateTime? p_dateProchaineElection)
        {
            CodeGeographique = p_codeGeographique;
            NomMunicipalite = p_nomMunicipalite;
            AdresseCourriel = p_adresseCourriel;
            AdresseWeb = p_adresseWeb;
            DateProchaineElection = p_dateProchaineElection;
        }

        public override bool Equals(object? p_obj)
        {
            if (p_obj is null || !(p_obj is Municipalite))
            {
                return false;
            }

            Municipalite municipaliteATester = (Municipalite)p_obj;

            return this.CodeGeographique == municipaliteATester.CodeGeographique
                && this.NomMunicipalite == municipaliteATester.NomMunicipalite
                && this.AdresseCourriel == municipaliteATester.AdresseCourriel
                && this.AdresseWeb == municipaliteATester.AdresseWeb
                && this.DateProchaineElection == municipaliteATester.DateProchaineElection;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.CodeGeographique, this.NomMunicipalite, this.AdresseCourriel, this.AdresseWeb, this.DateProchaineElection);
        }
    }
}
