using System;
using System.Text;
using System.Collections.Generic;

namespace CompilerApp
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			List<string> projects = new List<string>();
			for (int i = 0; i < 10; i++) projects.Add($"C:\\project_{i}");

			foreach (var project in projects)
			{
				Console.WriteLine($"Add project '{project}' to build");
				QueueCompiler.BuildProjectAsync((project)).ContinueWith(
					t => Console.WriteLine($"Project '{Encoding.UTF8.GetString(t.Result)}' builded")
				);	
			}

			Console.WriteLine("Press any key");
			Console.ReadKey();
		}
	}
}
