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
using System.Windows.Threading;

namespace ExamTask_Game

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer(); 

        List<Ellipse> removeThis = new List<Ellipse>();
        StartPage start = new StartPage();

        int spawnRate = 60; 
        int currentRate; 
        int lastScore = 0; 
        int health = 350; 
        int posX;
        int posY; 
        int score = 0;
        string name;

        double growthRate = 0.6; 
        
        Random rand = new Random(); 


        MediaPlayer playClickSound = new MediaPlayer();
        MediaPlayer playerPopSound = new MediaPlayer();


        Uri ClickedSound;
        Uri PoppedSound;

        Brush brush;

        public MainWindow()
        {
            InitializeComponent();

           

            gameTimer.Tick += GameLoop; 
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); 
            gameTimer.Start(); 

            currentRate = spawnRate;


            ClickedSound = new Uri("C://Users//Екатерина//source//repos//ExamTask_Game//ExamTask_Game//Sounds//clickedpop.mp3");
            PoppedSound = new Uri("C://Users//Екатерина//source//repos//ExamTask_Game//ExamTask_Game//Sounds//pop.mp3");

        }

        private void GameLoop(object sender, EventArgs e)
        {
            name = start.textBoxLogin.Text;
            txtScore.Content = "Score: " + score;
            txtName.Content = "Name: " + name;
            txtLastScore.Content = "Last Score: " + lastScore;
           
            currentRate -= 2;

            if (currentRate < 1)
            {
                currentRate = spawnRate;

                posX = rand.Next(15, 700);
                posY = rand.Next(50, 350);

                brush = new SolidColorBrush(Color.FromRgb((byte)rand.Next(1, 255), (byte)rand.Next(1, 255), (byte)rand.Next(1, 255)));


                Ellipse circle = new Ellipse
                {

                    Tag = "circle",
                    Height = 10,
                    Width = 10,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Fill = brush
                   
                };

                Canvas.SetLeft(circle, posX);
                Canvas.SetTop(circle, posY);
               
                MyCanvas.Children.Add(circle);
            }

           

            foreach (var x in MyCanvas.Children.OfType<Ellipse>())
            {
                x.Height += growthRate; 
                x.Width += growthRate; 
                x.RenderTransformOrigin = new Point(0.5, 0.5); 

                if (x.Width > 70)
                {
                    removeThis.Add(x);
                    health -= 15; 
                    playerPopSound.Open(PoppedSound);
                    playerPopSound.Play(); 

                }

            } 
            if (health > 1)
            {
               
                healthBar.Width = health;
            }
            else
            {
                GameOverFunction();
            }

            foreach (Ellipse i in removeThis)
            {
                
                MyCanvas.Children.Remove(i); 
            }

            if (score > 5)
            {
                spawnRate = 25;
            }
            if (score > 20)
            {
                spawnRate = 15;
                growthRate = 1.5;
            }

        }
        private void CanvasClicking(object sender, MouseButtonEventArgs e)
        {
           
            if (e.OriginalSource is Ellipse)
            {
                Ellipse circle = (Ellipse)e.OriginalSource;

                MyCanvas.Children.Remove(circle);

                score++;

                playClickSound.Open(ClickedSound);
                playClickSound.Play();
               
            }
        }

        private void GameOverFunction()
        {
           
            gameTimer.Stop(); 

            MessageBox.Show("Game Over" + Environment.NewLine + "You Scored: " + score + Environment.NewLine + "Click Ok to play again!", "Says: ");

            foreach (var y in MyCanvas.Children.OfType<Ellipse>())
            {
                removeThis.Add(y);
            }
            foreach (Ellipse i in removeThis)
            {
                MyCanvas.Children.Remove(i);
            }

          
            growthRate = .6;
            spawnRate = 60;
            lastScore = score;
            score = 0;
            currentRate = 5;
            health = 350;
            removeThis.Clear();
            gameTimer.Start();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AllPlayers all = new AllPlayers();
            all.Show();
            gameTimer.Stop();
            //Close();
        }
     
    }
}