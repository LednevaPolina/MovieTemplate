using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Movie.Models;

namespace Movie.TagHelpers
{
    public class MovieDetailsTagHelper: TagHelper
    {
        public Cinema Movie { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(Movie!=null)
            {
                output.TagName = "a";
                output.Attributes.Add("class", "btn btn-primary");
                output.Attributes.Add("href", $"/Home/MovieDetail/{Movie.imdbID}");
                output.Content.Append("Details");
                //var i = new TagBuilder("i");
                //if (Movie.Type == "game")
                //    i.AddCssClass("fa-solid fa-gamepad");
                //else if (Movie.Type == "series")
                //    i.AddCssClass("fa-solid fa-tv");
                //else if (Movie.Type == "movie")
                //    i.AddCssClass("fa-solid fa-film");

                //output.Content.AppendHtml(i);
                //output.Content.Append(" Details");

            }
            

        }
    }
}
