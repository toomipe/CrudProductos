using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudProductos.Lógica;

namespace CrudProductos.Presentación


{
    public partial class PantallaProductos : Form
    {
        private List<Producto> listaProductos;
        private GestorProducto gestorProducto;
        public PantallaProductos()
        {
            InitializeComponent();
            listaProductos = new List<Producto>();
            gestorProducto = new GestorProducto();
            cargarProductos();
        }

        private void cargarProductos(string filtro = "")
        {
            dgv_productos.Rows.Clear();
            dgv_productos.Refresh();
            listaProductos.Clear();
            listaProductos = gestorProducto.getProductos(filtro);

            foreach (Producto producto in listaProductos)
            {
                dgv_productos.Rows.Add(
                    producto.id,
                    producto.nombre,
                    producto.precio,
                    producto.cantidad,
                    producto.descripcion
                    );
            }
        }

        private void agregarProducto(string nombre, int precio, int cantidad, string descripcion)
        {
            Producto producto = new Producto();
            producto.nombre = nombre;
            producto.precio = precio;
            producto.cantidad = cantidad;
            producto.descripcion = descripcion;

            gestorProducto.addProducto(producto);
            txt_nombre.Clear();
            txt_precio.Clear();
            txt_cantidad.Clear();
            txt_descripcion.Clear();
            cargarProductos();
        }

        private void modificarProducto(int id, string nombre, int precio, int cantidad, string descripcion)
        {
            Producto producto = new Producto();
            producto.id = id;
            producto.nombre = nombre;
            producto.precio = precio;
            producto.cantidad = cantidad;
            producto.descripcion = descripcion;

            gestorProducto.setProducto(producto);
            txt_nombre.Clear();
            txt_precio.Clear();
            txt_cantidad.Clear();
            txt_descripcion.Clear();
            cargarProductos();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void txt_busqueda_TextChanged(object sender, EventArgs e)
        {
            cargarProductos(txt_busqueda.Text.Trim());
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
                agregarProducto(txt_nombre.Text, int.Parse(txt_precio.Text), int.Parse(txt_cantidad.Text), txt_descripcion.Text);
            }
            catch 
            {
                MessageBox.Show("Precio y Cantidad deben ser valores numéricos.");
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            DataGridViewRow registroSeleccionado = this.registroSeleccionado();
            if (registroSeleccionado != null )
            {
                gestorProducto.deleteProducto(Convert.ToInt32(registroSeleccionado.Cells["ID"].Value));
                cargarProductos();
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar.");
            }
        }

        private DataGridViewRow registroSeleccionado()
        {
            if (dgv_productos.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                return dgv_productos.SelectedRows[0];
            }
            return null;
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            DataGridViewRow registroSeleccionado = this.registroSeleccionado();
            if (registroSeleccionado != null)
            {
                try
                {
                    modificarProducto(Convert.ToInt32(registroSeleccionado.Cells["ID"].Value), txt_nombre.Text, int.Parse(txt_precio.Text), int.Parse(txt_cantidad.Text), txt_descripcion.Text);
                    cargarProductos();
                }
                catch
                {
                    MessageBox.Show("Precio y Cantidad deben ser valores numéricos.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para modificar.");
            }
        }

        private void filaSeleccionada_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow registroSeleccionado = dgv_productos.Rows[e.RowIndex];
                txt_nombre.Text = registroSeleccionado.Cells["nombreProducto"].Value.ToString();
                txt_precio.Text = registroSeleccionado.Cells["precioProducto"].Value.ToString();
                txt_cantidad.Text = registroSeleccionado.Cells["cantidadProducto"].Value.ToString();
                txt_descripcion.Text = registroSeleccionado.Cells["descripcionProducto"].Value.ToString();
                btn_agregar.Text = "Duplicar";
            }
        }

        private void filaSeleccionada_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_productos.ClearSelection();
            txt_nombre.Clear();
            txt_precio.Clear();
            txt_cantidad.Clear();
            txt_descripcion.Clear();
            cargarProductos();
        }
    }
}