using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBase_Datos
{
    public interface IBD
    {
        bool ExecuteTableQuery(string Query);
    }
}