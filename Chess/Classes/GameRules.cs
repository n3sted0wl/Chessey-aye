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
                        attackedSquares = getBishopAttackedSquares(attackingSquare);
                        break;
                    case Piece.Type.Knight:
                        attackedSquares = getKnightAttackedSquares(attackingSquare);
                        break;
                    case Piece.Type.Rook:
                        attackedSquares = getRookAttackedSquares(attackingSquare);
                        break;
                    case Piece.Type.Queen:
                        attackedSquares = getQueenAttackedSquares(attackingSquare);
                        break;
                    case Piece.Type.King:
                        attackedSquares = getKingAttackedSquares(attackingSquare);
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

        private static List<Square> getBishopAttackedSquares(Square attackingSquare)
        {
            #region Data
            List<Square> attackedSquares = new List<Square>();
            Piece currentBishop;
            int targetPosition;
            #endregion

            #region Logic
            // Check for a bishop
            if (!attackingSquare.IsOccupied)
                throw new InvalidOperationException("Square is not occupied");
            currentBishop = attackingSquare.OccupyingPiece;
            #endregion

            #region Logic
            // Up, Right
            targetPosition = attackingSquare.Position + 11;
            while (Square.AllPositions.Contains(targetPosition) &&
                !GameBoard.getSquareByPosition(targetPosition).IsOccupied)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                targetPosition += 11;
            }
            if (Square.AllPositions.Contains(targetPosition) &&
                GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor !=
                    currentBishop.PieceColor)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
            }

            // Up, Left
            targetPosition = attackingSquare.Position + 9;
            while (Square.AllPositions.Contains(targetPosition) &&
                !GameBoard.getSquareByPosition(targetPosition).IsOccupied)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                targetPosition += 9;
            }
            if (Square.AllPositions.Contains(targetPosition) &&
                GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor !=
                    currentBishop.PieceColor)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
            }

            // Down, Right
            targetPosition = attackingSquare.Position - 9;
            while (Square.AllPositions.Contains(targetPosition) &&
                !GameBoard.getSquareByPosition(targetPosition).IsOccupied)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                targetPosition -= 9;
            }
            if (Square.AllPositions.Contains(targetPosition) &&
                GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor !=
                    currentBishop.PieceColor)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
            }

            // Down, Left
            targetPosition = attackingSquare.Position - 11;
            while (Square.AllPositions.Contains(targetPosition) &&
                !GameBoard.getSquareByPosition(targetPosition).IsOccupied)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
                targetPosition -= 11;
            }
            if (Square.AllPositions.Contains(targetPosition) &&
                GameBoard.getSquareByPosition(targetPosition).IsOccupied &&
                GameBoard.getSquareByPosition(targetPosition).OccupyingPiece.PieceColor !=
                    currentBishop.PieceColor)
            {
                attackedSquares.Add(GameBoard.getSquareByPosition(targetPosition));
            }

            #endregion

            return attackedSquares;
        }

        private static List<Square> getQueenAttackedSquares(Square attackingSquare)
        {
            #region Data
            List<Square> attackedSquares = new List<Square>();
            #endregion

            #region Logic
            attackedSquares.AddRange(getBishopAttackedSquares(attackingSquare));
            attackedSquares.AddRange(getRookAttackedSquares(attackingSquare));
            #endregion

            return attackedSquares;
        }

        private static List<Square> getKnightAttackedSquares(Square attackingSquare)
        {
            #region Data
            List<Square> attackedSquares = new List<Square>();
            List<int> positionsToTest = new List<int>();
            Piece currentKnight;
            int position = attackingSquare.Position;
            #endregion

            #region Logic
            if (!attackingSquare.IsOccupied)
                throw new InvalidOperationException("Square is not occupied");
            currentKnight = attackingSquare.OccupyingPiece;

            positionsToTest.Add(position + 21);
            positionsToTest.Add(position + 19);
            positionsToTest.Add(position + 12);
            positionsToTest.Add(position + 8);
            positionsToTest.Add(position - 8);
            positionsToTest.Add(position - 12);
            positionsToTest.Add(position - 19);
            positionsToTest.Add(position - 21);

            foreach (int target in positionsToTest)
            {
                if (Square.AllPositions.Contains(target))
                {
                    if (
                        (!GameBoard.getSquareByPosition(target).IsOccupied) ||
                        (GameBoard.getSquareByPosition(target).IsOccupied && 
                        (GameBoard.getSquareByPosition(target).OccupyingPiece.PieceColor != currentKnight.PieceColor)
                    ))
                    {
                        attackedSquares.Add(GameBoard.getSquareByPosition(target));
                    }
                }
            }
            #endregion

            return attackedSquares;
        }

        private static List<Square> getKingAttackedSquares(Square attackingSquare)
        {

            #region Data
            List<Square> attackedSquares = new List<Square>();
            List<int> positionsToTest = new List<int>();
            Piece currentKing;
            int position = attackingSquare.Position;
            #endregion

            #region Logic
            if (!attackingSquare.IsOccupied)
                throw new InvalidOperationException("Square is not occupied");
            currentKing = attackingSquare.OccupyingPiece;

            positionsToTest.Add(position + 1);
            positionsToTest.Add(position + 9);
            positionsToTest.Add(position + 10);
            positionsToTest.Add(position + 11);
            positionsToTest.Add(position - 1);
            positionsToTest.Add(position - 9);
            positionsToTest.Add(position - 10);
            positionsToTest.Add(position - 11);

            foreach (int target in positionsToTest)
            {
                if (Square.AllPositions.Contains(target))
                {
                    if (
                        (!GameBoard.getSquareByPosition(target).IsOccupied) ||
                        (GameBoard.getSquareByPosition(target).IsOccupied &&
                        (GameBoard.getSquareByPosition(target).OccupyingPiece.PieceColor != currentKing.PieceColor)
                    ))
                    {
                        attackedSquares.Add(GameBoard.getSquareByPosition(target));
                    }
                }
            }
            #endregion

            return attackedSquares;
        }
        #endregion
        #endregion
    }
}

