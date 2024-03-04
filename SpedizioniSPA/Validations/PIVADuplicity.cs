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
    public class PIVADuplicity : ValidationAttribute
    {

        protected override ValidationResult IsValid(object PIVA, ValidationContext validationContext)
        {
            System.Diagnostics.Debug.WriteLine("Email: " + PIVA);

            string pIvaToCheck = PIVA.ToString();

            if (!ControllaPIva(pIvaToCheck))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("La PIVA inserita è già in uso");
            }

        }



        public static bool ControllaPIva(string pIva)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString(); ;
            string query = "SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS trovato FROM UAzienda WHERE PIVA = @PIVA;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PIVA", pIva);
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