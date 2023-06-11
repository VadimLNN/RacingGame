﻿using System;
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
            Canvas.SetTop( car_enemy_2, (Canvas.GetTop(car_enemy_2) + enemy2_car_speed));

            // перезапуск анимации 
            if (Canvas.GetTop(bg_img_up) >= 0)  
                Canvas.SetTop(bg_img_up, -640);

            if (Canvas.GetTop(bg_img_down) >= 640)
                Canvas.SetTop(bg_img_down, 0);

            // перезапуск движения врагов
            if (Canvas.GetTop(car_enemy_1) >= 640)
            {
                Canvas.SetTop(car_enemy_1, -322);
                Random rand = new Random();
                Canvas.SetLeft(car_enemy_1, rand.Next(110, 580));
            }

            if (Canvas.GetTop(car_enemy_2) >= 640)
            {
                Canvas.SetTop(car_enemy_2, -322);
                Random rand = new Random();
                Canvas.SetLeft(car_enemy_2, rand.Next(110, 580));
            }

        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // скорость перемещения машинки в стороны 
            int speed_car = 16;

            // ограничения передвижения дорогой 
            if ((e.Key == Key.Left || e.Key == Key.A) && Canvas.GetLeft(car_player) > 110)
                Canvas.SetLeft(car_player, Canvas.GetLeft(car_player) - speed_car);
            else if ((e.Key == Key.Right || e.Key == Key.D) && Canvas.GetLeft(car_player) < 580)
                Canvas.SetLeft(car_player, Canvas.GetLeft(car_player) + speed_car);

            // выход
            if (e.Key == Key.Escape)
            {
                timer.Stop();
                this.Close();
            }
        }

        // перемещение окна мышкой 
        private void Cnv_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
