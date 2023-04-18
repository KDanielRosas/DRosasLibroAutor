using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILibroService" in both code and config file together.
    [ServiceContract]
    public interface ILibroService
    {
        [OperationContract]
        List<Libro> GetAll();

        [OperationContract]
        bool Add(Libro obj);

        [OperationContract]
        Libro GetById(int idLibro);

        [OperationContract]
        bool Update(Libro obj);

        [OperationContract]
        bool Delete(int idLibro);
    }

    [DataContract]
    public class Libro
    {
        [DataMember] public int IdLibro;
        [DataMember] public string Nombre;
        [DataMember] public string FechaPublicacion;
        [DataMember] public int NumeroPaginas;
        [DataMember] public int IdAutor;
        [DataMember] public string AutorNombre;
    }
}
