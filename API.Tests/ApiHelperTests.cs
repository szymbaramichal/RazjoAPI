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

    //BEFORE TESTING, DROP DATABASE!
    //VARIABLES SHOULD BE SET AFTER CREATING FAMILY, CREATING BOTH TYPE OF USERS
    //
    public class ApiHelperTests
    {
        #region variables
        string psyId = "5f8e007f3959cc26088ffb86";
        string usrId = "5f8df9b8dc5dd44d8a60bf42";
        string familyId = "5f8e00a5314f1696d6ca53d5";
        #endregion

        #region UserMethodsTests
        [Fact]
        public async Task AddUser_PSY()
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
                Email = "test123psy@mail.com",
                FamilyId = new List<string>(),
                FirstName = "FirstName",
                Surname = "Surname",
                Role = "PSY"
            };
            #endregion

            #region Test_Method
            var result = await apiHelper.AddUser(user, "password");

            Assert.True(result);
            #endregion
        }

        [Fact]
        public async Task AddUser_USR()
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
                Email = "test123usr@mail.com",
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
            var result = await apiHelper.Login("test123usr@mail.com", "password");

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

            #region Test_Method
            var result = await apiHelper.ReturnUserRole(usrId);

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

            #region Test_Method
            var result = await apiHelper.ReturnUserName(usrId);

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

            #region Test_Method
            var result = await apiHelper.UpdateUserInfo(usrId, "FirstName" , "Surname");

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

            #region Test_Method
            var result = await apiHelper.CreateFamily(psyId, "FamilyName");

            Assert.NotNull(result);
            Assert.Equal("FamilyName", result.FamilyName);
            #endregion
        }

        [Fact]
        public async Task JoinToFamily()
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

            #region Test_Method
            var result = await apiHelper.JoinToFamily("QO8a8Txm", usrId);

            Assert.NotNull(result);
            Assert.Equal("FamilyName", result.FamilyName);
            #endregion
        }

        [Fact]
        public async Task ReturnFamilyInfo()
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

            #region Test_Method
            var result = await apiHelper.ReturnFamilyInfo(familyId, psyId);

            Assert.NotNull(result);
            Assert.Equal(psyId, result.PsyId);
            Assert.Null(result.UsrId);
            #endregion
        }

        [Fact]
        public async Task SendMailWithCode()
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

            #region Test_Method
            var result = await apiHelper.SendMailWithCode("mail@mail.mailki", familyId, psyId);

            Assert.True(result);
            #endregion
        }

        [Fact]
        public async Task DoesUserBelongToFamily()
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

            #region Test_Method
            var result = await apiHelper.DoesUserBelongToFamily(familyId, psyId);

            Assert.True(result);
            #endregion
        }

        #endregion

    }
}
