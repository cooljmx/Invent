using System.Windows;
using InventUI.Models;

namespace InventUI.UI
{
    /// <summary>
    /// Interaction logic for LoginUI.xaml
    /// </summary>
    public partial class LoginUI
    {
        private readonly ModelLogin model = ModelLogin.Load();
        public ModelLogin Model { get { return model; } }
        public LoginUI()
        {
            InitializeComponent();
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            Model.Login();
            if (Model.IsLogin) Close();
        }
    }
}
