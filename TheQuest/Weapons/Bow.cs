using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    class Bow : Weapon
    {
        private const int radius = 50;
        private const int damage = 1;
        public Bow(Game game, Point location)
            : base(game, location) { }

        public const string WeaponName = "Bow";

        public override string Name => WeaponName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="random"></param>
        public override void Attack(Direction direction, Random random)
        {
            DamageEnemy(direction, radius, damage, random);
        }
    }
}
