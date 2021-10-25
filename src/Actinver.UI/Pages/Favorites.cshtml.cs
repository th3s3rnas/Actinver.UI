namespace Actinver.UI.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FavoritesModel : PageModel
    {
        private readonly ILogger<FavoritesModel> _logger;
        public static List<CocktailEntity> CocktailEntityListFavorites { get; set; } = new List<CocktailEntity>();
        [BindProperty]
        public CocktailEntity CocktailEntityFav { get; set; }
        public List<CocktailEntity> CocktailEntityListFav { get; set; } = new List<CocktailEntity>();
        public FavoritesModel(ILogger<FavoritesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            CocktailEntityListFav = CocktailEntityListFavorites;
        }
    }
}
