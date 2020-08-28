
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    class Bat : Enemy
    {
        private const int batStartingHitPoints = 6;
        public Bat(Game game, Point location)
            : base(game, location, batStartingHitPoints)
        {

        }

        /// <summary>
        /// The bat starts with 6 hit points, It'll keep moving towards the player and attacking
        /// as long as it hasa one or more hit points. 
        /// When it moves, half the time it moves randomly, the other half it moves towards the employee.
        /// After it moves, it checks if it's nearby the player. If it is, attacks for up to two damage.
        /// </summary>
        /// <param name="random"></param>
        public override void Move(Random random)
        {
            if (HitPoints > 0)
            {
                switch (random.Next() % 2)
                {
                    case 0:
                        location = Move(FindPlayerDirection(game.PlayerLocation), game.Boundaries);
                        break;
                    default:
                        location = Move((Direction)random.Next(0, 3), game.Boundaries);
                        break;
                }
            }
                if (Nearby(game.PlayerLocation, 5))
                {
                    game.HitPlayer(2, random);
                }
        }
    }
}
