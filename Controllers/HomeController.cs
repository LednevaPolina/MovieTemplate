using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Movie.Services;
using System.Diagnostics;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieApiServices movieApiServices;

        public HomeController(IMovieApiServices movieApiServices)
        {
            this.movieApiServices=movieApiServices;
        }

        public IActionResult Index()
        {
            return View();
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
            }
            catch (Exception ex)
            {

                ViewBag.errorMessages = ex.Message;
            }

            return View(cinema);
        }
       
        public async Task<IActionResult> Search(string title)
        {
            MovieApiResponse result = null;
            //search movies
            try
            {
                result=await movieApiServices.SearchByTitleAsync(title);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessages=ex.Message;
            }
            
            ViewBag.searchMovie = title;
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}