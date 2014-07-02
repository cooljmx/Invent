using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;
using Common;
using FirebirdSql.Data.FirebirdClient;
using Invent.Entities;
using InventUI.NHibernate;
using NHibernate.Dialect;

namespace InventUI.Models
{
    [Serializable]
    [XmlRootAttribute("InventLogin", Namespace = "", IsNullable = false)]
    public class ModelLogin : VirtualNotifyPropertyChanged
    {
        private const string LoginFileName = "InventUI.LoginUI.xml";
        private string database;
        private string userName;
        private string password;

        public ModelLogin()
        {
            IsLogin = false;
        }

        public string Database { get { return database; } set { database = value; NotifyPropertyChanged("Database"); } }
        public string UserName { get { return userName; } set { userName = value; NotifyPropertyChanged("UserName"); } }
        public string Password { get { return password; } set { password = value; NotifyPropertyChanged("Password"); } }
        
        [XmlIgnore]
        public bool IsLogin { get; set; }

        public void Save()
        {
            using (var stream = new FileStream(LoginFileName, FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof (ModelLogin));
                serializer.Serialize(stream, this);
            }
        }

        public static ModelLogin Load()
        {
            if (!File.Exists(LoginFileName)) return new ModelLogin();
            using (var stream = new FileStream(LoginFileName, FileMode.Open, FileAccess.Read))
            {
                var serializer = new XmlSerializer(typeof (ModelLogin));
                return (ModelLogin) serializer.Deserialize(stream);
            }
        }

        public void Login()
        {
            try
            {
                var sb = new FbConnectionStringBuilder()
                {
                    Database = Database,
                    UserID = UserName,
                    Password = Password,
                    Charset = "UTF8",
                    Dialect = 3,
                    Pooling = true
                };
                Singleton<HbManager>.Instance.SetConnectionString(sb.ToString());
                IsLogin = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException == null ? ex.Message : ex.InnerException.Message, "InventUI",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
