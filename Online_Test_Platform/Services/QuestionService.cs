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
            var res = await context.Questions.AddAsync(entity);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        async Task<IEnumerable<Question>> IService<Question, int>.GetAsync()
        {
            var res = await context.Questions.ToListAsync();
            return res;
        }

        Task<Question> IService<Question, int>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
