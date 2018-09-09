using System;
using System.Collections.Generic;

namespace AutoRepository
{
    public interface IDataStore
    {
        IEnumerable<CarEntity> Cars { get; }
        void Add(CarEntity car);
        CarEntity FirstOrDefault(Func<CarEntity, bool> predicate);
        void Update(CarEntity car);
        bool Exists(Predicate<CarEntity> predicate);
    }
}