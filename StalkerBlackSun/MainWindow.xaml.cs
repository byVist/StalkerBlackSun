using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace StalkerBlackSun
{
    public partial class MainWindow : Window
    {
        private int step = 0;
        private string route = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            GameScreen.Visibility = Visibility.Visible;
            step = 0;
            UpdateStep();
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            string choice = btn.Content.ToString();

            if (choice == "В МЕНЮ") { Restart(); return; }

            txtLog.Text += " | " + choice;
            step++;
            UpdateStep(choice);
        }

        private void UpdateStep(string lastChoice = "")
        {
            btnLeft.Visibility = Visibility.Visible;
            btnRight.Visibility = Visibility.Visible;
            btnNext.Visibility = Visibility.Collapsed;

            //
            string imgName = "image_0.png";

            switch (step)
            {
                case 0:
                    txtScene.Text = "Вы на подступах к Припяти. Легендарное 'Черное Солнце' где-то в центре. Как пойдете?";
                    SetButtons("Через центр города", "В обход через окраины");
                    imgName = "image_0.png";
                    break;

                case 1:
                    route = lastChoice;
                    if (route == "Через центр города")
                    {
                        txtScene.Text = "Центральные улицы завалены остовами машин. Впереди пост 'Монолита'.";
                        SetButtons("Скрытно обойти", "Атаковать в лоб");
                        imgName = "image_1.png";
                    }
                    else
                    {
                        txtScene.Text = "На окраинах тихо, но жутко. Вдруг из кустов выпрыгивает Кровосос!";
                        SetButtons("Дать бой мутанту", "Попытаться убежать");
                        imgName = "image_8.png";
                    }
                    break;

                case 2:
                    if (route == "Через центр города")
                    {
                        if (lastChoice == "Атаковать в лоб")
                        {
                            txtScene.Text = "Монолитовцев слишком много. Очередь обрывает вашу жизнь. Зона поглотила вас.";
                            ShowNextOnly("В МЕНЮ");
                            imgName = "image_2.png";
                        }
                        else
                        {
                            txtScene.Text = "Вы пробрались дворами. Радиация зашкаливает, нужно принять антирад.";
                            ShowNextOnly("Использовать антирад");
                            imgName = "image_3.png";
                        }
                    }
                    else
                    {
                        if (lastChoice == "Попытаться убежать")
                        {
                            txtScene.Text = "Кровосос настигает вас. Острые когти вонзаются в спину. Конец пути.";
                            ShowNextOnly("В МЕНЮ");
                            imgName = "image_9.png";
                        }
                        else
                        {
                            txtScene.Text = "Тяжелый бой! Вы победили, но истекаете кровью. Срочно нужна аптечка.";
                            ShowNextOnly("Использовать аптечку");
                            imgName = "image_10.png";
                        }
                    }
                    break;

                case 3:
                    txtScene.Text = "Вы добрались до старого кинотеатра. В центре зала парит ОН — артефакт 'Черное Солнце'.";
                    ShowNextOnly("Приблизиться к алтарю");
                    imgName = "image_4.png";
                    break;

                case 4:
                    txtScene.Text = "Артефакт шепчет вам. Его мощь может изменить мир. Что выберете?";
                    SetButtons("Забрать себе", "Уничтожить артефакт");
                    imgName = "image_5.png";
                    break;

                case 5:
                    if (lastChoice == "Забрать себе")
                    {
                        txtScene.Text = "Вы стали новым лидером Монолита, рабом артефакта.";
                        imgName = "image_6.png";
                    }
                    else
                    {
                        txtScene.Text = "Вспышка очистила город от скверны. Вы спасли Зону.";
                        imgName = "image_7.png";
                    }
                    ShowNextOnly("В МЕНЮ");
                    break;
            }


            ChangeImage(imgName);
        }


        private void ChangeImage(string fileName)
        {
            try
            {
              
                string path = $"pack://application:,,,/img/{fileName}";
                imgScene.Source = new BitmapImage(new Uri(path));
            }
            catch
            {
                // Если картинка не подгрузится, ошибки не будет
            }
        }

        private void SetButtons(string left, string right)
        {
            btnLeft.Content = left; btnRight.Content = right;
        }

        private void ShowNextOnly(string nextText)
        {
            btnLeft.Visibility = Visibility.Collapsed;
            btnRight.Visibility = Visibility.Collapsed;
            btnNext.Visibility = Visibility.Visible;
            btnNext.Content = nextText;
        }

        private void Restart()
        {
            step = 0; txtLog.Text = "— Хроника похода —";
            GameScreen.Visibility = Visibility.Collapsed;
            MainMenu.Visibility = Visibility.Visible;
        }
    }
}