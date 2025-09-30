namespace Week_1
{
    internal class Question2
    {
        private static readonly Dictionary<string, int[]> scores = [];
        private const int GAME_COUNT = 3;
        private const int PLAYER_COUNT = 3; // entering 10 scores was too tedious

        public static void Run()
        {
            GetData();
            ShowMaxScores();
        }

        private static void GetData()
        {
            string currentGame;

            for (int i = 0; i < GAME_COUNT; i++)
            {
                // Add current game name
                currentGame = AddGame(i);

                for (int j = 0; j < PLAYER_COUNT; j++)
                {
                    int score = GetScore(j);
                    scores[currentGame][j] = score;
                }
                Console.WriteLine("---");
            }
        }

        private static string AddGame(int i)
        {
            while (true)
            {
                Console.Write($"Game {i + 1}'s name: ");
                string gameName = Console.ReadLine() ?? "";
                if (!scores.TryAdd(gameName, new int[PLAYER_COUNT]))
                {
                    Console.WriteLine("Game is already in the list.");
                }
                else
                {
                    return gameName;
                }
            }
        }

        private static int GetScore(int i)
        {
            while (true)
            {
                Console.Write($"Player {i + 1}'s score: ");
                if (!int.TryParse(Console.ReadLine(), out int score))
                {
                    Console.WriteLine("Invalid score.");
                }
                else
                {
                    return score;
                }
            }
        }

        private static void ShowMaxScores()
        {
            foreach (var gameName in scores.Keys)
            {
                var (player, score) = GetMaxScore(gameName);
                if (player == -1) return;

                Console.WriteLine("=> " + gameName);
                Console.WriteLine($"Best Player: {player}");
                Console.WriteLine($"Score: {score}\n");
            }
        }

        private static (int player, int score) GetMaxScore(string game)
        {
            if (!scores.TryGetValue(game, out int[]? gameScores) || gameScores.Length == 0)
                return (-1, -1);

            var (score, player) = scores[game]
                .Select((value, index) => (value, index))
                .MaxBy(x => x.value);

            return (player + 1, score);
        }
    }
}
