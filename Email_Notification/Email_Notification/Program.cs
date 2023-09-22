using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email_Notification
{

    class Program
    {

        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        static SqlConnection connection = new SqlConnection(connectionString);
        static void Main(string[] args)
        {
            connection.Open();

            var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("alucard.alucard21@gmail.com", "YouaremyBitch21"),
                EnableSsl = true,
            };

            string query_id = $"SELECT [direction],[date] FROM [directions]";
            SqlCommand command = new SqlCommand(query_id, connection);
            DateTime tomorrow = DateTime.Today.AddDays(1);
            SqlDataReader reader = command.ExecuteReader();
            List<ListOfDirections> s = new List<ListOfDirections>();
            ListOfDirections q;
            
            while (reader.Read())
            {
                q = new ListOfDirections();
                q.direction_name = reader[0].ToString();
                try 
                {
                    q.date = (DateTime)reader[1];
                }
                catch
                {

                }
                
                s.Add(q);
            }
            reader.Close();

            string direction;
            string email;
            foreach(var i in s)
            {
                query_id = $"SELECT [email],[Prefer_direction] FROM [user] ORDER BY [user_id]";
                command = new SqlCommand(query_id, connection);
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    direction = reader[1].ToString();
                    email = reader[0].ToString();

                    if (i.direction_name == direction && i.date == tomorrow)
                    {
                        if (tomorrow.Month > 9 && tomorrow.Day > 9)
                        {
                            smtpClient.Send("alucard.alucard21@gmail.com", $"{email}", "Нагадування", $"Завтра,{tomorrow.Day}.{tomorrow.Month}.{tomorrow.Year} розпочнуться презентації проектів за напрямком \"{reader[1].ToString()}\"");
                        }
                        else if (tomorrow.Month <= 9 && tomorrow.Day > 9)
                        {
                            smtpClient.Send("alucard.alucard21@gmail.com", $"{email}", "Нагадування", $"Завтра,{tomorrow.Day}.0{tomorrow.Month}.{tomorrow.Year} розпочнуться презентації проектів за напрямком \"{reader[1].ToString()}\"");
                        }
                        else if (tomorrow.Month <= 9 && tomorrow.Day <= 9)
                        {
                            smtpClient.Send("alucard.alucard21@gmail.com", $"{email}", "Нагадування", $"Завтра,0{tomorrow.Day}.0{tomorrow.Month}.{tomorrow.Year} розпочнуться презентації проектів за напрямком \"{reader[1].ToString()}\"");
                        }
                        else if (tomorrow.Month > 9 && tomorrow.Day <= 9)
                        {
                            smtpClient.Send("alucard.alucard21@gmail.com", $"{email}", "Нагадування", $"Завтра,0{tomorrow.Day}.{tomorrow.Month}.{tomorrow.Year} розпочнуться презентації проектів за напрямком \"{reader[1].ToString()}\"");
                        }
                    }

                }
                reader.Close();
            }

            connection.Close();
        }
    }
}
