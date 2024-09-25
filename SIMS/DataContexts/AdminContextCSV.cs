using System.Collections.Generic;
using System.IO;
using System.Linq;
using SIMS.Models;
using SIMS.Abstractions;

namespace SIMS.DataContexts
{
    public class AdminContextCSV : IAdmin
    {
        private readonly string _filePath;

        public AdminContextCSV(string filePath)
        {
            _filePath = filePath;
        }

        public Administrator GetAdminByUsername(string username)
        {
            return GetAllAdmins().FirstOrDefault(a => a.Username == username);
        }

        public bool ValidateAdmin(string username, string password)
        {
            var admin = GetAdminByUsername(username);
            return admin != null && admin.Password == password; // Passwords should be hashed and compared securely
        }

        public void AddAdmin(Administrator admin)
        {
            var admins = GetAllAdmins();
            admins.Add(admin);
            SaveAllAdmins(admins);
        }

        private List<Administrator> GetAllAdmins()
        {
            var admins = new List<Administrator>();
            if (File.Exists(_filePath))
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    admins.Add(new Administrator
                    {
                        Id = int.Parse(parts[0]),
                        Username = parts[1],
                        Password = parts[2]
                    });
                }
            }
            return admins;
        }

        private void SaveAllAdmins(List<Administrator> admins)
        {
            var lines = admins.Select(a => $"{a.Id},{a.Username},{a.Password}").ToArray();
            File.WriteAllLines(_filePath, lines);
        }
    }
}
