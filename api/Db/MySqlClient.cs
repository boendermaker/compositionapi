using MySqlConnector;

namespace CompositionApi {
    public partial class MySqlClient {
        public MySqlConnection CreateDbConnection(string server, string database, string user, string pass) {
            string connectionString = $"server={server};uid={user};pwd={pass};database={database}";
            return new MySqlConnection(connectionString);
        }

    //############################################################

        public MySqlConnection Connect() {
            return CreateDbConnection("mariadb-service", "apidemo", "root", "isahedron");
        }

    //############################################################

        public MySqlCommand Query(string sql, MySqlConnection connection) {
            if(connection != null) {
                return new MySqlCommand(sql, connection);
            }else {
                return null;
            }
        }

    //############################################################

    }

}