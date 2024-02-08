using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace HomeWork16
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataTable dt;
        DataRowView row;

        OleDbConnection oleDbCon;
        OleDbDataAdapter oleDbDa;
         


        public MainWindow()
        {
            //Information();
            InitializeComponent();
            PrepearingMSSQL();
            AddNewUser();
            PrepearingAccess();
        }

        private void PrepearingMSSQL()
        {
            #region Init

            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "UsersDB",
                IntegratedSecurity = true,
                
                Pooling = false
            };

            con = new SqlConnection(connectionStringBuilder.ConnectionString);
            dt = new DataTable();
            da = new SqlDataAdapter();

            #endregion

            #region select


            var sql = @"SELECT * FROM Users Order By Users.id";
            da.SelectCommand = new SqlCommand(sql, con);

            #endregion

            #region insert

            sql = @"INSERT INTO Users (surname,  name,  patronymic, phone, email) 
                                 VALUES (@surname,  @name,  @patronymic, @phone, @email); 
                     SET @id = @@IDENTITY;";

            da.InsertCommand = new SqlCommand(sql, con);

            da.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id").Direction = ParameterDirection.Output;
            da.InsertCommand.Parameters.Add("@surname", SqlDbType.NVarChar, 20, "surname");
            da.InsertCommand.Parameters.Add("@name", SqlDbType.NVarChar, 20, "name");
            da.InsertCommand.Parameters.Add("@patronymic", SqlDbType.NVarChar, 20, "patronymic");
            da.InsertCommand.Parameters.Add("@phone", SqlDbType.NVarChar, 20, "phone");
            da.InsertCommand.Parameters.Add("@email", SqlDbType.NVarChar, 20, "email");

            #endregion

            #region update

            sql = @"UPDATE Users SET 
                           surname = @surname,
                           name = @name, 
                           patronymic = @patronymic,
                           phone = @phone, 
                           email = @email 
                    WHERE id = @id";

            da.UpdateCommand = new SqlCommand(sql, con);
            da.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id").SourceVersion = DataRowVersion.Original;
            da.UpdateCommand.Parameters.Add("@surname", SqlDbType.NVarChar, 20, "surname");
            da.UpdateCommand.Parameters.Add("@name", SqlDbType.NVarChar, 20, "name");
            da.UpdateCommand.Parameters.Add("@patronymic", SqlDbType.NVarChar, 20, "patronymic");
            da.UpdateCommand.Parameters.Add("@phone", SqlDbType.NVarChar, 20, "phone");
            da.UpdateCommand.Parameters.Add("@email", SqlDbType.NVarChar, 20, "email");

            #endregion

            #region delete

            sql = "DELETE FROM Users WHERE id = @id";

            da.DeleteCommand = new SqlCommand(sql, con);
            da.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id");

            #endregion

            da.Fill(dt);

            gridView.DataContext = dt.DefaultView;



        }

        private void PrepearingAccess()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\swcat\\source\\repos\\HomeWork16\\HomeWork16\\bin\\Debug\\Products.accdb";
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM ProductsTable";
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader reader = command.ExecuteReader();


            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    gridViewBuy.DataContext = ds.Tables[0];
                }
            }

            //string outText = string.Empty;

            //while (reader.Read())
            //{
            //    outText += $"{reader["email"]} {reader["product"]}\n";
            //    // Обработка результатов запроса
            //}
            //MessageBox.Show(outText);

            reader.Close();

        }

        public void Information()
        {
            DataTable table = new OleDbEnumerator().GetElements();
            string inf = "";
            foreach (DataRow row in table.Rows)
                inf += row["SOURCES_NAME"] + "\n";

            MessageBox.Show(inf);
        }

        private void GVCurrentCellChanged(object sender, EventArgs e)
        {
            if (row == null) return;
            row.EndEdit();
            da.Update(dt);
        }

        private void GVCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem;
            row.BeginEdit();
        }

        private void AddNewUser()
        {
            DataRow r = dt.NewRow();

            r["surname"] = "qweqew";
            r["name"] = "sdfdsf";
            r["patronymic"] = "werwer";
            r["phone"] = "sdfdsf";
            r["email"] = "werwer";

            dt.Rows.Add(r);
            da.Update(dt);
        }
    }
}
