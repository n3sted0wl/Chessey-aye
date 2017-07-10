using System;
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
        private const string DEFAULT_IMAGE_PATH = 
            @"\Assets\Images\Pieces\NoPiece.png";

        Piece     _occupyingPiece;  // Can be null
        Rectangle _tile;            // Responds to events
        Image     _pieceImage;

        UpdateImageSource _updatePictureDelegate;

        int  _position;
        bool _isSelected;
        bool _isAttackable;
        #endregion

        #region Properties
        /* PieceImage and AssociatedSquare
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

        public Piece OccupyingPiece
        {
            get { return _occupyingPiece; }
            set
            {
                _occupyingPiece = value;

                if (PieceImage != null)
                {
                    if (_occupyingPiece != null)
                    {
                        UpdatePictureDelegate(
                            PieceImage, _occupyingPiece.ImagePath);
                    }
                    else
                    {
                        UpdatePictureDelegate(
                            PieceImage, DEFAULT_IMAGE_PATH);
                    }
                }
                else
                {
                    throw new NullReferenceException($"Image control not set for square {Position}");
                }
            }
        }

        public bool IsOccupied
        {
            get { return (_occupyingPiece != null); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            { // Set by the GameBoard
                _isSelected = value;

                if (IsSelected)
                {
                    highlightSquare(new SolidColorBrush(Colors.Green));
                }
                else
                {
                    removeHighlighting();
                }
            }

        }

        public bool IsAttackable
        {
            get { return _isAttackable; }
            set
            {
                _isAttackable = value;

                if (IsAttackable)
                {
                    if (IsOccupied)
                        highlightSquare(new SolidColorBrush(Colors.Red));
                    else
                        highlightSquare(new SolidColorBrush(Colors.Gold));
                }
                else
                {
                    removeHighlighting();
                }

            }
        }

        public UpdateImageSource UpdatePictureDelegate
        {
            get { return _updatePictureDelegate; }
            set { _updatePictureDelegate = value; }
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

        public string PositionName
        {
            get
            {
                string name = string.Empty;

                switch(Position % 10)
                {
                    case 1:
                        name += 'a';
                        break;
                    case 2:
                        name += 'b';
                        break;
                    case 3:
                        name += 'c';
                        break;
                    case 4:
                        name += 'd';
                        break;
                    case 5:
                        name += 'e';
                        break;
                    case 6:
                        name += 'f';
                        break;
                    case 7:
                        name += 'g';
                        break;
                    case 8:
                        name += 'h';
                        break;
                }
                name += (Position / 10).ToString();

                return name;
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
        public delegate void UpdateImageSource(Image image, string path);
        #endregion
        #endregion

        /*************************************************************/
        /*                       Functionality                       */
        /*************************************************************/
        #region Methods
        #region Constructors
        public Square(int newPosition)
        {
            this.Position = newPosition;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"{this.PositionName}";
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
