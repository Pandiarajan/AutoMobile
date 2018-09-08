using System;

namespace AutoApi.Model
{
    public class CarContract
    {
        public string Title { get; set; }
        public Fuel Fuel { get; set; }
        public double Price { get; set; }
        public bool IsNew { get; set; }
        public int Mileage { get; set; }
        public DateTime FirstRegistration { get; set; }
    }
}
