using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel;
using Windows.Services.Store;

namespace App.Service
{
    public class VersionService
    {
        public async Task VersionCheck()
        {
            StoreContext context = StoreContext.GetDefault();
            IReadOnlyList<StorePackageUpdate> updates = await GetUpdates(context);

            if (updates.Count == 0)
            {
                MessageBox.Show("アプリ更新なし");
                return;
            }

            string versions = "";
            foreach (StorePackageUpdate update in updates)
            {
                string version = GetPackageVersion(update);

                versions = versions + version + ";";
            }

            bool hasMandetory = updates.Any(u => u.Mandatory);
            if (hasMandetory)
            {
                MessageBox.Show("必須更新あり　件数：" + updates.Count.ToString() + ";　更新バージョン：" + versions);
                return;
            }
            else
            {
                MessageBox.Show("必須更新なし　件数：" + updates.Count.ToString() + ";　更新バージョン：" + versions);
                return;
            }
        }

        private async Task<IReadOnlyList<StorePackageUpdate>> GetUpdates(StoreContext context)
        {
            try
            {
                return await context.GetAppAndOptionalStorePackageUpdatesAsync();
            }
            catch
            {
                MessageBox.Show("アップデート失敗");
                return new List<StorePackageUpdate>().AsReadOnly();
            }
        }

        private string GetPackageVersion(StorePackageUpdate update)
        {
            PackageVersion version = update.Package.Id.Version;

            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
