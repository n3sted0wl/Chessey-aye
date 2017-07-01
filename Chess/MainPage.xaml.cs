using Chess.Classes;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

/* TODOs and Development
 * --------------------------------------------------------------------
 * TODO: Connect the front-end Rectangles and Images to the corresponding 
 *       objects in the Gameboard class. Need to call the 
 *       GameBoard.Initialize() function first.
 *       
 * TODO: Event handlers for the Squares and their corresponding images
 *       When either one is entered, call the same function which will 
 *       check what kind of object the sender is (just for casting
 *       purposes). Then get the corresponding Suqre item and modify
 *       it's Rectangle control.
 */

namespace Chess
{
    public sealed partial class MainPage : Page
    {
        /*************************************************************/
        /*                        Initializer                        */
        /*************************************************************/
        #region Initializer
        public MainPage()
        {
            this.InitializeComponent();

            // Build backend data objects and stuff
            GameBoard.initialize();

            // Connect front-end controls to each square and piece object
            this.mapControls();
            
            // Initialize game board object delegates
            this.initializeDelegates();

            // Set up the pieces
            GameBoard.resetPieces(); // Initializes AllPieces collection
        }
        #endregion

        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        private const string IMAGE_CONTROL_PREFIX = "img_piece_";
        private const string RECT_CONTROL_PREFIX  = "rct_space_";
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
        #region Initializers
        public void initializeDelegates()
        {
            foreach (Square square in GameBoard.AllSquares)
            {
                square.UpdatePictureDelegate += updateImageSource;
            }

            GameBoard.updateImageSource += updateImageSource;
            return;
        }

        private void mapControls()
        {
            #region Logic
            foreach (Square square in GameBoard.AllSquares)
            {
                square.Tile = (Rectangle) 
                    this.FindName($"{RECT_CONTROL_PREFIX}{square.Position.ToString()}");
                square.PieceImage = (Image)
                    this.FindName($"{IMAGE_CONTROL_PREFIX}{square.Position.ToString()}");
            }

            GameBoard.turnIndicator  = tbl_turnIndicator;
            GameBoard.messageConsole = tbl_MessageConsole;

            GameBoard.lv_takenWhitePieces = lv_takenWhite;
            GameBoard.lv_takenBlackPieces = lv_takenBlack;

            GameBoard.whiteScoreBoard = tbl_whiteScore;
            GameBoard.blackScoreBoard = tbl_blackScore;
            #endregion

            return;
        }
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
            Square selectedSquare;
            #endregion

            #region Logic
            // Check what kind of object is sending and cast appropriately
            // Get the associated Square control and highlight it's Rectangle Property
            if (sender is Image)
            {
                selectedSquare = 
                    GameBoard.AllSquares.Find(squ => squ.PieceImage == (sender as Image));
            }
            else if (sender is Rectangle)
            {
                selectedSquare = 
                    GameBoard.AllSquares.Find(squ => squ.Tile == (sender as Rectangle));
            }
            else
            {
                throw new InvalidCastException("Event sender is of wrong type");
            }

            if (selectedSquare == null)
                throw new ArgumentNullException("Could not find an associated Square object");

            if (!selectedSquare.IsSelected && !selectedSquare.IsAttackable &&
                selectedSquare.IsOccupied &&
                selectedSquare.OccupyingPiece.PieceColor == GameBoard.CurrentTurn)
                selectedSquare.highlightSquare(new SolidColorBrush(Colors.Orange));
            #endregion

            return;
        }

        private void rct_space_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            #region Data
            Square selectedSquare;
            #endregion

            #region Logic
            // Check what kind of object is sending and cast appropriately
            // Get the associated square control and Remove it's rectangle's highlighting
            if (sender is Image)
            {
                selectedSquare =
                    GameBoard.AllSquares.Find(squ => squ.PieceImage == (sender as Image));
            }
            else if (sender is Rectangle)
            {
                selectedSquare =
                    GameBoard.AllSquares.Find(squ => squ.Tile == (sender as Rectangle));
            }
            else
            {
                throw new InvalidCastException("Event sender is of wrong type");
            }

