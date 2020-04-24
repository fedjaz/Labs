using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanProject
{
    class SoccerGame
    {
        public string Team1Name { get; }
        public string Team2Name { get; private set; }
        delegate void GoalFunction(string teamName);
        event GoalFunction Goal;
        public SoccerGame(List<SoccerPlayer> team1, List<SoccerPlayer> team2)
        {
            if(team1.Count == 0 || team2.Count == 0)
            {
                throw new ArgumentException("Team can't be empty.");
            }
            Team1Name = team1.First().TeamName;
            Team2Name = team2.First().TeamName;
            foreach(SoccerPlayer player in team1)
            {
                if(player.TeamName != Team1Name)
                {
                    throw new ArgumentException("Each player in team must have identical team name.");
                }
                if(!player.IsAlive)
                {
                    throw new InvalidOperationException("This player can't participate in game because he is dead");
                }
                Goal += player.Goal;
            }
            foreach(SoccerPlayer player in team2)
            {
                if(player.TeamName != Team2Name)
                {
                    throw new ArgumentException("Each player in team must have identical team name.");
                }
                if(!player.IsAlive)
                {
                    throw new InvalidOperationException("This player can't participate in game because he is dead");
                }
                Goal += player.Goal;
            }
        }

        public void Penalty()
        {
            int result = new Random().Next(2);
            if(result == 0 && Goal != null)
            {
                Goal?.Invoke(Team1Name);
            }
            else
            {
                Goal?.Invoke(Team2Name);
            }
        }
    }
}
