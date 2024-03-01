using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Esercizio.Models;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Esercizio.Controllers
{
    public class AnagraficaController : Controller
    {
        private string connString = "Server=DESKTOP-VK00VHM\\SQLEXPRESS; Initial Catalog=Esercizio be-s2l5; Integrated Security=true; TrustServerCertificate=True";
        [HttpGet]
        public IActionResult Index()
        {
            var conn = new SqlConnection(connString);
            List<Anagrafica> anagrafiche = new List<Anagrafica>();
            try
            {
                conn.Open();
                var command = new SqlCommand("Select * from ANAGRAFICA", conn);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Anagrafica anagrafica = new Anagrafica();
                        anagrafica.IDanagrafica = (int)reader["IDanagrafica"];
                        anagrafica.Cognome = reader["Cognome"].ToString();
                        anagrafica.Nome = reader["Nome"].ToString();
                        anagrafica.Indirizzo = reader["Indirizzo"].ToString();
                        anagrafica.Città = reader["Città"].ToString();
                        anagrafica.CAP = reader["CAP"].ToString();
                        anagrafica.CodiceFiscale = reader["CodiceFiscale"].ToString();

                        anagrafiche.Add(anagrafica);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View(anagrafiche);
        }
        
        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Anagrafica anagrafica)
        {
            var error = true;
            var conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                var command = new SqlCommand(@"INSERT INTO [ANAGRAFICA] 
                             ([Cognome], [Nome], [Indirizzo], [Città], [CAP], [CodiceFiscale]) 
                             VALUES 
                             (@Cognome, @Nome, @Indirizzo, @Città, @CAP, @CodiceFiscale)", conn);
                
                command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                command.Parameters.AddWithValue("@Città", anagrafica.Città);
                command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                command.Parameters.AddWithValue("@CodiceFiscale", anagrafica.CodiceFiscale);

                
                var nRows = command.ExecuteNonQuery();
                error = false;
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }
            if (error) return View("Error");
            else return RedirectToAction("Add", "Anagrafica");
        }
    }
}
