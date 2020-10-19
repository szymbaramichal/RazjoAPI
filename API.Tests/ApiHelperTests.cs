using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using API.Models;
using AutoMapper;
using Xunit;

namespace API.Tests
{
    public class ApiHelperTests
    {
        #region UserMethodsTests
        [Fact]
        public async Task AddUser()
        {
            #region Create_Mapper_DatabaseSettings_And_Initialization_Of_ApiHelper
            var config = new MapperConfiguration(opts => {});
            var settings = new DatabaseSettings{
                ValuesCollectionName = "Test_Values",
                UsersCollectionName = "Test_Users",
                CalendarNotesCollectionName = "Test_CalendarNotes",
                VisitsCollectionName = "Test_Visits",
                FamiliesCollectionName = "Test_Families",
                PrivateNotesCollectionName = "Test_PrivateNotes",
                ConnectionString = "mongodb+srv://razjo:razjo@testrazjo.eqqzg.mongodb.net/<dbname>?retryWrites=true&w=majority",
                DatabaseName = "Test_Razjo"
            };

            var mapper = config.CreateMapper(); 
            var apiHelper = new ApiHelper(mapper, settings);
            #endregion

            #region Data_To_Database
            var user = new User{
                Email = "test123@mail.com",
                FamilyId = new List<string>(),
                FirstName = "FirstName",
                Surname = "Surname",
                Role = "USR"
            };
            #endregion

            #region Test_Method
            var result = await apiHelper.AddUser(user, "password");

            Assert.True(result);
            #endregion
        }

        [Fact]
        public async Task Login()
        {
            #region Create_Mapper_DatabaseSettings_And_Initialization_Of_ApiHelper
            var config = new MapperConfiguration(opts => {});
            var settings = new DatabaseSettings{
                ValuesCollectionName = "Test_Values",
                UsersCollectionName = "Test_Users",
                CalendarNotesCollectionName = "Test_CalendarNotes",
                VisitsCollectionName = "Test_Visits",
                FamiliesCollectionName = "Test_Families",
                PrivateNotesCollectionName = "Test_PrivateNotes",
                ConnectionString = "mongodb+srv://razjo:razjo@testrazjo.eqqzg.mongodb.net/<dbname>?retryWrites=true&w=majority",
                DatabaseName = "Test_Razjo"
            };

            var mapper = config.CreateMapper(); 
            var apiHelper = new ApiHelper(mapper, settings);
            #endregion

            #region Data_To_Database
            #endregion

            #region Test_Method
            var result = await apiHelper.Login("Email@mail.com", "password");

            Assert.NotNull(result);
            #endregion
        }

        [Fact]
        public async Task ReturnUserRole()
        {
            #region Create_Mapper_DatabaseSettings_And_Initialization_Of_ApiHelper
            var config = new MapperConfiguration(opts => {});
            var settings = new DatabaseSettings{
                ValuesCollectionName = "Test_Values",
                UsersCollectionName = "Test_Users",
                CalendarNotesCollectionName = "Test_CalendarNotes",
                VisitsCollectionName = "Test_Visits",
                FamiliesCollectionName = "Test_Families",
                PrivateNotesCollectionName = "Test_PrivateNotes",
                ConnectionString = "mongodb+srv://razjo:razjo@testrazjo.eqqzg.mongodb.net/<dbname>?retryWrites=true&w=majority",
                DatabaseName = "Test_Razjo"
            };

            var mapper = config.CreateMapper(); 
            var apiHelper = new ApiHelper(mapper, settings);
            #endregion

            #region Data_To_Database
            var id = "5f8df9b8dc5dd44d8a60bf42";
            #endregion

            #region Test_Method
            var result = await apiHelper.ReturnUserRole(id);

            Assert.NotNull(result);
            Assert.Equal("USR", result);
            #endregion
        }

        [Fact]
        public async Task ReturnUserName()
        {
            #region Create_Mapper_DatabaseSettings_And_Initialization_Of_ApiHelper
            var config = new MapperConfiguration(opts => {});
            var settings = new DatabaseSettings{
                ValuesCollectionName = "Test_Values",
                UsersCollectionName = "Test_Users",
                CalendarNotesCollectionName = "Test_CalendarNotes",
                VisitsCollectionName = "Test_Visits",
                FamiliesCollectionName = "Test_Families",
                PrivateNotesCollectionName = "Test_PrivateNotes",
                ConnectionString = "mongodb+srv://razjo:razjo@testrazjo.eqqzg.mongodb.net/<dbname>?retryWrites=true&w=majority",
                DatabaseName = "Test_Razjo"
            };

            var mapper = config.CreateMapper(); 
            var apiHelper = new ApiHelper(mapper, settings);
            #endregion

            #region Data_To_Database
            var id = "5f8df9b8dc5dd44d8a60bf42";
            #endregion

            #region Test_Method
            var result = await apiHelper.ReturnUserName(id);

            Assert.NotNull(result);
            Assert.Equal("FirstName" ,result[0]);
            Assert.Equal("Surname" ,result[1]);
            #endregion
        }

        [Fact]
        public async Task UpdateUserInfo()
        {
            #region Create_Mapper_DatabaseSettings_And_Initialization_Of_ApiHelper
            var config = new MapperConfiguration(opts => {});
            var settings = new DatabaseSettings{
                ValuesCollectionName = "Test_Values",
                UsersCollectionName = "Test_Users",
                CalendarNotesCollectionName = "Test_CalendarNotes",
                VisitsCollectionName = "Test_Visits",
                FamiliesCollectionName = "Test_Families",
                PrivateNotesCollectionName = "Test_PrivateNotes",
                ConnectionString = "mongodb+srv://razjo:razjo@testrazjo.eqqzg.mongodb.net/<dbname>?retryWrites=true&w=majority",
                DatabaseName = "Test_Razjo"
            };

            var mapper = config.CreateMapper(); 
            var apiHelper = new ApiHelper(mapper, settings);
            #endregion

            #region Data_To_Database
            var id = "5f8df9b8dc5dd44d8a60bf42";
            #endregion

            #region Test_Method
            var result = await apiHelper.UpdateUserInfo(id, "FirstName" , "Surname");

            Assert.NotNull(result);
            #endregion
        }

        #endregion

        #region FamilyTests
        [Fact]
        public async Task CreateFamily()
        {
            #region Create_Mapper_DatabaseSettings_And_Initialization_Of_ApiHelper
            var config = new MapperConfiguration(opts => {});
            var settings = new DatabaseSettings{
                ValuesCollectionName = "Test_Values",
                UsersCollectionName = "Test_Users",
                CalendarNotesCollectionName = "Test_CalendarNotes",
                VisitsCollectionName = "Test_Visits",
                FamiliesCollectionName = "Test_Families",
                PrivateNotesCollectionName = "Test_PrivateNotes",
                ConnectionString = "mongodb+srv://razjo:razjo@testrazjo.eqqzg.mongodb.net/<dbname>?retryWrites=true&w=majority",
                DatabaseName = "Test_Razjo"
            };

            var mapper = config.CreateMapper(); 
            var apiHelper = new ApiHelper(mapper, settings);
            #endregion

            #region Data_To_Database
            var id = "5f8df9b8dc5dd44d8a60bf42";
            #endregion

            #region Test_Method
            var result = await apiHelper.CreateFamily(id, "FamilyName");

            Assert.NotNull(result);
            Assert.Equal("FamilyName", result.FamilyName);
            #endregion
        }

        #endregion

    }
}
