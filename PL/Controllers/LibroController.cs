using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace PL.Controllers
{
    public class LibroController : Controller
    {
        // GET: Libro
        public ActionResult GetAll()
        {
            var serv = new LibroServiceReference.LibroServiceClient();
            var resultado = serv.GetAll();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            foreach (var item in resultado)
            {
                ML.Libro libro = new ML.Libro();
                libro.IdLibro = item.IdLibro;
                libro.Nombre = item.Nombre;
                libro.FechaPublicacion = item.FechaPublicacion;
                libro.NumeroPaginas = item.NumeroPaginas;
                libro.Autor = new ML.Autor();
                libro.Autor.IdAutor = item.IdAutor;
                libro.Autor.NombreCompleto = item.AutorNombre;

                result.Objects.Add(libro);
            }
            return View(result);
        }//GetAll

        [HttpGet]
        public ActionResult Form(int? idLibro)
        {
            ML.Libro libro = new ML.Libro();
            libro.Autor = new ML.Autor();
            ML.Result result = BL.Autor.GetAll();
            libro.Autor.Autores = result.Objects;
            if (idLibro == null)
            {
                //FormVacio
                return View(libro);
            }
            else
            {
                //GetById
                var serv = new LibroServiceReference.LibroServiceClient();
                var resultado = serv.GetById(idLibro.Value);
                string f = resultado.FechaPublicacion;
                string dd = f.Substring(0, 2);
                string MM = f.Substring(3, 2);
                string yyyy = f.Substring(6, 4);
                string fecha = MM + "-" + dd + "-" + yyyy;
                
                libro.IdLibro = resultado.IdLibro;
                libro.Nombre = resultado.Nombre;
                libro.FechaPublicacion = fecha;
                libro.NumeroPaginas = resultado.NumeroPaginas;
                libro.Autor.IdAutor = resultado.IdAutor;
                //libro.Autor.NombreCompleto = resultado.AutorNombre;

                return View(libro);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Libro libro)
        {
            if (libro.IdLibro == 0)
            {
                var serv = new LibroServiceReference.LibroServiceClient();
                var obj = new SL.Libro
                {
                    Nombre = libro.Nombre,
                    FechaPublicacion = libro.FechaPublicacion,
                    NumeroPaginas = libro.NumeroPaginas,
                    IdAutor = libro.Autor.IdAutor
                };
                bool r = serv.Add(obj);

                if (r)
                {
                    ViewBag.Message = "Registro almacenado exitosamente";
                }
                else
                {
                    ViewBag.Message = "Error al almacenar el registro";
                }
                return View("Modal");
            }
            else
            {
                //Update
                var serv = new LibroServiceReference.LibroServiceClient();
                var obj = new SL.Libro
                {
                    IdLibro = libro.IdLibro,
                    Nombre = libro.Nombre,
                    FechaPublicacion = libro.FechaPublicacion,
                    NumeroPaginas = libro.NumeroPaginas,
                    IdAutor = libro.Autor.IdAutor
                };
                bool r = serv.Update(obj);

                if (r)
                {
                    ViewBag.Message = "Registro actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "Error al editar el registro";
                }
                return View("Modal");
            }            
        }//Form(post)

        public ActionResult Delete(int idLibro)
        {
            var serv = new LibroServiceReference.LibroServiceClient();
            bool r = serv.Delete(idLibro);

            if (r)
            {
                ViewBag.Message = "Registro eliminado correctamente";
            }
            else
            {
                ViewBag.Message = "Error al eliminar";
            }
            return View("Modal");
        }
    }
}