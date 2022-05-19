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
            try
            {
                var res = await context.UserAnswers.AddAsync(entity);
                await context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<IEnumerable<UserAnswer>> IService<UserAnswer, int>.GetAsync()
        {
            try
            {
                var res = await context.UserAnswers.ToListAsync();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<UserAnswer> IService<UserAnswer, int>.GetByIdAsync(int id)
        {
            try
            {
                var res = await context.UserAnswers.FindAsync(id);
#pragma warning disable CS8603 // Possible null reference return.
                return res;
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
