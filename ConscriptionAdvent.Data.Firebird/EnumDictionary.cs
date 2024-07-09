using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Odbc;

namespace ConscriptionAdvent.Data.Firebird
{
    public class EnumDictionary
    {
        public List<string> Vidvslist { get; set; }
        public List<string> Gorodalist { get; set; }
        public List<string> Railroadlist { get; set; }
        public List<string> Vokruglist { get; set; }
        public List<string> Rezhkomlist { get; set; }
        public List<string> Speclist { get; set; } 
        public List<string> Zvanlist { get; set; }
        public List<string> Rvklist { get; set; }


        public EnumDictionary(string initialCatalog)
        {

            if (string.IsNullOrWhiteSpace(initialCatalog))
            {
                throw new ArgumentNullException(nameof(initialCatalog));
            }

            using (var ctx1 = new FormDbContext("FormDbContext", initialCatalog))
            {
                Speclist = ctx1.SPEC.Select(g => g.NAME).OrderBy(g => g).ToList();
            }
            Speclist.Insert(0, "");
        }
    }
}
