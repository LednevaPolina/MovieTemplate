using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Movie.Services;
using Movie.ViewModels;
using System.Diagnostics;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieApiServices movieApiServices;
        private readonly IRecentMovieStorage recentMovieStorage;

        public HomeController(IMovieApiServices movieApiServices, IRecentMovieStorage recentMovieStorage)
        {
            this.movieApiServices=movieApiServices;
            this.recentMovieStorage=recentMovieStorage;
        }

        public IActionResult Index()
        {
            
            var result = recentMovieStorage.GetRecent();
            return View(result);            
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> MovieDetail(string id)
        {
            Cinema cinema = null;

            try
            {
                cinema=await movieApiServices.SearchByIdAsync(id);
                recentMovieStorage.Add(cinema);
            }
            catch (Exception ex)
            {

                ViewBag.errorMessages = ex.Message;
            }

            return View(cinema);
        }

        public async Task<IActionResult> MovieDetailModal(string id)
        {
            Cinema cinema = null;

            try
            {
                cinema = await movieApiServices.SearchByIdAsync(id);
                recentMovieStorage.Add(cinema);
            }
            catch (Exception ex)
            {

                ViewBag.errorMessages = ex.Message;
            }

            return PartialView("_MovieDetailModalPartial",cinema);
        }



        public async Task<IActionResult> SearchResult(string title, int page = 1, int countViewPage=9)
        {
            
            SearchViewModel searchViewModel=new SearchViewModel();            
            try
            {
                MovieApiResponse result = await movieApiServices.SearchByTitleAsync(title, page);                
                searchViewModel.Movies = result.Cinemas;
                
            }
            catch (Exception ex)
            {
                searchViewModel.Error = ex.Message;
            }

            return PartialView("_MovieListPartial", searchViewModel.Movies);
        }

        public async Task<IActionResult> Search(string title, int page = 1, int countViewPage = 9)
        {

            SearchViewModel searchViewModel = new SearchViewModel();
            //search movies
            try
            {
                MovieApiResponse result = await movieApiServices.SearchByTitleAsync(title, page);

                searchViewModel.Title = title;
                searchViewModel.CountViewPage = countViewPage;
                searchViewModel.Movies = result.Cinemas;
                searchViewModel.Response = result.Response;
                searchViewModel.Error = result.Error;
                searchViewModel.TotalResults = result.TotalResults;
                searchViewModel.TotalPages = (int)Math.Ceiling(result.TotalResults / 10.0);
                searchViewModel.CurrentPage = page;
            }
            catch (Exception ex)
            {
                searchViewModel.Error = ex.Message;
            }

            return View(searchViewModel);
        }
        //public async Task<IActionResult> Search(string title, int page=1)
        //{
        //    MovieApiResponse result = null;
        //    //search movies
        //    try
        //    {
        //        result=await movieApiServices.SearchByTitleAsync(title, page);
        //        ViewBag.TotalPages = Math.Ceiling(result.TotalResults / 10.0);
        //        ViewBag.CurrentPage = page;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.errorMessages=ex.Message;
        //    }

        //    ViewBag.searchMovie = title;
        //    return View(result);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}