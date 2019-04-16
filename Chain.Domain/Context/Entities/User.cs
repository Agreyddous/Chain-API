using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Chain.Shared.Context.Entities;
using FluentValidator.Validation;

namespace Chain.Domain.Context.Entities
{
    public class User : Entity
    {
        protected User() { }

        public User(string name, string username, string password, List<Guid> groups)
        {
            Name = name;
            Username = username;
            Password = EncryptPassword(password);
            Groups = groups;

            ValidationContract contract = new ValidationContract();

            if (contract.IsNotNullOrEmpty(Name, "Name", "Value is Invalid").Valid)
                contract.HasMinLen(Name, 3, "Name", "Is too short")
                    .HasMaxLen(Name, 255, "Name", "Is too long");

            if (contract.IsNotNullOrEmpty(Username, "Username", "Value is Invalid").Valid)
                contract.HasMinLen(Username, 3, "Username", "Is too short")
                    .HasMaxLen(Username, 45, "Username", "Is too long");

            if (contract.IsNotNullOrEmpty(Password, "Password", "Value is Invalid").Valid)
                contract.HasMinLen(password, 3, "Password", "Is too short")
                    .HasMaxLen(password, 100, "Password", "Is too long");

            contract.IsNotNull(Groups, "Groups", "Value is Invalid");

            AddNotifications(contract);
        }

        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<Guid> Groups { get; private set; }

        public bool Authenticate(string username, string password)
        {
            bool valid = false;
            
            if (Username == username && Password == EncryptPassword(password))
                valid = true;

            AddNotification("User", "Invalid Username or Password");

            return valid;
        }

        private string EncryptPassword(string password)
        {
            string result = "";

            if (!string.IsNullOrEmpty(password))
            {
                MD5 md5 = System.Security.Cryptography.MD5.Create();

                byte[] data = md5.ComputeHash(Encoding.Default.GetBytes((password += "|2d331cca-f6c0-40c0-bb43-6e32989c2881")));

                StringBuilder strBuilder = new StringBuilder();
                foreach (var t in data)
                    strBuilder.Append(t.ToString("x2"));

                result = strBuilder.ToString();
            }

            return result;
        }
    }
}