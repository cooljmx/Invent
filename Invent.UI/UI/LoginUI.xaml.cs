using System.Windows;
using Invent.Models.Login;

namespace InventUI.UI
{
    /// <summary>
    /// Interaction logic for LoginUI.xaml
    /// </summary>
    public partial class LoginUi
    {
        private readonly ModelLogin model = ModelLogin.Load();
        public ModelLogin Model { get { return model; } }
        public LoginUi()
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
