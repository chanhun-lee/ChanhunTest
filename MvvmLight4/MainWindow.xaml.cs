using System.Windows;
using MvvmLight4.ViewModel;
using System.Runtime.InteropServices;
using System;
using System.Windows.Interop;

namespace MvvmLight4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern uint RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        public const uint HWND_BROADCAST = 0xffff;

        private uint message;
        private IntPtr handle;
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {///

            /////
            ///
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

            

            handle = new WindowInteropHelper(this).Handle;
            message = RegisterWindowMessage("User Message");
            ComponentDispatcher.ThreadFilterMessage += ComponentDispatcher_ThreadFilterMessage;
            MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location);

        }
        private void ComponentDispatcher_ThreadFilterMessage(ref MSG msg, ref bool handled)
        {
            if (msg.message == message && msg.wParam != handle)
            {
                MessageBox.Show("MFC Message : " + msg.lParam.ToString());
            }


        }

        private void cSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(textBlock1!=null)
            textBlock1.Text = e.NewValue.ToString();
            System.Console.WriteLine(e.NewValue.ToString());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            cSlider.Value -= 1;
            Console.WriteLine("asaa");
        }




        private void button_Click(object sender, RoutedEventArgs e)
        {
            PostMessage((IntPtr)HWND_BROADCAST, message, (uint)handle, 100);
        }
    }

}