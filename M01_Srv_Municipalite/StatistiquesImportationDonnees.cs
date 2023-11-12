using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Srv_Municipalite
{
    public class StatistiquesImportationDonnees
    {
        public int NombreEnregistrementsAjoutes { get; set; }
        public int NombreEnregistrementsModifies { get; set; }
        public int NombreEnregistrementsDesactives { get; set; }
        public int NombreEnregistrementsNonModifies { get; set; }

        public override string ToString()
        {
            return $"Enregistrements Ajoutés : {NombreEnregistrementsAjoutes}\n" +
                    $"Enregistrements Modifiés : {NombreEnregistrementsModifies}\n" +
                    $"Enregistrements Desactivés : {NombreEnregistrementsDesactives}\n" +
                    $"Enregistrements NonModifiés : {NombreEnregistrementsNonModifies}\n" +
                    $"Total : {NombreEnregistrementsAjoutes + NombreEnregistrementsModifies + NombreEnregistrementsDesactives + NombreEnregistrementsNonModifies}";
        }
    }
}
