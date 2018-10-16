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
            public Columna(string nom, TipoDeCampo tipo_columna, int largo_campo, bool es_null, object valor_default, Indice llave, string coment_column, object value)
            {
                nom = nombre;
                tipo_columna = column_type;
                largo_campo = lenght;
                es_null = nulleable;
                valor_default = default_value;
                llave = key;
                coment_column = column_comment;
                this.value = value;
            }
            public string nombre;
            public TipoDeCampo column_type;
            public int lenght;
            public bool nulleable;
            public object default_value;
            public Indice key;
            public string column_comment;
            public object value;
        }
    }

    public class Usuarios : Tabla
    {
        public Usuarios()
        {
            this.comment = "En esta tabla se guardará la información de los usuarios";
            this.columns.Add(new Columna("Nombre", TipoDeCampo.Varchar, 50, false, null, Indice.No, "Nombre de los usuarios", ""));
            this.columns.Add(new Columna("RFC", TipoDeCampo.Varchar, 13, true, null, Indice.No, "RFC para facturación", ""));
        }

        public void CrearCampo(string nom, TipoDeCampo tipo_columna, int largo_campo, bool es_null, object valor_default, Indice llave, string coment_column)
        {
            this.columns.Add(new Columna(nom, tipo_columna, largo_campo, es_null, valor_default, llave, coment_column));
        }
    }

    public class Ventas : Tabla
    {
        public Usuarios users = new Usuarios();

        public void DatosUsuarios()
        {
            users.columns[0].value = "Alexis";
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
