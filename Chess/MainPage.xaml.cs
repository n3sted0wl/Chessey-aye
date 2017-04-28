using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Chess
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /*************************************************************/
        /*                        Initializer                        */
        /*************************************************************/
        #region Initializer
        public MainPage()
        {
            this.InitializeComponent();
        }
        #endregion

        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Structures
        #endregion

        #region Enumerations
        #endregion

        #region Objects
        #endregion

        #region Collections
        #endregion

        #region Delegates
        #endregion
        #endregion

        /*************************************************************/
        /*                       Functionality                       */
        /*************************************************************/
        #region Methods
        #region Constructors
        #endregion

        #region Overrides
        #endregion

        #region Accessors
        #endregion

        #region Mutators
        #endregion

        #region Event Handlers
        private void rct_space_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            #region Data
            Rectangle       square      = (sender as Rectangle);
            SolidColorBrush borderColor = new SolidColorBrush(Colors.Orange);
            #endregion

            #region Logic
            // Set programmatically through the object
            square.Stroke          = borderColor;
            square.StrokeThickness = 3;
            #endregion

            return;
        }

        private void rct_space_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            #region Data
            Rectangle square = (sender as Rectangle);
            #endregion

            #region Logic
            // Set programmatically through the object
            square.StrokeThickness = 0;
            #endregion

            return;
        }
        #endregion

        #region Other methods
        #endregion
        #endregion
    }
}
