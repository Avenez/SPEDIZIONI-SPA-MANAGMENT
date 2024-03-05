using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SpedizioniSPA.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using static SpedizioniSPA.Validations.EmailDuplicity;
using static SpedizioniSPA.Validations.TelDuplicity;

namespace SpedizioniSPA.Models
{


    public class Privato
    {
        [ScaffoldColumn(false)]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Email Cliente")]
        [EmailAddress]
        [EmailDuplicity(ErrorMessage = "Email inserita già in uso")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nome Cliente")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Il campo Nome deve contenere solo lettere.")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Cognome Cliente")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Il campo Cognome deve contenere solo lettere.")]
        public string Cognome { get; set; }

        [Required]
        [Display(Name = "Codice Fiscale")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il campo Codice Fiscale deve contenere 16 caratteri.")]
        public string CF { get; set; }

        [Required]
        [Display(Name = "Indirizzo")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Il campo Nome deve essere lungo minimo 4 caratteri e massimo 50 caratteri.")]
        public string Indirizzo { get; set; }


        [Required]
        [Display(Name = "Regione")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Il campo regione deve contenere solo lettere.")]
        public string Regione { get; set; }


        [Required]
        [TelDuplicity]
        [Display(Name = "Numero di Telefono")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il campo Numero di Telefono deve contenere solo numeri.")]
        public string Telefono { get; set; }


        public string fullIdCliente => $"{Nome} {Cognome} {CF} {Indirizzo}";

        // Costruttore vuoto
        public Privato()
        {
        }

        // Costruttore con tutti i campi
        public Privato(int idCliente, string email, string nome, string cognome, string cf, string indirizzo, string regione, string telefono)
        {
            IdCliente = idCliente;
            Email = email;
            Nome = nome;
            Cognome = cognome;
            CF = cf;
            Indirizzo = indirizzo;
            Regione = regione;
            Telefono = telefono;
        }



        public static void InserisciNuovoPrivato(Privato nuovoPrivato)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO UPrivato (Email, Nome, Cognome, CF, Indirizzo, Regione, Telefono) VALUES (@Email, @Nome, @Cognome, @CF, @Indirizzo, @Regione, @Telefono)", conn);
                cmd.Parameters.AddWithValue("@Email", nuovoPrivato.Email);
                cmd.Parameters.AddWithValue("@Nome", nuovoPrivato.Nome);
                cmd.Parameters.AddWithValue("@Cognome", nuovoPrivato.Cognome);
                cmd.Parameters.AddWithValue("@CF", nuovoPrivato.CF);
                cmd.Parameters.AddWithValue("@Indirizzo", nuovoPrivato.Indirizzo);
                cmd.Parameters.AddWithValue("@Regione", nuovoPrivato.Regione);
                cmd.Parameters.AddWithValue("@Telefono", nuovoPrivato.Telefono);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Inserimento avvenuto con Successo");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }


        // Metodo per creare e restituire una lista di oggetti Privato
        public static List<Privato> GetListaPrivati()
        {
            List<Privato> listaPrivati = new List<Privato>();

            // Esempio di query per estrarre dati dal database e popolare la lista
            string queryString = "SELECT * FROM UPrivato";
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Costruire un oggetto Privato da ogni riga del database
                    Privato privato = new Privato();
                    privato.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                    privato.Email = reader["Email"].ToString();
                    privato.Nome = reader["Nome"].ToString();
                    privato.Cognome = reader["Cognome"].ToString();
                    privato.CF = reader["CF"].ToString();
                    privato.Indirizzo = reader["Indirizzo"].ToString();
                    privato.Regione = reader["Regione"].ToString();
                    privato.Telefono = reader["Telefono"].ToString();

                    listaPrivati.Add(privato);
                }
                reader.Close();
            }

            return listaPrivati;
        }



    }






}

