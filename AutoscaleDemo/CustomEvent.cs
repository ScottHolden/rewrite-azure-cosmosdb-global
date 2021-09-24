using System;

namespace AutoscaleDemo
{
    class CartOperationEvent
    {
        public string id { get; set; }

        public string CartID { get; set; }

        public string Action { get; set; }

        public string Item { get; set; }

        public string Price { get; set; }

        public string Username { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string Date { get; set; }

        public DateTime Timestamp { get; set; }
    }

    class CartOperationEventBasic
    {
        public string id { get; set; }

        public string CartID { get; set; }
    }

    class PaymentEvent
    {
        public string id { get; set; }

        public Decimal Amount { get; set; }

        public string TransactionType { get; set; }

        public string Currency { get; set; }

        public string Username { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string Date { get; set; }

        public DateTime Timestamp { get; set; }

    }

}
