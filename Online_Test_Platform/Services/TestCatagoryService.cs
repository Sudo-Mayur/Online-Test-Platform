using Microsoft.EntityFrameworkCore;
using Online_Test_Platform.Models;

namespace Online_Test_Platform.Services
{
    public class TestCatagoryService : IService<TestCatagory, int>
    {
        private readonly TestPlatformContext context;
        public TestCatagoryService(TestPlatformContext context)
        {
            this.context = context;
        }
        async Task<TestCatagory> IService<TestCatagory, int>.CreateAsync(TestCatagory entity)
        {
            var res = await context.TestCatagories.AddAsync(entity);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        async Task<IEnumerable<TestCatagory>> IService<TestCatagory, int>.GetAsync()
        {
            var res = await context.TestCatagories.ToListAsync();
            return res;
        }

        async Task<TestCatagory> IService<TestCatagory, int>.GetByIdAsync(int id)
        {
            var res=await context.TestCatagories.FindAsync(id);
            if(res == null)
            {
                return null;
            }
            return res;
        }
    }
}
