using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows;

namespace FitnesClubWpf
{
    public class Visitor
    {
        public int id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public string bday => Birthday.ToString("dd.MM.yyyy");
        public string Phone { get; set; }
        public double Balance { get; set; }
        public bool Status { get; set; } = false;
    }
    class FitnesClubDB
    {
        public static SqlConnection connection 
        { 
            get
            {
                return new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FitnesClub;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False    ");
            }
        }
        public static List<Visitor> SelectVisitors()
        {
            var c = connection;
            try
            {
                
                c.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT v.Id,v.FirstName,v.LastName,v.BirthDay,v.Phone,v.Balance,v.[Status] FROM dbo.Visitors v", c);
                List<Visitor> visitors = new List<Visitor>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Visitor v = new Visitor();
                    v.id = reader.GetInt32(0);
                    v.FirstName = reader.GetString(1);
                    v.LastName = reader.GetString(2);
                    v.Birthday = reader.GetDateTime(3);
                    v.Phone = reader.GetString(4);
                    v.Balance = decimal.ToDouble(reader.GetDecimal(5));
                    v.Status = reader.GetBoolean(6);
                    visitors.Add(v);
                }
                reader.Close();
                c.Close();
                return visitors;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            c.Close();
            return new List<Visitor>();
        }
        public static void Insert(Visitor visitor)
        {
            var c = connection;
            c.Open();
            string str = @"INSERT INTO dbo.Visitors(FirstName, LastName,  BirthDay, Phone,Balance,[Status]) VALUES (@FirstName, @LastName,  @BirthDay, @Phone, @Balance, @Status)";
            SqlCommand cmd = new SqlCommand(str, c);
            cmd.Parameters.Add(new SqlParameter("@FirstName", visitor.FirstName));
            cmd.Parameters.Add(new SqlParameter("@LastName", visitor.LastName));
            cmd.Parameters.Add(new SqlParameter("@BirthDay", visitor.Birthday));
            cmd.Parameters.Add(new SqlParameter("@Phone", visitor.Phone));
            cmd.Parameters.Add(new SqlParameter("@Balance", visitor.Balance));
            cmd.Parameters.Add(new SqlParameter("@Status", visitor.Status));
            cmd.ExecuteNonQuery();
            c.Close();
        }
        public static void Update(Visitor visitor)
        {
            var c = connection;
            c.Open();
            string str = @"UPDATE dbo.Visitors SET FirstName=@FirstName,LastName=@LastName,BirthDay=@BirthDay,Phone=@Phone,Balance = @Balance,[Status]=@Status WHERE dbo.Visitors.Id=" + visitor.id.ToString();
            SqlCommand cmd = new SqlCommand(str, c);
            cmd.Parameters.Add(new SqlParameter("@FirstName", visitor.FirstName));
            cmd.Parameters.Add(new SqlParameter("@LastName", visitor.LastName));
            cmd.Parameters.Add(new SqlParameter("@BirthDay", visitor.Birthday));
            cmd.Parameters.Add(new SqlParameter("@Phone", visitor.Phone));
            cmd.Parameters.Add(new SqlParameter("@Balance", visitor.Balance));
            cmd.Parameters.Add(new SqlParameter("@Status", visitor.Status));
            cmd.ExecuteNonQuery();
            c.Close();
        }
        public static void Delete(int Id)
        {

            string str = @"DELETE from dbo.npuxog WHERE dbo.npuxog.VisitorId = @Id;DELETE from  dbo.SectionVisitors where dbo.SectionVisitors.VisitorId=@Id;DELETE from dbo.Visitors WHERE dbo.Visitors.Id = @Id";
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("@Id", Id));
            cmd.ExecuteNonQuery();
        }
        public static void Delete(Visitor visitor)
        {
            var c = connection;
            c.Open();
            string str = @"DELETE from dbo.npuxog WHERE dbo.npuxog.VisitorId = @Id;DELETE from  dbo.SectionVisitors where dbo.SectionVisitors.VisitorId=@Id;DELETE from dbo.Visitors WHERE dbo.Visitors.Id = @Id";
            SqlCommand cmd = new SqlCommand(str, c);
            cmd.Parameters.Add(new SqlParameter("@Id", visitor.id));
            cmd.ExecuteNonQuery();
            c.Close();
        }
    }
}