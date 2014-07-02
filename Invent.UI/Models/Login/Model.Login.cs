using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Common;
using FirebirdSql.Data.FirebirdClient;
using Invent.Entities;
using InventUI.NHibernate;
using Rade.Tools;

namespace InventUI.Models.Login
{
    [Serializable]
    [XmlRoot("InventLogin", Namespace = "", IsNullable = false)]
    public class ModelLogin : VirtualNotifyPropertyChanged
    {
        private static readonly string LoginPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Invent";
        private static readonly string LoginFileName = LoginPath + "\\InventUI.LoginUI.xml";
        private string database;
        private string userName;
        private string password;
        private bool isPasswordSave;
        public ModelLogin()
        {
            IsLogin = false;
        }

        public string Database { get { return database; } set { database = value; NotifyPropertyChanged("Database"); } }
        public string UserName { get { return userName; } set { userName = value; NotifyPropertyChanged("UserName"); } }
        public string Password { get { return password; } set { password = value; NotifyPropertyChanged("DecryptedPassword"); } }

        public bool IsPasswordSave
        {
            get
            {
                return isPasswordSave;
            }
            set
            {
                isPasswordSave = value;
                NotifyPropertyChanged("IsPasswordSave");
            }
        }

        [XmlIgnore]
        public string DecryptedPassword
        {
            get
            {
                return StringTools.DecryptString(password);
            }
            set
            {
                password = StringTools.CryptString(value);
                NotifyPropertyChanged("DecryptedPassword");
            }
        }

        [XmlIgnore]
        public bool IsLogin { get; set; }

        public void Save()
        {
            if (!Directory.Exists(LoginPath)) Directory.CreateDirectory(LoginPath);
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
                var sb = new FbConnectionStringBuilder
                {
                    Database = Database,
                    UserID = UserName,
                    Password = DecryptedPassword,
                    Charset = "UTF8",
                    Dialect = 3,
                    Pooling = true
                };
                Singleton<HbManager>.Instance.SetConnectionString(sb.ToString());
                IsLogin = true;
                if (!IsPasswordSave)
                    password = null;
                Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException == null ? ex.Message : ex.InnerException.Message, "InventUI",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
