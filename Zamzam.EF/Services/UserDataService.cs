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

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByNameAsync(string name)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                User? entitie = context.Set<User>().FirstOrDefaultAsync((e) => e.UserName == name).Result;
                return entitie;
            }
        }

        public Task<User> UpdateAsync(Guid id, User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public User Create(User entity)
        {
            throw new NotImplementedException();
        }

        public User Update(Guid id, User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public User FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
