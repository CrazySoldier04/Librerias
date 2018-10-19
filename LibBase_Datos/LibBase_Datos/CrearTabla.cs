using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibBase_Datos;

namespace LibBase_Datos
{
    public class CrearTabla
    {
        MySql libm = new MySql("localhost", "mex_dev_so", "root","salazar");
        bool res;

        public bool CrearTabla(string Query)
        {
            res = libm.ExecuteTableQuery(Query);
            if (res)
            {

            }
        }
    }
}