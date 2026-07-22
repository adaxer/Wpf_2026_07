namespace Multithreading;

public class AsyncDataSource
{
    public AsyncDataSource(){}

    public string FastDP
    {
        get => "FAST";
    }

    public string SlowerDP
    {
        get
        {
            // This simulates a lengthy time before the
            // data being bound to is actualy available.
            Thread.Sleep(3000);
            return "SLOW";
        }
    }

    public string SlowestDP
    {
        get
        {
            // This simulates a lengthy time before the
            // data being bound to is actualy available.
            Thread.Sleep(5000);
            return "SLOWEST";
        }
    }
}
