using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace LibBase_Datos
{
    public class MySql
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        MySqlDataReader dr;
        DataTable dt;
        public static String errorMsge;
        public static String usuario;
        public static int id_usuario;
        public static int nivel;
        private bool res;
        private int ResultadoQueryORM;

        public MySql(string server, string db, string us, string pwd)
        {
            string cs = ("Server=" + server + ";Database=" + db + ";User Id=" + us + ";Password=" + pwd + ";");
            cn = new MySqlConnection(cs);
        }

        public bool AbrirConexión()
        {
            res = false;
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                    res = true;
                }
            }
            catch(MySqlException mysqlex)
            {
                errorMsge = ("Error al abrir la conexión a la base de datos: " + mysqlex.ToString());
            }
            catch(Exception ex)
            {
                errorMsge = ("Error general al abrir la conexión: " + ex.ToString());
            }
            return res;
        }

        public bool CerrarConexión()
        {
            res = false;
            try
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    res = true;
                }
            }
            catch (MySqlException mysqlex)
            {
                errorMsge = ("Error al cerrar la conexión en la base de datos: " + mysqlex.ToString());
            }
            catch(Exception ex)
            {
                errorMsge = ("Error general al cerrar la conexión: " + ex.ToString());
            }
            return res;
        }

        public bool IniciarSesión(String usuario, String pwd)
        {
            try
            {
                if (AbrirConexión())
                {
                    cmd = new MySqlCommand("SELECT * FROM usuarios WHERE  usuario_usuario= '" + usuario + "' AND " + "password_usuario= '" + pwd + "';", cn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (!dr.HasRows)
                    {
                        nivel = 5;
                        errorMsge = "Usuario no existe";
                        res = false;
                    }
                    else
                    {
                        usuario = dr[2].ToString() + " " + dr[3].ToString() + " " + dr[4].ToString(); ;
                        nivel = Convert.ToInt16(dr[1].ToString());
                        id_usuario = Convert.ToInt32(dr[0].ToString());
                        res = true;
                    }
                }
            }
            catch(MySqlException mysqlex)
            {
                errorMsge = ("Error en la base de datos al iniciar sesión: " + mysqlex.ToString());
            }
            catch(Exception ex)
            {
                errorMsge = ("Error general al iniciar sesión: " + ex.ToString());
            }
            finally
            {
                CerrarConexión();
            }
            return res;
        }

        public bool Registrar(String tabla, String campos, String valores)
        {
            res = false;
            try
            {
                if (AbrirConexión())
                {
                    cmd = new MySqlCommand("INSERT INTO '" + tabla + "' (" + campos + ") VALUES ('" + valores + "');", cn);
                    cmd.ExecuteNonQuery();
                    res = true;
                }
            }
            catch(MySqlException mysqlex)
            {
                errorMsge = ("Error en la base de datos al realizar el registro: " + mysqlex.ToString());
            }
            catch(Exception ex)
            {
                errorMsge = ("Error generar al realizar el registro: " + ex.ToString());
            }
            finally
            {
                CerrarConexión();
            }
            return res;
        }

        public bool BorrarRegistro(String tabla, String where)
        {
            res = false;
            try
            {
                if(AbrirConexión())
                {
                    cmd = new MySqlCommand("DELETE FROM '" + tabla + "' WHERE '" + where + "'", cn);
                    cmd.ExecuteNonQuery();
                    res = true;
                }
            }
            catch (MySqlException mysql)
            {
                errorMsge = ("Error en la base de datos al borrar el registro: " + mysql.ToString());
            }
            catch(Exception ex)
            {
                errorMsge = ("Error general al borrar el registro: " + ex.ToString());
            }
            finally
            {
                CerrarConexión();
            }
            return res;
        }

        public bool ActualizarRegistro(String tabla, String camposValores, String where)
        {
            res = false;
            try
            {
                if(AbrirConexión())
                {
                    cmd = new MySqlCommand("UPDATE '" + tabla + "' SET " + camposValores + " WHERE '" + where + "';", cn);
                    cmd.ExecuteNonQuery();
                    res = true;
                }
            }
            catch (MySqlException mysql)
            {
                errorMsge = ("Error en la base de datos al actualizar el registro: " + mysql.ToString());
            }
            catch(Exception ex)
            {
                errorMsge = ("Error general al actualizar el registro: " + ex.ToString());
            }
            finally
            {
                CerrarConexión();
            }
            return res;
        }

        public DataTable ConsultaTodosLosDatos(String tabla)
        {
            try
            {
                if (AbrirConexión())
                {
                    dt = new DataTable();
                    da = new MySqlDataAdapter("SELECT * FROM '" + tabla + "';", cn);
                    da.Fill(dt);
                }
                else
                {
                    errorMsge = ("No se encontraron registros");
                }
            }
            catch(MySqlException mysqlex)
            {
                errorMsge = ("Error en la base de datos al realizar la consulta: " + mysqlex.ToString());
            }
            catch(Exception ex)
            {
                errorMsge = ("Error general al realizar la consulta: " + ex.ToString());
            }
            finally
            {
                CerrarConexión();
            }
            return dt;
        }

        public DataTable ConsultaEspecífica(String tabla, String campos, String where)
        {
            try
            {
                if(AbrirConexión())
                {
                    dt = new DataTable();
                    da = new MySqlDataAdapter("SELECT '" + campos + "' FROM '" + tabla + "' WHERE '" + where + "';", cn);
                    da.Fill(dt);
                }
                else
                {
                    errorMsge = ("No se encontraron registros");
                }
            }
            catch(MySqlException mysqlex)
            {
                errorMsge = ("Error en la base de datos al realizar la consulta: " + mysqlex.ToString());
            }
            catch(Exception ex)
            {
                errorMsge = ("Error general al realizar la consulta: " + ex.ToString());
            }
            finally
            {
                CerrarConexión();
            }
            return dt;
        }

        public bool ExecuteTableQuery(string Query)
        {
            res = false;
            try
            {
                if(AbrirConexión())
                {
                    cmd = new MySqlCommand(Query, cn);
                    ResultadoQueryORM =  cmd.ExecuteNonQuery();
                    res = true;
                }
            }
            catch (MySqlException mysqlex)
            {
                errorMsge = "Error de MySql al ejecutar la instrucción: " + mysqlex.ToString();
            }
            catch (Exception ex)
            {
                errorMsge = "Error al ejecutar la instrucción: " + ex.ToString();
            }
            finally
            {
                CerrarConexión();
            }
            return res;
        }
    }
}