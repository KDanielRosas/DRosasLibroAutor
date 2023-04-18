using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Autor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            using (DRosasLibroAutorEntities context = new DRosasLibroAutorEntities())
            {
                var query = context.AutorGetAll();

                if (query != null)
                {
                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Autor autor = new ML.Autor();
                        autor.IdAutor = item.IdAutor;
                        autor.NombreCompleto = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;
                        result.Objects.Add(autor);
                    }
                }
            }
            return result;
        }//GetAll
    }
}
