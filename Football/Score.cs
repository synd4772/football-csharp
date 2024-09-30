using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class Score
    {
        public Stadium Stadium { get; private set; }
        public Team homeTeam { get; private set; }
        public Team awayTeam { get; private set; }

        public int homeTeamScore = 0, awayTeamScore = 0;
        public Score(Stadium stadium, Team homeTeam, Team awayTeam)
        {
            this.Stadium = stadium;
            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
        }

        public void ChangeScore(Team team)
        {
            if (this.homeTeam == team)
            {
                this.homeTeamScore++;
            }
            else if (this.awayTeam == team)
            {
                {
                    awayTeamScore++;
                }
            }
            this.Draw();
        }

        public void Draw()
        {
            ConsoleColor colorBefore = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(this.Stadium.Width + 1, 0);
            Console.Write($"Home team: {this.homeTeamScore}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(this.Stadium.Width + 1, 1);
            Console.Write($"Away team: {this.awayTeamScore}");
            Console.ForegroundColor = colorBefore;

        }
    }
}
