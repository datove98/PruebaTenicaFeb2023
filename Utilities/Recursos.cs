using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PruebaTenicaFeb2023.Utilities
{
    public class Recursos
    {
        public static List<SelectListItem> GetGeneros()
        {
            List<SelectListItem> Generos = new List<SelectListItem> {
            new SelectListItem() { Text="Hombre",Value="H"},
            new SelectListItem() { Text="Mujer", Value= "M"}
            };
            return Generos;
        }
    }
}
