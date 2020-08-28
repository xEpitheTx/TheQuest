using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public abstract class Enemy : Mover
    {
        private const int NearPlayerDistance = 25;
        public int HitPoints { get; private set; }

        public bool Dead
        {
            get
            {
                if (HitPoints <= 0)
                {
                    return true;
                }
                else return false;
            }
        }

        public Enemy(Game game, Point location, int hitPoints)
            : base(game, location)
        {
            HitPoints = hitPoints;
        }

        public abstract void Move(Random random);

        public void Hit(int maxDamage, Random random)
        {
            HitPoints -= random.Next(1, maxDamage);
        }

        protected bool NearPlayer()
        {
            return (Nearby(game.PlayerLocation, NearPlayerDistance));
        }

        /// <summary>
        /// Takes the players location and uses the base calss's location field to figure out where
        /// the player is in relation to the enemy.
        /// </summary>
        /// <param name="playerLocation">The players location.</param>
        /// <returns>Returns the direction to move towards the player.</returns>
        protected Direction FindPlayerDirection(Point playerLocation)
        {
            Direction directionToMove;
            if (playerLocation.X > location.X + 10)
            {
                directionToMove = Direction.Right;
            }
            else if (playerLocation.X < location.X - 10)
            {
                directionToMove = Direction.Left;
            }
            else if (playerLocation.Y < location.Y - 10)
            {
                directionToMove = Direction.Up;
            }
            else
            {
                directionToMove = Direction.Down;
            }
            return directionToMove;
        }
    }
}
