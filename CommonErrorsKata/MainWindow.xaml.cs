using System;
using System.IO;
using CommonErrors.Shared;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CommonErrorsKata
{
    public partial class MainWindow
    {
        private readonly AnswerQueue<TrueFalseAnswer> _answerQueue;
        private readonly string[] _files;
        private readonly SynchronizationContext _synchronizationContext;
        private int i = 100;
        private string _currentBaseName;
        private readonly string[] _possibleAnswers;

        public MainWindow()
        {
            InitializeComponent();

            _synchronizationContext = SynchronizationContext.Current;

            _files = Directory.GetFiles(Environment.CurrentDirectory + @"..\..\..\ErrorPics");
            _possibleAnswers = new[] { "Missing File", "null instance", "divide by zero" };
            listBox.ItemsSource = _possibleAnswers;
            _answerQueue = new AnswerQueue<TrueFalseAnswer>(15);

            Next();
            StartTimer();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            i = 100;
            var tokens = _currentBaseName.Split(' ');

            //TODO:  Figure out what is a valid answer.

            _answerQueue.Enqueue(new TrueFalseAnswer(true));
            Next();
        }

        private async void StartTimer()
        {
            await Task.Run(() =>
            {
                for (i = 100; i > 0; i--)
                {
                    UpdateProgress(i);
                    Thread.Sleep(50);
                }
                Message("Need to be quicker on your feet next time!  Try again...");
            });
        }

        private void Next()
        {
            if (_answerQueue.Count == 15 && _answerQueue.Grade >= 98)
            {
                MessageBox.Show("Congratulations you've defeated me!");
                Application.Current.Shutdown();
                return;
            }

            percentLabel.Content = _answerQueue.Grade + "%";
            var file = _files.GetRandom();
            _currentBaseName = Path.GetFileName(file);
            imageBox.Source = new BitmapImage(new Uri(file));
        }

        public void UpdateProgress(int value)
        {
            _synchronizationContext.Post(x =>
            {
                progress.Value = value;
            }, value);
        }

        public void Message(string value)
        {
            _synchronizationContext.Post(x =>
            {
                MessageBox.Show(value);
            }, value);
        }
    }
}
