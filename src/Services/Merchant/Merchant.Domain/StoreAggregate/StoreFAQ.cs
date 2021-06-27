using System;
using Joker.Domain.Entities;
using Joker.Exceptions;

namespace Merchant.Domain.StoreAggregate
{
    public class StoreFAQ : DomainEntity
    {
        public Guid Id { get; private set; }
        public string Question { get; private set; }
        public string Answer { get; private set; }
        public int Order { get; private set; }

        public StoreFAQ(Guid id,
            string question,
            string answer,
            int order)
        {
            Check.NotNull(id, nameof(id));
            Check.NotNullOrEmpty(question, nameof(question));
            Check.NotNullOrEmpty(answer, nameof(answer));

            Id = id;
            Question = question;
            Answer = answer;
            Order = order;
        }
    }
}