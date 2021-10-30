using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace KemberFrontend.View
{
    /// <summary>
    /// Interaction logic for GeneralWindowControl.
    /// </summary>
    public partial class GeneralWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralWindowControl"/> class.
        /// </summary>
        public GeneralWindowControl()
        {
            InitializeComponent();
            MainFrame.Content = new AutorisationPage(this);
        }

        
      
    }
}