using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.Clases
{
    class RegistroHorario
    {
        MySqlConnection con = null;
        public String personal_rut_personal { get; set; }
        public String fecha { get; set; }
        public String hora_llegada { get; set; }
        public String hora_salida { get; set; }

        public RegistroHorario(String personal_rut_personal,String fecha,String hora_llegada,String hora_salida)
        { 
            this.personal_rut_personal = personal_rut_personal;
            this.fecha = fecha;
            this.hora_llegada = hora_llegada;
            this.hora_salida = hora_salida;
        }
        public int save()
        {

            try
            {
                con = new Conexion().getConexion();
                MySqlCommand command = new MySqlCommand();
                command.Connection = con;
                command.CommandText = "Insert into registro_horario (personal_id_personal,fecha,hora_llegada,hora_salida,horario_id_horario)"
                + " VALUES (?personal_id_personal,?fecha,?hora_llegada,?hora_salida,1);";
                MySqlParameter personal_rut_personal = new MySqlParameter("?personal_id_personal", MySqlDbType.Int32, 11);
                MySqlParameter fecha = new MySqlParameter("?fecha", MySqlDbType.VarChar, 45);
                MySqlParameter hora_llegada = new MySqlParameter("?hora_llegada", MySqlDbType.VarChar, 45);
                MySqlParameter hora_salida = new MySqlParameter("?hora_salida", MySqlDbType.VarChar, 45);

                personal_rut_personal.Value = this.personal_rut_personal;
                fecha.Value = this.fecha;
                hora_llegada.Value = this.hora_llegada;
                hora_salida.Value = this.hora_salida;

                command.Parameters.Add(personal_rut_personal);
                command.Parameters.Add(fecha);
                command.Parameters.Add(hora_llegada);
                command.Parameters.Add(hora_salida);

                con.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error RegistroHorario.save() " + e.Message.ToString());
                return 0;
            }
        }

        public int update() {
            try
            {
                con = new Conexion().getConexion();
                MySqlCommand command = new MySqlCommand();
                command.Connection = con;
                command.CommandText = "UPDATE registro_horario set hora_salida=?hora_salida ;";
                //MySqlParameter hora_llegada = new MySqlParameter("?hora_llegada", MySqlDbType.VarChar, 45);
                MySqlParameter hora_salida = new MySqlParameter("?hora_salida", MySqlDbType.VarChar, 45);

                //hora_llegada.Value = this.hora_llegada;
                hora_salida.Value = this.hora_salida;

                //command.Parameters.Add(hora_llegada);
                command.Parameters.Add(hora_salida);

                con.Open();
                return command.ExecuteNonQuery();
          
            }catch(Exception ex){
                Console.WriteLine("ERROR RegistroHorario.update() "+ex.Message);
                return 0;
            }
        }
    }
}
