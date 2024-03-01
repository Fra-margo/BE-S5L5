using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Esercizio.Models;

namespace Esercizio.Controllers
{
    public class ReportController : Controller
    {
        private string connString = "Server=DESKTOP-VK00VHM\\SQLEXPRESS; Initial Catalog=Esercizio be-s2l5; Integrated Security=true; TrustServerCertificate=True";
        public IActionResult VerbaliPerTrasgressore()
        {
            List<VerbaliPerTrasgressore> totali = new List<VerbaliPerTrasgressore>();

                using (var connection = new SqlConnection(connString))
                {
                    string query = @"SELECT a.IDanagrafica, a.Cognome, a.Nome, COUNT(v.IDverbale) AS TotaleVerbali
                             FROM ANAGRAFICA a
                             LEFT JOIN VERBALE v ON a.IDanagrafica = v.IDanagrafica
                             GROUP BY a.IDanagrafica, a.Cognome, a.Nome";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var totale = new VerbaliPerTrasgressore
                            {
                                IDanagrafica = (int)reader["IDanagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                TotaleVerbali = (int)reader["TotaleVerbali"]
                            };
                            totali.Add(totale);
                        }
                    }
                }
            return View(totali);
        } 
        
        public IActionResult PuntiPerTrasgressore()
        {
            List<PuntiPerTrasgressore> totali = new List<PuntiPerTrasgressore>();

            using (var connection = new SqlConnection(connString))
            {
                string query = @"SELECT a.IDanagrafica, a.Cognome, a.Nome, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati
                             FROM ANAGRAFICA a
                             LEFT JOIN VERBALE v ON a.IDanagrafica = v.IDanagrafica
                             GROUP BY a.IDanagrafica, a.Cognome, a.Nome";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var totale = new PuntiPerTrasgressore
                        {
                            IDanagrafica = (int)reader["IDanagrafica"],
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            TotalePuntiDecurtati = (int)reader["TotalePuntiDecurtati"]
                        };
                        totali.Add(totale);
                    }
                }
            }

            return View(totali);
        }
        public ActionResult ViolazioneOltre10Punti()
        {
            List<ViolazioneOltre10Punti> violazioni = new List<ViolazioneOltre10Punti>();

            using (var connection = new SqlConnection(connString))
            {
                string query = @"SELECT v.Importo, a.Cognome, a.Nome, v.DataViolazione, v.DecurtamentoPunti
                             FROM VERBALE v
                             INNER JOIN ANAGRAFICA a ON v.IDanagrafica = a.IDanagrafica
                             WHERE v.DecurtamentoPunti > 10";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var violazione = new ViolazioneOltre10Punti
                        {
                            DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            DataViolazione = Convert.ToDateTime(reader["DataViolazione"]),
                            Importo = Convert.ToDecimal(reader["Importo"])

                        };
                        violazioni.Add(violazione);
                    }
                }
            }

            return View(violazioni);
        }
        public ActionResult ViolazioniSuperiori400()
        {
            List<ViolazioniSuperiori400> violazioni = new List<ViolazioniSuperiori400>();

            using (var connection = new SqlConnection(connString))
            {
                string query = @"SELECT v.Importo, a.Cognome, a.Nome, v.DataViolazione, v.DecurtamentoPunti
                             FROM VERBALE v
                             INNER JOIN ANAGRAFICA a ON v.IDanagrafica = a.IDanagrafica
                             WHERE v.Importo > 400";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var violazione = new ViolazioniSuperiori400
                        {
                            Importo = Convert.ToDecimal(reader["Importo"]),
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            DataViolazione = Convert.ToDateTime(reader["DataViolazione"]),
                            DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"])
                        };
                        violazioni.Add(violazione);
                    }
                }
            }

            return View(violazioni);
        }
    }
}
