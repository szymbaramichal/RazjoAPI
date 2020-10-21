using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Helpers;
using API.Models;
using AutoMapper;
using Xunit;

namespace API.Tests
{

    //BEFORE TESTING, DROP DATABASE!
    //VARIABLES SHOULD BE SET AFTER CREATING FAMILY AND CREATING BOTH TYPE OF USERS
    public class ApiHelperTests
    {
        #region variables
        string psyId = "5f9090d2c2086bb8832fa606";
        string usrId = "5f9090e9504e6df0ac825923";
        string familyId = "5f909153535e60df5a2c8d03";
        string familyInvitationCode = "zNmQB06m";
        #endregion

        #region Tests_With_Correct_Values
        
        
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
            var result = await apiHelper.JoinToFamily(familyInvitationCode, usrId);

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

        #region PrivateNotesMethods
        [Fact]
        public async Task AddPrivateNote()
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
            var result = await apiHelper.AddPrivateNote("MessageInPrivateNote" ,psyId);

            Assert.NotNull(result);
            Assert.Equal("MessageInPrivateNote", result.Message);
            Assert.Equal(psyId, result.UserId);
            Assert.Equal(DateTime.Now.Day.ToString(), result.CreationDate.Day);
            Assert.Equal(DateTime.Now.Month.ToString(), result.CreationDate.Month);
            Assert.Equal(DateTime.Now.Year.ToString(), result.CreationDate.Year);
            Assert.Equal(DateTime.Now.Minute.ToString(), result.CreationDate.Minute);
            Assert.Equal(DateTime.Now.Hour.ToString(), result.CreationDate.Hour);
            #endregion
        }
       
        [Fact]
        public async Task ReturnUserPrivateNotes()
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
            var result = await apiHelper.ReturnUserPrivateNotes(psyId);

