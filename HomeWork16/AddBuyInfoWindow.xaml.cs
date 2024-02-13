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
    /// Логика взаимодействия для AddBuyInfoWindow.xaml
    /// </summary>
    public partial class AddBuyInfoWindow : Window
    {
        public AddBuyInfoWindow()
        {
            InitializeComponent();
        }

        public AddBuyInfoWindow(DataRow row) : this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };
            okBtn.Click += delegate
            {
                row["email"] = txtEmail.Text;
                row["code"] = txtCode.Text;
                row["product"] = txtProduct.Text;
                this.DialogResult = !false;
            };

        }
    }
}
