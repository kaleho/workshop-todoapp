namespace Todo.Domain.Repositories
{
    public interface IRepository
    {
        int Count { get; }

        void Add(
            int key,
            string value);

        string GetByKey(
            int key);
    }

    public interface ITaskListRepository
    {
        int Count { get; }


    }
}