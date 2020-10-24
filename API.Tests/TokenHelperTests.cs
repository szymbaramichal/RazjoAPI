using System.Collections.Generic;
using System.Threading.Tasks;
using API.Helpers;
using API.Models;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace API.Tests
{
    public class TokenHelperTests
    {
        #region variables
        string testToken = 
        "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkxMjM0NTY3ODkxMjM0NTYiLCJlbWFpbCI6InRlc3RAdGVzdC50ZXN0IiwibmJmIjoxNjAzNTQyMDkwLCJleHAiOjE2NzI2NjU2OTAsImlhdCI6MTYwMzU0MjA5MH0.oFTj2usitqhrHZSuzgoT_9cTz5SYMfrxwbz3M5qpi9uQxSL1h0tQcI1SaHSGPkXgu1mXQC4eDR9-VQI05gqN6A";
        DatabaseSettings settings = new DatabaseSettings{
                ResetPasswordsName = "Test_ResetPasswords",
                UsersCollectionName = "Test_Users",
                CalendarNotesCollectionName = "Test_CalendarNotes",
                VisitsCollectionName = "Test_Visits",
                FamiliesCollectionName = "Test_Families",
                PrivateNotesCollectionName = "Test_PrivateNotes",
                ConnectionString = "mongodb+srv://razjo:razjo@testrazjo.eqqzg.mongodb.net/<dbname>?retryWrites=true&w=majority",
                DatabaseName = "Test_Razjo"
        };
        #endregion

        #region TestMethods
    
        [Fact]
        public void CreateToken()
        {
            
            #region Initialization_Of_TokenHelper
            
            var myConfiguration = new Dictionary<string, string>
            {
                {"TokenKey", "Test key to Test Aplication "},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var tokenHelper = new TokenHelper(configuration);
            #endregion

            #region Data   
            var user = new User{
                Id = "123456789123456789123456",
                Email = "test@test.test",
                FamilyId = new List<string>(),
                FirstName = "Test",
                Role = "USR",
                Surname = "Test"
            };
            #endregion


            #region Test_Method
            var result = tokenHelper.CreateToken(user);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            #endregion
        }

        [Fact]
        public void GetIdByToken()
        {
            
            #region Initialization_Of_TokenHelper
            
            var myConfiguration = new Dictionary<string, string>
            {
                {"TokenKey", "Test key to Test Aplication "},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var tokenHelper = new TokenHelper(configuration);
            #endregion

            #region Test_Method
            var result = tokenHelper.GetIdByToken(testToken);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal("123456789123456789123456", result);
            #endregion
        }

        #endregion
    }
}