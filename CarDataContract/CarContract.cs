using System;

namespace CarDataContract
{
    public class CarContract
    {
        public string Title { get; set; }
        public Fuel Fuel { get; set; }
        public int Price { get; set; }
        public bool IsNew { get; set; }
        public int Mileage { get; set; }
        public DateTime FirstRegistration { get; set; }
    }
}
