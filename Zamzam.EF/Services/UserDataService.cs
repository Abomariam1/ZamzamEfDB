using Microsoft.EntityFrameworkCore;
using Zamzam.Core;
using Zamzam.Core.Entites;

namespace Zamzam.EF.Services
{
    public class UserDataService : IDataService<User>
    {
        private readonly ZamzamDbContextFactory _dbContextFactory;

        public UserDataService(ZamzamDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task<User> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByNameAsync(string name)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                User? entitie = await context.Set<User>().FirstOrDefaultAsync((e) => e.UserName == name);
                return entitie;
            }
        }

        public Task<User> UpdateAsync(int id, User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User Create(User entity)
        {
            throw new NotImplementedException();
        }

        public User Update(int id, User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
