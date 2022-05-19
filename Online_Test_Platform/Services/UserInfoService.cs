using Microsoft.EntityFrameworkCore;
using Online_Test_Platform.Models;

namespace Online_Test_Platform.Services
{
    public class UserInfoService : IService<UserInfo, int>
    {
        private readonly TestPlatformContext context;
        public UserInfoService(TestPlatformContext context)
        {
            this.context = context;
        }
        async Task<UserInfo> IService<UserInfo, int>.CreateAsync(UserInfo entity)
        {
            try
            {
                var res = await context.UserInfos.AddAsync(entity);
                await context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception)
            {
                throw;
            }     
        }

        async Task<IEnumerable<UserInfo>> IService<UserInfo, int>.GetAsync()
        {
            try
            {
                var res = await context.UserInfos.ToListAsync();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        Task<UserInfo> IService<UserInfo, int>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
