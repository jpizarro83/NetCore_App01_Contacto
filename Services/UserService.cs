using System;
using System.Collections.Generic;
using System.Linq;

using contactos.Models;

namespace contactos.Services{
    public interface IUserService
    {
        User Authenticate (string username, string password);
        IEnumerable<User> GetAll();
        User GetByUserName(string username);
        User Create(User user, string password);
        void Update(User user, string password=null);
        void Delete(string username);
    }

    public class UserService : IUserService
    {
        private ContactosContext _context;

        public UserService(ContactosContext contexto)
        {
            _context = contexto;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.User.SingleOrDefault(x => x.username == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
                return null;
            
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password es requerido","password");
            
            if (_context.User.Any(x => x.username == user.username))
                throw new ArgumentException("Username - " + user.username + "- ya existe");
            
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password,out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(string username)
        {
            var user = _context.User.Find(username);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User;
        }

        public User GetByUserName(string username)
        {
            return _context.User.Find(username);
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.User.Find(userParam.username);
            
            if (user == null)
                throw new ArgumentException("Usuario no existe");

            user.email = userParam.email;
            user.fechacreado = userParam.fechacreado;

            if(!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password,out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.User.Update(user);
            _context.SaveChanges();
        }

        //Métodos Privados
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password==null) throw new ArgumentException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Valor no puede ser vacío o espacios en blanco");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Valor no puede ser vacío o espacios en blanco.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Longitud Inválida para PassWord Hash (Se esperaba 64 bytes).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Longitud Inválida para PassWord Slat (Se esperaba 128 bytes).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }

}