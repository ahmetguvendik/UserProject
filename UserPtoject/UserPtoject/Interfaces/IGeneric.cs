using UserPtoject.Models;

namespace UserPtoject.Interfaces
{
    public interface IGeneric <T>
    {
        public Task<T> CreateUser(T user);
        public void UpdateUser(T user);
        public void DeleteUser(string id);
        public List<T> GetAllUsers();
        public T GetUserById(string id);
    }
}
