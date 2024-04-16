using Newtonsoft.Json;

namespace Questao2
{
    class FootballAPI
    {
        private readonly HttpClient _client;

        public FootballAPI()
        {
            _client = new HttpClient();
        }

        public async Task<List<MatchData>> GetFootballMatches(int year, string teamName, string teamParameter, int page)
        {
            List<MatchData> allMatches = new List<MatchData>();            

            while (true)
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamParameter}={teamName}&page={page}";
                HttpResponseMessage response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var matchResponse = JsonConvert.DeserializeObject<MatchResponse>(content);

                    // Adicionar todos os jogos à lista de partidas
                    allMatches.AddRange(matchResponse.Data);

                    // Verificar se há mais páginas disponíveis
                    if (page >= matchResponse.TotalPages)
                    {
                        break; // Não há mais páginas disponíveis
                    }

                    page++;
                }
                else
                {
                    throw new HttpRequestException($"Falha na requisição. Código de status: {response.StatusCode}");
                }                
            }
            return allMatches;
        }
    }
}