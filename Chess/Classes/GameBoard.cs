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
        #region Controls
        public static TextBlock turnIndicator;
        #endregion

        #region Fields
        static Square      _selectedSquare;
        static Square      _previouslySelectedSquare;
        static Piece.Color _currentTurn;
        #endregion

        #region Properties
        public static Piece.Color CurrentTurn
        {
            set
            {
                _currentTurn = value;
                turnIndicator.Text = $"{_currentTurn.ToString()} to move";
            }
            get
            {
                return _currentTurn;
            }
        }

        public static Square SelectedSquare
        {
            get { return _selectedSquare; }
            set
            {
                clearAttackableSquares();

                if (_selectedSquare != null)
                {
                    _previouslySelectedSquare  = _selectedSquare;
                    _selectedSquare.IsSelected = false;
                }

                _selectedSquare            = value;
                _selectedSquare.IsSelected = true;

                // Highlight attackable squares
                if (SelectedSquare != null)
                {
                    foreach (Square attackableSquare in GameRules.getAttackedSquares(SelectedSquare))
                    {
                        attackableSquare.IsAttackable = true;
                    }
                }
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

            // Set the current turn
            CurrentTurn = Piece.Color.White;
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

        public static void attack(Square source, Square destination)
        {
            // Has no logic to validate source to destination
            #region Data
            #endregion

            #region Logic
            if (source.IsOccupied)
            {
                if (source.OccupyingPiece.PieceColor != GameBoard.CurrentTurn)
                    throw new InvalidOperationException("Not this team's turn to move");
                if (destination.IsOccupied &&
                    source.OccupyingPiece.PieceColor !=
                    destination.OccupyingPiece.PieceColor)
                {
                    destination.OccupyingPiece.PieceStatus = Piece.Status.Taken;
                }

                destination.OccupyingPiece = source.OccupyingPiece;
                source.OccupyingPiece = null;

                // Check if a Pawn has crossed the board
                if (destination.OccupyingPiece.PieceType == Piece.Type.Pawn)
                {
                    if (
                       (destination.OccupyingPiece.PieceColor == Piece.Color.White &&
                       (destination.Position / 10) == 8) ||
                       (destination.OccupyingPiece.PieceColor == Piece.Color.Black &&
                       (destination.Position / 10) == 1)
                       )
                    {
                        destination.OccupyingPiece.PieceType = Piece.Type.Queen;
                        destination.UpdatePictureDelegate(
                            destination.PieceImage,
                            destination.OccupyingPiece.ImagePath);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Source square has no piece");
            }

            // Toggle whose turn it is
            if (CurrentTurn == Piece.Color.Black)
                CurrentTurn = Piece.Color.White;
            else
                CurrentTurn = Piece.Color.Black;
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

            clearAttackableSquares();
        }

        public static void clearAttackableSquares()
        {
            foreach (Square square in GameBoard.AllSquares)
            {
                square.IsAttackable = false;
            }
        }
        #endregion
        #endregion
    }
}
