using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using Common;
using FirebirdSql.Data.FirebirdClient;
using InventUI.Models;
using InventUI.NHibernate;
using NHibernate;

namespace InventUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ThemeManager.ApplicationThemeName = "Seven";

            var sb = new FbConnectionStringBuilder()
            {
                Database = @"localhost:invent",
                UserID = "SYSDBA",
                Password = "masterkey",
                Charset = "UTF8",
                Dialect = 3,
                Pooling = true
            };
            Singleton<HbManager>.Instance.SetConnectionString(sb.ToString());
            //Singleton<HbManager>.Instance.NewSession.FlushMode = FlushMode.Never;
            //Singleton<ReferencesCollection>.Instance.Reload();

            //Singleton<ReferencesCollection>.Instance.SpPlacesCollection.Add(new SpPlaces() {Name = "test",CustomKey = -1});
            //Singleton<ReferencesCollection>.Instance.SavePlaces();
        }
    }
}
