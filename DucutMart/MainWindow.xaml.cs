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

namespace DucutMart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InventoryDBDataContext _db = new InventoryDBDataContext();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Text))
            {
                MessageBox.Show($"Logging In Username: {username.Text} \nPassword: {password.Text}");

                var login = from u in _db.Users 
                            join r in _db.Roles
                            on u.RoleID equals r.RoleID
                            where 
                            u.Username == username.Text && u.PasswordHash == password.Text
                            select new { User = u, Role = r };

                if (login.Count() == 1)
                {
                    foreach (var user in login)
                    {
                        UserDetails.dUsername = user.User.Username;
                        UserDetails.dFullname = user.User.FullName;
                        UserDetails.dRole = user.Role.RoleName;
                    }

                    MessageBox.Show($"Login Successful! Welcome {UserDetails.dFullname}!");

                    if(UserDetails.dRole == "Store Manager")
                    {
                        Dashboard dashboard = new Dashboard();
                        UserDetails.dFullname = UserDetails.dFullname;
                        dashboard.Show();
                        this.Close();
                    }
                    else if (UserDetails.dRole == "Cashier")
                    {
                        POS pos = new POS();
                        UserDetails.dFullname = UserDetails.dFullname;
                        pos.Show();
                        this.Close();
                    }
                    else if (UserDetails.dRole == "Inventory Staff")
                    {
                        Inventory inventory = new Inventory();
                        UserDetails.dFullname = UserDetails.dFullname;
                        inventory.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username/password");
                }
            }
            else
            {
                MessageBox.Show("Username or Password cannot be empty");
            }

        }

        /// <summary>
        /// Hashing method, hashed the password
        /// </summary>
        /// <param name="password"></param>
        private void hashing(string password)
        {

        }



    }
}
