using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;

namespace LibBase_Datos
{
    public class Tabla
    {
        public Tabla()
        {
            try
            {
                Tabla.estado = BanderaDeEstado.CREACION;
            }
            catch (Exception ex)
            {
                error = "Error: " + ex.ToString();
            }
        }

        public static BanderaDeEstado estado;
        public string error;
        protected int id;
        protected string comment;
        public List<Columna> columns = new List<Columna>();
        public string table_name;

        public string CreateTable()
        {
            string sentence = "DROP TABLE IF EXISTS " + this.table_name + "; CREATE TABLE ";
            try
            {
                sentence += this.table_name;
                sentence += " (Id int(16) NOT NULL PRIMARY KEY unsigned AUTO_INCREMENT,";
                foreach (Columna col in this.columns)
                {
                    int con = 0;
                    object _default = "";
                    string longitud = "";
                    string no_nulo = "";
                    if (col.lenght != 0)
                    {
                        longitud = "(" + col.lenght + " )";
                    }
                    if (!(col.default_value is null))
                    {
                        if (col.column_type == TipoDeCampo.Varchar || col.column_type == TipoDeCampo.Char || col.column_type == TipoDeCampo.Text)
                        {
                            _default = "DEFAULT '" + col.default_value + "'";
                        }
                        else
                        {
                            _default = "DEFAULT " + col.default_value;
                        }
                    }
                    if (col.column_comment == string.Empty)
                    {
                        comment = "COMMENT '" + col.column_comment + "'";
                    }
                    if (col.nulleable)
                    {
                        no_nulo = " NOT NULL ";
                    }
                    else
                    {
                        no_nulo = " NULL ";
                    }
                    sentence += " " + col.nombre + " " + col.column_type + longitud + " " + no_nulo + " " + _default + " " + comment;
                    con++;
                    if (con != columns.Count)
                    {
                        sentence += ", ";
                    }
                }
                sentence += ");";
            }
            catch (Exception ex)
            {
                error = "Error: " + ex.ToString();
                Tabla.estado = BanderaDeEstado.ERROR_DE_SINTAXYS;
            }
            return sentence;
        }

        public bool CrearLaTabla()
        {
            CrearTabla tabla = new CrearTabla();
            tabla.CrearTabla();
        }//Fin método CrearLaTabla().

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
    }//Fin clase Tabla.

    public class Usuarios : Tabla
    {
        public class SubUsuarios : Usuarios
        {

        }//Fin clase SubUsuarios.

        public Usuarios()
        {
            this.comment = "En esta tabla se guardará la información de los usuarios";
            this.columns.Add(new Columna("Nombre", TipoDeCampo.Varchar, 50, false, null, Indice.No, "Nombre", null));
            this.columns.Add(new Columna("Apellidos", TipoDeCampo.Varchar, 50, false, null, Indice.No, "Apellido", null));
            this.columns.Add(new Columna("Correo", TipoDeCampo.Varchar, 50, false, null, Indice.No, "Correo", null));
            this.columns.Add(new Columna("Teléfono", TipoDeCampo.Varchar, 10, true, null, Indice.No, "Teléfono", null));
            this.columns.Add(new Columna("RFC", TipoDeCampo.Varchar, 15, false, true, Indice.No, "RFC", null));
        }

        public void CrearCampo(string nom, TipoDeCampo tipo_columna, int largo_campo, bool es_null, object valor_default, Indice llave, string coment_column)
        {
            this.columns.Add(new Columna(nom, tipo_columna, largo_campo, es_null, valor_default, llave, coment_column,""));
        }

        public List<Usuarios> ColeccionRegistros = new List<Usuarios>();

        public string Name
        {
            set { this.columns[0].value = value; }
            get { return this.columns[0].value.ToString(); }
        }

        public string Apellido
        {
            set
            {
                if (value.Length == 10)
                {
                    this.columns[1].value = value;
                }
                else
                {
                    this.columns[1].value = "Formato incorrecto";
                }
            }
            get
            {
                return this.columns[0].value.ToString();
            }
        }

        public string Correo
        {
            set { this.columns[2].value = value; }
            get { return this.columns[4].value.ToString(); }
        }

        public string Telefono
        {
            set { this.columns[3].value = value; }
            get { return this.columns[0].value.ToString(); }
        }

        public string Rfc
        {
            set { this.columns[4].value = value; }
            get { return this.columns[4].value.ToString(); }
        }
    }//Fin clase Usuario.

    //public class Ventas : Tabla
    //{
    //    public Usuarios users = new Usuarios();

    //    public void DatosUsuarios()
    //    {
    //        users.Name = "Eliott";
    //        users.Apellido = "Salazar";
    //        users.Correo = "eliottsalazar@gmail.com";
    //        users.Telefono = "6622814620";
    //        users.Rfc = "kjdaiosdhiash23";
    //        string format = users.Telefono + "Incorrecto";
    //    }
    //}

    public enum TipoDeCampo
    {
        TinyInt, Int, Float, Double, Char, Varchar, Text, Date, TimeStamp, DateTime, Boolean
    }

    public enum Indice
    {
        No, Primary, Key,Unique, Mul, Spatial
    }

    public enum BanderaDeEstado
    {
        CREACION, CREADA, ALTERADA, BORRADO, ERROR_DE_SINTAXYS
    }
}