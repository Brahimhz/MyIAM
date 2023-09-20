namespace MyIAM.Databases.Contracts
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
