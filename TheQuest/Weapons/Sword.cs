using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    class Sword : Weapon
    {
        private const int radius = 25;
        private const int damage = 3;
        public Sword(Game game, Point location)
            : base(game, location)
        {

        }

        public const string WeaponName = "Sword";
        public override string Name => WeaponName;
        
        public override void Attack(Direction direction, Random random)
        {
            if (!DamageEnemy(direction, radius, damage, random))
            {
                if (!DamageEnemy(direction.ClockwiseDirection(), radius, damage, random))
                {
                    DamageEnemy(direction.CounterClockwiseDirection(), radius, damage, random);
                }
            }
        }
    }
}
