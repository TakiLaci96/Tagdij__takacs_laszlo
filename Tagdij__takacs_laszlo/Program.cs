using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Tagdij__takacs_laszlo
{
    internal class Program
    {
        static List<Tagok> tagokList = new List<Tagok>();
        static MySqlConnection connection = null;
        static MySqlCommand command = null;
        static void Main(string[] args)
        {
            beolvasas();
            listazas();
            ujTagFelvetele();
            //tagTorlese(1014);

            /*
             * Itt mindig ki kommenteltem éppen azt a metódust, amire nincs szükségem. Szóval egyszer futtatom az "ujTagfelvetele" metódust 
             * és a következő futtatásnál pedig eltávolítom a "//"-t a "tagTorlese" metódus elől és ki kommentelem az "ujTagFelvelete" metódust,
             * így végre hajtja a törlést.
             */

            Console.WriteLine("\nProgram vége!");
            Console.ReadLine();
        }
        private static void beolvasas()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "tagdij";
            builder.CharacterSet = "utf8";
            connection = new MySqlConnection(builder.ConnectionString);
            command = connection.CreateCommand();
            try
            {
                connection.Open();
                command.CommandText = "SELECT * FROM `ugyfel`";
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Tagok uj = new Tagok(dr.GetInt32("azon"), dr.GetString("nev"), dr.GetInt32("szulev"), dr.GetInt32("irszam"), dr.GetString("orsz"));
                        tagokList.Add(uj);
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }

        }

        private static void listazas()
        {
            foreach (var tag in tagokList)
            {
                Console.WriteLine(tag);
            }
        }

        private static void ujTagFelvetele()
        {
            Tagok tag = new Tagok(1014, "Teszt Elek", 2000, 1111, "H");
            command.CommandText = "INSERT INTO `ugyfel` (`azon`, `nev`, `szulev`, `irszam`, `orsz`) VALUES (@azon, @nev, @szulev, @irszam, @orsz)";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@azon", tag.azon);
            command.Parameters.AddWithValue("@nev", tag.nev);
            command.Parameters.AddWithValue("@szulev", tag.szulev);
            command.Parameters.AddWithValue("@irszam", tag.irszam);
            command.Parameters.AddWithValue("@orsz", tag.orsz);
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }

        private static void tagTorlese(int azon)
        {
            command.CommandText = "DELETE FROM `ugyfel` WHERE `azon` = @azon";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@azon", azon);
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine($"Sikeresen törölte a(z) {azon} számú azonosítóval ellátott ügyfelet!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
    }
}
