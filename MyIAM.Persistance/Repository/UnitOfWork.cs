using MyIAM.Core.Databases.Contracts;
using MyIAM.Persistance;

namespace MyIAM.Databases.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAMDbContext context;

        public UnitOfWork(IAMDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
