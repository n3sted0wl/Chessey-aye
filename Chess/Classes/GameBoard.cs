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
        public struct Squares // Indexer
        {
            public Square this [int index]
            {
                get
                {
                    if (!Square.AllPositions.Contains(index))
                        throw new ArgumentOutOfRangeException("Indexer is invalid");

                    return AllSquares.Find(squ => squ.Position == index);
                }
            }
        }
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
        #endregion
        #endregion
    }
}
