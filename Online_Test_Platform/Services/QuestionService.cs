using Microsoft.EntityFrameworkCore;
using Online_Test_Platform.Models;

namespace Online_Test_Platform.Services
{
    public class QuestionService:IService<Question,int>
    {
        private readonly TestPlatformContext context;
        public QuestionService(TestPlatformContext context)
        {
            this.context = context; 
        }

        async Task<Question> IService<Question, int>.CreateAsync(Question entity)
        {
            try
            {
                var res = await context.Questions.AddAsync(entity);
                await context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        async Task<IEnumerable<Question>> IService<Question, int>.GetAsync()
        {
            try
            {
                var res = await context.Questions.ToListAsync();
                return res;
            }
            catch (Exception)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        Task<Question> IService<Question, int>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}



