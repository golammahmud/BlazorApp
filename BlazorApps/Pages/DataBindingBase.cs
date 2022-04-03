using Microsoft.AspNetCore.Components;

namespace BlazorApps.Pages
{
    public class DataBindingBase:ComponentBase
    {
        protected string Name { get; set; } = "Tom";

        protected string Gender { get; set; } = "Male";


        [Parameter]
        public string buttonText { get; set; } 
        protected string Colour { get; set; } = "background-color:white";
    }
}
