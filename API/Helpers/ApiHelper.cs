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


//TODO:
//usuwanie rodziny
//testy do TokenHelper

namespace API.Helpers
{
    public class ApiHelper : IApiHelper
    {
        #region Variables
        private IMapper _mapper;
        private IMongoCollection<User> _users;
        private IMongoCollection<CalendarNote> _calendarNotes;
        private IMongoCollection<Family> _familes;
        private IMongoCollection<PrivateNote> _privateNotes;
        private IMongoCollection<Visit> _visits;
        private IMongoCollection<ResetPassword> _resetPasswords;
        private IMongoDatabase database;

        #endregion

        #region Constructor
        public ApiHelper(IMapper mapper, IDatabaseSettings settings)
        {
            _mapper = mapper;

            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
            _calendarNotes = database.GetCollection<CalendarNote>(settings.CalendarNotesCollectionName);
            _familes = database.GetCollection<Family>(settings.FamiliesCollectionName);
            _privateNotes = database.GetCollection<PrivateNote>(settings.PrivateNotesCollectionName);
            _visits = database.GetCollection<Visit>(settings.VisitsCollectionName);
            _resetPasswords = database.GetCollection<ResetPassword>(settings.ResetPasswordsName);
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
                Subject = "Dziękujemy za rejestracje w naszej aplikacji!",
                Body = File.ReadAllText("./../API/RegistrationMail.txt"),
                IsBodyHtml = true
            };

            mailMessage.To.Add(user.Email);

            await smtpClient.SendMailAsync(mailMessage);

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
            if(user == null) return null;

            return user.Role;
        }
        
        public async Task<string[]> ReturnUserName(string id)
        {
            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();

            if(user == null) return null;

            string[] names = {user.FirstName, user.Surname};
            return names;
        }
        
        public async Task<User> UpdateUserInfo(string id, string firstName, string surname)
        {
            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();
            if(user == null) return null;
        
            user.FirstName = firstName;
            user.Surname = surname;

            await _users.FindOneAndReplaceAsync<User>(x => x.Id == id, user);

            return user;
        }

        public async Task<bool> SendResetPasswordMail(string email)
        {
            if(await _resetPasswords.Find<ResetPassword>(x => x.Email == email).FirstOrDefaultAsync() != null) return false;

            var user = await _users.Find<User>(x => x.Email == email).FirstOrDefaultAsync();
            if(user == null) return false;

            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var resetPasswordCode = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

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
                Subject = resetPasswordCode,
                Body = File.ReadAllText("./../API/ResetPasswordMail.txt"),
                IsBodyHtml = true
            };
                
            mailMessage.To.Add(email);
            await smtpClient.SendMailAsync(mailMessage);

            var resetPassword = new ResetPassword{
                Email = email,
                ResetCode = resetPasswordCode
            };

            await _resetPasswords.InsertOneAsync(resetPassword);

