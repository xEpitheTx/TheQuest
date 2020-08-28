using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public class RedPotion : Weapon, IPotion
    {
        public RedPotion(Game game, Point location)
            : base(game, location)
        {

        }

        public const string WeaponName = "Red Potion";

        public override string Name => WeaponName;

        public bool used { get; private set; }

        public override void Attack(Direction direction, Random random)
        {
            game.IncreasePlayerHealth(10, random);
            used = true;
        }
    }
}
