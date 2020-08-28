using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    class Ghost : Enemy
    {
        private const int ghostStartingHitPoints = 8;
        public Ghost(Game game, Point location)
            : base(game, location, ghostStartingHitPoints)
        {

        }

        /// <summary>
        /// Only move and attack if hitpoints are above zero.
        /// When it moves, 1:3 chance that it'll move toward the player.
        /// 2:3 chance it'll stand still. If it's near the player, attacks for up to 3 damage.
        /// </summary>
        /// <param name="random"></param>
        public override void Move(Random random)
        {
            if (HitPoints > 0)
            {
                switch (random.Next() % 3)
                {
                    case 1:
                    location = Move(FindPlayerDirection(game.PlayerLocation), game.Boundaries);
                        break;
                    default:
                        break;
                }
                if (Nearby(game.PlayerLocation, 1))
                {
                    game.HitPlayer(3, random);
                }
            }
        }
    }
}
