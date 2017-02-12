using System;
using System.Threading;
using System.Text;

namespace CompilerApp {
	public sealed class Compiler {
		public byte[] BuildProject(string projectPath) {
			// Имитируем бурную деятельность. 
			Thread.Sleep(500);

			// В реальности здесь будут байты собранной dll-ки. 
			return Encoding.UTF8.GetBytes(projectPath);
		}
	}
}
