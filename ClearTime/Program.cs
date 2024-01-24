using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

internal class Program
{
    static TimeOnly timeStart = new TimeOnly(10,10);
    static TimeOnly timeEnd = new TimeOnly(21,20);
    static string name = "mspaint";

    private static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            Parse(args);
        }
        while (timeStart < TimeOnly.FromDateTime(DateTime.Now)
            && timeEnd > TimeOnly.FromDateTime(DateTime.Now))
        {
            RemoveProcess(name);
        }
    }

    private static void Parse(string[] args)
    {
        for(var i = 0; i>args.Length; i++)
        {
            switch (args[i])
            {
                case "-ts":
                    {
                        TimeOnly.TryParse(args[++i], out timeStart);
                        break;
                    }
                case "-te":
                    {
                        TimeOnly.TryParse(args[++i], out timeEnd);
                        break;
                    }
                case "-n":
                    {
                        name = args[++i].ToLower();
                        break;
                    }
            }
        }
    }

    public static void RemoveProcess(string process)
    {
        Process[] processes = Process.GetProcesses();
        foreach (Process proc in processes)
        {
            string ProsessName = proc.ProcessName;          
            if (ProsessName.Equals(process))
            {
                proc.Kill();
                proc.WaitForExit();
                proc.Dispose();
            }
        }

    }
}