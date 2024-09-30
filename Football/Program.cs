namespace Football
{
    internal class Program
    {
        static public void Main(string[] args)
        {
            int count = 0;
            string homeTeamName = "Gaimin Gladiators";
            string awayTeamName = "Team Spirit";

            

            Team homeTeam = new Team(homeTeamName, ConsoleColor.Red);
            Team awayTeam = new Team(awayTeamName, ConsoleColor.Green);

            Stadium stad = new Stadium(90, 25, '#', homeTeam, awayTeam);
            stad.Draw();

            List<Team> teams = new List<Team>() { homeTeam, awayTeam };
            foreach (var team in teams)
            {
                for(int i = 0; i < 10; i++)
                {
                    team.AddPlayer(new Player($"{team.Name}:{i + 1}"));
                }
            }
            Score score = new Score(stad, homeTeam, awayTeam);
            score.Draw();
            Game game = new Game(homeTeam, awayTeam, stad, score);

            game.Start();
            while (true)
            {
                
                Console.SetCursorPosition(0, stad.Height + 1);
                Console.Write($"render times: {count}");
                count++;
                game.Move();
            }
        }
    }
}