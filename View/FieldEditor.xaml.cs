using AnimeTool.Binding;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeTool.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FieldEditor : Page
    {
        public FieldEditor()
        {
            InitializeComponent();
        }

        private void StatsChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as MainVM).UpdateStats(sender, e);
        }
    }
}
