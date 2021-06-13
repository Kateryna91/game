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
    /// Interaction logic for Players.xaml
    /// </summary>
    public partial class Players : Window
    {
        public Players()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();
           

            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Name is to short! Min 5 letters";
                textBoxLogin.Background = Brushes.Red;

            }
            if (pass.Length < 4)
            {
                passBox.ToolTip = "Name is to short! Min 4 letters";
                textBoxLogin.Background = Brushes.Red;
            }
            else
            {
                Player old_player = null;
                using(ApplicationContext db = new ApplicationContext())
                {
                    old_player = db.Players.Where(b=> b.Login == login && b.Pass == pass).FirstOrDefault();
                }
                if (old_player != null)
                {
                    MainWindow gameWindow = new MainWindow();
                    gameWindow.Owner = this;
                    gameWindow.Show();
                }
                else
                    MessageBox.Show("Player is not found");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StartPage start = new StartPage();
            start.Show();
            Hide();
        }
    }
}
