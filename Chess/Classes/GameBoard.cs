using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Chess.Classes
{
    public static class GameBoard
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        static object _selectedObject;
        #endregion

        #region Properties
        public static object SelectedObject
        {
            get { return _selectedObject; }
            set { _selectedObject = value; }
        }
        #endregion

        #region Structures
        #endregion

        #region Enumerations
        #endregion

        #region Objects
        #endregion

        #region Collections
        public static List<Square> AllSquares;
        #endregion

        #region Delegates
        #endregion
        #endregion

        /*************************************************************/
        /*                       Functionality                       */
        /*************************************************************/
        #region Methods
        #region Initializer
        public static void initialize()
        {
            #region Logic
            populateAllSquares();
            #endregion

            return;
        }

        public static void resetPieces()
        {
            #region Data
            #endregion

            #region Logic
            // Clear the gameboard
            foreach (Square square in AllSquares)
            {
                square.OccupyingPiece = null;
            }

            // Insert pieces where they should be
            for (int tile = 21; tile <= 28; tile += 1)
            {
                getSquareByPosition(tile).OccupyingPiece =
                    new Piece(Piece.Color.White, Piece.Type.Pawn);
            }

            getSquareByPosition(11).OccupyingPiece =
                new Piece(Piece.Color.White, Piece.Type.Rook);

            getSquareByPosition(12).OccupyingPiece =
                new Piece(Piece.Color.White, Piece.Type.Knight);

            getSquareByPosition(13).OccupyingPiece =
                new Piece(Piece.Color.White, Piece.Type.Bishop);

            getSquareByPosition(14).OccupyingPiece =
                new Piece(Piece.Color.White, Piece.Type.Queen);

            getSquareByPosition(15).OccupyingPiece =
                new Piece(Piece.Color.White, Piece.Type.King);

            getSquareByPosition(16).OccupyingPiece =
                new Piece(Piece.Color.White, Piece.Type.Bishop);

            getSquareByPosition(17).OccupyingPiece =
                new Piece(Piece.Color.White, Piece.Type.Knight);

            getSquareByPosition(18).OccupyingPiece =
                new Piece(Piece.Color.White, Piece.Type.Rook);

            for (int tile = 71; tile <= 78; tile += 1)
            {
                getSquareByPosition(tile).OccupyingPiece =
                    new Piece(Piece.Color.Black, Piece.Type.Pawn);
            }

            getSquareByPosition(81).OccupyingPiece =
                new Piece(Piece.Color.Black, Piece.Type.Rook);

            getSquareByPosition(82).OccupyingPiece =
                new Piece(Piece.Color.Black, Piece.Type.Knight);

            getSquareByPosition(83).OccupyingPiece =
                new Piece(Piece.Color.Black, Piece.Type.Bishop);

            getSquareByPosition(84).OccupyingPiece =
                new Piece(Piece.Color.Black, Piece.Type.Queen);

            getSquareByPosition(85).OccupyingPiece =
                new Piece(Piece.Color.Black, Piece.Type.King);

            getSquareByPosition(86).OccupyingPiece =
                new Piece(Piece.Color.Black, Piece.Type.Bishop);

            getSquareByPosition(87).OccupyingPiece =
                new Piece(Piece.Color.Black, Piece.Type.Knight);

            getSquareByPosition(88).OccupyingPiece =
                new Piece(Piece.Color.Black, Piece.Type.Rook);
            #endregion

            return;
        }
        #endregion

        #region Overrides
        #endregion

        #region Accessors
        private static void populateAllSquares()
        {
            AllSquares = new List<Square>();

            #region Logic
            foreach (int position in Square.AllPositions)
            {
                AllSquares.Add(new Square(position));
            }
            #endregion

            return;
        }
        #endregion

        #region Mutators
        #endregion

        #region Other Methods
        public static Square getSquareByPosition(int position)
        {
            return (AllSquares.Find(squ => squ.Position == position));
        }

        #endregion
        #endregion
    }
}
