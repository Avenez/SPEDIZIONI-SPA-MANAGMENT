using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpedizioniSPA.Validations
{
    public class StateDuplicity
    {


        public static bool ControllaStato(int idSpedizione)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString(); ;
            string query = "SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS trovato FROM Aggiornamenti WHERE idSpedizione = @idSpedizione;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idSpedizione", idSpedizione);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.Read())
                    {
                        return Convert.ToBoolean(reader["trovato"]);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Errore" + ex);
                    return false;
                }

                finally
                {
                    connection.Close();
                }



            }


        }

    }
}