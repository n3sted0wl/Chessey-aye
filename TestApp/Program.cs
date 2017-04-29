using System;
using System.Collections.Generic;

namespace TestApp
{
    class Program
    {
        public static List<int> PossiblePositions
        {
            get
            {
                List<int> possiblePositions = new List<int>();

                for (int position = 11; position <= 88; position += 1)
                {
                    if ((position % 10 <= 8) && (position % 10 != 0))
                    {
                        possiblePositions.Add(position);
                    }
                }

                return possiblePositions;
            }
        }

        static void Main(string[] args)
        {
            foreach (int thing in PossiblePositions)
            {
                Console.WriteLine(thing.ToString());
            }
        }
    }
}