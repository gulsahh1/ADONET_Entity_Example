using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdoNetDemo
{
    class ProductDal
    {
        SqlConnection _connection = new SqlConnection(@"server = desktop-caso95i\sqlexpress; initial catalog=ETrade; integrated security= true");
        public List<Product> GetAll()
        {

            ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Products", _connection); // sql'le iletişim kurduk.

            SqlDataReader reader = sqlCommand.ExecuteReader(); // komutu ExecuteReader ile çalıştırdık.

            List<Product> products = new List<Product>();

            while (reader.Read()) // data' yı tek tek oku diyoruz. 
            {
                Product product = new Product // her okunan veriyi product' a aktardık. 
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    StockAmount = Convert.ToInt32(reader["StockAmount"])
                };
                products.Add(product); // aktarılan her veriyi listeye ekledik.

            }

            reader.Close();
            _connection.Close();
            return products; // listeyi döndük.

        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open(); // sql baglantısını kontrol sağlayarak açtık.

            }
        }

        //public DataTable GetAll2()
        //{
        //    

        //    if (_connection.State == ConnectionState.Closed)
        //    {
        //        _connection.Open(); // sql baglantısını kontrol sağlayarak açtık.

        //    }

        //    SqlCommand sqlCommand = new SqlCommand("Select * from Products", _connection); // sql'le iletişim kurduk.

        //    SqlDataReader reader = sqlCommand.ExecuteReader(); // komutu ExecuteReader ile çalıştırdık.

        //    DataTable dataTable = new DataTable(); // ürünleri listeleme işlemini yaptık. en sonunda bağlantıları kapattık ve returnle sonucu döndürdük.
        //    dataTable.Load(reader);
        //    reader.Close();
        //    _connection.Close();
        //    return dataTable;
        //}

        public void Add(Product product)
        {
            ConnectionControl();
            SqlCommand sqlCommand = new SqlCommand(
                "Insert into Products values(@name, @unitPrice, @stockAmount)", _connection);
            sqlCommand.Parameters.AddWithValue("@name", product.Name);
            sqlCommand.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            sqlCommand.ExecuteNonQuery();

            _connection.Close();

        }

        public void Update(Product product)
        {
            ConnectionControl();
            SqlCommand sqlCommand = new SqlCommand(
                "Update Products set Name = @name, UnitPrice = @unitPrice, StockAmount = @stockAmount where Id = @id", _connection);
            sqlCommand.Parameters.AddWithValue("@name", product.Name);
            sqlCommand.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            sqlCommand.Parameters.AddWithValue("@id", product.Id);
            sqlCommand.ExecuteNonQuery();

            _connection.Close();

        }

        public void Delete(int id)
        {
            ConnectionControl();
            SqlCommand sqlCommand = new SqlCommand(
               "Delete from Products where Id = @id", _connection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.ExecuteNonQuery();

            _connection.Close();

        }
    }
}
