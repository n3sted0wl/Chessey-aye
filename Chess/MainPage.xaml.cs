﻿using Chess.Classes;
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

            // Initialize game board object delegates
            this.initializeDelegates();

            // Connect front-end controls to each square and piece object
            this.mapControls();
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

            if (!selectedSquare.IsSelected)
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

            if (!selectedSquare.IsSelected)
                selectedSquare.removeHighlighting();
            #endregion

            return;
        }

        private void rct_space_PointerReleased(object sender, PointerRoutedEventArgs e)
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

            // The appropriate Square object has been selected
            if (selectedSquare == GameBoard.SelectedSquare)
            {
                // Toggle off the selected square
                GameBoard.clearSelectedSquares();
            }
            else
            { // A different square was clicked
                // Check if that square was occupied by a friend or foe
                if (GameBoard.SelectedSquare != null &&
                    GameBoard.SelectedSquare.IsOccupied)
                {
                    if (selectedSquare.IsOccupied)
                    {
                        if (selectedSquare.OccupyingPiece.PieceColor ==
                            GameBoard.SelectedPiece.PieceColor)
                        { // Selected a teammate; Do not take
                            GameBoard.SelectedSquare = selectedSquare;
                        }
                        else
                        {
                            // Selected an enemy piece; move there and take it
                            GameBoard.tryMove(GameBoard.SelectedPiece, selectedSquare.Position);
                            GameBoard.clearSelectedSquares();

                            // Display all taken pieces
                            string message = String.Empty;
                            foreach (Piece piece in GameBoard.PiecesTaken)
                            {
                                message += "\n" + piece.ToString();
                            }
                            tbl_MessageConsole.Text = "Taken: " + message;
                        }
                    }
                    else
                    { // Destination is unoccupied; move there
                        GameBoard.tryMove(GameBoard.SelectedPiece, selectedSquare.Position);
                        GameBoard.clearSelectedSquares();
                    }
                }
                else // Original square is unoccupied; select the new square
                {
                    GameBoard.SelectedSquare = selectedSquare;
                }
            }

            #endregion

            return;
        }

        private void bt_resetBoard_Click(object sender, RoutedEventArgs e)
        {
            GameBoard.resetPieces();
            GameBoard.clearSelectedSquares();
        }
        #endregion

        #region Other methods
        public void updateImageSource(Image image, string path) =>
            image.Source = new BitmapImage(new Uri(this.BaseUri, path));
        #endregion

        #endregion

        private void Background_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            GameBoard.clearSelectedSquares();
        }
    }
}
