using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Movie.Models;

namespace Movie.TagHelpers
{
    [HtmlTargetElement("img", Attributes ="movie")]
    public class MovieImgTagHelper: TagHelper
    {
        public Cinema Movie { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {           
                output.TagName = "img";                
                output.Attributes.Add("width", "20");
                if(Movie.Type == "game")
                    output.Attributes.Add("src", "/images/game.png");
                else if (Movie.Type == "series")
                    output.Attributes.Add("src", "/images/serial.png");
                else if (Movie.Type == "movie")
                    output.Attributes.Add("src", "/images/movie.png");                            
                
        }
    }
}
