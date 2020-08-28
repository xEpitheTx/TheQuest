using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public class Ghoul : Enemy
    {
        private const int ghoulStartingHitPoints = 10;
        public Ghoul(Game game, Point location)
            : base(game, location, ghoulStartingHitPoints)
        {

        }

        /// <summary>
        /// Only moves and attacks if hitpoints are great than 0. When it moves,
        /// 2:3 chance that it'll move towards the player. 1:3 chance it stands still.
        /// If it's near the player, it attacks for up to 4 damage.
        /// </summary>
        /// <param name="random">used to determine damage.</param>
        public override void Move(Random random)
        {
            if (HitPoints > 0)
            {
                switch (random.Next() % 3)
                {
                    case 1:
                    case 2:
                        location = Move(FindPlayerDirection(game.PlayerLocation), game.Boundaries);
                        break;
                    default:
                        break;
                }
                if (Nearby(game.PlayerLocation, 1))
                {
                    game.HitPlayer(4, random);
                }
            }
        }
    }
}
