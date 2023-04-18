using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LibroService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LibroService.svc or LibroService.svc.cs at the Solution Explorer and start debugging.
    public class LibroService : ILibroService
    {
        public List<Libro> GetAll()
        {  
            ML.Result result = BL.Libro.GetAll();
            var list = new List<Libro>();

            foreach (ML.Libro libro in result.Objects)
            {
                var item = new Libro()
                {
                    IdLibro = libro.IdLibro,
                    Nombre = libro.Nombre,
                    FechaPublicacion = libro.FechaPublicacion,
                    NumeroPaginas = libro.NumeroPaginas,
                    IdAutor = libro.Autor.IdAutor,
                    AutorNombre = libro.Autor.NombreCompleto

                };
                list.Add(item);
            }
            return list;
        }//GetAll

        public bool Add(Libro obj)
        {
            ML.Libro Libro = new ML.Libro();
            Libro.Nombre = obj.Nombre;
            Libro.FechaPublicacion = obj.FechaPublicacion;
            Libro.NumeroPaginas = obj.NumeroPaginas;
            Libro.Autor = new ML.Autor();
            Libro.Autor.IdAutor = obj.IdAutor;

            bool result = BL.Libro.Add(Libro);

            return result;

        }//Add

        public Libro GetById(int idLibro)
        {
            ML.Result result = BL.Libro.GetById(idLibro);
            Libro book = new Libro();

            var item = (ML.Libro)result.Object;
            book.IdLibro = item.IdLibro;
            book.Nombre = item.Nombre;
            book.NumeroPaginas = item.NumeroPaginas;
            book.FechaPublicacion = item.FechaPublicacion;
            book.IdAutor = item.Autor.IdAutor;
            book.AutorNombre = item.Autor.NombreCompleto;

            return book;
        }//GetById

        public bool Update(Libro obj)
        {
            ML.Libro Libro = new ML.Libro();
            Libro.IdLibro = obj.IdLibro;
            Libro.Nombre = obj.Nombre;
            Libro.FechaPublicacion = obj.FechaPublicacion;
            Libro.NumeroPaginas = obj.NumeroPaginas;
            Libro.Autor = new ML.Autor();
            Libro.Autor.IdAutor = obj.IdAutor;

            bool result = BL.Libro.Update(Libro);

            return result;
        }//Update

        public bool Delete(int idLibro)
        {
            bool result = BL.Libro.Delete(idLibro);

            return result;
        }//Delete
    }
}
