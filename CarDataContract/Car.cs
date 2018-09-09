using System;

namespace CarDataContract
{
    public class Car
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Fuel Fuel { get; set; }
        public double Price { get; set; }
        public bool IsNew { get; set; }
        public int Mileage { get; set; }
        public DateTime FirstRegistration { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public string CreatedByEmail { get; set; }
    }
}
