using Zamzam.Core;

namespace Zamzam.EF.Services
{
    public class DepartmentServices : GenericDataServices<Department>
    {
        private readonly ZamzamDbContextFactory _dbContextFactory;
        public DepartmentServices(ZamzamDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IEnumerable<Department> GetDepartments()
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                return context.Set<Department>().ToList();
            }
        }
        public bool IsEmpty()
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                return !context.Set<Department>().Any();
            }
        }
    }
}
