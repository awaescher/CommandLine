using System;

namespace CommandLine
{
	internal class ConsoleColorizer
	{
		private int _current = 0;

		internal ConsoleColor Next() => Colors[_current++ % Colors.Length];

		internal ConsoleColor[] Colors { get; } = new[] { ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.Yellow };
	}
}
