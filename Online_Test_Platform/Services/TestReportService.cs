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
            var res = await context.TestReports.AddAsync(entity);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        async Task<IEnumerable<TestReport>> IService<TestReport, int>.GetAsync()
        {
            var res = await context.TestReports.ToListAsync();
            return res;
        }

        async Task<TestReport> IService<TestReport, int>.GetByIdAsync(int id)
        {
            var res = await context.TestReports.FindAsync(id);
            if (res == null)
            {
                return null;
            }
            return res;
        }
    }
}

