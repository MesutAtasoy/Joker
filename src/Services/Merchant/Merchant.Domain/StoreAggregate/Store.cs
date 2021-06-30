using System;
using System.Collections.Generic;
using System.Linq;
using Joker.Domain;
using Joker.Domain.Entities;
using Joker.Exceptions;
using Merchant.Domain.Refs;
using Merchant.Domain.StoreAggregate.Events;
using Merchant.Domain.StoreAggregate.Rules;

namespace Merchant.Domain.StoreAggregate
{
    public class Store : DomainEntity, IAggregateRoot
    {
        private Store()
        {
            _storeFAQs = new List<StoreFAQ>();
            _storeBusinessHours = new List<StoreBusinessHour>();
        }

        public Guid Id { get; private set; }
        public MerchantRef Merchant { get; private set; }
        public string Name { get; private set; }
        public string Slogan { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string Description { get; private set; }
        public StoreLocation Location { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }
        public bool IsDeleted { get; private set; }

        private List<StoreFAQ> _storeFAQs;
        public IReadOnlyList<StoreFAQ> StoreFAQs => _storeFAQs.AsReadOnly();

        private List<StoreBusinessHour> _storeBusinessHours;
        public IReadOnlyList<StoreBusinessHour> StoreBusinessHours => _storeBusinessHours.AsReadOnly();


        /// <summary>
        /// Creates a new store
        /// </summary>
        /// <param name="id"></param>
        /// <param name="merchant"></param>
        /// <param name="name"></param>
        /// <param name="slogan"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="description"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Store Create(Guid id,
            MerchantRef merchant,
            string name,
            string slogan,
            string phoneNumber,
            string email,
            string description,
            StoreLocation location)
        {
            Check.NotEmpty(id, nameof(id));
            Check.NotNull(merchant, nameof(merchant));
            Check.NotNull(location, nameof(location));
            Check.NotNullOrEmpty(name, nameof(name));

            var store = new Store
            {
                Id = id,
                Merchant = merchant,
                Name = name,
                Slogan = slogan,
                PhoneNumber = phoneNumber,
                Email = email,
                Description = description,
                Location = location,
                EmailConfirmed = false,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow
            };

            store.AddDomainEvent(new StoreCreatedEvent(id, 
                merchant,
                name,
                slogan, 
                phoneNumber,
                email,
                description,
                location));

            return store;
        }


        /// <summary>
        /// Updates store
        /// </summary>
        /// <param name="name"></param>
        /// <param name="slogan"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="description"></param>
        public void Update(string name,
            string slogan,
            string phoneNumber,
            string email,
            string description)
        {
            Check.NotNullOrEmpty(name, nameof(name));


            if (Name != name)
            {
                AddDomainEvent(new StoreNameUpdatedEvent(Id, Name, name));
            }

            Name = name;
            Slogan = slogan;
            PhoneNumber = phoneNumber;
            Description = description;
            Email = email;
            if (Email != email)
            {
                EmailConfirmed = false;
            }

            ModifiedDate = DateTime.UtcNow;

            AddDomainEvent(new StoreUpdatedEvent(Id, 
                Merchant, 
                name,
                slogan,
                phoneNumber,
                email,
                description,
                Location));
        }

        /// <summary>
        /// Mark as confirmed mail 
        /// </summary>
        public void MarkAsConfirmedMail()
        {
            EmailConfirmed = true;
            ModifiedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates store's location
        /// </summary>
        /// <param name="location"></param>
        public void UpdateLocation(StoreLocation location)
        {
            Location = location;
            ModifiedDate = DateTime.UtcNow;
            
            AddDomainEvent(new StoreUpdatedEvent(Id, 
                Merchant, 
                Name,
                Slogan,
                PhoneNumber,
                Email,
                Description,
                Location));
        }

        /// <summary>
        /// Mark as deleted
        /// </summary>
        /// <returns></returns>
        public void MarkAsDeleted()
        {
            IsDeleted = true;
            ModifiedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Add a business hour to store
        /// </summary>
        /// <param name="storeBusinessHour"></param>
        public void AddBusinessHour(StoreBusinessHour storeBusinessHour)
        {
            Check.NotNull(storeBusinessHour, nameof(storeBusinessHour));

            if (!_storeBusinessHours.Any(x => x.DayOfWeek == storeBusinessHour.DayOfWeek))
            {
                _storeBusinessHours.Add(storeBusinessHour);
            }
        }

        /// <summary>
        /// Remove a business hour to store
        /// </summary>
        /// <param name="storeBusinessHour"></param>
        public void RemoveBusinessHour(StoreBusinessHour storeBusinessHour)
        {
            Check.NotNull(storeBusinessHour, nameof(storeBusinessHour));

            RemoveBusinessHour(storeBusinessHour.DayOfWeek);
        }

        /// <summary>
        /// Remove a business hour to store
        /// </summary>
        /// <param name="dayOfWeek"></param>
        public void RemoveBusinessHour(int dayOfWeek)
        {
            CheckRule(new DayOfWeekValidRule(dayOfWeek));

            var businessHour = _storeBusinessHours.FirstOrDefault(x => x.DayOfWeek == dayOfWeek);

            if (businessHour != null)
            {
                _storeBusinessHours.Remove(businessHour);
            }
        }

        /// <summary>
        /// Add a new faq
        /// </summary>
        /// <param name="storeFaq"></param>
        public void AddFAQ(StoreFAQ storeFaq)
        {
            Check.NotNull(storeFaq, nameof(storeFaq));

            _storeFAQs.Add(storeFaq);
        }

        /// <summary>
        /// Remove a faq
        /// </summary>
        /// <param name="storeFaqId"></param>
        public void RemoveFAQ(Guid storeFaqId)
        {
            var faq = _storeFAQs.FirstOrDefault(x => x.Id == storeFaqId);

            if (faq != null)
            {
                _storeFAQs.Remove(faq);
            }
        }
    }
}