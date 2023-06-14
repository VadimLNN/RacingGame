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

namespace RacingGame
{
    public partial class MainWindow : Window
    {
        bool lose = false;

        // создание переменной Таймер
        System.Windows.Threading.DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            // инициализация переменной таймер
            timer = new System.Windows.Threading.DispatcherTimer();

            // назначение обработчика события Тик
            timer.Tick += new EventHandler(Tick);

            // установка интервала таймера 
            timer.Interval = new TimeSpan(0, 0, 0, 0, 30);

            // запуск таймера 
            timer.Start();
        }

        // переменная для установки сдвига задних фонов и машин (для анимации)
        int background_speed = 10;
        int enemy1_car_speed = 8;
        int enemy2_car_speed = 7;
        private void Tick(object sender, EventArgs e)
        {
            // сдвиг нижнего и верхнего фона + движение врагов  
            Canvas.SetTop( bg_img_down, (Canvas.GetTop(bg_img_down) + background_speed));
            Canvas.SetTop( bg_img_up,   (Canvas.GetTop(bg_img_up)   + background_speed));

            Canvas.SetTop( car_enemy_1, (Canvas.GetTop(car_enemy_1) + enemy1_car_speed));
            Canvas.SetTop( HitBoxCarEnemy_1, (Canvas.GetTop(car_enemy_1) + enemy1_car_speed - 8));

            Canvas.SetTop( car_enemy_2, (Canvas.GetTop(car_enemy_2) + enemy2_car_speed));
            Canvas.SetTop( HitBoxCarEnemy_2, (Canvas.GetTop(car_enemy_2) + enemy2_car_speed - 8));

            // перезапуск анимации фона
            if (Canvas.GetTop(bg_img_up) >= 0)  
                Canvas.SetTop(bg_img_up, -640);

            if (Canvas.GetTop(bg_img_down) >= 640)
                Canvas.SetTop(bg_img_down, 0);

            // перезапуск движения врагов
            if (Canvas.GetTop(car_enemy_1) >= 640)
                RestarMovingEnemy(car_enemy_1, HitBoxCarEnemy_1, -340);

            if (Canvas.GetTop(car_enemy_2) >= 640)
                RestarMovingEnemy(car_enemy_2, HitBoxCarEnemy_2, -150);

            // проверка на пересечение хитбоксов
            if (DetectCollisions(HitBoxCarPlayer, HitBoxCarEnemy_1) != Rect.Empty ||
                DetectCollisions(HitBoxCarPlayer, HitBoxCarEnemy_2) != Rect.Empty)
            {
                timer.Stop();
                gamover.Visibility = Visibility.Visible;
                restart.Visibility = Visibility.Visible;
                lose = true;
            }

        }
        
        private void RestarMovingEnemy(Image car, Shape HitBox, int distantion)
        {
            Canvas.SetTop(car, distantion);
            Random rand = new Random();
            Canvas.SetLeft(car, rand.Next(110, 580));

            Canvas.SetTop(HitBox, (Canvas.GetTop(car) + enemy1_car_speed - 8));
            Canvas.SetLeft(HitBox, (Canvas.GetLeft(car) + 24));
        }
        private Rect DetectCollisions(FrameworkElement rect1, FrameworkElement rect2)
        {
            var r1 = new Rect(Canvas.GetLeft(rect1), Canvas.GetTop(rect1), rect1.ActualWidth, rect1.ActualHeight);
            var r2 = new Rect(Canvas.GetLeft(rect2), Canvas.GetTop(rect2), rect2.ActualWidth, rect2.ActualHeight);
            r1.Intersect(r2);
            return r1;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // выход
            if (e.Key == Key.Escape)
            {
                timer.Stop();
                this.Close();
            }

            if (lose) return;

            // скорость перемещения машинки в стороны 
            int speed_car = 16;

            // передвижение машинки, ограниченные границами дороги
            if ((e.Key == Key.Left || e.Key == Key.A) && Canvas.GetLeft(car_player) > 110)
            {
                Canvas.SetLeft(car_player, Canvas.GetLeft(car_player) - speed_car);
                Canvas.SetLeft(HitBoxCarPlayer, Canvas.GetLeft(car_player) - speed_car + 40);
            }
            else if ((e.Key == Key.Right || e.Key == Key.D) && Canvas.GetLeft(car_player) < 580)
            {
                Canvas.SetLeft(car_player, Canvas.GetLeft(car_player) + speed_car);
                Canvas.SetLeft(HitBoxCarPlayer, Canvas.GetLeft(car_player) - speed_car + 40);
            }

            
        }

        // перемещение окна мышкой 
        private void Cnv_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            RestarMovingEnemy(car_enemy_1, HitBoxCarEnemy_1, -340);
            RestarMovingEnemy(car_enemy_2, HitBoxCarEnemy_2, -150);
            gamover.Visibility = Visibility.Hidden;
            restart.Visibility = Visibility.Hidden;
            timer.Start();
            lose = false;
        }
    }
}