            if (selectedSquare == null)
                throw new ArgumentNullException("Could not find an associated Square object");

            if (!selectedSquare.IsSelected && !selectedSquare.IsAttackable)
                selectedSquare.removeHighlighting();
            #endregion

            return;
        }

        private void rct_space_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            #region Data
            Square clickedSquare;
            #endregion

            #region Logic
            // Check what kind of object is sending and cast appropriately
            // Get the associated square control and Remove it's rectangle's highlighting
            if (sender is Image)
            { clickedSquare = GameBoard.AllSquares.Find(squ => squ.PieceImage == (sender as Image)); }
            else if (sender is Rectangle)
            { clickedSquare = GameBoard.AllSquares.Find(squ => squ.Tile == (sender as Rectangle)); }
            else
            { throw new InvalidCastException("Event sender is of wrong type"); }
            if (clickedSquare == null)
            { throw new ArgumentNullException("Could not find an associated Square object"); }

            /*********************************************************
             * LOGIC FOR HANDLING MOVES
             * ******************************************************/
            // The appropriate Square object has been found
            // At this point, clickedSquare is the one being selected
            // GameBoard.SelectedSquare is the one previously selected
            #region Piece Moving logic
            try
            {
                if (clickedSquare.IsOccupied)
                {
                    if (GameBoard.SelectedSquare == null)
                    { // Nothing has been selected yet; select the clicked square
                        if (clickedSquare.OccupyingPiece.PieceColor == GameBoard.CurrentTurn)
                            GameBoard.SelectedSquare = clickedSquare;
                    }
                    else // A square has already been selected
                    { // Check if it is the clicked one
                        if (GameBoard.SelectedSquare == clickedSquare)
                        { // The user clicked the selected square; deselect it
                            GameBoard.clearSelectedSquares();
                        }
                        else // A square was selected & another one was clicked
                        { // Check if it's attackable or should be activated
                            if (clickedSquare.IsAttackable)
                            { // attack the square
                                GameBoard.attack(
                                    GameBoard.SelectedSquare,
                                    clickedSquare);
                                GameBoard.clearSelectedSquares();
                            }
                            else // Square cannot be attacked
                            { // Select the clicked square
                                GameBoard.SelectedSquare = clickedSquare;
                            }
                        }
                    }
                }
                else
                {
                    if (clickedSquare.IsAttackable)
                    {
                        GameBoard.attack(GameBoard.SelectedSquare, clickedSquare);
                    }
                    GameBoard.clearSelectedSquares();
                }
                // If the clicked piece is unoccupied, do nothing
            }
            catch (KingCapturedException)
            {
                string winningTeam = string.Empty;

                if (GameBoard.CurrentTurn == Piece.Color.White)
                    winningTeam = "White";
                else if (GameBoard.CurrentTurn == Piece.Color.Black)
                    winningTeam = "Black";

                tbl_turnIndicator.Text = winningTeam + " has won";

                GameBoard.clearAttackableSquares();
                GameBoard.clearSelectedSquares();
                GameBoard.CurrentTurn = null;
            }

            #endregion
            #endregion

            return;
        }

        private void bt_resetBoard_Click(object sender, RoutedEventArgs e)
        {
            GameBoard.resetPieces();
            GameBoard.clearSelectedSquares();
            GameBoard.clearMessageConsole();
            GameBoard.clearTakenPieces();
            GameBoard.resetScores();
        }

        private void Background_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            GameBoard.clearSelectedSquares();
        }
        #endregion

        #region Other methods
        public void updateImageSource(Image image, string path) =>
            image.Source = new BitmapImage(new Uri(this.BaseUri, path));
        #endregion
        #endregion
    }
}
