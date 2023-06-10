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
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            Timer.Start();
        }

        int background_position = 0;
        private void Tick(object sender, EventArgs e)
        {
            background_position += 10;

            Canvas.SetTop( bg_img, background_position);

            if (background_position >= 650)
                background_position = 0;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Timer.Stop();
                this.Close();
            }
        }
    }
}
