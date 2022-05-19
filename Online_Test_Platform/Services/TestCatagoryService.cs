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
            try
            {
                var res = await context.TestCatagories.AddAsync(entity);
                await context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<IEnumerable<TestCatagory>> IService<TestCatagory, int>.GetAsync()
        {
            try
            {
                var res = await context.TestCatagories.ToListAsync();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<TestCatagory> IService<TestCatagory, int>.GetByIdAsync(int id)
        {
            try
            {
                var res = await context.TestCatagories.FindAsync(id);
                if (res == null)
                {
#pragma warning disable CS8603 // Possible null reference return.
                    return null;
#pragma warning restore CS8603 // Possible null reference return.
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
