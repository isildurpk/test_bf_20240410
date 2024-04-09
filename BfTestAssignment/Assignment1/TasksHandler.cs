namespace BfTestAssignment.Assignment1;

public class TasksHandler : ITasksHandler
{
    private readonly Queue<Task> _tasksQueue = new();

    public void AddTask(Task task)
    {
        _tasksQueue.Enqueue(task);
    }

    public System.Threading.Tasks.Task RunAsync()
    {
        return System.Threading.Tasks.Task.Run(async () =>
        {
            while (_tasksQueue.Count > 0)
            {
                var task = _tasksQueue.Dequeue();

                try
                {
                    await System.Threading.Tasks.Task
                        .Run(() => task())
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred while performing Task:" + Environment.NewLine + ex);
                }
            }
        });
    }
}