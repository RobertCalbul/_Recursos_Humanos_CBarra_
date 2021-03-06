﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.Clases
{
    class Persona
    {
        MySqlConnection con = null;
        public String rut { get; set; }
        public String nombre { get; set; }
        public String fecha_nac {get; set;}
        public Persona() { }
        public Persona(String rut)
        {
            this.rut = rut;
        }
        public Persona(String rut, String nombre,String fecha_nac)
        {
            this.rut = rut;
            this.nombre = nombre;
            this.fecha_nac = fecha_nac;
        }
        public int get_idPersonal()
        {
            try
            {
                con = new Clases.Conexion().getConexion();
                con.Open();

                MySqlCommand sqlCom = new MySqlCommand(string.Format("SELECT id_personal FROM personal WHERE rut = '{0}'", this.rut), con);
                MySqlDataReader res = sqlCom.ExecuteReader();

                int resultados = -1;

                while (res.Read()) resultados = res.GetInt32(0);
                con.Close();
                return resultados;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public Persona findByRut()
        {
            Persona per = null;
            try
            {
                con = new Conexion().getConexion();
                con.Open();
                MySqlCommand sqlCom = new MySqlCommand(string.Format("SELECT rut, nombre, fecha_nacimiento FROM personal WHERE rut = '{0}'", this.rut), con);
                MySqlDataReader res = sqlCom.ExecuteReader();
                while (res.Read())
                {
                    per = new Persona(res.GetString(0), res.GetString(1), res.GetString(2));
                }
                con.Close();
                return per;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Persona.findByRut() "+e.Message.ToString());
                return per;               
            }
            finally
            {
                con.Close();
            }
        }
    }
}
