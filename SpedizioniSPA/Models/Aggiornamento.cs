using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpedizioniSPA.Models
{
    public class Aggiornamenti
    {
        public int idSpedizione { get; set; }

        public string Stato { get; set; }

        public string Posizione { get; set; }

        public string Descrizione { get; set; }

        public DateTime Aggiornamento { get; set; }

        public Aggiornamenti() { }


        public Aggiornamenti(int idSpedizione, string stato, string posizione, string descrizione, DateTime aggiornamento)
        {
            this.idSpedizione = idSpedizione;
            this.Stato = stato;
            this.Posizione = posizione;
            this.Descrizione = descrizione;
            this.Aggiornamento = aggiornamento;
        }


        // Metodo statico per inserire un nuovo aggiornamento nella tabella Aggiornamenti
        public static void InserisciNuovoAggiornamento(Aggiornamenti nuovoAggiornamento)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
             
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Aggiornamenti (idSpedizione, Stato, Posizione, Descrizione, Aggiornamento) VALUES (@IdSpedizione, @Stato, @Posizione, @Descrizione, @Aggiornamento)", conn);
                cmd.Parameters.AddWithValue("@IdSpedizione", nuovoAggiornamento.idSpedizione);
                cmd.Parameters.AddWithValue("@Stato", nuovoAggiornamento.Stato);
                cmd.Parameters.AddWithValue("@Posizione", nuovoAggiornamento.Posizione);
                cmd.Parameters.AddWithValue("@Descrizione", nuovoAggiornamento.Descrizione);
                cmd.Parameters.AddWithValue("@Aggiornamento", DateTime.Now);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Inserimento avvenuto con successo");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante l'inserimento: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        // Metodo statico per recuperare una lista di tutti gli aggiornamenti dalla tabella Aggiornamenti
        public static List<Aggiornamenti> GetListaAggiornamenti()
        {
            List<Aggiornamenti> listaAggiornamenti = new List<Aggiornamenti>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Aggiornamenti", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Aggiornamenti aggiornamento = new Aggiornamenti();
                    aggiornamento.idSpedizione = (int)reader["idSpedizione"];
                    aggiornamento.Stato = reader["Stato"].ToString();
                    aggiornamento.Posizione = reader["Posizione"].ToString();
                    aggiornamento.Descrizione = reader["Descrizione"].ToString();
                    aggiornamento.Aggiornamento = (DateTime)reader["Aggiornamento"];

                    listaAggiornamenti.Add(aggiornamento);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante il recupero degli aggiornamenti: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return listaAggiornamenti;
        }

    }
}