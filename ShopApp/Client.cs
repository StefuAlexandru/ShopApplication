using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;




public class Utilizator
{
    public int ID { get; set; }
    private string passwordHash;
    private string username;
    private byte[] salt;

    public string PasswordHash
    {
        get { return passwordHash; }
        set { passwordHash = value; }
    }
 
    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public Utilizator()
    {
        salt = GenerateSalt();
    }

    public Utilizator(string username, string password) : this()
    {
        this.Username = username;
        this.PasswordHash = HashPassword(password, salt);
    }

    private byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }
    }

    private string HashPassword(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
    }

    public bool VerifyPassword(string password)
    {
        byte[] hashBytes = Convert.FromBase64String(passwordHash);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
        }
        return true;
    }
}
public class Client : Utilizator
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Adresa Adresa { get; set; }
    public Client() : base()
    {
      
    }
    public Client(string firstName, string lastName, string email, string username, string password)
         : base(username, password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Adresa = new Adresa();
    }
   
    public bool IsValidPhoneNumber()
    {
        return Regex.IsMatch(PhoneNumber, @"^(\+4|)?(07[0-8]\d{7})$");
    }
    public void SaveToDatabase()
    {
        using (SqlConnection connection = new SqlConnection("Data Source=WIN10\\MYDATABASE;Initial Catalog=Users;User ID=root;Password=toor;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        {
            connection.Open();
            string query = "INSERT INTO Clients (FirstName, LastName, Email, Username, PasswordHash) VALUES (@FirstName, @LastName, @Email, @Username, @PasswordHash)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                command.ExecuteNonQuery();
            }
        }
    }
    public static Client LoadFromDatabase(string username)
    {
        Client client = null;

        using (SqlConnection connection = new SqlConnection("Data Source=WIN10\\MYDATABASE;Initial Catalog=Users;User ID=root;Password=toor;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        {
            connection.Open();
            string query = "SELECT * FROM Clients WHERE Username = @Username";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                            Adresa = new Adresa
                            {
                                Tara = reader.IsDBNull(reader.GetOrdinal("Tara")) ? null : reader.GetString(reader.GetOrdinal("Tara")),
                                Judet = reader.IsDBNull(reader.GetOrdinal("Judet")) ? null : reader.GetString(reader.GetOrdinal("Judet")),
                                Oras = reader.IsDBNull(reader.GetOrdinal("Oras")) ? null : reader.GetString(reader.GetOrdinal("Oras")),
                                Strada = reader.IsDBNull(reader.GetOrdinal("Strada")) ? null : reader.GetString(reader.GetOrdinal("Strada")),
                                CodPostal = reader.IsDBNull(reader.GetOrdinal("CodPostal")) ? null : reader.GetString(reader.GetOrdinal("CodPostal")),
                                Bloc = reader.IsDBNull(reader.GetOrdinal("Bloc")) ? null : reader.GetString(reader.GetOrdinal("Bloc")),
                                Numar = reader.IsDBNull(reader.GetOrdinal("Numar")) ? 0 : int.Parse(reader.GetString(reader.GetOrdinal("Numar"))),
                                Scara = reader.IsDBNull(reader.GetOrdinal("Scara")) ? null : reader.GetString(reader.GetOrdinal("Scara")),
                                NumarDeTelefon = reader.IsDBNull(reader.GetOrdinal("NumarDeTelefon")) ? null : reader.GetString(reader.GetOrdinal("NumarDeTelefon"))
                            }
                        };
                    }
                }
            }
        }

        return client;
    }
    public static Client LoadFromDatabase(int id)
    {
        Client client = null;

        using (SqlConnection connection = new SqlConnection("Data Source=WIN10\\MYDATABASE;Initial Catalog=Users;User ID=root;Password=toor;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        {
            connection.Open();
            string query = "SELECT * FROM Clients WHERE ID = @ID";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                            Adresa = new Adresa
                            {
                                Tara = reader.IsDBNull(reader.GetOrdinal("Tara")) ? null : reader.GetString(reader.GetOrdinal("Tara")),
                                Judet = reader.IsDBNull(reader.GetOrdinal("Judet")) ? null : reader.GetString(reader.GetOrdinal("Judet")),
                                Oras = reader.IsDBNull(reader.GetOrdinal("Oras")) ? null : reader.GetString(reader.GetOrdinal("Oras")),
                                Strada = reader.IsDBNull(reader.GetOrdinal("Strada")) ? null : reader.GetString(reader.GetOrdinal("Strada")),
                                CodPostal = reader.IsDBNull(reader.GetOrdinal("CodPostal")) ? null : reader.GetString(reader.GetOrdinal("CodPostal")),
                                Bloc = reader.IsDBNull(reader.GetOrdinal("Bloc")) ? null : reader.GetString(reader.GetOrdinal("Bloc")),
                                Numar = reader.IsDBNull(reader.GetOrdinal("Numar")) ? 0 : int.Parse(reader.GetString(reader.GetOrdinal("Numar"))),
                                Scara = reader.IsDBNull(reader.GetOrdinal("Scara")) ? null : reader.GetString(reader.GetOrdinal("Scara")),
                                NumarDeTelefon = reader.IsDBNull(reader.GetOrdinal("NumarDeTelefon")) ? null : reader.GetString(reader.GetOrdinal("NumarDeTelefon"))
                            }
                        };
                    }
                }
            }
        }

        return client;
    }

    public static bool UsernameExists(string username)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=WIN10\\MYDATABASE;Initial Catalog=Users;User ID=root;Password=toor;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM Clients WHERE Username = @Username";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
    public override string ToString()
    {
        return $"ID: {ID} \nNume Complet: {FirstName} {LastName}\nEmail: {Email}\nAdresa: {Adresa}";
    }

}
