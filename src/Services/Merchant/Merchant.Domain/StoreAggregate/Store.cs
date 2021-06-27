using System;
using System.Collections.Generic;
using System.Linq;
using Joker.Domain;
using Joker.Domain.Entities;
using Joker.Exceptions;
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
        public Guid MerchantId { get; private set; }
        public string Name { get; private set; }
        public string Slogan { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string Description { get; private set; }
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
        /// <param name="merchantId"></param>
        /// <param name="name"></param>
        /// <param name="slogan"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static Store Create(Guid id,
            Guid merchantId,
            string name,
            string slogan,
            string phoneNumber,
            string email,
            string description)
        {
            Check.NotEmpty(id, nameof(id));
            Check.NotEmpty(merchantId, nameof(merchantId));
            Check.NotNullOrEmpty(name, nameof(name));

            return new Store
            {
                Id = id,
                MerchantId = merchantId,
                Name = name,
                Slogan = slogan,
                PhoneNumber = phoneNumber,
                Email = email,
                Description = description,
                EmailConfirmed = false,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow
            };
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