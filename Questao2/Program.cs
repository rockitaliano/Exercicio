using Newtonsoft.Json;
using Questao2;

public class Program
{
    public const string team1 = "team1";
    public const string team2 = "team2";
    public static async Task Main()
    {
        string teamName1 = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName1, year);

        Console.WriteLine("Team "+ teamName1 +" scored "+ totalGoals.ToString() + " goals in "+ year);

        string teamName2 = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName2, year);

        Console.WriteLine("Team " + teamName2 + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoals(string teamName, int year)
    {

        FootballAPI api = new FootballAPI();
        int totalGoals = 0;

        // Loop para percorrer todas as páginas da API para team1
        for (int page = 1; page <= 3; page++)
        {
            List<MatchData> matches = await api.GetFootballMatches(year, teamName, team1, page);
            
            foreach (var match in matches)
            {
                totalGoals += match.Team1Goals;
            }
        }

        //Loop para percorrer todas as páginas da API para team2
        for (int page = 1; page <= 3; page++)
        {
            List<MatchData> matches = await api.GetFootballMatches(year, teamName, team2, page);
            
            foreach (var match in matches)
            {
                totalGoals += match.Team2Goals;
            }
        }
        return totalGoals;
    }
}