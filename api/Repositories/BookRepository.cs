using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace CompositionApi {
    public class BookRepository { 

        private Api api;
        public BookRepository(Api _api) {
            api = _api;
        }

    //############################################################

        public List<BookModel>GetAllBooks() {
            string sql = $"SELECT * FROM books";
            MySqlConnection connection = api.mySqlClient.Connect();
            connection.Open();
            MySqlCommand cmd = api.mySqlClient.Query(sql, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<BookModel>books = new List<BookModel>{};
            while (rdr.Read()) {
                books.Add(new BookModel {
                    Id = rdr.GetInt32(0),
                    Title = rdr.GetString(1),
                    Author = rdr.GetString(2),
                    Genre = rdr.GetString(3)
                });
            }
            rdr.Close();
            connection.Close();
            return books;
        }

    //############################################################

        public BookModel GetBookById(int id) {
            string sql = $"SELECT * FROM books WHERE Id = {id}";
            MySqlConnection connection = api.mySqlClient.Connect();
            connection.Open();
            MySqlCommand cmd = api.mySqlClient.Query(sql, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            BookModel book = new BookModel();
            while (rdr.Read()) {
                book = new BookModel {
                    Id = rdr.GetInt32(0),
                    Title = rdr.GetString(1),
                    Author = rdr.GetString(2),
                    Genre = rdr.GetString(3)
                };
            }
            rdr.Close();
            connection.Close();
            return book;
        }

    //############################################################

        public void AddBook(BookModel book) {
            if(book != null) {
                string sql = $"INSERT INTO books (Title, Author, Genre) VALUES ('{book.Title}', '{book.Author}', '{book.Genre}')";
                MySqlConnection connection = api.mySqlClient.Connect();
                connection.Open();
                MySqlCommand cmd = api.mySqlClient.Query(sql, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

    //############################################################

    }

}