using System;
using System.Drawing;
using System.Windows.Forms;

namespace TheQuest
{
    public partial class Form1 : Form
    {
        private Game game;
        private Random random = new Random();

        enum WeaponTypes
        {
            Weapon,
            Potion,
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(new Rectangle(78, 57, 420, 155));
            game.NewLevel(random);
            UpdateCharacters();
        }

        private void swordPictureBox_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(swordPictureBox, Sword.WeaponName);
            SetWeaponTypeControls(WeaponTypes.Weapon);
        }

        private void macePictureBox_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(macePictureBox, Mace.WeaponName);
            SetWeaponTypeControls(WeaponTypes.Weapon);
        }

        private void redPotionPictureBox_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(redPotionPictureBox, RedPotion.WeaponName);
            SetWeaponTypeControls(WeaponTypes.Potion);
        }

        private void bowPictureBox_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(bowPictureBox, Bow.WeaponName);
            SetWeaponTypeControls(WeaponTypes.Weapon);
        }
        private void bluePotionPictureBox_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(bluePotionPictureBox, BluePotion.WeaponName);
            SetWeaponTypeControls(WeaponTypes.Potion);
        }

        private void SelectInventoryItem(PictureBox picturebox, string weaponName)
        {
            if (game.CheckPlayerInventory(weaponName))
            {
                game.Equip(weaponName);
                DisableSelection(picturebox);
            }
        }

        private void SetWeaponTypeControls(WeaponTypes weaponType)
        {
            switch (weaponType)
            {
                case WeaponTypes.Weapon:
                    SetControlsForWeapon();
                    break;
                case WeaponTypes.Potion:
                    SetControlsForPotion();
                    break;
                default:
                    break;
            }
        }

        private WeaponTypes SelectedWeapon()
        {
            if (redPotionPictureBox.BorderStyle == BorderStyle.FixedSingle
                || bluePotionPictureBox.BorderStyle == BorderStyle.FixedSingle)
                return WeaponTypes.Potion;
            else
                return WeaponTypes.Weapon;
        }

        private void SetControlsForWeapon()
        {
            attackUp.Text = "↑";
            attackRight.Visible = true;
            attackDown.Visible = true;
            attackLeft.Visible = true;
        }

        private void SetControlsForPotion()
        {
            attackUp.Text = "Drink";
            attackRight.Visible = false;
            attackDown.Visible = false;
            attackLeft.Visible = false;
        }

        private void DisableSelection(PictureBox thingToSelect)
        {
            swordPictureBox.BorderStyle = BorderStyle.None;
            bluePotionPictureBox.BorderStyle = BorderStyle.None;
            bowPictureBox.BorderStyle = BorderStyle.None;
            redPotionPictureBox.BorderStyle = BorderStyle.None;
            macePictureBox.BorderStyle = BorderStyle.None;

            thingToSelect.BorderStyle = BorderStyle.FixedSingle;
        }

        private void moveLeft_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Left, random);
            UpdateCharacters();
        }

        private void moveUp_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Up, random);
            UpdateCharacters();
        }

        private void moveRight_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Right, random);
            UpdateCharacters();
        }

        private void moveDown_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Down, random);
            UpdateCharacters();
        }

        private void attackUp_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Up, random);
            UpdateCharacters();
            var weaponType = SelectedWeapon();
            switch (weaponType)
            {
                case WeaponTypes.Potion:
                    swordPictureBox_Click(null, null);
                    break;
            }
        }

        private void attackRight_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Right, random);
            UpdateCharacters();
        }

        private void attackDown_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Down, random);
            UpdateCharacters();
        }

        private void attackLeft_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Left, random);
            UpdateCharacters();
        }

        public void UpdateCharacters()
        {
            SetPlayer();
            SetEnemies();
            EnableDungeonItemsUI();
            EnableInventoryUI();
            CheckForGameOver();
            CheckForLevelWon(SetEnemies());

        }

        public bool UpdateEnemyLocation(Enemy enemy, PictureBox enemyPictureBox, Label hitPoints)
        {
            enemyPictureBox.Location = enemy.Location;
            hitPoints.Text = enemy.HitPoints.ToString();
            enemyPictureBox.Visible = enemy.HitPoints > 0;
            return enemy.HitPoints > 0;
        }

        private void EnableInventoryUI()
        {
            swordPictureBox.Visible = false;
            bowPictureBox.Visible = false;
            bluePotionPictureBox.Visible = false;
            redPotionPictureBox.Visible = false;
            macePictureBox.Visible = false;
            foreach (string weapon in game.PlayerWeapons)
            {
                if (game.CheckPlayerInventory(weapon))
                {
                    switch (weapon)
                    {
                        case Sword.WeaponName:
                            swordPictureBox.Visible = true;
                            break;
                        case Bow.WeaponName:
                            bowPictureBox.Visible = true;
                            break;
                        case BluePotion.WeaponName:
                            bluePotionPictureBox.Visible = true;
                            break;
                        case RedPotion.WeaponName:
                            redPotionPictureBox.Visible = true;
                            break;
                        case Mace.WeaponName:
                            macePictureBox.Visible = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void EnableDungeonItemsUI()
        {
            swordDungeon.Visible = false;
            bowDungeon.Visible = false;
            bluePotionDungeon.Visible = false;
            redPotionDungeon.Visible = false;
            maceDungeon.Visible = false;
            Control weaponControl = null;
            switch (game.WeaponInRoom.Name)
            {
                case Sword.WeaponName:
                    weaponControl = swordDungeon;
                    break;
                case Bow.WeaponName:
                    weaponControl = bowDungeon;
                    break;
                case BluePotion.WeaponName:
                    weaponControl = bluePotionDungeon;
                    break;
                case RedPotion.WeaponName:
                    weaponControl = redPotionDungeon;
                    break;
                case Mace.WeaponName:
                    weaponControl = maceDungeon;
                    break;
                default:
                    break;
            }
            weaponControl.Visible = true;
            PickUpWeapon(weaponControl);
        }

        private void CheckForGameOver()
        {
            if (game.PlayerHitPoints <= 0)
            {
                MessageBox.Show("You died");
                Application.Exit();
            }
        }

        private void CheckForLevelWon(int enemiesShown)
        {
            if (enemiesShown < 1)
            {
                MessageBox.Show("You have defeated the enemies on this level");
                game.NewLevel(random);
                UpdateCharacters();
            }
        }

        private void SetPlayer()
        {
            playerPictureBox.Visible = true;
            playerPictureBox.Location = game.PlayerLocation;
            playerHitPoints.Text = game.PlayerHitPoints.ToString();
        }

        private int SetEnemies()
        {
            int enemiesShown = 0;
            foreach (Enemy enemy in game.Enemies)
            {
                if (enemy is Bat bat
                    && UpdateEnemyLocation(bat, batPictureBox, batHitPoints))
                {
                    enemiesShown++;
                }
                if (enemy is Ghost ghost
                    && UpdateEnemyLocation(ghost, ghostPictureBox, ghostHitPoints))
                {
                    enemiesShown++;
                }
                if (enemy is Ghoul ghoul
                    && UpdateEnemyLocation(ghoul, ghoulPictureBox, ghoulHitPoints))
                {
                    enemiesShown++;
                }
            }
            return enemiesShown;
        }

        private void PickUpWeapon(Control weaponControl)
        {
            weaponControl.Location = game.WeaponInRoom.Location;
            if (game.WeaponInRoom.PickedUp)
            {
                weaponControl.Visible = false;
            }
        }
    }
}
