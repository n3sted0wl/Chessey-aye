using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Chess.Classes
{
    class Square
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        Piece  _occupyingPiece;     // Can be null
        Square _associatedSquare;   // Responds to events
        #endregion

        #region Properties
        public Image PieceImage
        {
            // Read only
            // Get from the Piece object
            // Check for null
            get { return occupyingPiece.Picture; }
        }

        public Square AssociatedSquare
        {
            get { return _associatedSquare; }
            set { _associatedSquare = value; }
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
            get;
            set;
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
        #endregion

        #region Other Methods
        #endregion
        #endregion
    }
}
