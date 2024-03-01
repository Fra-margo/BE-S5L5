using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Esercizio.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Esercizio.Controllers
{
    public class VerbaleController : Controller
    {
        private string connString = "Server=DESKTOP-VK00VHM\\SQLEXPRESS; Initial Catalog=Esercizio be-s2l5; Integrated Security=true; TrustServerCertificate=True";
        [HttpGet]
        public IActionResult Index()
        {
            var conn = new SqlConnection(connString);
            List<Verbale> verbali = new List<Verbale>();

            try
            {
                conn.Open();
                var command = new SqlCommand("select * from VERBALE", conn);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale();
                        verbale.IDverbale = (int)reader["IDverbale"];
                        verbale.DataViolazione = (DateTime)reader["DataViolazione"];
                        verbale.IndirizzoViolazione = reader["IndirizzoViolazione"].ToString();
                        verbale.NominativoAgente = reader["NominativoAgente"].ToString();
                        verbale.DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"];

                        decimal importo = Convert.ToDecimal(reader["Importo"]);
                        verbale.Importo = Math.Round(importo, 2);

                        verbale.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                        verbale.IDanagrafica = (int)reader["IDanagrafica"];
                        verbale.IDviolazione = (int)reader["IDviolazione"];

                        verbali.Add(verbale);
                    }
                }
            }
            catch (Exception ex)
            {
                
                return View("Error");
            }

            return View(verbali);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<Anagrafica> anagrafiche = new List<Anagrafica>();
            List<TipoViolazione> violazioni = new List<TipoViolazione>();

            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand($"Select * from [ANAGRAFICA]", conn);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var anagrafica = new Anagrafica()
                {
                    Cognome = (string)reader["Cognome"],
                    Nome = (string)reader["Nome"],
                    IDanagrafica = (int)reader["IDanagrafica"]
                };
                anagrafiche.Add(anagrafica);
            }
            conn.Close();
            conn.Open();
            var command2 = new SqlCommand($"Select * from [TIPO_VIOLAZIONE]", conn);
            var reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                var violazione = new TipoViolazione()
                {
                    IDviolazione = (int)reader2["IDviolazione"],
                    Descrizione = (string)reader2["Descrizione"]
                };
                violazioni.Add(violazione);
            }
            conn.Close();

            ViewBag.Anagrafiche = anagrafiche;
            ViewBag.Violazioni = violazioni;

            return View();
        }

        [HttpPost]
        public IActionResult Add(Verbale verbale)
        {
            var error = true;
            var conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                var command = new SqlCommand(@"INSERT INTO [VERBALE] 
                             ([DataViolazione], [IndirizzoViolazione], [NominativoAgente], 
                              [DataTrascrizioneVerbale], [Importo], [DecurtamentoPunti], 
                              [IDanagrafica], [IDviolazione]) 
                             VALUES 
                             (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, 
                              @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, 
                              @IDanagrafica, @IDviolazione)", conn);
                command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                command.Parameters.AddWithValue("@NominativoAgente", verbale.NominativoAgente);
                command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                command.Parameters.AddWithValue("@Importo", verbale.Importo);
                command.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                command.Parameters.AddWithValue("@IDanagrafica", verbale.IDanagrafica);
                command.Parameters.AddWithValue("@IDviolazione", verbale.IDviolazione);

                var nRows = command.ExecuteNonQuery();
                error = false;
            }
            catch (Exception ex) { }
            finally { conn.Close(); }
            if (error) return View("Error");
            else return RedirectToAction("Add", "Verbale");
        }
    }
}
