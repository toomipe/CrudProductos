using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudProductos.Persistencia
{
    internal class ConexiónMySQL : Conexión
    {
        private MySqlConnection connection;
        private string cadenaconnection;
        public ConexiónMySQL()
        {
            cadenaconnection = "Database=" + database + "; DataSource=" + server + "; User Id= " + user + "; password= " + password;

            connection = new MySqlConnection(cadenaconnection);
        }

        public MySqlConnection GetConnection()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
            }
            catch (Exception e)
            {
                {
                    MessageBox.Show(e.ToString());
                }
            }
            return connection;
        }
    }
}
