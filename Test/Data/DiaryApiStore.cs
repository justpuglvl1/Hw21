using Newtonsoft.Json;
using System.Text;
using Test.Models;

namespace Test.Data
{
    public class DiaryApiStore : IDiary
    {
        private readonly HttpClient _httpClient;
        private const string _apiUrl = @"https://localhost:7084/api/diary";

        public DiaryApiStore()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Notes>> AllNotesAsync()
        {
            string json = await _httpClient.GetStringAsync(_apiUrl);
            return JsonConvert.DeserializeObject<IEnumerable<Notes>>(json);
        }

        public async Task<Notes> GetNoteByIdAsync(int id)
        {
            string json = await _httpClient.GetStringAsync(_apiUrl + $"/{id}");
            return JsonConvert.DeserializeObject<Notes>(json);
        }

        public async Task AddNoteAsync(Notes note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_apiUrl, content);
        }

        public async Task DeleteNoteAsync(int id)
        {
            await _httpClient.DeleteAsync(_apiUrl + $"/{id}");
        }

        public async Task UpdateNoteAsync(Notes note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(_apiUrl, content);
        }
    }
}
