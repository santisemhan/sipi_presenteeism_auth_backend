namespace SIPI_PRESENTEEISM.Core.Repositories.Interfaces
{
    public interface IStamentRepository
    {
        /// <summary>
        ///     Guarda los cambios en la base de datos.
        ///     Ejecuta un resolvedor de conflictos en caso de que las mismas 
        ///     hayan sufrido modificaciones luego de ingresar al contexto.
        ///     El resolvedor se puede ejecutar un número indefinido de veces, o
        ///     limitarlo, evitando que la resolución recaiga en ciclos
        ///     infinitos.
        /// </summary>     
        /// <returns>
        ///    Devuelve la cantidad de entidades persistidas, en el mismo
        ///    sentido que el método <em>SaveChanges()</em> de la clase
        ///    <em>Microsoft.EntityFrameworkCore.DbContext</em>.
        /// </returns>
        Task<int> SaveChanges();
    }
}
