using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpedizioniSPA.Validations
{
    public class TelDuplicity : ValidationAttribute
    {
        protected override ValidationResult IsValid(object Telefono, ValidationContext validationContext)
        {
            System.Diagnostics.Debug.WriteLine("Telefono: " + Telefono);

            string telToCheck = Telefono.ToString();

            if (!ControllaEmail(telToCheck))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Il numero ti telefono inserito inserita è già in uso");
            }

        }



        public static bool ControllaEmail(string tel)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString(); ;
            string query = "SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS trovato FROM UPrivato WHERE telefono = @Telefono;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Telefono", tel);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToBoolean(reader["trovato"]);
                    }
                }
            }

            return false; // Se non si trovano righe o c'è un errore durante l'operazione
        }
    }
}