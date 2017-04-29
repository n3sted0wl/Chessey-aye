using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Classes
{
    public static class GameRules
    {
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

        #region Other Methods
        public static List<Square> getAttackedSquares(Square attackingSquare)
        {
            #region Data
            List<Square> attackedSquares = new List<Square>();
            #endregion

            #region Logic
            if (attackingSquare.IsOccupied)
            {
                switch (attackingSquare.OccupyingPiece.PieceType)
                {
                    case Piece.Type.Pawn:
                        attackedSquares = getPawnAttackedSquares(attackingSquare);
                        break;
                    case Piece.Type.Bishop:
                        break;
                    case Piece.Type.Knight:
                        break;
                    case Piece.Type.Rook:
                        attackedSquares = getRookAttackedSquares(attackingSquare);
                        break;
                    case Piece.Type.Queen:
                        break;
                    case Piece.Type.King:
                        break;
                    default:
                        throw new ArgumentException(
                            $"Occupying Piece type {attackingSquare.OccupyingPiece.PieceType.ToString()} not recognized");
                }
            }
            else
            {
                throw new InvalidOperationException("Attacking square has no piece");
            }
            #endregion

            return attackedSquares;
        }

        private static List<Square> getPawnAttackedSquares(Square attackingSquare)
        {
            #region Data
            List<Square> attackedSquares = new List<Square>();
            Piece currentPawn;
            int targetPosition;
            #endregion

            #region Logic
            // Check for a Pawn
            if (!attackingSquare.IsOccupied)
                throw new InvalidOperationException("Square is not occupied");
            if (attackingSquare.OccupyingPiece.PieceType != Piece.Type.Pawn)
                throw new ArgumentException("Square does not have a pawn on it");
            currentPawn = attackingSquare.OccupyingPiece;

            if (currentPawn.PieceColor == Piece.Color.White)
            {
                // Can move forward if it is not occupied
                if (!GameBoard.getSquareByPosition(attackingSquare.Position + 10).IsOccupied)
                    attackedSquares.Add(GameBoard.getSquareByPosition(attackingSquare.Position + 10));

                // If it's on the second rank for its color, can move 'forward' two
                if (((attackingSquare.Position / 10) == 2) &&
                    !GameBoard.getSquareByPosition(attackingSquare.Position + 20).IsOccupied)
                {
                    attackedSquares.Add(GameBoard.getSquareByPosition(attackingSquare.Position + 20));
                }

                // If there's an enemy piece to its forward diagonal, can attack it
                // Right diagonal
                targetPosition = attackingSquare.Position + 11;
                if ((Square.AllPositions.Contains(targetPosition)) &&
                    GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                    GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor == Piece.Color.Black)
                {
                    attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                }

                // Left diagonal
                targetPosition = attackingSquare.Position + 9;
                if ((Square.AllPositions.Contains(targetPosition)) &&
                    GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                    GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor == Piece.Color.Black)
                {
                    attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                }

                // TODO: En Passant ???
            }
            else if (currentPawn.PieceColor == Piece.Color.Black)
            {
                // Can move forward if it is not occupied
                if (!GameBoard.getSquareByPosition(attackingSquare.Position - 10).IsOccupied)
                    attackedSquares.Add(GameBoard.getSquareByPosition(attackingSquare.Position - 10));

                // If it's on the second rank for its color, can move 'forward' two
                if (((attackingSquare.Position / 10) == 7) &&
                   !GameBoard.getSquareByPosition(attackingSquare.Position - 20).IsOccupied)
                {
                    attackedSquares.Add(GameBoard.getSquareByPosition(attackingSquare.Position - 20));
                }

                // If there's an enemy piece to its forward diagonal, can attack it
                // Right diagonal
                targetPosition = attackingSquare.Position - 9;
                if ((Square.AllPositions.Contains(targetPosition)) &&
                    GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                    GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor == Piece.Color.White)
                {
                    attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                }

                // Left diagonal
                targetPosition = attackingSquare.Position - 11;
                if ((Square.AllPositions.Contains(targetPosition)) &&
                    GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                    GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor == Piece.Color.White)
                {
                    attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                }

                // TODO: En Passant ???
            }
            #endregion
            return attackedSquares;
        }

        private static List<Square> getRookAttackedSquares(Square attackingSquare)
        {
            #region Data
            List<Square> attackedSquares = new List<Square>();
            Piece currentRook;
            int targetPosition;
            #endregion

            #region Logic
            // Check for a rook
            if (!attackingSquare.IsOccupied)
                throw new InvalidOperationException("Square is not occupied");
            if (attackingSquare.OccupyingPiece.PieceType != Piece.Type.Rook)
                throw new ArgumentException("Square does not have a rook on it");
            currentRook = attackingSquare.OccupyingPiece;

            // Forward
            targetPosition = attackingSquare.Position + 10;
            while (Square.AllPositions.Contains(targetPosition) &&
                !GameBoard.getSquareByPosition(targetPosition).IsOccupied)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                targetPosition += 10;
            }
            if (Square.AllPositions.Contains(targetPosition) &&
                GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor !=
                    currentRook.PieceColor)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
            }

            // Back
            targetPosition = attackingSquare.Position - 10;
            while (Square.AllPositions.Contains(targetPosition) &&
                !GameBoard.getSquareByPosition(targetPosition).IsOccupied)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                targetPosition -= 10;
            }
            if (Square.AllPositions.Contains(targetPosition) &&
                GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor !=
                    currentRook.PieceColor)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
            }

            // Left
            targetPosition = attackingSquare.Position - 1;
            while (Square.AllPositions.Contains(targetPosition) &&
                !GameBoard.getSquareByPosition(targetPosition).IsOccupied)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                targetPosition -= 1;
            }
            if (Square.AllPositions.Contains(targetPosition) &&
                GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor !=
                    currentRook.PieceColor)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
            }

            // Right
            targetPosition = attackingSquare.Position + 1;
            while (Square.AllPositions.Contains(targetPosition) &&
                !GameBoard.getSquareByPosition(targetPosition).IsOccupied)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                targetPosition += 1;
            }
            if (Square.AllPositions.Contains(targetPosition) &&
                GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor !=
                    currentRook.PieceColor)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
            }

            #endregion

            return attackedSquares;
        }
        #endregion
        #endregion
    }
}
