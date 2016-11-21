using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Memotest
{
    class Configuracion
    {
        public static string ConnectionString
        {
            get
            {
                string strValor = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                return strValor;
                
            }
        }

        public static string RootFolder
        {
            get
            {
                string strValor = ConfigurationManager.AppSettings["RootFolder"].ToString();
                return strValor;
            }
        }
    }
}
