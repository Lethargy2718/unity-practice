namespace Week1
{
    internal class Question1
    {
        private static readonly Dictionary<string, Func<double, double, double>> actions = new()
        {
            { "addition", (a, b) => a + b },
            { "subtraction", (a,b) => a - b },
            { "multiplication", (a, b) => a * b },
            { "division", (a, b) => {
                    if (b == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return a / b;
               
                }
            },
        };


        public static void Run()
        {
            var keys = actions.Keys.ToList();

            while (true)
            {
                Console.WriteLine("> Operations: ");
                PrintOptions();
                Console.Write("> Pick an operation: ");

                int operationIndex;

                // Not an int or not a valid key
                while (!int.TryParse(Console.ReadLine(), out operationIndex) || operationIndex < 0 || operationIndex >= actions.Keys.Count)
                {
                    Console.WriteLine("Not a valid operation index.");
                }

                string operationName = keys[operationIndex];
                var operation = actions[operationName];
                Console.WriteLine($"> Enter the operands to perform {operationName} on: ");

                double num1 = ReadNumber("Number 1: ");
                double num2 = ReadNumber("Number 2: ");

                try
                {
                    Console.WriteLine("Result: " + operation(num1, num2));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something wrong happened: " + e.Message);
                }
                
                Console.WriteLine("=-------------------------=");
            }
        }

        private static void PrintOptions()
        {
            var keys = actions.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                string key = keys[i];
                Console.WriteLine($"{i}. {char.ToUpper(key[0]) + key[1..]}");
            }
        }
        
        private static double ReadNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;
                Console.WriteLine("Invalid number, try again.");
            }
        }


    }
}
