using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using srvm = M01_Srv_Municipalite;

namespace M01_DAL_Municipalite_SQLServer
{
    public class DepotMunicipalitesMySQL : srvm.IDepotMunicipalites
    {
        private MunicipaliteContextMYSQL m_context;

        public DepotMunicipalitesMySQL(MunicipaliteContextMYSQL p_contexte)
        {
            this.m_context = p_contexte ?? throw new ArgumentNullException(nameof(p_contexte));
        }

        public void Dispose()
        {
            ;
        }

        public void AjouterMunicipalite(srvm.Municipalite p_municipalite)
        {
            IQueryable<Municipalite> requete = this.m_context.MUNICIPALITES;
            Municipalite municipaliteCherchee = requete.SingleOrDefault(m => m.MunicipaliteID == p_municipalite.CodeGeographique);

            if (municipaliteCherchee is null)
            {
                Municipalite nouvelleMunicipalite = new Municipalite(p_municipalite);
                nouvelleMunicipalite.Actif = true;
                this.m_context.MUNICIPALITES.Add(nouvelleMunicipalite);
                this.m_context.SaveChanges();
                this.m_context.ChangeTracker.Clear();
                p_municipalite.CodeGeographique = nouvelleMunicipalite.MunicipaliteID;
            }
        }

        public srvm.Municipalite? ChercherMunicipaliteParCodeGeographique(int p_codeGeographique)
        {
            IQueryable<Municipalite> requete = this.m_context.MUNICIPALITES;
            return requete.SingleOrDefault(m => m.MunicipaliteID == p_codeGeographique)?.VersEntite();
        }

        public void DesactiverMunicipalite(srvm.Municipalite p_municipalite)
        {
            IQueryable<Municipalite> requete = this.m_context.MUNICIPALITES;
            Municipalite municipaliteCherchee = requete.SingleOrDefault(m => m.MunicipaliteID == p_municipalite.CodeGeographique);

            if (municipaliteCherchee is not null)
            {
                municipaliteCherchee.Actif = false;
                this.m_context.SaveChanges();
                this.m_context.ChangeTracker.Clear();
            }
            else
            {
                Console.WriteLine("La municipalité n'a pas été trouvée");
            }
        }

        public IEnumerable<srvm.Municipalite> ListerMunicipalitesActives()
        {
            try
            {
                IQueryable<Municipalite> requete = this.m_context.MUNICIPALITES;
                List<Municipalite> municipalites = requete.Where(m => m.Actif == true).ToList();

                return municipalites.Select(m => m.VersEntite()).ToList();
            }
            catch (Exception)
            {
                return new List<srvm.Municipalite>();
            }
        }

        public void MAJMunicipalite(srvm.Municipalite p_municipalite)
        {
            IQueryable<Municipalite> requete = this.m_context.MUNICIPALITES;
            Municipalite municipaliteCherchee = requete.SingleOrDefault(m => m.MunicipaliteID == p_municipalite.CodeGeographique);

            if (municipaliteCherchee is not null)
            {
                municipaliteCherchee.NomMunicipalite = p_municipalite.NomMunicipalite;
                municipaliteCherchee.AdresseCourriel = p_municipalite.AdresseCourriel;
                municipaliteCherchee.AdresseWeb = p_municipalite.AdresseWeb;
                municipaliteCherchee.DateProchaineElection = p_municipalite.DateProchaineElection;
                municipaliteCherchee.Actif = true;
                this.m_context.SaveChanges();
            }
        }
    }
}
