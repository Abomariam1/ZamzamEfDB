namespace Zamzam.EF.Services
{
    //public class DepartmentServices : GenericDataServices<Department>
    //{
    //    private readonly ZamzamDbContextFactory _dbContextFactory;
    //    private readonly string[] _constr;

    //    public DepartmentServices(ZamzamDbContextFactory dbContextFactory) : base(dbContextFactory)
    //    {
    //        _dbContextFactory = dbContextFactory;
    //        //_constr = constr;
    //    }

    //    public IEnumerable<Department> GetDepartments()
    //    {
    //        using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
    //        {
    //            var dep = context.Set<Department>().Include(e => e.Employees).ToList();
    //            return dep;
    //        }
    //    }
    //    public bool IsEmpty()
    //    {
    //        using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
    //        {
    //            return !context.Set<Department>().Any();
    //        }
    //    }
    //    public Department GetDepartmentByName(string name)
    //    {
    //        using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
    //        {
    //            var department = context.Set<Department>().FirstOrDefault(d => d.DepName == name);
    //            return department;
    //        }
    //    }
    //}
}
