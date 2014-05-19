using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace WpfApplication1.Clases
{
    class Conexion
    {

       public MySqlConnection getConexion()
        {
            return new MySqlConnection("data source=localhost; user id=root; password=12345678; database=recursos_humanos");
        }
    
        }
}
