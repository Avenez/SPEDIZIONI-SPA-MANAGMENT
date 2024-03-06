using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpedizioniSPA.Models
{
    public class Aggiornamenti
    {
        [ScaffoldColumn(false)]
        public int idSpedizione { get; set; }

        [Required]
        [Display(Name = "Stato della Spedizione")]
        public string Stato { get; set; }

        [Required]
        [Display(Name = "Posizione della spedizione")]
        public string Posizione { get; set; }

        [Required]
        [Display(Name = "Descrizione dello stato")]
        public string Descrizione { get; set; }

        [ScaffoldColumn(false)]
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
        public static void InserisciNuovoAggiornamento(Aggiornamenti nuovoAggiornamento, int IdSpedizione)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
             
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Aggiornamenti (idSpedizione, Stato, Posizione, Descrizione, Aggiornamento) VALUES (@IdSpedizione, @Stato, @Posizione, @Descrizione, @Aggiornamento)", conn);
                cmd.Parameters.AddWithValue("@IdSpedizione", IdSpedizione);
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



        // Metodo statico per inserire un nuovo aggiornamento nella tabella Aggiornamenti quando viene creata una nuova spedizione
        //Questo presenta valori predefiniti in modo da inserire un aggiornamento di "Presa in carica dell'ordine"
        public static void InserisciNuovoAggiornamentoIniziale(int IdSpedizione)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Aggiornamenti (idSpedizione, Stato, Posizione, Descrizione, Aggiornamento) VALUES (@IdSpedizione, @Stato, @Posizione, @Descrizione, @Aggiornamento)", conn);
                cmd.Parameters.AddWithValue("@IdSpedizione", IdSpedizione);
                cmd.Parameters.AddWithValue("@Stato", "1 - Preso in carico");
                cmd.Parameters.AddWithValue("@Posizione", "Sede Centrale");
                cmd.Parameters.AddWithValue("@Descrizione", "Spedizione presa in carico");
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


        // Metodo statico per recuperare una lista di tutti gli aggiornamenti dalla tabella Aggiornamenti attraverso il numero di ordine
        //Restituisce una List di Aggiornamento
        public static List<Aggiornamenti> GetListaAggiornamenti(int idSpedizione)
        {
            List<Aggiornamenti> listaAggiornamenti = new List<Aggiornamenti>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Aggiornamenti WHERE idSpedizione = @idSpedizione ORDER BY Stato DESC", conn);
                cmd.Parameters.AddWithValue("@idSpedizione", idSpedizione);
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

            //Ritorno una List di Aggiornamento
            return listaAggiornamenti;
        }

    }
}