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
        Task<UserInfo> IService<UserInfo, int>.CreateAsync(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<UserInfo>> IService<UserInfo, int>.GetAsync()
        {
            var res = await context.UserInfos.ToListAsync();
            return res;
        }

        Task<UserInfo> IService<UserInfo, int>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
