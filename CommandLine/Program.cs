using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("CommandLine is:");

            Console.ResetColor();

			if (args == null || args.Length == 0)
			{
				Console.WriteLine("(EMPTY)");
			}
			else
			{
				Console.WriteLine(string.Join(" ", args));
				Console.WriteLine(string.Join(" ", GetLengthStrings(args)));
			}

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("");
            Console.WriteLine("Press space to enter an exit code or any other key to quit.");
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                int? exitCode = null;

                while (exitCode == null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine();
                    Console.Write("Enter a numeric exit code: ");
                    string number = Console.ReadLine();

                    int parsed = -1;
                    if (Int32.TryParse(number, out parsed))
                    {
                        exitCode = parsed;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Could not parse \"" + number + "\" to a numeric exit code.");
                    }
                }

                Environment.Exit(exitCode.GetValueOrDefault());
            }
        }

		private static string[] GetLengthStrings(string[] args)
		{
			var result = new string[args.Length];

			for (int i = 0; i < args.Length; i++)
			{
				var length = args[i].Length;


				if (length == 0)
				{
					result[i] = "";
				}
				else
				{
					result[i] = "".PadRight(length, '─');

					if (length == 1)
						result[i] = "^";
					else
						result[i] = "└" + result[i].Substring(1, length - 2) + "┘";

					if (length > 3)
					{
						var lenghtStringLength = length.ToString().Length;
						int charsLeftRight = (result[i].Length - lenghtStringLength) / 2;
						int charsLeft = (charsLeftRight * 2 + lenghtStringLength == result[i].Length) ? charsLeftRight : charsLeftRight + 1;
						result[i] = result[i].Substring(0, charsLeft) + length.ToString() + result[i].Substring(result[i].Length - charsLeftRight, charsLeftRight);
					}
				}
			}

			return result;
		}
	}
}
