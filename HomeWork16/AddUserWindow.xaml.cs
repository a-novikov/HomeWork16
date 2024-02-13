using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace HomeWork16
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        public AddUserWindow(DataRow row) : this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };
            okBtn.Click += delegate
            {
                row["surname"] = txtSurname.Text;
                row["name"] = txtName.Text;
                row["patronymic"] = txtPatronymic.Text;
                row["email"] = txtEmail.Text;
                row["phone"] = txtPhone.Text;
                this.DialogResult = !false;
            };

        }
    }
}
