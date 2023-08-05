using Microsoft.EntityFrameworkCore;
using Zamzam.Core;

namespace Zamzam.EF.Services
{
    public class EmployeeDataServices : GenericDataServices<Employee>
    {
        private readonly ZamzamDbContextFactory _dbContextFactory;
        public EmployeeDataServices(ZamzamDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IEnumerable<Employee> GetEmployeesInDepartment()
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                return context.Set<Employee>().Include(d => d.Department).ToList();
            }
        }
    }
}

