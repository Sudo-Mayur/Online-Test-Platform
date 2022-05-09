using Microsoft.EntityFrameworkCore;
using Online_Test_Platform.Models;

namespace Online_Test_Platform.Services
{
    public class UserAnswerService : IService<UserAnswer, int>
    {
        private readonly TestPlatformContext context;
        public UserAnswerService(TestPlatformContext context)
        {
            this.context = context;
        }
        async Task<UserAnswer> IService<UserAnswer, int>.CreateAsync(UserAnswer entity)
        {
            var res = await context.UserAnswers.AddAsync(entity);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        async Task<IEnumerable<UserAnswer>> IService<UserAnswer, int>.GetAsync()
        {
            var res = await context.UserAnswers.ToListAsync();
            return res;
        }

        async Task<UserAnswer> IService<UserAnswer, int>.GetByIdAsync(int id)
        {
            var res = await context.UserAnswers.FindAsync(id);
            if (res == null)
            {
                return null;
            }
            return res;
        }
    }
}
