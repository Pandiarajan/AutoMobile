using System;

namespace AutoApi.Model
{
    public class Car
    {
        public Car(int id, string title, 
            Fuel fuel, double price, bool isNew, int mileage,
            DateTime firstRegistration)
        {
            Id = id;
            Title = title;
            Fuel = fuel;
            Price = price;
            IsNew = isNew;
            Mileage = mileage;
            FirstRegistration = firstRegistration;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public Fuel Fuel { get; set; }
        public double Price { get; set; }
        public bool IsNew { get; set; }
        public int Mileage { get; set; }
        public DateTime FirstRegistration { get; set; }
    }
}
