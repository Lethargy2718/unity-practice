namespace Week2.Utils
{
    internal static class InputHelper
    {
        public static string GetString(string prompt, bool required = true)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (!required || !string.IsNullOrEmpty(input))
                    return input;

                Console.WriteLine("This field is required. Please try again.");
            }
        }

        public static int GetInt(string prompt, int? min = null, int? max = null)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if ((min == null || value >= min) && (max == null || value <= max))
                        return value;
                }
                Console.WriteLine("Invalid number. Please try again.");
            }
        }

        public static double GetDouble(string prompt, double? min = null, double? max = null)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    if ((min == null || value >= min) && (max == null || value <= max))
                        return value;
                }
                Console.WriteLine("Invalid number. Please try again.");
            }
        }

        public static DateTime GetDate(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    return date;

                Console.WriteLine("Invalid date format. Please try again.");
            }
        }

        public static T GetEnumChoice<T>(string prompt) where T : Enum
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var values = Enum.GetValues(typeof(T));
                int i = 1;
                foreach (var val in values)
                {
                    Console.WriteLine($"{i}. {val}");
                    i++;
                }

                int choice = GetInt("Choose an option", 1, values.Length);
                return (T)values.GetValue(choice - 1);
            }
        }

        public static void WaitForBack()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }

    }
}
