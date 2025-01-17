﻿using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modelo
{
    public class Sentencias
    {
        Conexion con = new Conexion();

        public string[] queryLogin(string user)
        {
            string[] Campos = new string[300];
            string[] auto = new string[300];
            int i = 0;
            string datos = "pk_id_usuario, username_usuario, password_usuario";
            string sql = "SELECT " + datos + " FROM tbl_usuarios where username_usuario='" + user + "';";
            try
            {
                OdbcCommand command = new OdbcCommand(sql, con.conexion());
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Campos[i] = reader.GetValue(0).ToString();
                    Campos[(i + 1)] = reader.GetValue(1).ToString();
                    Campos[(i + 2)] = reader.GetValue(2).ToString();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message.ToString() + " \nError en query hacia la tabla de usuarios"); }
            return Campos;
        }

        public void insertBitacora(string values)
        {
            string campos = "fk_id_usuario, fk_id_aplicacion, fecha_bitacora, hora_bitacora, host_bitacora, ip_bitacora, accion_bitacora";
            string sql = "INSERT INTO tbl_bitacoraDeEventos (" + campos + ") values (" + values + ");";
            OdbcCommand cmd = new OdbcCommand(sql, con.conexion());
            cmd.ExecuteNonQuery();
        }

        public string queryNombreApp(string app)
        {
            string nombreApp = "";
            string sql = "SELECT nombre_aplicacion FROM tbl_aplicaciones where pk_id_aplicacion='" + app + "';";
            try
            {
                OdbcCommand command = new OdbcCommand(sql, con.conexion());
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nombreApp = reader.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + " \nError en obtener el nombre de la aplicación");
            }
            return nombreApp;
        }

        public int[] getPerfilesUsuario(string user)
        {
            int[] perfiles = new int[100];
            int i = 0;
            string sql = "SELECT fk_id_perfil FROM tbl_asignacionesPerfilsUsuario WHERE fk_id_usuario='" + user + "';";
            try
            {
                OdbcCommand command = new OdbcCommand(sql, con.conexion());
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    perfiles[i] = int.Parse(reader.GetValue(0).ToString());
                    i++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + " \nError en obtener el nombre de la aplicación");
            }

            return perfiles;
        }

        public int[] getPerfilAplicacion(int perfil)
        {
            int[] modulos = new int[300];
            int i = 0;
            string sql = "SELECT fk_id_aplicacion FROM tbl_permisosAplicacionPerfil WHERE fk_id_perfil='" + perfil + "';";
            try
            {
                OdbcCommand command = new OdbcCommand(sql, con.conexion());
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    modulos[i] = int.Parse(reader.GetValue(0).ToString());
                    i++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + " \nError en obtener las aplicaciones del perfil");
            }
            return modulos;
        }

        public int getModuloAplicacion(int aplicacion)
        {
            int idModulo = 0;
            int i = 0;
            string sql = "SELECT fk_id_modulos FROM tbl_AsignacionModuloAplicacion where fk_id_aplicacion='" + aplicacion + "';";
            try
            {
                OdbcCommand command = new OdbcCommand(sql, con.conexion());
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    idModulo = int.Parse(reader.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + " \nError en obtener el modulo de la aplicacion #" + aplicacion);
            }

            return idModulo;
        }

        public int[] getPermisos(int perfil, int aplicacion)
        {
            int[] permisos = new int[5];
            int i = 0;
            string campos = "guardar_permiso, modificar_permiso, eliminar_permiso, buscar_permiso, imprimir_permiso";
            string sql = "SELECT " + campos + " FROM tbl_permisosAplicacionPerfil WHERE fk_id_perfil='" + perfil + "' AND " + aplicacion + ";";
            try
            {
                OdbcCommand command = new OdbcCommand(sql, con.conexion());
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    permisos[i] = int.Parse(reader.GetValue(0).ToString());
                    permisos[i + 1] = int.Parse(reader.GetValue(1).ToString());
                    permisos[i + 2] = int.Parse(reader.GetValue(2).ToString());
                    permisos[i + 3] = int.Parse(reader.GetValue(3).ToString());
                    permisos[i + 4] = int.Parse(reader.GetValue(4).ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + " \nError en obtener las aplicaciones del perfil");
            }
            return permisos;
        }


        public OdbcDataAdapter buscarlogin(string tabla, string dato1, string dato2)
        {
            
            string sql = "SELECT usuario, contra FROM " + tabla + " where usuario='" +dato1+ "' and contra='" +dato2+"';" ;
            OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, con.conexion());
            return dataTable;
        }

        public void insertar(string dato, string tipo, string tabla)
        {
            string sql = "insert into " + tabla + "(" + tipo + ") values (" + dato + ")";
            OdbcCommand cmd = new OdbcCommand(sql, con.conexion());
            cmd.ExecuteNonQuery();
        }

        public void busqueda(TextBox[] textbox, string tabla,int num, string condicion)
        {
            string sql = "Select *from " + tabla + " where "+ condicion +" " + num+ ";" ;
            OdbcCommand cmd = new OdbcCommand(sql, con.conexion());
            cmd.ExecuteNonQuery();

            OdbcDataReader leer = cmd.ExecuteReader();
            if (leer.Read()  == true)
            {
                //MessageBox.Show("Ingreso");
                //
                /*string dato1 = leer[txtCamps[0].ToString()].ToString();
                string dato2 = leer[txtCamps[1].ToString()].ToString();
                string dato3 = leer[txtCamps[2].ToString()].ToString();
                string dato4 = leer[txtCamps[3].ToString()].ToString();*/
                MessageBox.Show("Encontrado ");
            }
            else
            {
                MessageBox.Show("No encontrado");
            }
        }

        public OdbcDataAdapter llenarTbl(string tabla)// metodo  que obtinene el contenio de una tabla
        {
            //string para almacenar los campos de OBTENERCAMPOS y utilizar el 1ro
            string sql = "SELECT * FROM " + tabla + "  ;";
            OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, con.conexion());
            return dataTable;
        }


        public void actualizar(string dato, string condicion, string tabla,int num)
        {

            string sql = "Update " + tabla + " " + dato + " where " + condicion + " " + num+ "; ";
            OdbcCommand cmd = new OdbcCommand(sql, con.conexion());
            cmd.ExecuteNonQuery();

        }

        public void eliminar(string tabla,string condicion,int campo)
        {
            string sql = "delete from " + tabla + " where " + condicion + " " + campo + " ;";
            OdbcCommand cmd = new OdbcCommand(sql, con.conexion());
            cmd.ExecuteNonQuery();
        }

        

        public void actualizarcontra(string dato, string condicion, string tabla, int num)
        {

            string sql = "Update " + tabla + " " + dato + " where " + condicion + " " + num + "; ";
            OdbcCommand cmd = new OdbcCommand(sql, con.conexion());
            cmd.ExecuteNonQuery();

        }

      




    }
}
