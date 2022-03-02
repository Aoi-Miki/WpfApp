using App.Service;
using System;
using System.Reflection;
using System.Windows;
using Windows.ApplicationModel;

namespace WpfApp1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        VersionService versionService;

        public MainWindow()
        {
            this.versionService = new();

            InitializeComponent();
            try
            {
                AppVerLabel.Content = $"APP : {GetAppVersion()}";
                PkgVerLabel.Content = $"PKG： {GetPkgVersion()}";
            }
            catch
            {
                // Packageされていないと、GetPkgVersionにて例外が発生する。
                // 何もしない
            }
        }

        private string GetAppVersion()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }

        private string GetPkgVersion()
        {
            PackageVersion version = Package.Current.Id.Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await versionService.VersionCheck();
        }

        private async void LaunchStoreApp_Click(object sender, RoutedEventArgs e)
        {
            await LaunchService.Execute("ms-windows-store://pdp/?productid=9N1B1DNDQSB1");
        }

        private async void LaunchStoreBrw_Click(object sender, RoutedEventArgs e)
        {
            await LaunchService.Execute("https://www.microsoft.com/store/apps/9N1B1DNDQSB1");
        }
    }
}
