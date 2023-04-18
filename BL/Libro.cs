using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Libro
    {
        public static ML.Result GetAll()
        {   
            ML.Result result = new ML.Result();
            using (DRosasLibroAutorEntities context = new DRosasLibroAutorEntities())
            {
                var query = context.LibroGetAll();                

                if (query != null)
                {
                    result.Objects = new List<object>();
                    foreach (var obj in query)
                    {
                        ML.Libro libro = new ML.Libro();
                        libro.IdLibro = obj.IdLibro;
                        libro.Nombre = obj.Nombre;
                        libro.FechaPublicacion = obj.FechaPublicacion;
                        libro.NumeroPaginas = obj.NumeroPaginas.Value;
                        libro.Autor = new ML.Autor();
                        libro.Autor.IdAutor = obj.IdAutor.Value;
                        libro.Autor.NombreCompleto = obj.AutorNombre + " " + obj.AutorApellidoPaterno + " " + obj.AutorApellidoMaterno;

                        result.Objects.Add(libro);
                    }
                    result.Correct = true;
                }
            }
            return result;
        }//GetAll 

        public static ML.Result GetById(int idLibro)
        {
            ML.Result result = new ML.Result();

            using (DRosasLibroAutorEntities context = new DRosasLibroAutorEntities())
            {
                var query = context.LibroGetById(idLibro).FirstOrDefault();

                if (query != null)
                {
                    result.Correct = true;
                    ML.Libro libro = new ML.Libro();
                    libro.IdLibro = query.IdLibro;
                    libro.Nombre = query.Nombre;
                    libro.FechaPublicacion = query.FechaPublicacion;
                    libro.NumeroPaginas = query.NumeroPaginas.Value;
                    libro.Autor = new ML.Autor();
                    libro.Autor.IdAutor = query.IdAutor.Value;
                    libro.Autor.NombreCompleto = query.AutorNombre + " " + query.AutorApellidoPaterno + " " + query.AutorApellidoMaterno;

                    result.Object = libro;
                }
            }
            return result;
        }//GetById

        public static bool Add(ML.Libro libro)
        {
            using (DRosasLibroAutorEntities context = new DRosasLibroAutorEntities())
            {
                int query = context.LibroAdd(libro.Nombre, DateTime.Parse(libro.FechaPublicacion), 
                    libro.NumeroPaginas, libro.Autor.IdAutor);
                if (query > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }//Add

        public static bool Update(ML.Libro libro)
        {
            using (DRosasLibroAutorEntities context = new DRosasLibroAutorEntities())
            {
                int query = context.LibroUpdate(libro.IdLibro, libro.Nombre, DateTime.Parse(libro.FechaPublicacion),
                    libro.NumeroPaginas, libro.Autor.IdAutor);

                if (query > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }//Update

        public static bool Delete(int idLibro)
        {
            using (DRosasLibroAutorEntities context = new DRosasLibroAutorEntities())
            {
                int query = context.LibroDelete(idLibro);

                if (query > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }//Delete
    }
}
