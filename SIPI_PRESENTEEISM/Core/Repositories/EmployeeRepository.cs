namespace SIPI_PRESENTEEISM.Core.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SIPI_PRESENTEEISM.Core.Domain.Entities;
    using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
    using SIPI_PRESENTEEISM.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _dbContext;

        public EmployeeRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Employee entity)
        {
            await _dbContext.Employee.AddAsync(entity);
        }

        public async Task<Employee?> FindEmployee(Expression<Func<Employee, bool>> expression)
        {
            return await _dbContext.Employee
                .Include(e => e.Zone)
                .Include(e => e.ImagesToIdentify)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _dbContext.Employee.ToListAsync();
        }
    }
}
