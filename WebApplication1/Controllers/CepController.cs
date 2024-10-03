using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CepController : Controller
    {
        private readonly HttpClient _httpClient;

        public CepController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                ModelState.AddModelError("", "Insira um Cep");
                return View("Index");  
            }

            var response = await _httpClient.GetStringAsync($"viacep.com.br/ws/${cep}/json/");
            var cepModel = JsonSerializer.Deserialize<CepModel>(response);

            return View("Index", cepModel);
        }

        
    }

}
