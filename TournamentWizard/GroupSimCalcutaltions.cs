using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentWizard.Models;
using TournamentWizard.Extensions;

namespace TournamentWizard
{
    public static class GroupSimCalcutaltions
    {
        /// <summary>
        /// returns the expected number of matches in a given group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static int ExpectedNumberOfGroupMatches(Group group)
        {
            int count = 0;
            for (int i = group.Teams.Count - 1; i >= 1; i--)
            {
                count += i;
            }
            return count;
        }
        /// <summary>
        /// returns the expected number of group-matches in a given competition
        /// </summary>
        /// <param name="competition"></param>
        /// <returns></returns>
        public static int ExpectedNumberOfGroupMatches(Competition competition)
        {
            int count = 0;
            foreach(Group group in competition.Groups)
            {
                count += ExpectedNumberOfGroupMatches(group);
            }
            return count;
        }
        /// <summary>
        ///  returns the expected number of group-matches in a given SportEvent
        /// </summary>
        /// <param name="sportEvent"></param>
        /// <returns></returns>
        public static int ExpectedNumberOfGroupMatches(SportEvent sportEvent)
        {
            int count = 0;
            foreach (Competition competition in sportEvent.Competitions)
            {
                count += ExpectedNumberOfGroupMatches(competition);
            }
            return count;
        }
        /// <summary>
        /// returns the number of time slices needed for a given number of matches on a given number of fields
        /// </summary>
        /// <param name="matches"></param>
        /// <param name="fieldCount"></param>
        /// <returns></returns>
        public static double TimeSlicesNeeded(int matches, int fieldCount)
        {
            return Math.Round((double)matches / fieldCount, 2);
        }
        /// <summary>
        /// Calculates the total time needed for all group matches in a given competition. Takes incomplete TimeSlices into account as compete ones.
        /// </summary>
        /// <param name="sportEvent"></param>
        /// <returns></returns>
        public static TimeSpan TotalTimeNeededForGroupMatches(SportEvent sportEvent)
        {
            TimeSpan totalTime = new TimeSpan();
            foreach (Competition comp in sportEvent.Competitions)
            {
                int slices = (int)Math.Ceiling(TimeSlicesNeeded(ExpectedNumberOfGroupMatches(comp), sportEvent.FieldCount));
                totalTime = totalTime + TimeSpanExtension.Multiply(comp.MatchDuration, slices);
            }
            return totalTime;
        }
    }
}
