using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpedizioniSPA.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Data.SqlClient;

    public class Spedizione
    {
        [ScaffoldColumn(false)]
        public int IdSpedizione { get; set; }

        [Required]
        [Display(Name = "Data Spedizione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataSpedizione { get; set; }

        [Required]
        [Display(Name = "Peso Imballaggio in kg")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il del peso deve contenere solo numeri.")]
        public decimal Peso { get; set; }

        [Required]
        [Display(Name = "Costo Spedizione")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il campo costo deve contenere solo numeri.")]
        public decimal Costo { get; set; }

        [Required]
        [Display(Name = "Data Consegna")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataConsegna { get; set; }

        [Required]
        [Display(Name = "Città di Destinazione")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Il campo Città deve contenere solo lettere.")]
        public string CittaDestinazione { get; set; }


        [Required]
        [Display(Name = "Destinatario")]
        public int IdDestinatario { get; set; }

        // Costruttore vuoto
        public Spedizione()
        {
        }

        // Costruttore con tutti i campi
        public Spedizione(int idSpedizione, DateTime dataSpedizione, decimal peso, decimal costo, DateTime dataConsegna, string cittaDestinazione, int idDestinatario)
        {
            IdSpedizione = idSpedizione;
            DataSpedizione = dataSpedizione;
            Peso = peso;
            Costo = costo;
            DataConsegna = dataConsegna;
            CittaDestinazione = cittaDestinazione;
            IdDestinatario = idDestinatario;
        }


        public static void InserisciNuovaSpedizione(Spedizione S)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Spedizione2 (dataSpedizione, peso, costo, dataConsegna, cittaDestinazione, idDestinatario) VALUES (@dataSpedizione, @peso, @costo, @dataConsegna, @cittaDestinazione, @idDestinatario)", conn);
                
                cmd.Parameters.AddWithValue("@dataSpedizione", S.DataSpedizione);
                cmd.Parameters.AddWithValue("@peso", S.Peso.ToString());
                cmd.Parameters.AddWithValue("@costo", S.Costo.ToString());
                cmd.Parameters.AddWithValue("@dataConsegna", S.DataConsegna);
                cmd.Parameters.AddWithValue("@cittaDestinazione", S.CittaDestinazione);
                cmd.Parameters.AddWithValue("@idDestinatario", S.IdDestinatario.ToString());

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


        public static int GetIdSpedizione(Spedizione S)
        {
            int idSpedizione = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT idSpedizione FROM Spedizione2 WHERE dataSpedizione = @dataSpedizione AND peso = @peso AND costo = @costo AND dataConsegna = @dataConsegna AND cittaDestinazione = @cittaDestinazione AND idDestinatario = @idDestinatario", conn);

                // Aggiungi i parametri al comando
                cmd.Parameters.AddWithValue("@dataSpedizione", S.DataSpedizione);
                cmd.Parameters.AddWithValue("@peso", S.Peso);
                cmd.Parameters.AddWithValue("@costo", S.Costo);
                cmd.Parameters.AddWithValue("@dataConsegna", S.DataConsegna);
                cmd.Parameters.AddWithValue("@cittaDestinazione", S.CittaDestinazione);
                cmd.Parameters.AddWithValue("@idDestinatario", S.IdDestinatario);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Leggi l'ID della spedizione dal lettore
                    idSpedizione = (int)reader["idSpedizione"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }

            return idSpedizione;
        }





        public static List<Spedizione> GetListaSpedizioni()
        {
            List<Spedizione> listaSpedizioni = new List<Spedizione>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Spedizione2", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Spedizione spedizione = new Spedizione();
                    spedizione.IdSpedizione = (int)reader["idSpedizione"];
                    spedizione.DataSpedizione = (DateTime)reader["dataSpedizione"];
                    spedizione.Peso = (decimal)reader["peso"];
                    spedizione.Costo = (decimal)reader["costo"];
                    spedizione.DataConsegna = (DateTime)reader["dataConsegna"];
                    spedizione.CittaDestinazione = (string)reader["cittaDestinazione"];
                    spedizione.IdDestinatario = (int)reader["idDestinatario"];

                    listaSpedizioni.Add(spedizione);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }

            return listaSpedizioni;
        }

    }

}