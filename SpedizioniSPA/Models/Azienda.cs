using SpedizioniSPA.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SpedizioniSPA.Validations.PecValidation;
using static SpedizioniSPA.Validations.PecDuplicity;
using static SpedizioniSPA.Validations.PIVADuplicity;

namespace SpedizioniSPA.Models
{
    public class Azienda
    {

        [ScaffoldColumn(false)]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Il campo Nome deve essere lungo minimo 2 caratteri e massimo 50 caratteri.")]
        [Display(Name = "Nome Azienda")]
        
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [PecValidation]
        [PecDuplicity]
        [Display(Name = "Pec Azienda")]
        public string Pec { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Il campo Indirizzo deve essere lungo minimo 5 caratteri e massimo 50 caratteri.")]
        [Display(Name = "Indirizzo")]
        public string Indirizzo { get; set; }

        [Required]
        [PIVADuplicity]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il campo PIVA deve contenere solo numeri.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Il campo PIVA deve essere di 11 caratteri.")]
        [Display(Name = "Partita Iva")]
        public string PartitaIVA { get; set; }

        public string fullIdCliente => $"{Nome} {Indirizzo} {PartitaIVA}";

        // Costruttore vuoto
        public Azienda()
        {
        }

        // Costruttore con tutti i campi
        public Azienda(int idCliente, string nome, string pec, string indirizzo, string partitaIVA)
        {
            IdCliente = idCliente;
            Nome = nome;
            Pec = pec;
            Indirizzo = indirizzo;
            PartitaIVA = partitaIVA;
        }


        public static void InserisciNuovaAzienda(Azienda nuovaAzienda)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO UAzienda (Nome, Pec, Indirizzo, PIVA) VALUES (@Nome, @Pec, @Indirizzo, @PIVA)", conn);
                cmd.Parameters.AddWithValue("@Nome", nuovaAzienda.Nome);
                cmd.Parameters.AddWithValue("@Pec", nuovaAzienda.Pec);
                cmd.Parameters.AddWithValue("@Indirizzo", nuovaAzienda.Indirizzo);
                cmd.Parameters.AddWithValue("@PIVA", nuovaAzienda.PartitaIVA);

                cmd.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine("Inserimento avvenuto con Successo");
                Console.WriteLine("Inserimento avvenuto con Successo");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                System.Diagnostics.Debug.WriteLine("Erroe" + ex);
            }
            finally
            {
                conn.Close();
            }
        }


        public static List<Azienda> GetListaAziende()
        {
            List<Azienda> listaAziende = new List<Azienda>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT IdCliente, Nome, Pec, Indirizzo, PIVA FROM UAzienda", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idCliente = Convert.ToInt32(reader["IdCliente"]);
                    string nome = reader["Nome"].ToString();
                    string pec = reader["Pec"].ToString();
                    string indirizzo = reader["Indirizzo"].ToString();
                    string partitaIVA = reader["PIVA"].ToString();

                    Azienda azienda = new Azienda(idCliente, nome, pec, indirizzo, partitaIVA);
                    listaAziende.Add(azienda);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                System.Diagnostics.Debug.WriteLine("Errore: " + ex);
            }
            finally
            {
                conn.Close();
            }

            return listaAziende;
        }

    }

}