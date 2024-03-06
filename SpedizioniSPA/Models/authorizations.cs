﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SpedizioniSPA.Models
{
    public class authorizations : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        //-----METODO PER LE AUTORIZZAZIONI----------
        //Metodo per le autorizzazioni preso ugualmente ad esempi
        public override string[] GetRolesForUser(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SELECT Ruolo FROM Utenti WHERE Username=@Username", conn);
            cmd.Parameters.AddWithValue("Username", username);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            List<string> roles = new List<string>();

            while (reader.Read())
            {
                string ruolo = reader["Ruolo"].ToString();
                roles.Add(ruolo); //aggiungo alla lista
            }

            conn.Close();
            return roles.ToArray(); //IMPORTANTE: il tipo di ritorno prevede un array di stringhe

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}