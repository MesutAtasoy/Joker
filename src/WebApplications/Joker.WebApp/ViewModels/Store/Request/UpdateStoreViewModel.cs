using System;

namespace Joker.WebApp.ViewModels.Store.Request
{
    public class UpdateStoreViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}