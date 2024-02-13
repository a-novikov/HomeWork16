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
        DataRowView oleDbRow;
        DataSet oleDbDs;
        DataTable oleDbDt;
        DataRowView OleDbRow;

        int currentId;
        string currentEmail;
        int currentCode;
        string currentProduct;

        string outMessage = string.Empty;
        string currentUserEmail;


        public MainWindow()
        {
            InitializeComponent();
            PrepearingMSSQL();
            PrepearingAccess();
        }

  
        private void PrepearingMSSQL()
        {
            try
            {
                #region Init

                var connectionStringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = @"(localdb)\MSSQLLocalDB",
                    InitialCatalog = "UsersDB",
                    IntegratedSecurity = true,
                    UserID = "your_user",
                    Password = "your_password",
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
                outMessage += $"{DateTime.Now.ToString()} Подключение к БД MSSQL успешно\n";
            }
            catch (Exception ex)
            {
                outMessage += $"{DateTime.Now.ToString()} Ошибка подключения к БД MSSQL. Ошибка: {ex.Message}\n";
            }
            ConsoleText.Text = outMessage; 
        }

        private void PrepearingAccess()
        {
            try
            {
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Environment.CurrentDirectory}\\Products.accdb";
                oleDbCon = new OleDbConnection(connectionString);
                oleDbCon.Open();

                string query = "SELECT * FROM ProductsTable";
                OleDbCommand command = new OleDbCommand(query, oleDbCon);
                OleDbDataReader reader = command.ExecuteReader();

                string sql = "SELECT * FROM ProductsTable";
                oleDbDa = new OleDbDataAdapter(sql, oleDbCon);
                oleDbDs = new DataSet();
                oleDbDa.Fill(oleDbDs, "ProductsTable");
                oleDbDt = oleDbDs.Tables["ProductsTable"];

                oleDbDa.InsertCommand = new OleDbCommand("INSERT INTO ProductsTable (email, code, product) VALUES (?, ?, ?)", oleDbCon);
                oleDbDa.UpdateCommand = new OleDbCommand("UPDATE ProductsTable SET email = ?, code = ?, product = ? WHERE id = ?", oleDbCon);
                oleDbDa.DeleteCommand = new OleDbCommand("DELETE FROM ProductsTable WHERE id = ?", oleDbCon);

                oleDbDa.InsertCommand.Parameters.Add("@email", OleDbType.VarChar, 40, "email");
                oleDbDa.InsertCommand.Parameters.Add("@code", OleDbType.Char, 5, "code");
                oleDbDa.InsertCommand.Parameters.Add("@product", OleDbType.VarChar, 40, "product");

                oleDbDa.UpdateCommand.Parameters.Add("@email", OleDbType.VarChar, 40, "email");
                oleDbDa.UpdateCommand.Parameters.Add("@code", OleDbType.Char, 5, "code");
                oleDbDa.UpdateCommand.Parameters.Add("@product", OleDbType.VarChar, 40, "product");
                oleDbDa.UpdateCommand.Parameters.Add("@oldid", OleDbType.Char, 5, "id").SourceVersion = DataRowVersion.Original;

                oleDbDa.DeleteCommand.Parameters.Add("@id", OleDbType.Char, 5, "id").SourceVersion = DataRowVersion.Original;

                oleDbDt = new DataTable();
                oleDbDa.Fill(oleDbDt);

                gridViewBuy.DataContext = oleDbDt.DefaultView;
                reader.Close();
                outMessage += $"{DateTime.Now.ToString()} Подключение к БД Access успешно\n";
            }
            catch (Exception ex)
            {
                outMessage += $"{DateTime.Now.ToString()} Ошибка подключения к БД Access. Ошибка: {ex.Message}\n";
            }
            ConsoleText.Text = outMessage;
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

        private void GVCurrentCellChangedBuy(object sender, EventArgs e)
        {
            OleDbRow = (DataRowView)gridViewBuy.SelectedItem;
            currentId = Convert.ToInt32(OleDbRow.Row["id"]);
            currentEmail = OleDbRow.Row["email"].ToString();
            currentCode = Convert.ToInt32(OleDbRow.Row["code"].ToString());
            currentProduct = OleDbRow.Row["product"].ToString();
            SetData();
        }

        private void GVCellEditEndingBuy(object sender, DataGridCellEditEndingEventArgs e)
        {           
            if (OleDbRow == null) return;
        }

        private void SetData()
        {
            for (int i = 0; i < oleDbDt.Rows.Count; i++)
            {
                if (oleDbDt.Rows[i].Field<int>("id") == currentId)
                {
                    oleDbDt.Rows[i].BeginEdit();
                    oleDbDt.Rows[i].SetField("email", currentEmail);
                    oleDbDt.Rows[i].SetField("code", currentCode);
                    oleDbDt.Rows[i].SetField("product", currentProduct);
                    oleDbDt.Rows[i].EndEdit();
                    break;
                }
            }
            oleDbDa.Update(oleDbDt);
        }

        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {
            DataRow r = dt.NewRow();
            AddUserWindow add = new AddUserWindow(r);
            add.ShowDialog();


            if (add.DialogResult.Value)
            {
                dt.Rows.Add(r);
                da.Update(dt);
            }
        }

        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem;
            row.Row.Delete();
            da.Update(dt);
        }

        private void BuyInfoClick(object sender, RoutedEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem;
            currentUserEmail = row.Row["email"].ToString();
            string outString = $"Покупки клиента: {currentUserEmail}\n Код товара | Наименование товара\n";

            for (int i = 0; i < oleDbDt.Rows.Count; i++)
            {
                if (oleDbDt.Rows[i]["email"].ToString() == currentUserEmail)
                {
                    outString += $"{oleDbDt.Rows[i][2]} | {oleDbDt.Rows[i][3]}\n";
                }
            }

            MessageBox.Show(outString);
            

        }

        private void MenuItemAddBuyClick(object sender, RoutedEventArgs e)
        {
            DataRow r = oleDbDt.NewRow();
            AddBuyInfoWindow add = new AddBuyInfoWindow(r);
            add.ShowDialog();


            if (add.DialogResult.Value)
            {
                oleDbDt.Rows.Add(r);
                oleDbDa.Update(oleDbDt);
            }
        }

        private void MenuItemDeleteBuyClick(object sender, RoutedEventArgs e)
        {
            oleDbRow = (DataRowView)gridViewBuy.SelectedItem;
            oleDbRow.Row.Delete();
            oleDbDa.Update(oleDbDt);
        }
    }
}
