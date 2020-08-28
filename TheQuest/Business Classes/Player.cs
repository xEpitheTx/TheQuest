using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public class Player : Mover
    {
        private Weapon equippedWeapon;
        public int HitPoints { get; private set; }
        private List<Weapon> inventory = new List<Weapon>();
        public IEnumerable<string> Weapons
        {
            get
            {
                List<string> names = new List<string>();
                foreach (Weapon weapon in inventory)
                {
                    names.Add(weapon.Name);
                }
                return names;
            }
        }

        public Player(Game game, Point point)
            : base(game, point)
        {
            HitPoints = 10;
        }

        public void Hit(int maxDamage, Random random)
        {
            HitPoints -= random.Next(1, maxDamage);
        }

        public void IncreaseHealth(int health, Random random)
        {
            HitPoints += random.Next(1, health);
        }

        /// <summary>
        /// Tells the player to equip one of his weapons. 
        /// The game object calls this method when one of the inventory items is clicked.
        /// </summary>
        /// <param name="weaponName">name of the weapon the player is trying to equip.</param>
        public void Equip(string weaponName)
        {
            foreach (Weapon weapon in inventory)
            {
                if (weapon.Name == weaponName)
                {
                    equippedWeapon = weapon;
                }
            }
        }

        public void Move(Direction direction)
        {
            base.location = Move(direction, game.Boundaries);
            if (!game.WeaponInRoom.PickedUp)
            {
                //see if the weapon is nearby, and possibly pick it up
                //check to see if the weapon is near the player within a single unit of distance.
                //if the weapon is nearby, pick up the weapon and add it to the players inventory.
                //if the weapon is the only one the player has, equip it for them.
                if (Nearby(game.WeaponInRoom.Location, 1))
                {
                    inventory.Add(game.WeaponInRoom);
                    if (!game.CheckPlayerInventory(game.WeaponInRoom.ToString()))
                    {
                        game.WeaponInRoom.PickUpWeapon();
                        if (inventory.Count == 1)
                        {
                            Equip(game.WeaponInRoom.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Figures out which weapon is equipped and calls that weapons attack method.
        /// If the weapon is a potion, Attack removes it from the inventory after the player drinks
        /// If the players doens't have a weapon, this does nothing.
        /// </summary>
        /// <param name="direction">direction to attack in.</param>
        /// <param name="random">figures out the damage to deal.</param>
        public void Attack(Direction direction, Random random)
        {
            if (equippedWeapon != null)
            {
                equippedWeapon.Attack(direction, random);
                if (equippedWeapon is IPotion)
                {
                    //remove from inventory
                    inventory.Remove(equippedWeapon);
                    Equip("Sword");
                }
            }
        }
    }
}
