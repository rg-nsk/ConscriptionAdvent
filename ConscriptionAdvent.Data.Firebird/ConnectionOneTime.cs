using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConscriptionAdvent.Data.Firebird
{
    public class ConnectionOneTime
    {
        public string Constring { set; get; }

        private ConnectionOneTime() { }
        private static ConnectionOneTime instance;

        public static ConnectionOneTime Instance()
        {
            if (instance == null)
            {
                instance = new ConnectionOneTime();
            }

            return instance;
        }
    }
}
