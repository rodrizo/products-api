using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductoCRUD.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Cors;

namespace ProductoCRUD.Controllers
{
    [EnableCors("corsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly string stringSQL;

        public ProductoController(IConfiguration config)
        {
            stringSQL = config.GetConnectionString("StringSQL");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Producto> list = new List<Producto>();

            try
            {
                using (var conexion = new SqlConnection(stringSQL))
                {
                    conexion.Open();
                    var command = new SqlCommand("Listar", conexion);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                Nombre = (reader["Nombre"].ToString()),
                                Marca = (reader["Marca"].ToString()),
                                Categoria = (reader["Categoria"].ToString()),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"])
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = list });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = e.Message, response = list });
            }
        }

        [HttpGet]
        [Route("Get/{idProducto:int}")]
        public IActionResult Get(int idProducto)
        {
            List<Producto> list = new List<Producto>();
            Producto producto = new Producto();

            try
            {
                using (var conexion = new SqlConnection(stringSQL))
                {
                    conexion.Open();
                    var command = new SqlCommand("Listar", conexion);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                Nombre = (reader["Nombre"].ToString()),
                                Marca = (reader["Marca"].ToString()),
                                Categoria = (reader["Categoria"].ToString()),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"])
                            });
                        }
                    }

                }
                producto = list.Where(item => item.IdProducto == idProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = producto });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = e.Message, response = producto });
            }
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Get([FromBody] Producto producto)
        {

            try
            {
                using (var conexion = new SqlConnection(stringSQL))
                {
                    conexion.Open();
                    var command = new SqlCommand("Guardar", conexion);
                    command.Parameters.AddWithValue("Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("Marca", producto.Marca);
                    command.Parameters.AddWithValue("Categoria", producto.Categoria);
                    command.Parameters.AddWithValue("Precio", producto.Precio);
                    command.Parameters.AddWithValue("Cantidad", producto.Cantidad);
                    command.CommandType = CommandType.StoredProcedure;

                    command.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" } );
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = e.Message });
            }
        }

        [HttpPut]
        [Route("Edit/{idProducto:int}")]
        public IActionResult Edit([FromBody] Producto producto)
        {

            try
            {
                using (var conexion = new SqlConnection(stringSQL))
                {
                    conexion.Open();
                    var command = new SqlCommand("Editar", conexion);
                    command.Parameters.AddWithValue("IdProducto", producto.IdProducto);
                    command.Parameters.AddWithValue("Nombre", producto.Nombre is null ? DBNull.Value : producto.Nombre);
                    command.Parameters.AddWithValue("Marca", producto.Marca is null ? DBNull.Value : producto.Marca);
                    command.Parameters.AddWithValue("Categoria", producto.Categoria is null ? DBNull.Value : producto.Categoria);
                    command.Parameters.AddWithValue("Precio", producto.Precio);
                    command.Parameters.AddWithValue("Cantidad", producto.Cantidad);
                    command.CommandType = CommandType.StoredProcedure;

                    command.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "EDITED" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = e.Message }); 
            }
        }

        [HttpDelete]
        [Route("Delete/{idProducto:int}")]
        public IActionResult Delete(int idProducto)
        {
            try
            {
                using (var conexion = new SqlConnection(stringSQL))
                {
                    conexion.Open();
                    var command = new SqlCommand("Eliminar", conexion);
                    command.Parameters.AddWithValue("IdProducto", idProducto);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();

                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "DELETED"} );
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = e.Message });
            }
        }




    }
}
