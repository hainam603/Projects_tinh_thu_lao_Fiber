using System.Collections.Generic;

namespace ClassModel.Connection.Reponsitory
{
    public interface IReponsitory<T> where T : class
    {
        IEnumerable<T> getAll();
        T getById(object id);
        void insert(T obj);
        void update(T obj);
        void delete(object id);
        void save();
    }
}
