namespace Football
{
    internal class Program
    {
        static public void Main(string[] args)
        {
            int count = 0;
            string homeTeamName = "Gaimin Gladiators";
            string awayTeamName = "Team Spirit";

            Stadium stad = new Stadium(90, 25, '#');
            stad.Draw();

            Team homeTeam = new Team(homeTeamName, ConsoleColor.Red);
            Team awayTeam = new Team(awayTeamName, ConsoleColor.Green);

            List<Team> teams = new List<Team>() { homeTeam, awayTeam };
            foreach (var team in teams)
            {
                for(int i = 0; i < 10; i++)
                {
                    team.AddPlayer(new Player($"{team.Name}:{i + 1}"));
                }
            }

            Game game = new Game(homeTeam, awayTeam, stad);

            game.Start();
            while (true)
            {
                Thread.Sleep(500);
                Console.SetCursorPosition(0, stad.Height + 1);
                Console.Write($"render times: {count}");
                count++;
                game.Move();
            }
        }
    }
}