using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
            
namespace CompilerApp {
	public static class QueueCompiler {
		private static Lazy<Compiler> compiler = new Lazy<Compiler>();
		private static ConcurrentQueue<Task<byte[]>> queue = new ConcurrentQueue<Task<byte[]>>();

		public static async Task<byte[]> BuildProjectAsync(string projectPath) {
			var task = CreateTask(projectPath);
			queue.Enqueue(task);
			return await ProcessQueue(task);
		}

		private static Task<byte[]> CreateTask(string projectPath) {
			return new Task<byte[]>((path) => compiler.Value.BuildProject((string)path), projectPath);
		}

		private static async Task<byte[]> ProcessQueue(Task<byte[]> task) {
			Task<byte[]> _task;
			queue.TryPeek(out _task);

			if (_task == task) {
				task.ContinueWith(_ => {
					queue.TryDequeue(out _task);
					if (queue.TryPeek(out _task)) ProcessQueue(_task);
				});

				task.Start();
			}

			return await task;
		}
	}
}
