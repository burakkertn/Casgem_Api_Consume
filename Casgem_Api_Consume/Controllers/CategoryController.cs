﻿using Casgem_Api_Consume.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text;

namespace Casgem_Api_Consume.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://localhost:44375/api/Category");

            if (responseMessage.IsSuccessStatusCode)
            {
                var JSonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(JSonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateResultCategory createResultCategory)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createResultCategory);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44375/api/Category", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();

        }
        public async Task<IActionResult> DeleteCategory(int id)

        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.DeleteAsync($"https://localhost:44375/api/Category/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44375/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCategory>(jsonData);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategory updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44375/api/Category/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
