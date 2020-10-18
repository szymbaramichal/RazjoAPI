using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using AutoMapper;
using MongoDB.Driver;

namespace API.Helpers
{
    public class ApiHelper : IApiHelper
    {
        #region Variables
        private IMapper _mapper;
        private IMongoCollection<User> _users;
        private IMongoCollection<Value> _values;
        private IMongoCollection<CalendarNote> _calendarNotes;
        private IMongoCollection<Family> _familes;
        private IMongoCollection<PrivateNote> _privateNotes;
        private IMongoCollection<Visit> _visits;
        private IMongoDatabase database;

        #endregion

        #region Constructor
        public ApiHelper(IMapper mapper)
        {
            _mapper = mapper;

            var client = new MongoClient("mongodb+srv://test:test@main.qhp4n.mongodb.net/<dbname>?retryWrites=true&w=majority");
            database = client.GetDatabase("Razjo");
            _users = database.GetCollection<User>("Users");
            _values = database.GetCollection<Value>("Values");
            _calendarNotes = database.GetCollection<CalendarNote>("CalendarNotes");
            _familes = database.GetCollection<Family>("Families");
            _privateNotes = database.GetCollection<PrivateNote>("PrivateNotes");
            _visits = database.GetCollection<Visit>("Visits");
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
            user.FamilyId = new List<string>();

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("testrazjo@gmail.com", "TestRazjo2115"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("Razjo@razjo.com"),
                Subject = "Dziękujemy za skorzystanie z naszej aplikacji!",
                Body = File.ReadAllText("mail.txt"),
                IsBodyHtml = true,
            };

            mailMessage.To.Add(user.Email);

            smtpClient.Send(mailMessage);

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

        public async Task<string> ReturnUserRole(string id)
        {
            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();

            return user.Role;
        }
        
        public async Task<string[]> ReturnUserName(string id)
        {
            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();

            string[] names = {user.FirstName, user.Surname};
            return names;
        }
        
        public async Task<User> UpdateUserInfo(string id, string firstName, string surname)
        {
            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();
        
            user.FirstName = firstName;
            user.Surname = surname;

            await _users.FindOneAndReplaceAsync<User>(x => x.Id == id, user);

            return user;
        }
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
    
        #region CalendarMethods
        public async Task<CalendarNote> AddCalendarNote(CalendarNote calendarNote, string userId)
        {
            calendarNote.Date = new Date{
                Day = DateTime.Now.Day,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                Minute = DateTime.Now.Minute,
                Hour = DateTime.Now.Hour
            };

            calendarNote.UserId = userId;
            calendarNote.UserRole = await ReturnUserRole(userId);

            await _calendarNotes.InsertOneAsync(calendarNote);
            
            return calendarNote;
        }
        public async Task<List<CalendarNote>> ReturnActualMonthNotes(string familyId, string userId)
        {
            var family = await _familes.Find<Family>(x => x.Id == familyId).FirstOrDefaultAsync();

            if(family.PSYId == userId || family.USRId == userId)
            {
                var notes = await _calendarNotes.Find<CalendarNote>(x => x.FamilyId == familyId && 
                x.Date.Month == DateTime.Now.Month && 
                x.Date.Year == DateTime.Today.Year).ToListAsync();
                return notes;
            }

            else return null;
        }
        public async Task<List<CalendarNote>> ReturnNotesForMonth(string familyId, string userId, int month)
        {
            var family = await _familes.Find<Family>(x => x.Id == familyId).FirstOrDefaultAsync();

            if(family.PSYId == userId || family.USRId == userId)
            {
                var notes = await _calendarNotes.Find<CalendarNote>(x => x.FamilyId == familyId && x.Date.Month == month).ToListAsync();
                return notes;
            }
            else return null;
        }

       
        public async Task<Visit> AddVisit(Visit visit, string userId)
        {
            var family = await ReturnFamilyInfo(visit.FamilyId, userId);
            if(family == null)
            {
                return null;
            }
            
            await _visits.InsertOneAsync(visit);

            return visit;
        }
        public async Task<List<Visit>> ReturnCurrentMonthVisits(string familyId, string userId)
        {
            var family = await _familes.Find<Family>(x => x.Id == familyId).FirstOrDefaultAsync();

            if(family.PSYId == userId || family.USRId == userId)
            {
                var visits = await _visits.Find<Visit>(x => x.FamilyId == familyId && 
                x.Date.Month == DateTime.Now.Month && 
                x.Date.Year == DateTime.Today.Year).ToListAsync();
                return visits;
            }

            else return null;
        }
        public async Task<List<Visit>> ReturnVisitsForMonth(string familyId, string userId, int month)
        {
            var family = await _familes.Find<Family>(x => x.Id == familyId).FirstOrDefaultAsync();

            if(family.PSYId == userId || family.USRId == userId)
            {
                var visits = await _visits.Find<Visit>(x => x.FamilyId == familyId && x.Date.Month == month).ToListAsync();
                return visits;
            }
            else return null;
        }
        #endregion
    
        #region FamilyMethods
        public async Task<Family> CreateFamily(string id, string familyName)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var invitationCode = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            Family familyToAdd = new Family{
                FamilyName = familyName,
                PSYId = id,
                InvitationCode = invitationCode
            };

            await _familes.InsertOneAsync(familyToAdd);

            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();
            user.FamilyId.Add(familyToAdd.Id);

            await _users.FindOneAndReplaceAsync<User>(x => x.Id == id, user);
            
            return familyToAdd;
        }
        public async Task<Family> JoinToFamily(string invitationCode, string id)
        {
            var family = await _familes.Find<Family>(x => x.InvitationCode == invitationCode).FirstOrDefaultAsync();

            if(family == null) return null;

            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();

            if(user.Role == "USR")
            {
                if(user.FamilyId.Count > 0 || family.USRId != null) return null;
            }

            family.USRId = id;

            user.FamilyId.Add(family.Id);

            await _users.FindOneAndReplaceAsync<User>(x => x.Id == id, user);
            await _familes.FindOneAndReplaceAsync<Family>(x => x.Id == family.Id, family);

            return family;
        }
        public async Task<ReturnFamilyDTO> ReturnFamilyInfo(string familyId, string userId)
        {
            var family = await _familes.Find<Family>(x => x.Id == familyId).FirstOrDefaultAsync();;

            if(family == null) return null;

            if(family.PSYId != userId && family.USRId != userId) return null;

            var familyInfo = new ReturnFamilyDTO();
            familyInfo.CalendarNotes = new List<ReturnCalendarNoteDTO>();
            familyInfo.Visits = new List<ReturnVisitDTO>();

            if(family.USRId != null)
            {
                var usr = await _users.Find<User>(x => x.Id == family.USRId).FirstOrDefaultAsync();
                familyInfo.UserNames = usr.FirstName + " " + usr.Surname;

                familyInfo.UsrId = usr.Id;
            }

            var notes = await ReturnActualMonthNotes(familyId, userId);
            var visits = await ReturnCurrentMonthVisits(familyId, userId);

            var psy = await _users.Find<User>(x => x.Id == family.PSYId).FirstOrDefaultAsync();
            
            familyInfo.PsyId = psy.Id;
            familyInfo.PsychologistNames = psy.FirstName + " " + psy.Surname;
            
            familyInfo.FamilyId = family.Id;
            familyInfo.FamilyName = family.FamilyName;
            
            familyInfo.InvitationCode = family.InvitationCode;
            
            foreach (var note in notes)
            {
                familyInfo.CalendarNotes.Add(_mapper.Map<ReturnCalendarNoteDTO>(note));
            }

            foreach (var visit in visits)
            {
                familyInfo.Visits.Add(_mapper.Map<ReturnVisitDTO>(visit));
            }

            return familyInfo;
        }
        #endregion
    
        #region PrivateNotesMethods
        public async Task<PrivateNote> AddPrivateNote(string message, string userId)
        {
            PrivateNote noteToAdd = new PrivateNote{
                Message = message,
                UserId = userId,
                CreationDate = new Date{
                    Day = DateTime.Now.Day,
                    Month = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    Minute = DateTime.Now.Minute,
                    Hour = DateTime.Now.Hour
                }
            };
            await _privateNotes.InsertOneAsync(noteToAdd);

            return noteToAdd;
        }
        public async Task<List<PrivateNote>> ReturnUserPrivateNotes(string userId)
        {
            List<PrivateNote> notes = await _privateNotes.Find<PrivateNote>(x => x.UserId == userId).ToListAsync();    

            return notes;        
        }
        #endregion
    }
}