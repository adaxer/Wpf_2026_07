using System.Net;
using System.Windows;
using System.Windows.Threading;

namespace Multithreading;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();

        Btn_Reset_Click(null, null);

        InitializeTimer();
    }

    private void Btn_Reset_Click(object sender, RoutedEventArgs e)
    {
        //Reset aller Beispiele
        Tbl_LongTaskWithFreeze.Text = "Not Started";
        Tbl_LongTaskWithDispatcher.Text = "Not Started";
        Tbl_LongTaskWithAsync.Text = "Not Started";
        Tbl_LongTasksMulti.Text = "Not Started";

        Tbl_ShortTask.Text = "Not Started";
        Counter = 0;

        Spl_PriorityBinding.DataContext = new AsyncDataSource();

        timerCount = 0;
        Tbl_Timer.Text = "Not Started";
    }

    #region TPL-Beispiele
    //Beispiele zur Verwendung der C#-TaskParallelLibrary


    //Beispiel für schnelle Standartaktion (läuft im UI-Thread)
    public int Counter { get; set; } = 0;
    private void Btn_ShortTask_Click(object sender, RoutedEventArgs e)
    {
        Counter += 1;
        Tbl_ShortTask.Text = Counter.ToString();
    }

    //Beispiel für langsame Standartaktion (läuft im UI-Thread und pausiert diesen -> UI friert ein)
    private void Btn_LongTaskWithFreeze_Click(object sender, RoutedEventArgs e)
    {
        Btn_LongTaskWithFreeze.IsEnabled = false;

        //Simulation einer länger andauernden Aktion (z.B. Serverabfrage, etc)
        Thread.Sleep(2000);
        Tbl_LongTaskWithFreeze.Text = "Long Task Completed";

        Btn_LongTaskWithFreeze.IsEnabled = true;
    }

    //Beispiel für die Verwendung der TPL unter Zuhilfenahme des WPF-Dispatchers
    private void Btn_LongTaskWithDispatcher_Click(object sender, RoutedEventArgs e)
    {
        Btn_LongTaskWithDispatcher.IsEnabled = false;

        //Starten eines seperaten Tasks zur Ausführung der länger andauernden Aktion
        //  -> friert die UI nicht ein
        //  -> ABER: der neue Task darf nicht auf UI-Elemente zugreifen, da dies nur der UI-Thread darf
        var task = Task.Run(() =>
        {
            //Simulation der länger andauernden Aktion
            Thread.Sleep(2000);
            //Ergebniss, dass in der UI angezeigt werden soll
            return "Long Task Completed";
        });

        //Damit der Task doch auf UI-Elemente zugreifen kann, muss der Dispatcher
        //(Element, mit freiem Zugriff auf UI-Thread) verwendet werden.
        task.ContinueWith(t =>
        {
            Dispatcher.Invoke(() =>
            {
                Btn_LongTaskWithDispatcher.IsEnabled = true;
                Tbl_LongTaskWithDispatcher.Text = task.IsFaulted ? task.Exception.InnerException.Message : task.Result;
            });
        });
    }

    //Alternativ zum Dispatcher kann eine Async-Methode verwendet werden. Diese pausiert bei Await-Befehlen, ohne
    //den UI-Thread anzuhalten.
    private async void Btn_LongTaskWithAsync_Click(object sender, RoutedEventArgs e)
    {
        Btn_LongTaskWithAsync.IsEnabled = false;
        string taskResult = "";
        try
        {
            taskResult = await Task.Run(() =>
            {
                Thread.Sleep(2000);
                return "Long Task Completed";
            });
        }
        catch (Exception ex)
        {
            taskResult = ex.Message;
        }
        Btn_LongTaskWithAsync.IsEnabled = true;
        Tbl_LongTaskWithAsync.Text = taskResult;
    }

    //Bsp für das Erwarten mehrerer Tasks
    private async void Btn_LongTasksMulti_Click(object sender, RoutedEventArgs e)
    {
        Btn_LongTasksMulti.IsEnabled = false;

        var task1 = Task.Run(() =>
        {
            Thread.Sleep(2000);
            return "Long Task 1 Completed";
        });
        var task2 = Task.Run(() =>
        {
            Thread.Sleep(3000);
            return "Long Task 2 Completed";
        });
        var task3 = Task.Run(() =>
        {
            Thread.Sleep(4000);
            return "Long Task 3 Completed";
        });

        var results = await Task.WhenAll(task1, task2, task3);

        Tbl_LongTasksMulti.Text = $"{results[0]} - {results[1]} - {results[2]}";
        Btn_LongTasksMulti.IsEnabled = true;
    }

    #endregion

    #region Längeres Dispather-Beispiel

    private void Btn_AskForWeatherForecast_Click(object sender, RoutedEventArgs e)
    {
        //Initialisierung
        Btn_AskForWeatherForecast.IsEnabled = false;
        Btn_AskForWeatherForecast.Content = "Contacting Server";
        Lbl_WeatherForecast.Content = "";
        Pgb_WaitForWeatherForecast.IsIndeterminate = true;

        //Starte Abfrage
        Task.Run(FetchWeatherFromServer);

    }

    Random rand = new Random();

    private async void FetchWeatherFromServer()
    {
        // Simuliere Verzögerung beim Netzwerkzugriff
        Thread.Sleep(2000);

        string wc, temp;

        using (WebClient client = new WebClient())
        {
            string json = await client.DownloadStringTaskAsync(new Uri("https://api.open-meteo.com/v1/forecast?latitude=52.5244&longitude=13.4105&daily=weathercode,temperature_2m_max&timezone=Europe%2FBerlin&forecast_days=1"));
            string[] parts = json.Split(',');

            wc = parts[parts.Length - 2];

            temp = parts[parts.Length - 1];
        }

        wc = wc.Substring(wc.IndexOf("[") + 1);
        wc = wc.Substring(0, wc.IndexOf("]"));
        int WC = int.Parse(wc);

        temp = temp.Substring(temp.IndexOf("[") + 1);
        temp = temp.Substring(0, temp.IndexOf("]")).Replace('.', ',');
        double TEMP = double.Parse(temp);

        String weather = CheckWeatherCode(WC);

        // Simulation der Wetterabfrage (hier zufällig)
        //if (rand.Next(0, 2) == 0)
        //{
        //    weather = "rainy";
        //}
        //else
        //{
        //    weather = "sunny";
        //}


        // Schedule the update function in the UI thread.
        await this.Dispatcher.BeginInvoke
            (
                System.Windows.Threading.DispatcherPriority.Normal,
                new Action<string, double>(UpdateUserInterface),
                weather,
                TEMP
            );
    }

    private string CheckWeatherCode(int code) => code switch
    {
        0 => "clear sky",
        1 => "Mainly clear",
        2 => "partly cloudy",
        3 => "overcast",
        45 => "Fog",
        48 => "depositing rime fog",
        51 => "light drizzle",
        53 => "moderate drizzle",
        55 => "dense drizzle",
        56 => "light freezing drizzle",
        57 => "dense freezing drizzle",
        61 => "light rain",
        63 => "moderate rain",
        65 => "dense rain",
        66 => "light freezing rain",
        67 => "heavy freezing rain",
        71 => "slight snow fall",
        73 => "moderate snow fall",
        75 => "heavy snow fall",
        77 => "Snow grains",
        80 => "slight rain showers",
        81 => "moderate rain showers",
        82 => "violent rain showers",
        85 => "slight snow showers",
        86 => "heavy snow showers",
        95 => "Thunderstorm: Slight or moderate",
        96 => "Thunderstorm with slight hail",
        99 => "Thunderstorm with heavy hail"
    };

    private void UpdateUserInterface(String weather, double temp)
    {
        //Show Forecast
        Lbl_WeatherForecast.Content = $"{weather} ({temp}°C)";

        //Stop progressbar animation
        Pgb_WaitForWeatherForecast.IsIndeterminate = false;

        //Update UI text
        Btn_AskForWeatherForecast.IsEnabled = true;
        Btn_AskForWeatherForecast.Content = "Fetch Forecast";
    }

    #endregion

    #region Dispatcher-Timer

    DispatcherTimer timer;
    int timerCount;

    private void InitializeTimer()
    {
        timerCount = 0;

        timer = new DispatcherTimer();
        
        timer.Tick += (s, e) =>
        {
            timerCount++;
            Tbl_Timer.Text = timerCount.ToString();
        };
        timer.Interval = TimeSpan.FromSeconds(1);
    }

    private void Btn_StartTimer_Click(object sender, RoutedEventArgs e) => timer.Start();

    private void Btn_StopTimer_Click(object sender, RoutedEventArgs e) => timer.Stop();
    #endregion
}
