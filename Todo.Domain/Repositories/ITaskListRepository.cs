using Todo.Dialect;

namespace Todo.Domain.Repositories
{
    public interface ITaskListRepository
    {
        int Count { get; }

        void Add(
            int key,
            TaskList taskList);

        TaskList GetByKey(
            int key);

        void Update(
            int key,
            TaskList taskList);
    }
}