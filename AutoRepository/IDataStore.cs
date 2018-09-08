using System.Collections.Generic;

namespace AutoRepository
{
    public interface IDataStore
    {
        IEnumerable<CarEntity> Get();
    }
}