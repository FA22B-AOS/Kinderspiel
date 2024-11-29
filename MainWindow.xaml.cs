using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Configuration;


namespace Kinderspiel
{
    public partial class MainWindow : Window
    {

        private System.Timers.Timer timer;
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "0";

            this.Loaded += MainWindow_Loaded;

            timer = new System.Timers.Timer(2000);
            timer.Elapsed += MyTimer_Elapsed;

            timer.AutoReset = true;

            timer.Start();
        }

        private void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Reset_Buttons();
            SetLucas();

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AddRandomButtons();
        }

        private void AddRandomButtons()
        {
            int numberOfButtons = 10;

            Random random = new Random();

            double canvasWidth = ButtonCanvas.ActualWidth;
            double canvasHeight = ButtonCanvas.ActualHeight;

            if (canvasWidth <= 0 || canvasHeight <= 0)
            {
                MessageBox.Show("Canvas-Größe ist nicht korrekt initialisiert.");
                return;
            }

            Style noHoverStyle = new Style(typeof(Button));
            noHoverStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Colors.Blue)));
            noHoverStyle.Setters.Add(new Setter(Button.BorderBrushProperty, new SolidColorBrush(Colors.Blue)));

            Trigger hoverTrigger = new Trigger
            {
                Property = Button.IsMouseOverProperty,
                Value = true
            };
            hoverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Colors.Blue)));
            noHoverStyle.Triggers.Add(hoverTrigger);

            for (int i = 1; i <= numberOfButtons; i++)
            {
                Button btn = new Button();
                btn.Content = "";
                btn.Width = 40;
                btn.Height = 40;
                btn.Background = new SolidColorBrush(Colors.Blue);
                btn.Style = noHoverStyle;

                double left = random.Next(0, (int)(canvasWidth - btn.Width));
                double top = random.Next(0, (int)(canvasHeight - btn.Height));

                Canvas.SetLeft(btn, left);
                Canvas.SetTop(btn, top);

                btn.Click += Button_Click;

                ButtonCanvas.Children.Add(btn);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                int titleNum = int.Parse(this.Title);
                titleNum++;
                this.Title = titleNum.ToString();
                timer.Interval -= 25;
                Reset_Buttons();
               
            }
        }

        private void Reset_Buttons()
        {
            ButtonCanvas.Dispatcher.Invoke(() =>
            {
                Style noHoverStyle = new Style(typeof(Button));
                noHoverStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Colors.Blue)));
                noHoverStyle.Setters.Add(new Setter(Button.BorderBrushProperty, new SolidColorBrush(Colors.Blue)));

                Trigger hoverTrigger = new Trigger
                {
                    Property = Button.IsMouseOverProperty,
                    Value = true
                };
                hoverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Colors.Blue)));
                noHoverStyle.Triggers.Add(hoverTrigger);
                for (int i = 0; i < ButtonCanvas.Children?.Count; i++)
                {
                    if (ButtonCanvas.Children[i] is Button button)
                    {
                        button.Background = new SolidColorBrush(Colors.Blue);

                        button.Click -= Button_Click;
                        button.Style = noHoverStyle;
                    }
                }
            });
        }

        private void SetLucas()
        {
            
            ButtonCanvas.Dispatcher.Invoke(() =>
            {
                Random rnd = new Random();
                int count = ButtonCanvas.Children.Count;
                int num = rnd.Next(0, count);

                Style noHoverStyle = new Style(typeof(Button));
                noHoverStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Colors.Red)));
                noHoverStyle.Setters.Add(new Setter(Button.BorderBrushProperty, new SolidColorBrush(Colors.Red)));

                Trigger hoverTrigger = new Trigger
                {
                    Property = Button.IsMouseOverProperty,
                    Value = true
                };
                hoverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Colors.Red)));
                noHoverStyle.Triggers.Add(hoverTrigger);

                if (ButtonCanvas.Children[num] is Button button)
                {
                    button.Background = new SolidColorBrush(Colors.Red);
                    button.Click += Button_Click;
                    button.Style = noHoverStyle;
                }
            });
        }

    }
}