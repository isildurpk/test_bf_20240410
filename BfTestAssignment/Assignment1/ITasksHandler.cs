namespace BfTestAssignment.Assignment1;

/*
 * Задание 1. Напишите простой класс асинхронной обработки задач. Класс должен иметь метод для
 * добавления задач, принимающий объекты типа Task:
 * public delegate void Task();
 * Задачи должны выполняться последовательно. Также должна быть возможность дождаться
 * окончания выполнения всех заданий.
 */

public interface ITasksHandler
{
    /// <summary>
    /// Добавить задачу в очередь на выполнение
    /// </summary>
    /// <param name="task">Делегат задачи</param>
    void AddTask(Task task);

    /// <summary>
    /// Запустить последовательное выполнение задач из очереди.<br/>
    /// Завершение работы возвращаемым <see cref="System.Threading.Tasks.Task" /> означает завершение выполнения всех заданий.
    /// </summary>
    /// <returns><see cref="System.Threading.Tasks.Task" />, последовательно выполняющий задачи из очереди</returns>
    System.Threading.Tasks.Task RunAsync();
}