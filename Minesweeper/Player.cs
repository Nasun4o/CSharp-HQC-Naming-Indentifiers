using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Player
    {
            private string name;
            private int points;

            public Player()
            {
            }

            public Player(string name, int points)
            {
                this.name = name;
                this.points = points;
            }

            public string Name
            {
                get
                {
                    return this.name;
                }

                set
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new ArgumentException("Name cannot be null or empty");
                    }
                    this.name = value;
                }
            }
            public int Points
            {
                get
                {
                    return this.points;
                }

                set
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException("Points cannot be a negative number");
                    }
                    this.points = value;
                }
            }
    }
}
