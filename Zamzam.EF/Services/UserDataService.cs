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

        public Task<bool> DeleteAsync(Ulid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Ulid id)
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

        public Task<User> UpdateAsync(Ulid id, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
