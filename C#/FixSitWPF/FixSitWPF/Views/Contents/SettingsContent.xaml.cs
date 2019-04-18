using System.Windows.Controls;

namespace FixSitWPF.Views.Contents
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// Interaction logic for SettingsContent.xaml
    /// </summary>
    public partial class SettingsContent : UserControl
    {
        public SettingsContent(Models.SettingsModel settings)
        {

            InitializeComponent();
            DataContext = settings;
        }
    }
       
}
