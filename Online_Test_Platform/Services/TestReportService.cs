using Microsoft.EntityFrameworkCore;
using Online_Test_Platform.Models;

namespace Online_Test_Platform.Services
{
    public class TestReportService : IService<TestReport, int>
    {
        private readonly TestPlatformContext context;
        public TestReportService(TestPlatformContext context)
        {
            this.context = context;
        }
        async Task<TestReport> IService<TestReport, int>.CreateAsync(TestReport entity)
        {
            try
            {
                var res = await context.TestReports.AddAsync(entity);
                await context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<IEnumerable<TestReport>> IService<TestReport, int>.GetAsync()
        {
            try
            {
                var res = await context.TestReports.ToListAsync();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<TestReport> IService<TestReport, int>.GetByIdAsync(int id)
        {
            try
            {
                var res = await context.TestReports.FindAsync(id);
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

