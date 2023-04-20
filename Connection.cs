using Microsoft.Data.SqlClient;

namespace Assessment4_Solution
{
    public class Connection
    {
        public SqlConnection conn;

        public Connection()
        {
            conn = new SqlConnection("Data Source = 5CG95011NR; Initial Catalog = editor; Integrated Security = True; Encrypt = False");
        }


    }
}
