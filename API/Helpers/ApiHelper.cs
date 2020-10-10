using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using MongoDB.Driver;

namespace API.Helpers
{
    public class ApiHelper : IApiHelper
    {
        #region Variables
        private IMongoCollection<User> _users;
        private IMongoCollection<Value> _values;
        private IMongoDatabase database;

        #endregion

        #region Constructor
        public ApiHelper()
        {
            var client = new MongoClient("mongodb+srv://test:test@main.qhp4n.mongodb.net/<dbname>?retryWrites=true&w=majority");
            database = client.GetDatabase("Main");
            _users = database.GetCollection<User>("Users");
            _values = database.GetCollection<Value>("Values");
        }
        #endregion

        #region UserMethods
        public async Task<bool> AddUser(User user, string password)
        {
            if(await _users.Find<User>(x => x.Email == user.Email).AnyAsync()) return false;

            using var hmac = new HMACSHA512();

            user.Email = user.Email.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            user.PasswordSalt = hmac.Key;

            await _users.InsertOneAsync(user);

            return true;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _users.Find<User>(x => x.Email == email.ToLower()).FirstOrDefaultAsync();
            if (user == null) return null;

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return null;
            }

            return user;
        }

        #endregion

        #region CalendarMethods
        
        #endregion

        #region TestMethods
        public async Task<Value> GetValueById(string id)
        {
            return await _values.Find<Value>(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Value> AddValue(Value value)
        {
            await _values.InsertOneAsync(value);
            return value;
        }
        
        #endregion
    }
}