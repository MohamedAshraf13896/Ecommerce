using System.Collections.Generic;

namespace Project.Repositories
{
    public interface CRUDrepo<T>
    {
        int DeleteById(int id);
        int Edit(T newProduct);
        List<T> GetAll();
        T GetById(int id);
        int Insert(T prod);
    }
}
