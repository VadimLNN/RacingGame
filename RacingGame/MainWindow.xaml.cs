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
        // создание переменной Таймер
        System.Windows.Threading.DispatcherTimer Timer;
        public MainWindow()
        {
            InitializeComponent();

            //инициализация переменной таймер
            Timer = new System.Windows.Threading.DispatcherTimer();

            //назначение обработчика события Тик
            Timer.Tick += new EventHandler(Tick);

            //установка интервала между тиками
            //TimeSpan – переменная для хранения времени в формате часы/минуты/секунды
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 30);

            Timer.Start();
        }

        int background_position = 0;
        private void Tick(object sender, EventArgs e)
        {
            background_position += 10;

            Canvas.SetTop( bg_img_down, background_position);
            Canvas.SetTop( bg_img_up, -638+background_position);

            if (background_position >= 638)
                background_position = 0;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int speed_car = 16;
            if ((e.Key == Key.Left || e.Key == Key.A) && Canvas.GetLeft(car) > -10)
                Canvas.SetLeft(car, Canvas.GetLeft(car) - speed_car);
            else if ((e.Key == Key.Right || e.Key == Key.D) && Canvas.GetLeft(car) < 708)
                Canvas.SetLeft(car, Canvas.GetLeft(car) + speed_car);

            if (e.Key == Key.Escape)
            {
                Timer.Stop();
                this.Close();
            }
        }
    }
}
