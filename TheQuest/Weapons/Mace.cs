using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    class Mace : Weapon
    {
        private const int radius = 40;
        private const int damage = 6;

        public Mace(Game game, Point location)
            : base(game, location) { }

        public const string WeaponName = "Mace";

        public override string Name => WeaponName;

        /// <summary>
        /// Attacks in every direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="random"></param>
        public override void Attack(Direction direction, Random random)
        {
            if (!DamageEnemy(direction, radius, damage, random))
            {
                if (!DamageEnemy(direction.ClockwiseDirection(), radius, damage, random))
                {
                    if (!DamageEnemy(direction.InverseDirection(), radius, damage, random))
                    {
                        DamageEnemy(direction.CounterClockwiseDirection(), radius, damage, random);
                    }
                }
            }
        }
    }
}
