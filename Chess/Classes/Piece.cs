using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Chess.Classes
{
    public class Piece
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        private static string IMAGE_DIRECTORY = @"Assets/Images/Pieces/";

        public static int PAWN_VALUE   = 1;
        public static int BISHOP_VALUE = 3;
        public static int KNIGHT_VALUE = 3;
        public static int ROOK_VALUE   = 5;
        public static int QUEEN_VALUE  = 9;
        public static int KING_VALUE   = 1000;

        Image _picture; // Must be set by constructor
        #endregion

        #region Properties
        public Image Picture
        {
            // Read only
            get { return _picture; }
        }

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

        public int Value
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

        private string RelativeImagePath
        {
            // Read only
            get
            {
                // Generate based on color and type
                return $"{IMAGE_DIRECTORY}/{PieceColor.ToString()}{PieceType.ToString()}.png";
            }
        }

        public Status PieceStatus
        {
            get; set;
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
        public Piece()
        {
            /* MUST initialize:
             *  Image control
             *  type
             *  color
             *  position
             *  status
             * */
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
