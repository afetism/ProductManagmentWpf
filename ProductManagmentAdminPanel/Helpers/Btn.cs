
using System.Windows.Controls;
using System.Windows;

namespace ProductManagmentAdminPanel.Helpers
{

    public class Btn : RadioButton
    {
        static Btn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Btn), new FrameworkPropertyMetadata(typeof(Btn)));
        }
    }

}

