﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDataContract;

namespace AutoRepository
{
    public class CarRepository : ICarRepository
    {
        List<CarEntity> cars = new List<CarEntity>();
        static volatile int id;
        object _lockObject_ = new object();
        private readonly IMapper mapper;
        private readonly IDataStore dataStore;

        public CarRepository(IMapper mapper, IDataStore dataStore)
        {
            this.mapper = mapper;
            this.dataStore = dataStore;
            cars.AddRange(dataStore.Get());
            id = cars.Count;
        }

        public Car Add(CarContract carContract)
        {
            var car = mapper.Map<CarEntity>(carContract);
            car.Id = GetNextNumber();
            cars.Add(car);
            return mapper.Map<Car>(car);
        }

        public bool Delete(int carId)
        {
            var car = cars.FirstOrDefault(c => c.Id == carId && !c.IsDeleted);
            if (car != null)
            {
                car.IsDeleted = true;
                return true;
            }
            return false;
        }

        public Car GetCarById(int carId)
        {
            var car = cars.FirstOrDefault(c => c.Id == carId && !c.IsDeleted);
            if (car != null)
            {
                return mapper.Map<Car>(car);
            }
            return null;
        }

        public IQueryable<Car> GetCars()
        {
            return GetActiveCars().AsQueryable();
                        
        }

        private IEnumerable<Car> GetActiveCars()
        {
            foreach (var carEntity in cars)
            {
                if (!carEntity.IsDeleted)
                    yield return mapper.Map<Car>(carEntity);
            }
        }

        private int GetNextNumber()
        {
            lock (_lockObject_)
            {
                return ++id;
            }
        }

        public void Update(Car car)
        {
            int index = cars.IndexOf(cars.Single(c => c.Id == car.Id));
            cars.RemoveAt(index);
            cars.Insert(index, mapper.Map<CarEntity>(car));
        }

        public bool Exists(int carId)
        {
            return cars.Exists(c => !c.IsDeleted && c.Id == carId);
        }
    }
}