            Assert.NotNull(result);
            #endregion
        }
        #endregion
    
        #region CalendarTests
        [Fact]
        public async Task AddCalendarNote()
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
            CalendarNote calendarNote = new CalendarNote{
                Date = new Date {
                    Day = DateTime.Now.Day.ToString(),
                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    Minute = DateTime.Now.Minute.ToString(),
                    Hour = DateTime.Now.Hour.ToString()
                },
                FamilyId = familyId,
                Message = "message",
            };

            #endregion

            #region Test_Method
            var result = await apiHelper.AddCalendarNote(calendarNote ,psyId);

            Assert.NotNull(result);
            Assert.Equal("message", result.Message);
            Assert.Equal(psyId, result.UserId);
            Assert.Equal(DateTime.Now.Day.ToString(), result.Date.Day);
            Assert.Equal(DateTime.Now.Month.ToString(), result.Date.Month);
            Assert.Equal(DateTime.Now.Year.ToString(), result.Date.Year);
            Assert.Equal(DateTime.Now.Minute.ToString(), result.Date.Minute);
            Assert.Equal(DateTime.Now.Hour.ToString(), result.Date.Hour);
            #endregion
        }

        [Fact]
        public async Task ReturnCurrentMonthNotes()
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
            var result = await apiHelper.ReturnCurrentMonthNotes(familyId, psyId);

            Assert.NotNull(result);
            #endregion
        }

        [Fact]
        public async Task ReturnNotesForMonth()
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
            var result = await apiHelper.ReturnNotesForMonth(familyId, psyId, "10");

            Assert.NotNull(result);
            #endregion
        }
        
        [Fact]
        public async Task AddVisit()
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
            var visit = new Visit{
                Date = new Date {
                    Day = DateTime.Now.Day.ToString(),
                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    Minute = DateTime.Now.Minute.ToString(),
                    Hour = DateTime.Now.Hour.ToString()
                },
                FamilyId = familyId,
                Message = "message"
            };

            #endregion

            #region Test_Method
            var result = await apiHelper.AddVisit(visit, psyId);

            Assert.NotNull(result);
            Assert.Equal("message", result.Message);
            Assert.Equal(familyId, result.FamilyId);
            Assert.Equal(DateTime.Now.Day.ToString(), result.Date.Day);
            Assert.Equal(DateTime.Now.Month.ToString(), result.Date.Month);
            Assert.Equal(DateTime.Now.Year.ToString(), result.Date.Year);
            Assert.Equal(DateTime.Now.Minute.ToString(), result.Date.Minute);
            Assert.Equal(DateTime.Now.Hour.ToString(), result.Date.Hour);
            #endregion
        }

        [Fact]
        public async Task ReturnCurrentMonthVisits()
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
            var result = await apiHelper.ReturnCurrentMonthVisits(familyId, psyId);

            Assert.NotNull(result);
            #endregion
        }

        [Fact]
        public async Task ReturnVisitsForMonth()
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
            var result = await apiHelper.ReturnVisitsForMonth(familyId, psyId, "10");

            Assert.NotNull(result);
            #endregion
        }

        #endregion


        #endregion

        #region Tests_With_Incorrect_Values
        #region UserMethodsTests
        //works, if correct values tests already passed
        [Fact]
        public async Task AddUser_PSY_Invalid()
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

            Assert.False(result);
            #endregion
        }
        
        //works, if correct values tests already passed
        [Fact]
        public async Task AddUser_USR_Invalid()
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

            Assert.False(result);
            #endregion
        }

        [Fact]
        public async Task Login_Invalid()
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
            var result = await apiHelper.Login("test123usr@mail.com", "passwordInvalid");

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task ReturnUserRole_Invalid()
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
            var result = await apiHelper.ReturnUserRole("1f8df9b8dc5ed44d8a60bf42");

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task ReturnUserName_Invalid()
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
            var result = await apiHelper.ReturnUserName("1f8df9b8dc5ed44d8a60bf42");

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task UpdateUserInfo_Invalid()
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
            var result = await apiHelper.UpdateUserInfo("1f8df9b8dc5ed44d8a60bf42", "FirstName" , "Surname");

            Assert.Null(result);
            #endregion
        }

        #endregion

        #region FamilyTests
        [Fact]
        public async Task CreateFamily_Invalid()
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
            var result = await apiHelper.CreateFamily(usrId, "FamilyName");

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task JoinToFamily_Invalid()
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
            var result = await apiHelper.JoinToFamily("xxxxxxxx", psyId);

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task ReturnFamilyInfo_Invalid()
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
            var result = await apiHelper.ReturnFamilyInfo("1e8e007f3959cc26088ffb86", psyId);

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task SendMailWithCode_Invalid()
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
            var result = await apiHelper.SendMailWithCode("mail@mail.mailki", "1e8e007f3959cc26088ffb86", usrId);

            Assert.False(result);
            #endregion
        }

        [Fact]
        public async Task DoesUserBelongToFamily_Invalid()
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
            var result = await apiHelper.DoesUserBelongToFamily(familyId, "1e8e007f3959cc26088ffb86");

            Assert.False(result);
            #endregion
        }

        #endregion

        #region PrivateNotesMethods
        [Fact]
        public async Task AddPrivateNote_Invalid()
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
            var result = await apiHelper.AddPrivateNote("MessageInPrivateNote", "1e8e007f3959cc26088ffb86");

            Assert.Null(result);
            #endregion
        }
       
        [Fact]
        public async Task ReturnUserPrivateNotes_Invalid()
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
            var result = await apiHelper.ReturnUserPrivateNotes("1e8e007f3959cc26088ffb86");

            List<PrivateNote> emptyList = new List<PrivateNote>();
            Assert.Equal(emptyList, result);
            #endregion
        }
        #endregion
    
        #region CalendarTests
        [Fact]
        public async Task AddCalendarNote_Invalid()
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
            CalendarNote calendarNote = new CalendarNote{
                Date = new Date {
                    Day = DateTime.Now.Day.ToString(),
                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    Minute = DateTime.Now.Minute.ToString(),
                    Hour = DateTime.Now.Hour.ToString()
                },
                FamilyId = familyId,
                Message = "message",
            };

            #endregion

            #region Test_Method
            var result = await apiHelper.AddCalendarNote(calendarNote ,"1e8e007f3959cc26088ffb86");

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task ReturnCurrentMonthNotes_Invalid()
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
            var result = await apiHelper.ReturnCurrentMonthNotes(familyId, "1e8e007f3959cc26088ffb86");

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task ReturnNotesForMonth_Invalid()
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
            var result = await apiHelper.ReturnNotesForMonth(familyId, "1e8e007f3959cc26088ffb86", "12");

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task AddVisit_Invalid()
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
            var visit = new Visit{
                Date = new Date {
                    Day = DateTime.Now.Day.ToString(),
                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    Minute = DateTime.Now.Minute.ToString(),
                    Hour = DateTime.Now.Hour.ToString()
                },
                FamilyId = familyId,
                Message = "message"
            };

            #endregion

            #region Test_Method
            var result = await apiHelper.AddVisit(visit, "1e8e007f3959cc26088ffb86");

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task ReturnCurrentMonthVisits_Invalid()
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
            var result = await apiHelper.ReturnCurrentMonthVisits(familyId, "1e8e007f3959cc26088ffb86");

            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task ReturnVisitsForMonth_Invalid()
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
            var result = await apiHelper.ReturnVisitsForMonth(familyId, "1e8e007f3959cc26088ffb86", "10");

            Assert.Null(result);
            #endregion
        }
        #endregion

        #endregion
    }
}
