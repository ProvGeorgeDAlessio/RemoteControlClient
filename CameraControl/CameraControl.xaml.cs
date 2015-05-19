using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ViscaLibrary;


namespace CameraControl
{
    /// <summary>
    /// Interaction logic for ToolboxControl.xaml
    /// </summary>
    [ProvideToolboxControl("General", true)]
    public partial class CameraControl : UserControl
    {
        private DispatcherTimer mouseDownTimer = new DispatcherTimer();

        private CameraControlModel _controlModel;
        private ViscaService service;
        private byte[] CurrentCommand { get; set; }
        public CameraControl()
        {
            this.DataContext = new CameraControlModel();
            _controlModel = (CameraControlModel)DataContext;
            InitializeComponent();
            Application.Current.Exit += CurrentOnExit;
            mouseDownTimer.Tick += new EventHandler(mouseDownTimerTick);
            mouseDownTimer.Interval = TimeSpan.FromMilliseconds(50);
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                service = new ViscaService();
            }
        }

        private void CurrentOnExit(object sender, ExitEventArgs e)
        {
            service.ClosePort();
        }

        public static readonly DependencyProperty titleProperty =
            DependencyProperty.Register("titleText", typeof(string),
                typeof(CameraControl), new FrameworkPropertyMetadata(string.Empty));

        public string titleText
        {
            get { return GetValue(titleProperty).ToString(); }
            set { SetValue(titleProperty, value); }
        }
        private void up_up(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            mouseDownTimer.Stop();
        }
        private void up_down(object sender, RoutedEventArgs e)
        {
            //service = new ViscaService();
            //service.Left();
            CurrentCommand = service.SetPositionRelative(0, 2, 0, 24);
            service.Up(CurrentCommand);
            mouseDownTimer.Start();
            e.Handled = true;
        }
        private void down_up(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            mouseDownTimer.Stop();
        }
        private void down_down(object sender, RoutedEventArgs e)
        {
            //service = new ViscaService();
            //service.Left();
            CurrentCommand = service.SetPositionRelative(0, -2, 0, 24);
            service.Down(CurrentCommand);
            mouseDownTimer.Start();
            e.Handled = true;
        }
        //private void left_Click(object sender, RoutedEventArgs e)
        //{
        //   //ViscaService service = new ViscaService();
        //    //MessageBox.Show(service.GetProlificPort());
        //   service.Left();
        //}

        private void mouseDownTimerTick(object sender, EventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                service.Left(CurrentCommand);
            }

        }
        private void left_down(object sender, RoutedEventArgs e)
        {
            CurrentCommand = service.SetPositionRelative(10, 0, 24, 0);
            service.Left(CurrentCommand);
            mouseDownTimer.Start();
            e.Handled = true;
        }

        private void left_up(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            mouseDownTimer.Stop();
        }
        private void right_up(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            mouseDownTimer.Stop();
        }
        private void right_down(object sender, RoutedEventArgs e)
        {
            //service = new ViscaService();
            //service.Left();
            CurrentCommand = service.SetPositionRelative(-10, 0, 24, 0);
            service.Right(CurrentCommand);
            mouseDownTimer.Start();
            e.Handled = true;
        }

        //private void right_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "We are inside {0}.right_Click()", this.ToString()));
        //}
        private void home_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "We are inside {0}.home_Click()", this.ToString()));
        }

        private void zoomin_down(object sender, RoutedEventArgs e)
        {
            byte[] zoomin = new byte[] { 0x81, 0x01, 0x04, 0x07, 0x24, 0xFF };
            service.ZoomIn(zoomin);
            mouseDownTimer.Start();
            e.Handled = true;
        }

        private void zoomin_up(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            mouseDownTimer.Stop();
        }
        private void zoomout_down(object sender, RoutedEventArgs e)
        {
            byte[] zoomout = new byte[] { 0x81, 0x01, 0x04, 0x07, 0x34, 0xFF };
            service.ZoomOut(zoomout);
            mouseDownTimer.Start();
            e.Handled = true;
        }

        private void zoomout_up(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            mouseDownTimer.Stop();
        }
        private void on_Click(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            //MessageBox.Show(service.GetProlificPort());

            service.On();
        }
        private void off_Click(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            //MessageBox.Show(service.GetProlificPort());
            service.Off();
        }
        private void ClosePort_Click(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            //MessageBox.Show(service.GetProlificPort());
            service.ClosePort();
        }
        private void OpenPort_Click(object sender, RoutedEventArgs e)
        {
            //ViscaService service = new ViscaService();
            //MessageBox.Show(service.GetProlificPort());
            service.OpenPort();
        }
    }
}
