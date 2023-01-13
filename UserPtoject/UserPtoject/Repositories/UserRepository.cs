using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserPtoject.Interfaces;
using UserPtoject.Models;

namespace UserPtoject.Repositories
{
    public class UserRepository : IUser
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "AuthSecret",
            BasePath = "BasePath"
        };
        IFirebaseClient client;
        public async Task<User> CreateUser(User user)
        {

            client = new FireSharp.FirebaseClient(config);
            var data = user;
            var pushResponse = client.Push("Users/", data);

            user.Id = pushResponse.Result.name;


            var setResponse = client.Set("Users/" + user.Id, data);
            return data;
        }

        public void DeleteUser(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Users/" + id);
        }

        public List<User> GetAllUsers()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Users");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<User>();
            if(data != null) //Hiç veri olmazsa hata vermemesi için
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<User>(((JProperty)item).Value.ToString()));
                }
            }

            return list;
        }

        public User GetUserById(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Users/"+id);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var user = new User();
            if(data != null) //Hiç veri olmazsa hata vermemesi için
            {
                user.Name = data.Name;
                user.LastName = data.LastName;
            }
            return user;
        }

        public void UpdateUser(User user)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Users/" + user.Id);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            if (data == null) //Arama çubuğunda /Update/1 yazarak kayıt ekleme işlemini engellemek için
            {
                throw new Exception("Olmayan Bir Kullanıcıyı Güncellemeye Çalıştınız.");
            }
            var setResponse = client.Update("Users/" + user.Id, user);

        }
    }
}

