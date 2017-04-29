﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Chess.Classes
{
    public class Square
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        Piece     _occupyingPiece;  // Can be null
        Rectangle _tile;            // Responds to events
        Image     _pieceImage;

        int _position;
        #endregion

        #region Properties
        /**************************************************************
         * PieceImage and AssociatedSquare
         * ------------------------------------
         * On the GUI, the image control sits on top of the rectangle
         * control for each square on the board. The image control
         * therefore will be used to detect when the user is hovering
         * or clicking.
         * The underlying rectangle control will respond with border 
         * effects and stuff.
        **************************************************************/
        public Image PieceImage
        {
            // Will be used to detect events like hover and click
            // The image control itself is set on initialization
            // Changing the updating the image is handled in MainPage
            //   using a delegate function
            get { return _pieceImage; }
            set { _pieceImage = value; }
        }

        public Rectangle Tile
        {
            // Responds to events detected by the image that sits
            //   on top of is
            get { return _tile; }
            set { _tile = value; }
        }

        public Piece occupyingPiece
        {
            get { return _occupyingPiece; }
            set { _occupyingPiece = value; }
        }

        public bool IsOccupied
        {
            get { return (occupyingPiece != null); }
        }

        public int Position
        {
            get { return _position; }
            set
            {
                if (AllPositions.Contains(value))
                { _position = value; }
                else
                { throw new ArgumentOutOfRangeException("Invalid position for piece"); }
            }
        }

        public static List<int> AllPositions // List of valid piece positions
        {
            get
            {
                List<int> possiblePositions = new List<int>();

                for (int position = 11; position <= 88; position += 1)
                {
                    if ((position % 10 <= 8) && (position % 10 != 0))
                    { possiblePositions.Add(position); }
                }

                return possiblePositions;
            }
        }
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
        public Square(int initialPosition)
        {
            this.Position = initialPosition;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            throw new NotImplementedException(
                message: "ToString() override not implemented");
        }
        #endregion

        #region Accessors
        #endregion

        #region Mutators
        public void highlightSquare(SolidColorBrush highlightColor)
        {
            #region Logic
            Tile.StrokeThickness = 3;
            Tile.Stroke          = highlightColor;
            #endregion

            return;
        }

        public void removeHighlighting()
        {
            #region Logic
            Tile.StrokeThickness = 0;
            #endregion

            return;
        }
        #endregion

        #region Other Methods
        #endregion
        #endregion
    }
}
