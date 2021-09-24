using Bogus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoscaleDemo
{
    class Util
    {
        static internal List<CartOperationEvent> GenerateRandomCartOperationEvents(int numberOfDocumentsPerBatch)
        {
            var actions = new[] { "viewed", "addedToCart", "purchased" };

            // have only 10k posts
            var faker = new Faker();

            var operationEvent = new Faker<CartOperationEvent>()

                //Ensure all properties have rules. 
                .StrictMode(true)

                .RuleFor(o => o.id, f => Guid.NewGuid().ToString())

                //Generate event
                .RuleFor(o => o.CartID, f => Guid.NewGuid().ToString()) //TODO: Make it an integer value
                .RuleFor(o => o.Action, f => f.PickRandom(actions))

                .RuleFor(o => o.Item, f => f.Commerce.ProductName())
                .RuleFor(o => o.Price, f => f.Commerce.Price())
                .RuleFor(o => o.Username, f => f.Internet.UserName())
                .RuleFor(o => o.Country, f => f.Address.Country())
                //.RuleFor(o => o.timestamp, f => f.Date.Between(DateTime.Now, DateTime.Now.AddDays(-1))) //3 days ago
                .RuleFor(o => o.Address, f => f.Address.StreetAddress())
                .RuleFor(o => o.Timestamp, f => DateTime.Now) // just today's date
                .RuleFor(o => o.Date, (f, m) => $"{m.Timestamp.ToString("yyyy-MM-dd")}");

            var events = operationEvent.Generate(numberOfDocumentsPerBatch);

            return events;

        }

        static internal List<PaymentEvent> GenerateRandomPaymentEvent(int numberOfDocumentsPerBatch)
        {
            var faker = new Faker();

            var paymentEvent = new Faker<PaymentEvent>()
                .StrictMode(true)
                //Generate event
                .RuleFor(o => o.id, f => Guid.NewGuid().ToString())
                .RuleFor(o => o.Amount, f => f.Finance.Amount())
                .RuleFor(o => o.TransactionType, f => f.Finance.TransactionType())
                .RuleFor(o => o.Currency, f => f.Finance.Currency().ToString())
                .RuleFor(o => o.Username, f => f.Internet.UserName())
                .RuleFor(o => o.Country, f => f.Address.Country())
                .RuleFor(o => o.Address, f => f.Address.StreetAddress())
                .RuleFor(o => o.Timestamp, f => DateTime.Now) // just today's date
                .RuleFor(o => o.Date, (f, m) => $"{m.Timestamp.ToString("yyyy-MM-dd")}");

            var events = paymentEvent.Generate(numberOfDocumentsPerBatch);

            return events;

        }
    }
}

