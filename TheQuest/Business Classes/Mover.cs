using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public abstract class Mover
    {
        private const int MoveInterval = 10;
        protected Point location;
        public Point Location => location;
        protected Game game;

        public Mover(Game game, Point location)
        {
            this.game = game;
            this.location = location;
        }

        /// <summary>
        /// The nearby method checks a point against this objects current location.
        /// If they're within distance of each other, it returns true. Otherwise false;
        /// </summary>
        /// <param name="locationToCheck">The location to check to see if something is nearby.</param>
        /// <param name="distance">distance from this opbject.</param>
        /// <returns></returns>
        public bool Nearby(Point locationToCheck, int distance)
        {
            return NearbyMath(location, locationToCheck, distance);
        }

        /// <summary>
        /// Compares two points and returns true if they're within the 
        /// specified distance of each other.
        /// </summary>
        /// <param name="firstLocationToCheck"></param>
        /// <param name="secondLocationToCheck"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static bool Nearby(Point firstLocationToCheck, Point secondLocationToCheck, int distance)
        {
            return NearbyMath(firstLocationToCheck, secondLocationToCheck, distance);
        }

        /// <summary>
        /// This method tries to move one step in a direction. If it can, it reutrns the new point.
        /// If it hits a boundary, it returns the original point.
        /// </summary>
        /// <param name="direction">Direction in which an object is trying to move.</param>
        /// <param name="boundaries">Boundaries of the game space.</param>
        /// <returns></returns>
        public Point Move(Direction direction, Rectangle boundaries)
        {
            Point newLocation = location;
            switch (direction)
            {
                case Direction.Up:
                    if (newLocation.Y - MoveInterval >= boundaries.Top)
                    {
                        newLocation.Y -= MoveInterval;
                    }
                    break;
                case Direction.Down:
                    if (newLocation.Y + MoveInterval <= boundaries.Bottom)
                    {
                        newLocation.Y += MoveInterval;
                    }
                    break;
                case Direction.Left:
                    if (newLocation.X - MoveInterval >= boundaries.Left)
                    {
                        newLocation.X -= MoveInterval;
                    }
                    break;
                case Direction.Right:
                    if (newLocation.X + MoveInterval <= boundaries.Right)
                    {
                        newLocation.X += MoveInterval;
                    }
                    break;
                default:
                    break;
            }
            return newLocation;
        }

        /// <summary>
        /// Moves a Point in a direction and returns the new point.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Point Move(Direction direction, Point target, Rectangle rectangle)
        {
            Point newPoint = target;
            newPoint = Move(direction, rectangle);
            return newPoint;
#warning I'm not sure this is right, test meeeeeeeeee. page 480
        }

        /// <summary>
        /// Returns true if points are near eachother based on location and distance.
        /// </summary>
        /// <param name="locationToCheck"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        private static bool NearbyMath(Point location, Point locationToCheck, int distance)
        {
            return Math.Abs(location.X - locationToCheck.X) < distance
                && Math.Abs(location.Y - locationToCheck.Y) < distance;
        }
    }
}
