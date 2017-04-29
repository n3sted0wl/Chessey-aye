using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Chess.Classes
{
    /// <summary>
    /// A chess piece that can be placed on a square
    /// </summary>
    public class Piece
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        // Directories
        private static string IMAGE_DIRECTORY = @"Assets/Images/Pieces/";

        // Piece point values
        public static int PAWN_VALUE   = 1;
        public static int BISHOP_VALUE = 3;
        public static int KNIGHT_VALUE = 3;
        public static int ROOK_VALUE   = 5;
        public static int QUEEN_VALUE  = 9;
        public static int KING_VALUE   = 1000;

        // Associated controls
        #endregion

        #region Properties
        public Type PieceType
        {
            get;
            set;
        }

        public Color PieceColor
        {
            get;
            set;
        }

        public int Value // Read only
        {
            get
            {
                int value;

                switch (this.PieceType)
                {
                    case Type.Pawn:
                        value = PAWN_VALUE;
                        break;
                    case Type.Bishop:
                        value = BISHOP_VALUE;
                        break;
                    case Type.Knight:
                        value = KNIGHT_VALUE;
                        break;
                    case Type.Rook:
                        value = ROOK_VALUE;
                        break;
                    case Type.Queen:
                        value = QUEEN_VALUE;
                        break;
                    case Type.King:
                        value = KING_VALUE;
                        break;
                    default:
                        throw new ArgumentException("Unknown piece type detected");
                        break;
                }

                return value;
            }
        }

        public string ImagePath // Read only
        {
            get
            { // Generate based on color and type
                return $"{IMAGE_DIRECTORY}/{PieceColor.ToString()}{PieceType.ToString()}.png";
            }
        }

        public Status PieceStatus
        {
            get;
            set;
        }
        #endregion

        #region Structures
        #endregion

        #region Enumerations
        public enum Type
        {
            Bishop,
            King,
            Knight,
            Pawn,
            Queen,
            Rook
        };

        public enum Color
        {
            White,
            Black
        };

        public enum Status
        {
            Taken,
            Alive
        };
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
        public Piece(
            Color  newColor,
            Type   newType,
            Status newStatus = Status.Alive)
        {
            PieceColor  = newColor;
            PieceType   = newType;
            PieceStatus = newStatus;
        }
        #endregion

        #region Overrides
        public override string ToString() =>
            $"{PieceColor.ToString()} {PieceType.ToString()}";
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
