namespace SIPI_PRESENTEEISM.Core.Repositories.Interfaces
{
    using SIPI_PRESENTEEISM.Core.Domain.Entities;
    using System.Linq.Expressions;

    public interface IEmployeeRepository
    {
        Task<Employee?> FindEmployee(Expression<Func<Employee, bool>> expression);
    }
}
