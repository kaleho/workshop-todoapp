namespace ConsoleApp1.Repositories
{
    public interface IRepository
    {
        int Count { get; }

        void Add(
            int index,
            string value);

        string GetByKey(
            int key);
    }
}