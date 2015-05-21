using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class Game
    {
        Player player; //nowa instancja gracza

        public List<Enemy> Enemies = new List<Enemy>(); //lista przeciwnikow
        public List<SpecialWeapon> SpecialWeapons = new List<SpecialWeapon>(); //lista specjalnych przedmiotow(książki, pieniądze)

        private Rectangle boundaries; // obszar po ktorym porusza się nasza postac
        public Rectangle Boundaries { get { return boundaries; } }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="boundaries"> obszar gry </param>
        /// <param name="Name"> nazwa gracza </param>
        public Game(Rectangle boundaries, string name, int health, int money, int experience, int speed, int attack)
        {
            this.boundaries = boundaries;
            player = new Player(new Point(boundaries.Left + 10, boundaries.Top + 10), name, health, money, experience, speed, attack);
        }

        #region Obsługa gracza

        public Point PlayerLocation { get { return player.PlayerLocation; } }
        public string PlayerName { get { return player.Name; } }
        public int PlayerHealth { get { return player.Health; } }
        public int PlayerMoney { get { return player.Money; } }
        public int PlayerExperience { get { return player.Experience; } }
        public int PlayerAttack { get { return player.Attack; } }
        public int PlayerDefence { get { return player.Defence; } }
        public List<Weapon> PlayerWeapons { get { return player.Weapons; } }

        /// <summary>
        /// Metoda zmienia przedmiot, którym posługuje się gracz
        /// </summary>
        /// <param name="weapon"></param>
        public void ChangePlayerWeapon(WeaponType weapon)
        {
            foreach (var item in PlayerWeapons)
            {
                if (weapon == WeaponType.SwordSteel)
                {
                    if (item is SwordSteel)
                    {
                        player.GiveNewAttackItem(item.ReturnSpecialProperty());
                    }
                }

                if (weapon == WeaponType.Bow)
                {
                    if (item is Bow)
                    {
                        player.GiveNewAttackItem(item.ReturnSpecialProperty());
                    }
                }

                if (weapon == WeaponType.Ax)
                {
                    if (item is Ax)
                    {
                        player.GiveNewAttackItem(item.ReturnSpecialProperty());
                    }
                }

                if (weapon == WeaponType.Shield)
                {
                    if (item is IDefendItem)
                    {
                        player.GiveNewDefenceItem(item.ReturnSpecialProperty());
                    }
                }

                if (weapon == WeaponType.Potion)
                {
                    if (item is IPotion)
                    {
                        player.Hit(-item.ReturnSpecialProperty());
                        player.DeletePotion();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Metoda zwracająca liczbę potek, które posiada gracz
        /// </summary>
        /// <returns></returns>
        public int NumberOfPlayerPotions()
        {
            int temp = 0;
            foreach (var item in PlayerWeapons)
            {
                if (item is IPotion)
                {
                    temp++;
                }
            }
            return temp;
        }

        #endregion

        #region Metody służące do obsługi obiektów gry

        /// <summary>
        /// Metoda przekazuje polecenie poruszania do obiektu gracza
        /// </summary>
        /// <param name="direction"> kierunek </param>
        public void Move(Direction direction)
        {
            player.Move(direction, boundaries);
        }

        /// <summary>
        /// Metoda przekazuje polecenie poruszania do obiektów potworów
        /// </summary>
        public void MoveEnemies()
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Move(boundaries);
            }
        }

        /// <summary>
        /// Metoda służąca do inizjalizacji wrogów
        /// </summary>
        /// <param name="enemyType"></param> //rodzaj przeciwnika
        public void AddEnemy(EnemiesTypes enemyType)
        {
            if (enemyType == EnemiesTypes.Skeleton)
            {
                Enemies.Add(new Skeleton(new Point(boundaries.Left + 400, boundaries.Top + 30), 60, 10, 10));
                                        //pozycja wroga, nazwa wroga, zdrowie wroga, predkość wroga, atak wroga
            }

            if (enemyType == EnemiesTypes.Death)
            {
                Enemies.Add(new Death(new Point(boundaries.Left + 10, boundaries.Top + 220), 50, 15, 30));
            }

            if (enemyType == EnemiesTypes.Ghost)
            {
                Enemies.Add(new Ghost(new Point(boundaries.Left + 480, boundaries.Top + 220), 40, 20 , 50));
            }
            if (enemyType == EnemiesTypes.Spider)
            {
                Enemies.Add(new Spider(new Point(boundaries.Left + 440, boundaries.Top + 180), 30, 25 , 70));
            }
        }

        /// <summary>
        /// Metoda służąca do inicjalizacji przedmiotów specjalnych
        /// </summary>
        /// <param name="specialWeapon"></param> //rodzaj specjalnego przedmiotu
        public void AddSpecialWeapon(SpecialWeaponsTypes specialWeapon)
        {
            if (specialWeapon == SpecialWeaponsTypes.Book)
            {
                SpecialWeapons.Add(new Book(20));
                                           //ilość doświadczenia, które dostaniemu jak podniesiemy książkę
            }
            if (specialWeapon == SpecialWeaponsTypes.Gold)
            {
                SpecialWeapons.Add(new Gold(10));
                                           //ilość złota, które dostaniemy jak podniesiemy sztabkę
            }
        }

        /// <summary>
        /// Metoda sprawdza, czy występują kolizja pomiędzy obiektami. 
        /// Jak tak wykonuje dla danych kolizcji odpowiednie czynności.
        /// </summary>
        public void CheckCollisions() //Sprawdzanie, czy obiekty gry kolidują z obiektem gracza
        {
            foreach (var specjalWeapon in SpecialWeapons) //kolizje przedmiot - gracz
            {
                if (CollisionChecker.CheckColision(PlayerLocation, specjalWeapon.Location))
                {
                    if (specjalWeapon is Book)
                    {
                        player.GiveExperience(specjalWeapon.ReturnSpecialValue()); //aktualizujemy staty gracza
                        specjalWeapon.ChangeItemLocation(); //respimy item
                    }
                    if (specjalWeapon is Gold)
                    {
                        player.GiveMoney(specjalWeapon.ReturnSpecialValue()); //aktualizujemy staty gracza
                        specjalWeapon.ChangeItemLocation(); //respimy item
                    }
                }
            }
            foreach (var enemy in Enemies) //kolizje potwór - gracz
            {
                if (CollisionChecker.CheckColision(PlayerLocation, enemy.EnemyLocation))
                {
                    if (enemy is Skeleton)
                    {
                        player.Hit(enemy.AttackPower());
                        enemy.Hit(PlayerAttack);
                    }
                    if (enemy is Death)
                    {
                        player.Hit(enemy.AttackPower());
                        enemy.Hit(PlayerAttack);
                    }
                    if (enemy is Spider)
                    {
                        player.Hit(enemy.AttackPower());
                        enemy.Hit(PlayerAttack);
                    }
                    if (enemy is Ghost)
                    {
                        player.Hit(enemy.AttackPower());
                        enemy.Hit(PlayerAttack);
                    }
                }
            }
        }

        /// <summary>
        /// Metoda sprawdza, czy przeciwnicy nadal żyją. 
        /// Martwi zostają usunięci z listy przeciwników znajdujących się na planszy
        /// </summary>
        /// <returns>zwraca listę martwych przeciwników, do usunięcia z formularza</returns>
        public List<Enemy> CheckEnemyAlive()
        {
            List<Enemy> enemiesToDelete = new List<Enemy>();
            foreach (var enemy in Enemies)
            {
                if (!enemy.Live())
                {
                    enemiesToDelete.Add(enemy);
                }
            }
            foreach (var enemy in enemiesToDelete)
            {
                Enemies.Remove(enemy);
            }
            return enemiesToDelete;
        }

        /// <summary>
        /// Metoda sprawdza, czy lista przeciwników znajdujących się na planszy jest pusta
        /// </summary>
        /// <returns> czy na planszy znajdują się przeciwnicy </returns>
        public bool CheckAnyEnemyAlive()
        {
            if (Enemies.Count == 0)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Obsługa sklepu

        /// <summary>
        /// Metoda służąca do kupowania przedmiotu
        /// </summary>
        /// <param name="weaponType"></param>
        public bool BuyWeapon(Weapon weaponType)
        {
            if (weaponType is IPotion) //jak jest potion, to możemy kupić więcej niż raz
            {
                if (weaponType.Price <= player.Money)
                {
                    player.Weapons.Add(weaponType);
                    player.GiveMoney(-weaponType.ReturnPrice());
                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Masz za mało złota");
                    return false;
                }
            }
            else
            {
                bool temp = false;
                foreach (var item in PlayerWeapons)
                {
                    if (item.GetType() == weaponType.GetType())
                    {
                        temp = true;
                    }
                }
                //temp = PlayerWeapons.Contains(weaponType);
                if (temp)
                {
                    System.Windows.Forms.MessageBox.Show("Masz już ten przedmiot");
                    return false;
                }
                else
                {
                    if (weaponType.Price <= player.Money)
                    {
                        player.Weapons.Add(weaponType);
                        player.GiveMoney(-weaponType.ReturnPrice());
                        return true;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Masz za mało złota");
                        return false;
                    }
                }
            }
        }

        #endregion
    }
}
