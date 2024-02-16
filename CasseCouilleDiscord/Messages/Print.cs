using System;

namespace CasseCouilleDiscord.Messages
{
    internal class Print
    {
        public static object Question(string question, string typeVar)
        {
            if (typeVar == "str")
            {
                Console.Write($"{question} ? ");
                Console.ForegroundColor = ConsoleColor.Green;
                return Convert.ToString(Console.ReadLine());
            }
            else if (typeVar == "int")
            {
                Console.Write($"{question} ? ");
                Console.ForegroundColor = ConsoleColor.Green;
                return Convert.ToInt32(Console.ReadLine());
            }

            return null;
        }
    }
}
