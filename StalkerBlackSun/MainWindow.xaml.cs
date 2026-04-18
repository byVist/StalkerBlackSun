using System;
using System.Windows;
using System.Windows.Controls;

namespace StalkerBlackSun
{
    public partial class MainWindow : Window
    {
        private int step = 0;
        private string route = ""; //

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
            string choice = (sender as Button).Content.ToString();
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

            switch (step)
            {
                case 0:
                    txtScene.Text = "Вы на подступах к Припяти. Легендарное 'Черное Солнце' где-то в центре. Как пойдете?";
                    SetButtons("Через центр города", "В обход через окраины");
                    break;

                case 1:
                    route = lastChoice;
                    if (route == "Через центр города")
                    {
                        txtScene.Text = "Центральные улицы завалены остовами машин. Впереди пост 'Монолита'. Они фанатично охраняют проход.";
                        SetButtons("Скрытно обойти", "Атаковать в лоб");
                    }
                    else
                    {
                        txtScene.Text = "На окраинах тихо, но жутко. Вдруг из кустов выпрыгивает Кровосос! Он уже почуял вашу кровь.";
                        SetButtons("Дать бой мутанту", "Попытаться убежать");
                    }
                    break;

                case 2:
                    if (route == "Через центр города")
                    {
                        if (lastChoice == "Атаковать в лоб")
                        {
                            txtScene.Text = "Монолитовцев слишком много. Очередь из Гаусс-пушки обрывает вашу жизнь. Зона поглотила вас.";
                            ShowNextOnly("В МЕНЮ");
                        }
                        else
                        {
                            txtScene.Text = "Вы проползли через дренажные трубы. Уровень радиации зашкаливает, нужно принять антирад.";
                            ShowNextOnly("Использовать антирад");
                        }
                    }
                    else
                    {
                        if (lastChoice == "Попытаться убежать")
                        {
                            txtScene.Text = "Кровосос настигает вас в два прыжка. Острые когти вонзаются в спину. Конец пути.";
                            ShowNextOnly("В МЕНЮ");
                        }
                        else
                        {
                            txtScene.Text = "Тяжелый бой! Вы зарезали тварь, но сами истекаете кровью. Нужно срочно подлататься.";
                            ShowNextOnly("Использовать аптечку");
                        }
                    }
                    break;

                case 3:
                    txtScene.Text = "Вы добрались до старого кинотеатра. В центре зала, в столбе черного пламени, парит ОН - артефакт 'Черное Солнце'.";
                    ShowNextOnly("Приблизиться к алтарю");
                    break;

                case 4:
                    txtScene.Text = "Артефакт шепчет вам. Его мощь может изменить историю или уничтожить её. Выбор за вами.";
                    SetButtons("Забрать себе", "Уничтожить артефакт");
                    break;

                case 5:
                    if (lastChoice == "Забрать себе")
                    {
                        txtScene.Text = "Вы коснулись артефакта. Ваше тело наполнилось энергией, но разум померк. Вы стали новым лидером Монолита, рабом Черного Солнца.";
                    }
                    else
                    {
                        txtScene.Text = "Вы бросили артефакт в аномальный разлом. Ослепительная вспышка очистила Припять от скверны. Вы спасли Зону ценой великого сокровища.";
                    }
                    ShowNextOnly("В МЕНЮ");
                    break;
            }
        }

        private void SetButtons(string left, string right)
        {
            btnLeft.Content = left;
            btnRight.Content = right;
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
            step = 0;
            txtLog.Text = "— Хроники пути —";
            GameScreen.Visibility = Visibility.Collapsed;
            MainMenu.Visibility = Visibility.Visible;
        }
    }
}