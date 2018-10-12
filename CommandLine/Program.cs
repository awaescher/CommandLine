using System;

namespace CommandLine
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("CommandLine is:");

			Console.ResetColor();

			if (args?.Length == 0)
			{
				Console.WriteLine("(empty)");
			}
			else
			{
				Console.Write("Index   ");
				WriteColorizedLine(GetLegendString(LegendOrientation.Above, args, GetIndexes(args)));
				Console.Write("        ");
				WriteColorizedLine(args);
				Console.Write("Lenght  ");
				WriteColorizedLine(GetLegendString(LegendOrientation.Below, args, GetArgumentLengths(args)));
			}

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("");
			Console.WriteLine("Press any key to quit.");
			Console.ReadKey();
		}

		private static void WriteColorizedLine(string[] tokensToColorize)
		{
			var colorizer = new ConsoleColorizer();

			try
			{
				for (int i = 0; i < tokensToColorize.Length; i++)
				{
					if (i > 0)
						Console.Write(" ");

					Console.ForegroundColor = colorizer.Next();
					Console.Write(tokensToColorize[i]);
				}
				Console.WriteLine("");
			}
			finally
			{
				Console.ResetColor();
			}
		}

		private static string[] GetLegendString(LegendOrientation orientation, string[] arguments, string[] labels)
		{
			var chars = orientation == LegendOrientation.Above ? "┌┬┐" : "└┴┘";

			var result = new string[arguments.Length];

			for (int i = 0; i < arguments.Length; i++)
			{
				var length = arguments[i].Length;

				if (length == 0)
				{
					result[i] = "";
				}
				else
				{
					result[i] = "".PadRight(length, '─');

					if (length == 1)
						result[i] = chars[1].ToString();
					else
						result[i] = chars[0].ToString() + result[i].Substring(1, length - 2) + chars[2].ToString();

					if (length > 3)
					{
						var label = labels[i];
						int charsLeftRight = (result[i].Length - label.Length) / 2;
						int charsLeft = (charsLeftRight * 2 + label.Length == result[i].Length) ? charsLeftRight : charsLeftRight + 1;
						result[i] = result[i].Substring(0, charsLeft) + label + result[i].Substring(result[i].Length - charsLeftRight, charsLeftRight);
					}
				}
			}

			return result;
		}

		private static string[] GetIndexes(string[] args)
		{
			// what a pitty, we don't have Linq in .NET 2.0

			var result = new string[args.Length];
			for (int i = 0; i < args.Length; i++)
				result[i] = i.ToString();

			return result;
		}

		private static string[] GetArgumentLengths(string[] args)
		{
			// what a pitty, we don't have Linq in .NET 2.0

			var result = new string[args.Length];
			for (int i = 0; i < args.Length; i++)
				result[i] = args[i].Length.ToString();

			return result;
		}
	}
}
