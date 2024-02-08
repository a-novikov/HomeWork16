using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HomeWork16.Connections
{
    public class MSSQLConnection
    {
        public MSSQLConnection()
        {
            SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "UsersDB",
                IntegratedSecurity = true,
                Pooling = true
            };


        }
    }
}
