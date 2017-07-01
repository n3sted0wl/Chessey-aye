// Programmers: Paul Antonio
//
// Date:        June 28, 2017
// File Name:   KingCapturedException.cs

#region Development Notes and TODOs
// --------------------
// TODO: Change file header file name
// TODO: Remove unnecessary using statements
// TODO: Remove unnecessary documentation
// TODO: Add exception documentation, if any
// TODO: Override ToString()
// TODO: Make the class public (probably)
// TODO: Copy-paste the Class_Organization Template into class body
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Classes
{
    public class KingCapturedException: Exception
    {
        public string message
        {
            get; set;
        }

        public KingCapturedException()
        {
        }

        public KingCapturedException(string message)
        {
            this.message = message;
        }
    }
}
