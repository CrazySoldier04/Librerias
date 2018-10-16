using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBase_Datos
{
    public class Tabla
    {
        protected int id;
        protected string comment;
        public List<Columna> columns;

        public class Columna
        {
            public Columna(string nom, TipoDeCampo tipo_columna, int largo_campo, bool es_null, object valor_default, Indice llave, string coment_column)
            {
                nom = nombre;
                tipo_columna = column_type;
                largo_campo = lenght;
                es_null = nulleable;
                valor_default = default_value;
                llave = key;
                coment_column = column_comment;
            }
            public string nombre;
            public TipoDeCampo column_type;
            public int lenght;
            public bool nulleable;
            public object default_value;
            public Indice key;
            public string column_comment;
        }
    }

    public class Usuarios : Tabla
    {
        public void CrearCampo(string nom, TipoDeCampo tipo_columna, int largo_campo, bool es_null, object valor_default, Indice llave, string coment_column)
        {

        }
    }

    public enum TipoDeCampo
    {
        TinyInt, Int, Float, Double, Varchar, Text, Date, TimeStamp, DateTime, Boolean
    }

    public enum Indice
    {
        No, Primary, Key,Unique, Mul, Spatial
    }
}
