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
        [Display(Name = "Stato Spedizione")]
        public string Stato { get; set; }

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
        public DateTime? DataConsegna { get; set; }

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
        public Spedizione(int idSpedizione, DateTime dataSpedizione, decimal peso, decimal costo, DateTime? dataConsegna, string cittaDestinazione, int idDestinatario)
        {
            IdSpedizione = idSpedizione;
            DataSpedizione = dataSpedizione;
            Peso = peso;
            Costo = costo;
            DataConsegna = dataConsegna;
            CittaDestinazione = cittaDestinazione;
            IdDestinatario = idDestinatario;
        }


        public static void InserisciNuovaSpedizione(Spedizione nuovaSpedizione)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Spedizione (dataSpedizione, peso, costo, dataConsegna, cittaDestinazione, idDestinatario) VALUES (@DataSpedizione, @Peso, @Costo, @DataConsegna, @CittaDestinazione, @IdDestinatario)", conn);
                cmd.Parameters.AddWithValue("@DataSpedizione", nuovaSpedizione.DataSpedizione);
                cmd.Parameters.AddWithValue("@Peso", nuovaSpedizione.Peso);
                cmd.Parameters.AddWithValue("@Costo", nuovaSpedizione.Costo);
                cmd.Parameters.AddWithValue("@DataConsegna", (object)nuovaSpedizione.DataConsegna ?? DBNull.Value); // Handling possible null value for DataConsegna
                cmd.Parameters.AddWithValue("@CittaDestinazione", nuovaSpedizione.CittaDestinazione);
                cmd.Parameters.AddWithValue("@IdDestinatario", nuovaSpedizione.IdDestinatario);

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

    }

}