            return true;
        }

        public async Task<bool> SetNewPassword(string resetPasswordCode, string email, string newPassowrd)
        {
            var resetPassword = await _resetPasswords.Find<ResetPassword>(x => x.Email == email && x.ResetCode == resetPasswordCode).FirstOrDefaultAsync();
            if(resetPassword == null) return false;

            var user = await _users.Find<User>(x => x.Email == email).FirstOrDefaultAsync();
            if(user == null) return false;

            using var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassowrd));
            user.PasswordSalt = hmac.Key;

            await _resetPasswords.DeleteOneAsync(x => x.Email == email && x.ResetCode == resetPasswordCode);
            await _users.FindOneAndReplaceAsync(x => x.Id == user.Id, user);

            return true;
        }

        public async Task<bool> ValidateResetPasswordCode(string resetPasswordCode, string email)
        {
            var resetPassword = await _resetPasswords.Find<ResetPassword>(x => x.Email == email && x.ResetCode == resetPasswordCode).FirstOrDefaultAsync();
            if(resetPassword == null) return false;
            
            return true;
        }

        #endregion
    
        #region CalendarMethods
        public async Task<CalendarNote> AddCalendarNote(CalendarNote calendarNote, string userId)
        {
            if(!await DoesUserBelongToFamily(calendarNote.FamilyId, userId)) return null;

            calendarNote.Date = new Date{
                Day = DateTime.Now.Day.ToString(),
                Month = DateTime.Now.Month.ToString(),
                Year = DateTime.Now.Year.ToString(),
                Minute = DateTime.Now.Minute.ToString(),
                Hour = DateTime.Now.Hour.ToString()
            };

            calendarNote.UserId = userId;
            calendarNote.UserRole = await ReturnUserRole(userId);

            await _calendarNotes.InsertOneAsync(calendarNote);
            
            return calendarNote;
        }
        public async Task<List<CalendarNote>> ReturnCurrentMonthNotes(string familyId, string userId)
        {
            var family = await _familes.Find<Family>(x => x.Id == familyId).FirstOrDefaultAsync();

            if(family.PSYId == userId || family.USRId == userId)
            {
                var notes = await _calendarNotes.Find<CalendarNote>(x => x.FamilyId == familyId && 
                x.Date.Month == DateTime.Now.Month.ToString() && 
                x.Date.Year == DateTime.Now.Year.ToString()).ToListAsync();
                return notes;
            }

            else return null;
        }
        public async Task<List<CalendarNote>> ReturnNotesForMonth(string familyId, string userId, string month)
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
            if(!await DoesUserBelongToFamily(visit.FamilyId, userId)) return null;
            if(await ReturnUserRole(userId) != "PSY") return null;

            await _visits.InsertOneAsync(visit);

            return visit;
        }
        public async Task<List<Visit>> ReturnCurrentMonthVisits(string familyId, string userId)
        {
            if(!await DoesUserBelongToFamily(familyId, userId)) return null;

            var visits = await _visits.Find<Visit>(x => x.FamilyId == familyId && 
            x.Date.Month == DateTime.Now.Month.ToString() && 
            x.Date.Year == DateTime.Now.Year.ToString()).ToListAsync();
            return visits;

        }
        public async Task<List<Visit>> ReturnVisitsForMonth(string familyId, string userId, string month)
        {
            if(!await DoesUserBelongToFamily(familyId, userId)) return null;


            var visits = await _visits.Find<Visit>(x => x.FamilyId == familyId && x.Date.Month == month).ToListAsync();
            return visits;
        }
        #endregion
    
        #region FamilyMethods
        public async Task<Family> CreateFamily(string id, string familyName)
        {
            if(await ReturnUserRole(id) != "PSY") return null;

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
            if(await ReturnUserRole(id) == "PSY") return null;
            
            var family = await _familes.Find<Family>(x => x.InvitationCode == invitationCode).FirstOrDefaultAsync();

            if(family == null) return null;

            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();

            if(user.FamilyId.Count > 0 || family.USRId != null) return null;

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

            var notes = await ReturnCurrentMonthNotes(familyId, userId);
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
        public async Task<bool> SendMailWithCode(string userMail, string familyId, string performerId)
        {
            var family = await _familes.Find<Family>(x => x.Id == familyId).FirstOrDefaultAsync();

            if(family == null) return false;
            if(family.PSYId == performerId)
            {
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
                    Subject = family.FamilyName + " - " + family.InvitationCode,
                    Body = File.ReadAllText("./../API/CodeMail.txt"),
                    IsBodyHtml = true
                };
                
                mailMessage.To.Add(userMail);

                await smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            else return false;
        }
        public async Task<bool> DoesUserBelongToFamily(string familyId, string userId)
        {
            var family = await _familes.Find<Family>(x => x.Id == familyId).FirstOrDefaultAsync();
            if(family != null)
            {
                if(family.PSYId == userId || family.USRId == userId) return true;
                else return false;
            }
            else return false;
        }

        public async Task<bool> DeleteFamily(string userId, string familyId)
        {
            if(await ReturnUserRole(userId) == "PSY")
            {
                var family = await _familes.Find<Family>(x => x.Id == familyId).FirstOrDefaultAsync();
                if(family.USRId != userId && family.PSYId != userId) return false;
            
                var psy = await _users.Find<User>(x => x.Id == userId).FirstOrDefaultAsync();
                if(family.USRId != null)
                {
                    var usr = await _users.Find<User>(x => x.Id == family.USRId).FirstOrDefaultAsync();
                    usr.FamilyId.Remove(family.Id);
                    await _users.FindOneAndReplaceAsync(x => x.Id == usr.Id, usr);
                }

                psy.FamilyId.Remove(family.Id);

                await _calendarNotes.DeleteManyAsync(x => x.FamilyId == family.Id);
                await _visits.DeleteManyAsync(x => x.FamilyId == family.Id);
                
                await _familes.DeleteOneAsync(x => x.Id == familyId);

                await _users.FindOneAndReplaceAsync(x => x.Id == userId, psy);

                return true;
            }
            else return false;
        }
        
        #endregion
    
        #region PrivateNotesMethods
        public async Task<PrivateNote> AddPrivateNote(string message, string userId)
        {
            if(await _users.Find<User>(x => x.Id == userId).FirstOrDefaultAsync() == null) return null;

            PrivateNote note = new PrivateNote{
                Message = message,
                UserId = userId,
                CreationDate = new Date{
                    Day = DateTime.Now.Day.ToString(),
                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    Minute = DateTime.Now.Minute.ToString(),
                    Hour = DateTime.Now.Hour.ToString()
                }
            };

            await _privateNotes.InsertOneAsync(note);

            return note;
        }
        public async Task<List<PrivateNote>> ReturnUserPrivateNotes(string userId)
        {
            List<PrivateNote> notes = await _privateNotes.Find<PrivateNote>(x => x.UserId == userId).ToListAsync();    

            return notes;        
        }
        
        public async Task<PrivateNote> UpdateNote(string message, string noteId, string userId)
        {
            var note = await _privateNotes.Find<PrivateNote>(x => x.Id == noteId).FirstOrDefaultAsync();

            if(note == null) return null;

            if(note.UserId != userId) return null;

            note.Message = message;

            await _privateNotes.FindOneAndReplaceAsync<PrivateNote>(x => x.Id == noteId, note);

            return note;
        }
        #endregion
    }
}