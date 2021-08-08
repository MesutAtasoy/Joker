using System;

namespace Joker.WebApp.ViewModels.Management
{
    public class CurrencyViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}