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
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace ExamTask_Game
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {

        ApplicationContext db = new ApplicationContext();
        
        public StartPage()
        {
            InitializeComponent();
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 450;
            animation.Duration = TimeSpan.FromSeconds(3);
            startButton.BeginAnimation(Button.WidthProperty, animation);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();
            string pass2 = passBox2.Password.Trim();
            string email = textBoxEmail.Text.Trim().ToLower();
            

            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Name is to short! Min 5 letters";
                textBoxLogin.Background = Brushes.Red;

            }
            if (pass.Length < 4)
            {
                passBox.ToolTip = "Name is to short! Min  letters";
                textBoxLogin.Background = Brushes.Red;
            }
            else if (pass != pass2)
            {
                passBox2.ToolTip = "Password is not the same";
                textBoxLogin.Background = Brushes.Red;
            }
            else if (email.Length<5 || !email.Contains("@") || !email.Contains("."))
            {
                textBoxLogin.ToolTip = "Wrong email!";
                textBoxLogin.Background = Brushes.Red;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
                passBox2.ToolTip = "";
                passBox2.Background = Brushes.Transparent;
                textBoxEmail.ToolTip = "";
                textBoxEmail.Background = Brushes.Transparent;

                MessageBox.Show("All is correct!");
                Player players = new Player(login, email, pass);
                db.Players.Add(players);

                db.SaveChanges();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow gameWindow = new MainWindow();
            
            gameWindow.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Players players = new Players();
           
            players.Show();
            Close();
        }
    }
}
