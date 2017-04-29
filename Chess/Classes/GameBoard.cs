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
        static Square _selectedSquare;
        static Square _previouslySelectedSquare;
        #endregion

        #region Properties
        public static Square SelectedSquare
        {
            get { return _selectedSquare; }
            set
            {
                if (_selectedSquare != null)
                {
                    _previouslySelectedSquare  = _selectedSquare;
                    _selectedSquare.IsSelected = false;
                }
                _selectedSquare = value;
                _selectedSquare.IsSelected = true;
            }
        }

        public static Square PreviouslySelectedSquare
        {
            get { return _previouslySelectedSquare; }
        }

        public static Piece SelectedPiece
        {
            get
            {
                return SelectedSquare.OccupyingPiece;
            }
        }

        public static List<Piece> BlackPieces
        {
            get
            {
                return (
                    from piece in AllPieces
                    where piece.PieceColor == Piece.Color.Black
                    select piece
                    ).ToList();
            }
        }

        public static List<Piece> WhitePieces
        {
            get
            {
                return (
                    from piece in AllPieces
                    where piece.PieceColor == Piece.Color.White
                    select piece
                    ).ToList();
            }

        }

        public static List<Piece> BlackPiecesRemaining
        {
            get
            {
                return (
                    from piece in BlackPieces
                    where piece.PieceStatus == Piece.Status.Alive
                    select piece
                    ).ToList();
            }
        }

        public static List<Piece> WhitePiecesRemaining
        {
            get
            {
                return (
                    from piece in WhitePieces
                    where piece.PieceStatus == Piece.Status.Alive
                    select piece
                    ).ToList();

            }
        }

        public static List<Piece> PiecesTaken
        {
            get
            {
                return (
                    from piece in AllPieces
                    where piece.PieceStatus == Piece.Status.Taken
                    select piece
                    ).ToList();
            }
        }

        public static List<Piece> BlackPiecesTaken
        {
            get
            {
                return (
                    from piece in BlackPieces
                    where piece.PieceStatus == Piece.Status.Taken
                    select piece
                    ).ToList();
            }
        }

        public static List<Piece> WhitePiecesTaken
        {
            get
            {
                return (
                    from piece in WhitePieces
                    where piece.PieceStatus == Piece.Status.Taken
                    select piece
                    ).ToList();

            }
        }

        public static int BlackPoints
        {
            get
            {
                return WhitePiecesTaken.Sum(piece => piece.Value);
            }
        }

        public static int WhitePoints
        {
            get
            {
                return BlackPiecesTaken.Sum(piece => piece.Value);
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
        public static List<Square> AllSquares;
        public static List<Piece>  AllPieces;
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
            resetPieces(); // Initializes AllPieces collection

            #endregion

            return;
        }

        public static void resetPieces()
        {
            #region Data
            #endregion

            #region Logic
            // Clear the gameboard
            if (AllSquares != null)
            {
                foreach (Square square in AllSquares)
                {
                    square.OccupyingPiece = null;
                }
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

            // Populate the AllPieces collection
            AllPieces = new List<Piece>();
            foreach (Square square in AllSquares)
            {
                if (square.IsOccupied)
                {
                    AllPieces.Add(square.OccupyingPiece);
                }
            }
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
        public static Square getSquareByPosition(int position) =>
            (AllSquares.Find(squ => squ.Position == position));

        public static Square getSquareByPiece(Piece piece) =>
            (AllSquares.Find(squ => squ.OccupyingPiece == piece));

        public static void tryMove(Piece pieceToMove, int newPosition)
        {
            #region Data
            Square source       = getSquareByPiece(pieceToMove);
            Square destination  = getSquareByPosition(newPosition);
            #endregion

            #region Logic
            // TODO: Check if the destination is reachable

            if (destination.IsOccupied &&
                source.OccupyingPiece.PieceColor != 
                destination.OccupyingPiece.PieceColor)
            {
                destination.OccupyingPiece.PieceStatus = Piece.Status.Taken;
            }

            destination.OccupyingPiece = pieceToMove;
            source.OccupyingPiece      = null;
            #endregion

            return;
        }

        public static void clearSelectedSquares()
        {
            if (_previouslySelectedSquare != null)
                _previouslySelectedSquare.IsSelected = false;
            _previouslySelectedSquare = null;

            if (_selectedSquare != null)
                _selectedSquare.IsSelected = false;
            _selectedSquare = null;
        }
        #endregion
        #endregion
    }
}
