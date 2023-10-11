namespace MyIAM.Core.Databases.Contracts
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
