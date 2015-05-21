using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    abstract class Mover
    {
        protected int speed; //dlugosc ruchu(szybkosc)
        protected Point location; //lokalizacja naszego obiektu\

        /// <summary>
        /// komstruktor
        /// </summary>
        /// <param name="location"></param> //lokalizacja naszego punktu
        public Mover(Point location)
        {
            this.location = location;
        }

        /// <summary>
        /// Metoda służąca do przemieszczania punktu
        /// </summary>
        /// <param name="direction"></param> //kierunek
        /// <param name="boundaries"></param> //obiekt mapy
        /// <returns> lokalizacja naszego punktu po przemieszczeniu </returns>
        public Point Move(Direction direction, Rectangle boundaries)
        {
            Point newLocation = location;
            switch (direction)
	        {
		        case Direction.Top:
                    if (newLocation.Y - speed >= boundaries.Top)
	                {
		                newLocation.Y -= speed;
	                }
                    break;

                case Direction.Bottom:
                    if (newLocation.Y + speed <= boundaries.Bottom)
	                {
		                newLocation.Y += speed;
	                }
                    break;

                case Direction.Left:
                    if (newLocation.X - speed >= boundaries.Left)
	                {
		                newLocation.X -= speed;
	                }
                    break;

                case Direction.Right:
                    if (newLocation.X + speed <= boundaries.Right)
	                {
		                newLocation.X += speed;
	                }
                    break;

                default: 
                    break;
	        }
            return newLocation;
        }
    }
}
