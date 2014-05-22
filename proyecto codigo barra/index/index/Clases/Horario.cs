using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.Clases
{
    class Horario
    {
        MySqlConnection con = null;
        public Horario() { }

        public int save() {
            
            try {
                con = new Conexion().getConexion();
                MySqlCommand command = new MySqlCommand();
                command.Connection = con;
                command.CommandText = "Insert into personal ()"
                + " VALUES ();";
                //MySqlParameter fileNameParameter = new MySqlParameter("?rut", MySqlDbType.VarChar, 20);
                //fileNameParameter.Value = this.rut;
                //command.Parameters.Add(fileNameParameter);
                con.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception e) { 
                Console.WriteLine("Error Horario.save() "+e.Message.ToString());
                return 0;
            }
        }
    }
}
