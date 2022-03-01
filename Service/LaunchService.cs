using System;
using System.Threading.Tasks;
using Windows.System;

namespace App.Service
{
    public static class LaunchService
    {
        public static async Task Execute(string uriStr)
        {
            Uri uri = new(uriStr);
            await Launcher.LaunchUriAsync(uri);

        }
    }
}
