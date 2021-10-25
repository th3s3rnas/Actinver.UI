namespace Actinver.UI.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class IndexModel : PageModel
    {
        #region Properties
        private readonly ILogger<IndexModel> _logger;
        public string Message { get; set; }
        public string ValueInput { get; set; }
        [BindProperty]
        public string Filter { get; set; }
        public string[] Filters = new[] { "byName", "byIngredient" };

        [BindProperty]
        public CocktailEntity CocktailEntity { get; set; }
        public List<CocktailEntity> CocktailEntityList { get; set; } = new List<CocktailEntity>();
        public bool ShowResults { get; set; }
        public string FiltersString { get; set; }
        #endregion

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            CocktailEntityList.Clear();
            ShowResults = false;
            FiltersString = string.Empty;
        }

        public async Task OnPost()
        {
            string valueCocktail = Request.Form["ValueInput"];
            string filter = Request.Form["Filter"];
            FiltersString = $"{filter}/{valueCocktail}";
            try
            {
                CocktailEntityList.Clear();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:9902/Cocktail/{filter.ToString()}/{valueCocktail.ToString()}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var cocktailList = JsonConvert.DeserializeObject<ResultApi<CocktailsEntity>>(apiResponse);
                        if (cocktailList is not null)
                        {
                            ShowResults = true;
                            CocktailEntityList = cocktailList.Response.drinks;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                _logger.LogError($"Error en OnPost. {error.Message}");
            }
        }
    }

    public class CocktailsEntity
    {
        public List<CocktailEntity> drinks { get; set; } = new List<CocktailEntity>();
    }
    public class CocktailEntity
    {
        public string idDrink { get; set; }
        public string strDrink { get; set; }
        public string strTags { get; set; }
        public string strCategory { get; set; }
        public string strIBA { get; set; }
        public string strAlcoholic { get; set; }
        public string strGlass { get; set; }
        public string strInstructions { get; set; }
        public string strDrinkThumb { get; set; }
        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }
        public string strIngredient6 { get; set; }
        public string strIngredient7 { get; set; }
        public string strIngredient8 { get; set; }
        public string strIngredient9 { get; set; }
        public string strIngredient10 { get; set; }
        public string strIngredient11 { get; set; }
        public string strIngredient12 { get; set; }
        public string strIngredient13 { get; set; }
        public string strIngredient14 { get; set; }
        public string strIngredient15 { get; set; }
        public string dateModified { get; set; }
    }
    public class ResultApi<T>
    {
        public T Response { get; set; }
        public ResultApiError Error { get; set; } = new ResultApiError();
        public string TimeExecution { set; get; } = string.Empty;
    }

    public class ResultApiError
    {
        public string Message { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
