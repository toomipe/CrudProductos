using CrudProductos.Persistencia;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudProductos.Lógica
{
    internal class GestorProducto
    {
        private ConexiónMySQL conexiónMySQL;
        private List<Producto> listaProductos;

        public GestorProducto()
        {
            conexiónMySQL = new ConexiónMySQL();
            listaProductos =  new List<Producto>();
        }

        public List<Producto> getProductos(string filtro)
        {
            string QUERY = "SELECT * FROM producto";
            MySqlDataReader mReader = null;
            try
            {
                if (filtro != "")
                {
                    QUERY += " WHERE " + "nombre LIKE '%" + filtro + "%' OR " +
                        "precio LIKE '%" + filtro + "%' OR " +
                        "cantidad LIKE '%" + filtro + "%' OR " +
                        "descripcion LIKE '%" + filtro + "%'";
                }

                MySqlCommand miComando = new MySqlCommand(QUERY);
                miComando.Connection = conexiónMySQL.GetConnection();
                mReader = miComando.ExecuteReader();

                Producto unProducto;
                while (mReader.Read())
                {
                    unProducto = new Producto();
                    unProducto.id = mReader.GetInt32("id");
                    unProducto.nombre = mReader.GetString("nombre");
                    unProducto.precio = mReader.GetInt32("precio");
                    unProducto.descripcion = mReader.GetString("descripcion");
                    unProducto.cantidad = mReader.GetInt32("cantidad");
                    listaProductos.Add(unProducto);
                }
                mReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return listaProductos;
        }

        public void addProducto(Producto producto)
        {
            string QUERY = "INSERT INTO `producto`(`nombre`, `precio`, `cantidad`, `descripcion`)";
            try
            {
                QUERY += "  VALUES ('"+ producto.nombre +"','"+ producto.precio +"','"+ producto.cantidad +
                    "','"+ producto.descripcion +"') ";

                MySqlCommand miComando = new MySqlCommand(QUERY);
                miComando.Connection = conexiónMySQL.GetConnection();
                miComando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void deleteProducto(int id)
        {
            string QUERY = "DELETE FROM `producto` WHERE id=" + id;
            try
            {
                MySqlCommand miComando = new MySqlCommand(QUERY);
                miComando.Connection = conexiónMySQL.GetConnection();
                miComando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void setProducto(Producto producto)
        {
            string QUERY = "UPDATE `producto` SET `nombre`='"+ producto.nombre +"',`precio`='"+ producto.precio +
                "',`cantidad`='"+ producto.cantidad +"',`descripcion`='"+ producto.descripcion +"' WHERE id=" + producto.id;

            // MessageBox.Show(QUERY);
            try
            {
                MySqlCommand miComando = new MySqlCommand(QUERY);
                miComando.Connection = conexiónMySQL.GetConnection();
                miComando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
