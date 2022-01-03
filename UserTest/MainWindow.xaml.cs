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

namespace UserTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class UserRepository : Window
    {
        public UserRepository()
        {
            InitializeComponent();           
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-M3POGDB\SQLEXPRESS;Initial Catalog=UserTest;Integrated Security=True");

        public void GetOrderedUsers()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from [User]", conn);
                DataTable dt = new DataTable();
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                conn.Close();
                userdatagrid.ItemsSource = dt.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }
        public void AddUser()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [User] VALUES (@ID,@Name,@Sername)", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", id_txt.Text);
                cmd.Parameters.AddWithValue("@Name", name_txt.Text);
                cmd.Parameters.AddWithValue("@Sername", sername_txt.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();                
                MessageBox.Show("Пользователь успешно добавлен!");
                ClearData();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            
        }
        public void GetUser()
        {

        }
        public void ClearData()
        {
            id_txt.Clear();
            name_txt.Clear();
            sername_txt.Clear();
        }

        private void add_user_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
        }

        private void display_list_Click(object sender, RoutedEventArgs e)
        {
            GetOrderedUsers();
        }

        private void clear_user_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }
    }    
}
