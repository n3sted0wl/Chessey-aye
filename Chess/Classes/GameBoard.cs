using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// TODO: logic for checking if a king can castle

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
        public static TextBlock messageConsole;
        public static TextBlock whiteScoreBoard;
        public static TextBlock blackScoreBoard;

        public static ListView lv_takenBlackPieces;
        public static ListView lv_takenWhitePieces;
        #endregion

        #region Fields
        static Square       _selectedSquare;
        static Square       _previouslySelectedSquare;
        static Piece.Color? _currentTurn;
        static int          _whiteScore = 0,
                            _blackScore = 0;

        static UpdateImageSource _updatePictureDelegate; 
        #endregion

        #region Properties
        public static int whiteScore
        {
            get { return _whiteScore; }
            set
            {
                _whiteScore = value;
                if (whiteScoreBoard != null)
                    whiteScoreBoard.Text = whiteScore.ToString();
            }
        }

        public static int blackScore
        {
            get { return _blackScore; }
            set
            {
                _blackScore = value;
                if (blackScoreBoard != null)
                    blackScoreBoard.Text = blackScore.ToString();
            }
        }

        /*
         * Rules for castling
         * Pieces are on the starting rank
         * King has not moved
         * Chosen rook has not moved
         * No pieces are between the king and rook
         * King is not in check
         * King does not pass through an attacked square
         * King will not be in check after the castle
         * */
        public static bool WhiteCanCastleQueenSide
        {
            get
            {
                return
                    !whiteKingHasMoved                    &&
                    !whiteQueensRookHasMoved              &&
                     whiteQueenCastleSquaresAreUnoccupied &&
                    !whiteKingIsChecked                   &&
                    !whiteQueenCastleSquaresAreAttacked;
            }
        }

        public static bool WhiteCanCastleKingSide
        {
            get
            {
                return
                    !whiteKingHasMoved &&
                    !whiteKingsRookHasMoved &&
                     whiteKingCastleSquaresAreUnoccupied &&
                    !whiteKingIsChecked &&
                    !whiteKingCastleSquaresAreAttacked;
            }
        }

        public static bool BlackCanCastleKingSide
        {
            get
            {
                return
                    !blackKingHasMoved &&
                    !blackKingsRookHasMoved &&
                     blackKingCastleSquaresAreUnoccupied &&
                    !blackKingIsChecked &&
                    !blackKingCastleSquaresAreAttacked;
            }
        }

        public static bool BlackCanCastleQueenSide
        {
            get
            {
                return
                    !blackKingHasMoved &&
                    !blackQueensRookHasMoved &&
                     blackQueenCastleSquaresAreUnoccupied &&
                    !blackKingIsChecked &&
                    !blackQueenCaslteSquaresAreAttacked;
            }
        }

        public static bool whiteKingHasMoved
        {
            // Initialized in initializer method
            // Updated in the attack method
            get;
            set;
        }

        public static bool blackKingHasMoved
        {
            // Initialized in the initializer method
            // Updated in the attack method
            get;
            set;
        }

        public static bool whiteKingsRookHasMoved
        {
            // Initialized in the initializer method
            // Updated in the attack method
            get; set;
        }

        public static bool whiteQueensRookHasMoved
        {
            // Initialized in the initializer method
            // Updated in the attack method
            get; set;
        }

        public static bool blackKingsRookHasMoved
        {
            // Initialized in the initializer method
            // Updated in the attack method
            get; set;
        }

        public static bool blackQueensRookHasMoved
        {
            // Initialized in the initializer method
            // Updated in the attack method
            get; set;
        }

        public static bool whiteQueenCastleSquaresAreUnoccupied
        {
            get
            {
                return
                    !getSquareByPosition(12).IsOccupied &&
                    !getSquareByPosition(13).IsOccupied &&
                    !getSquareByPosition(14).IsOccupied;
            }
        }

        public static bool whiteQueenCastleSquaresAreAttacked
        {
            get
            {
                GameRules.ignoreCastling = true;
                bool squaresAreAttacked =
                    (GameRules.getAttackingBlackPieces(getSquareByPosition(12)).Count() > 0) ||
                    (GameRules.getAttackingBlackPieces(getSquareByPosition(13)).Count() > 0) ||
                    (GameRules.getAttackingBlackPieces(getSquareByPosition(14)).Count() > 0);
                GameRules.ignoreCastling = false;

                return squaresAreAttacked;
            }
        }

        public static bool whiteKingCastleSquaresAreUnoccupied
        {
            get
            {
                return
                    !getSquareByPosition(16).IsOccupied &&
                    !getSquareByPosition(17).IsOccupied;
            }
        }

        public static bool whiteKingCastleSquaresAreAttacked
        {
            get
            {
                GameRules.ignoreCastling = true;
                bool squaresAreAttacked = 
                    (GameRules.getAttackingBlackPieces(getSquareByPosition(16)).Count() > 0) ||
                    (GameRules.getAttackingBlackPieces(getSquareByPosition(17)).Count() > 0);
                GameRules.ignoreCastling = false;
                return squaresAreAttacked;
            }
        }

        public static bool blackKingCastleSquaresAreUnoccupied
        {
            get
            {
                return
                    !getSquareByPosition(86).IsOccupied &&
                    !getSquareByPosition(87).IsOccupied;
            }
        }

        public static bool blackKingCastleSquaresAreAttacked
        {
            get
            {
                GameRules.ignoreCastling = true;
                bool squaresAreAttacked = 
                    (GameRules.getAttackingWhitePieces(getSquareByPosition(86)).Count() > 0) ||
                    (GameRules.getAttackingWhitePieces(getSquareByPosition(87)).Count() > 0);
                GameRules.ignoreCastling = false;
                return squaresAreAttacked;
            }
        }

        public static bool blackQueenCastleSquaresAreUnoccupied
        {
            get
            {
                return
                    !getSquareByPosition(82).IsOccupied &&
                    !getSquareByPosition(83).IsOccupied &&
                    !getSquareByPosition(84).IsOccupied;
            }
        }

        public static bool blackQueenCaslteSquaresAreAttacked
        {
            get
            {
                GameRules.ignoreCastling = true;
                bool squaresAreAttacked =
                    (GameRules.getAttackingWhitePieces(getSquareByPosition(82)).Count() > 0) ||
                    (GameRules.getAttackingWhitePieces(getSquareByPosition(83)).Count() > 0) ||
                    (GameRules.getAttackingWhitePieces(getSquareByPosition(84)).Count() > 0);
                GameRules.ignoreCastling = false;
                return squaresAreAttacked;
            }
        }

        public static bool blackKingIsChecked
        {
            get
            {
                return
                    GameRules.getAttackingWhitePieces(
                        getSquareByPiece(AllPieces.Find(piece => piece.PieceColor == Piece.Color.Black && piece.PieceType == Piece.Type.King))
                    ).Count() >= 1;
            }
        }

        public static bool whiteKingIsChecked
        {
            get
            {
                return
                    GameRules.getAttackingBlackPieces(
                        getSquareByPiece(AllPieces.Find(piece => piece.PieceColor == Piece.Color.White && piece.PieceType == Piece.Type.King))
                    ).Count() >= 1;
            }
        }

        public static Piece.Color? CurrentTurn
        {
            set
            {
                _currentTurn = value;
                if (CurrentTurn != null)
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

        public static UpdateImageSource updateImageSource
        {
            get { return _updatePictureDelegate; }
            set { _updatePictureDelegate = value; }
        }
        #endregion

        #region Structures
        private struct MoveLogRecord
        {
            // Fields
            Square _sourceSquare;
            Square _destinationSquare;
            Piece  _sourcePiece;
            Piece  _destinationPiece;

            // Properties
            public Piece.Color movingTeam
            {
                get { return SourceSqaure.OccupyingPiece.PieceColor; }
            }
            
            public Square SourceSqaure
            {
                get { return _sourceSquare; }
                set { _sourceSquare = value; }
            }

            public Square DestinationSquare
            {
                get { return _destinationSquare; }
                set { _destinationSquare = value; }
            }

            public Piece SourePiece
            {
                get { return _sourcePiece; }
                set { _sourcePiece = value; }
            }

            public Piece DestinationPiece
            {
                get { return _destinationPiece; }
                set { _destinationPiece = value; }
            }

            public bool PieceWasTaken
            {
                get { return DestinationPiece != null; }
            }

            // Constructor
            public MoveLogRecord(
                Square sourceSquare, 
                Square destinationSquare, 
                Piece  sourcePiece, 
                Piece  destinationPiece)
            {
                _sourceSquare      = sourceSquare;
                _destinationSquare = destinationSquare;
                _sourcePiece       = sourcePiece;
                _destinationPiece  = destinationPiece;
            }

            // Methods
            public override string ToString()
            {
                return "Initialize this";
            }

            public void save()
            {
                MoveLog.Enqueue(this);
            }
        }
        #endregion

        #region Enumerations
        #endregion

        #region Objects
        #endregion

        #region Collections
        public  static List<Square>         AllSquares;
        public  static List<Piece>          AllPieces;
        private static Queue<MoveLogRecord> MoveLog;
        #endregion

        #region Delegates
        public delegate void UpdateImageSource(Image image, string path);
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
            // Initialize properties
            whiteKingHasMoved       = false;
            blackKingHasMoved       = false;
            whiteKingsRookHasMoved  = false;
            whiteQueensRookHasMoved = false;
            blackKingsRookHasMoved  = false;
            blackQueensRookHasMoved = false;

            populateAllSquares();
            clearMoveLog();
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

            // Reset the conditions for a castle
            whiteKingHasMoved = false;
            blackKingHasMoved = false;
            #endregion

            return;
        }

        private static void clearMoveLog()
        {
            if (MoveLog == null)
            {
                MoveLog = new Queue<MoveLogRecord>();
            }
            else
            {
                MoveLog.Clear();
            }                
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

        public static void attack(Square source, Square destination, bool ignoreTurnToggle = false)
        {
            // Has no logic to validate source to destination
            #region Data
            #endregion

            #region Logic
            if (source.IsOccupied)
            {
                if (source.OccupyingPiece.PieceColor != GameBoard.CurrentTurn)
                    throw new InvalidOperationException("Not this team's turn to move");

                // Attacking an opposite team piece
                if (destination.IsOccupied &&
                    source.OccupyingPiece.PieceColor != destination.OccupyingPiece.PieceColor)
                {
                    // Change the attacked piece's status
                    destination.OccupyingPiece.PieceStatus = Piece.Status.Taken;

                    // Log the taken piece in the message console
                    addMessageToConsole($"Taken: {destination.OccupyingPiece.ToString()}");
                    displayTakenPieceInConsole(destination.OccupyingPiece);

                    // Log the taken piece's point value
                    if (destination.OccupyingPiece.PieceColor == Piece.Color.White)
                    {
                        whiteScore += destination.OccupyingPiece.Value;
                        whiteScoreBoard.Text = whiteScore.ToString();
                    }
                    else if (destination.OccupyingPiece.PieceColor == Piece.Color.Black)
                    {
                        blackScore += destination.OccupyingPiece.Value;
                        blackScoreBoard.Text = blackScore.ToString();
                    }

                    // Check if the king was taken
                    if (destination.OccupyingPiece.PieceType == Piece.Type.King)
                        throw new KingCapturedException();
                }

                // Log the move
                

                // Visually moves the piece
                destination.OccupyingPiece = source.OccupyingPiece;
                source.OccupyingPiece      = null;

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
                        // Display a dialog box and ask what kind of piece to change into
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

            // Check if there was a castle and move the rook
            if (destination.OccupyingPiece.PieceType == Piece.Type.King)
            {
                if (source.Position == 15 && destination.Position == 13)
                    attack(getSquareByPosition(11), getSquareByPosition(14), true);
                else if (source.Position == 15 && destination.Position == 17)
                    attack(getSquareByPosition(18), getSquareByPosition(16), true);
                else if (source.Position == 85 && destination.Position == 83)
                    attack(getSquareByPosition(81), getSquareByPosition(84), true);
                else if (source.Position == 85 && destination.Position == 87)
                    attack(getSquareByPosition(88), getSquareByPosition(86), true);
            }

            // If the king moved, set the flag variables
            if (destination.OccupyingPiece.PieceType == Piece.Type.King)
            {
                if (destination.OccupyingPiece.PieceColor == Piece.Color.White)
                    whiteKingHasMoved = true;
                else if (destination.OccupyingPiece.PieceColor == Piece.Color.Black)
                    blackKingHasMoved = true;
            }

            if (destination.OccupyingPiece.PieceType == Piece.Type.Rook)
            {
                if (source.Position == 88)
                    blackKingsRookHasMoved = true;
                if (source.Position == 81)
                    blackQueensRookHasMoved = true;
                if (source.Position == 1)
                    whiteQueensRookHasMoved = true;
                if (source.Position == 81)
                    whiteKingsRookHasMoved = true;
            }

            // Toggle whose turn it is
            if (!ignoreTurnToggle)
            {
                if (CurrentTurn == Piece.Color.Black)
                    CurrentTurn = Piece.Color.White;
                else
                    CurrentTurn = Piece.Color.Black;
            }

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

        public static void clearMessageConsole() =>
            messageConsole.Text = string.Empty;

        public static void resetScores() =>
            whiteScore = blackScore = 0;

        public static void addMessageToConsole(string message)
        {
            messageConsole.Text += $"{message}\n";

            return;
        }

        private static void displayTakenPieceInConsole(Piece takenPiece)
        {
            #region Data
            Image newTakenPieceImage;
            #endregion

            #region Logic
            // Create the image to be logged
            newTakenPieceImage        = new Image();
            newTakenPieceImage.Width  = 70;
            newTakenPieceImage.Height = 70;
            updateImageSource(newTakenPieceImage, takenPiece.ImagePath);

            // Put the new taken piece's image in the console
            if (takenPiece.PieceColor == Piece.Color.White)
            {
                lv_takenWhitePieces.Items.Add(newTakenPieceImage);
            }
            else // Taken piece is black
            {
                lv_takenBlackPieces.Items.Add(newTakenPieceImage);
            }
            #endregion
            return;
        }

        private static void logLastMove()
        {
            // Create a new move log entry
            // Save the new log entry
            return;
        }

        public static void clearTakenPieces()
        {
            lv_takenWhitePieces.Items.Clear();
            lv_takenBlackPieces.Items.Clear();
        }
        #endregion
        #endregion
    }
}
