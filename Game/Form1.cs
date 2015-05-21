using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        Game game;
        Point newPoint;
        Weapon weapon;
        PlayerScore playerScore; // nasz obiekt do zapisania wyniku gracza
        List<PlayerScore> playerScores; //lista wyników naszych graczy
        int roundTime; //czas trwania rundy

        public Form1()
        {
            InitializeComponent();
        }

        #region Obsługa Bazy Danych

        /// <summary>
        /// Metoda Zapisuje wynik gracza do bazy danych
        /// </summary>
        public void SavePlayerScore()
        {
            playerScore = new PlayerScore(game.PlayerName, game.PlayerExperience);
            playerScores = Serialization.Deserializacja();
            playerScores.Add(playerScore);
            Serialization.serialize(playerScores);
        }

        #endregion

        #region Działania na bazie danych (wyświetlanie, sortowanie, przeszukiwanie)

        /// <summary>
        /// Metoda sprawdza, czy dany podany nick występuje już w bazie
        /// </summary>
        /// <param name="nick"></param>
        /// <returns></returns>
        public bool FindNick(string nick)
        {
            playerScores = Serialization.Deserializacja();
            foreach (var player in playerScores)
            {
                if (player.name == nick)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Metoda wyświetla wyniki graczy od najgorszych
        /// </summary>
        public void WriteScoresFromTheBaddest()
        {
            PlayerCompareFromBest comparer = new PlayerCompareFromBest();
            playerScores = Serialization.Deserializacja();
            playerScores.Sort(comparer);
            playerScores.Reverse();
            WriteScores(playerScores);
        }

        /// <summary>
        /// Metoda wyświetla wyniki graczy od najgorszych
        /// </summary>
        public void WriteScoresFromTheBest()
        {
            PlayerCompareFromBest comparer = new PlayerCompareFromBest();
            playerScores = Serialization.Deserializacja();
            playerScores.Sort(comparer);
            WriteScores(playerScores);
        }

        /// <summary>
        /// Przeciążona metoda uzupełniająca panel z listą wyników graczy
        /// </summary>
        /// <param name="playerScores"> lista, którą chcemy wyświetlić </param>
        public void WriteScores(List<PlayerScore> playerScores)
        {
            scoresList.Items.Clear();

            foreach (var playerScore in playerScores)
            {
                scoresList.Items.Add(playerScore.ToString());
            }
        }

        /// <summary>
        /// Metoda uzupełniająca panel z listą wyników graczy
        /// </summary>
        public void WriteScores()
        {
            playerScores = Serialization.Deserializacja();

            scoresList.Items.Clear();

            foreach (var playerScore in playerScores)
            {
                scoresList.Items.Add(playerScore.ToString());
            }
        }

        #endregion

        #region Obsługa rozgrywki

        /// <summary>
        /// Metoda kończąca lewel wcześniejszy i inicjalizująca nowy
        /// </summary>
        public void StartNewGame()
        {
            roundTime = 500;
            int random = RandomUtility.MyRandom.Next(1, 5); //losuje lewel
            HiddenEnemiesPictureBoxes(); //ukrywa pictureboxy wrogów
            InitializeGame(random); //initializuje grę
        }

        /// <summary>
        /// Metoda implementująca nową grę (tworząca gracza)
        /// </summary>
        /// <param name="playerName"></param>
        public void InitializePlayer(string playerName)
        {
            game = new Game(new Rectangle(100, 100, 490, 250), playerName, 295, 300, 10, 20, 10);
            //(obszar gry, nazwa gracza, zycie gracza, pieniadze gracza, doswiadczenie gracza, predkosc, atak)

            enemyMoveTimer.Interval = 300; //reserujemy szybkość timera
            HiddenItemsControls();
            playerPictureBox.Visible = true; //wyswietlamy gracza
            playerNameLabel.Text = playerName; //aktualizujemy label z nazwą gracza
            UpdateInformationAboutPlayer(); //aktualizujemy labele opisujące gracza
            UpdatePlayerPosition(); //aktualizujemy pozycję gracza
        }

        /// <summary>
        /// Metoda tworzaca nowy level
        /// </summary>
        /// <param name="level"></param> //poziom gry
        /// <param name="playerName"></param> //nazwa gracza
        public void InitializeGame(int level)
        {
            HiddenEnemiesPictureBoxes(); //ukrywa pictureBoxy przeciwników
            game.Enemies.Clear();
            game.SpecialWeapons.Clear();
            switch (level)
            {
                case 1:
                    game.AddEnemy(EnemiesTypes.Skeleton);//dodajemy szkielet
                    game.AddSpecialWeapon(SpecialWeaponsTypes.Book); //dodajemy książkę
                    game.AddSpecialWeapon(SpecialWeaponsTypes.Gold); //dodajemu złoto
                    break;

                case 2:
                    game.AddEnemy(EnemiesTypes.Skeleton);//dodajemy szkielet
                    game.AddEnemy(EnemiesTypes.Death);//dodajemy szkielet
                    game.AddSpecialWeapon(SpecialWeaponsTypes.Book); //dodajemy książkę
                    game.AddSpecialWeapon(SpecialWeaponsTypes.Gold); //dodajemu złoto
                    break;

                case 3:
                    game.AddEnemy(EnemiesTypes.Skeleton); //dodajemy szkielet
                    game.AddEnemy(EnemiesTypes.Death); //dodajemy szkielet
                    game.AddEnemy(EnemiesTypes.Ghost); //dodajemy ducha
                    game.AddSpecialWeapon(SpecialWeaponsTypes.Book); //dodajemy książkę
                    game.AddSpecialWeapon(SpecialWeaponsTypes.Gold); //dodajemu złoto
                    break;

                case 4:
                    game.AddEnemy(EnemiesTypes.Skeleton); //dodajemy szkielet
                    game.AddEnemy(EnemiesTypes.Death); //dodajemy szkielet
                    game.AddEnemy(EnemiesTypes.Ghost); //dodajemy ducha
                    game.AddEnemy(EnemiesTypes.Spider); //dodajemy pająka
                    game.AddSpecialWeapon(SpecialWeaponsTypes.Book); //dodajemy książkę
                    game.AddSpecialWeapon(SpecialWeaponsTypes.Gold); //dodajemu złoto
                    break;
            }
            playerPictureBox.Visible = true; //wyswietlamy gracza
            UpdateEnemiesPositions(); //aktualizujemy pozycję wrogów
            UpdateSpecialWeaponsPosition(); //aktualizuje pozycje specjalnych przedmiotów
            enemyMoveTimer.Interval -= 20; //zwiększamy szybkość wykonywania ruchów graczy
            roundTime = 400;
            enemyMoveTimer.Start();
    }

        #region Sprawdzanie statusu gry (Czy powinna się już zakończyć)

        /// <summary>
        /// metoda sprawdza, czy przeciwnicy nadal żyją. Jeżeli nie, ukrywa obiekty sceny które je reprezentują
        /// </summary>
        public void CheckEnemyAlive()
        {
            List<Enemy> enemiesToDelete = game.CheckEnemyAlive();
            foreach (var enemy in enemiesToDelete)
            {
                if (enemy is Skeleton)
                {
                    skeletonPictureBox.Visible = false;
                    skeletonHealths.Visible = false;
                }

                if (enemy is Death)
                {
                    deathPictureBox.Visible = false;
                    DeathHealths.Visible = false;
                }

                if (enemy is Ghost)
                {
                    ghostPictureBox.Visible = false;
                    ghostHealths.Visible = false;
                }

                if (enemy is Spider)
                {
                    spiderPictureBox.Visible = false;
                    spiderHealths.Visible = false;
                }
            }
        }

        /// <summary>
        /// Metoda Sprawdza, czy na mapie znajdują się jeszcze jacyś przeciwnicy. Jak nie, rozpoczyna nowy poziom
        /// </summary>
        public void ChceckAnyEnemyAlive()
        {
            if (!game.CheckAnyEnemyAlive())
            {
                enemyMoveTimer.Stop();
                MessageBox.Show("Wygrałeś!!!");
                StartNewGame();
            }
        }

        /// <summary>
        /// Metoda sprawdza, czy gracz nadal żyje. Jeżeli nie rozpoczyna nową grę
        /// </summary>
        public void CheckPlayerAlive()
        {
            if (game.PlayerHealth <= 0)
            {
                enemyMoveTimer.Stop();
                SavePlayerScore();
                MessageBox.Show("Niestety przegrałeś :(");
                loginPanel.Visible = true;
            }
        }

        /// <summary>
        /// Metoda sprawdza, czy czas się nie śkończył
        /// </summary>
        public void CheckTimeLeft()
        {
            if (roundTime <= 0)
            {
                enemyMoveTimer.Stop();
                SavePlayerScore();
                MessageBox.Show("Koniec czasu :/");
                loginPanel.Visible = true;
            }
        }

        #endregion

        #endregion

        #region Aktulalizacja pozycji elementów formularza

        #region Pozycje wrogów
        /// <summary>
        /// aktualizuje pozycje picture boxow/labelow przeciwnikow
        /// </summary>
        public void UpdateEnemiesPositions()
        {
            foreach (var enemy in game.Enemies)
            {
                if (enemy is Skeleton)
                {
                    skeletonPictureBox.Visible = true; //wyswietlamy szkielet
                    skeletonHealths.Visible = true;
                    skeletonPictureBox.Location = enemy.EnemyLocation; //przypisujemy picture boxowi lokalizację z klasy
                    skeletonHealths.Location = skeletonPictureBox.Location;
                    skeletonHealths.Text = enemy.NumberOfHealths.ToString();
                }

                if (enemy is Death)
                {
                    deathPictureBox.Visible = true;
                    DeathHealths.Visible = true;
                    deathPictureBox.Location = enemy.EnemyLocation;
                    DeathHealths.Location = deathPictureBox.Location;
                    DeathHealths.Text = enemy.NumberOfHealths.ToString();

                }

                if (enemy is Ghost)
                {
                    ghostPictureBox.Visible = true;
                    ghostHealths.Visible = true;
                    ghostPictureBox.Location = enemy.EnemyLocation;
                    ghostHealths.Location = ghostPictureBox.Location;
                    ghostHealths.Text = enemy.NumberOfHealths.ToString();
                }

                if (enemy is Spider)
                {
                    spiderPictureBox.Visible = true;
                    spiderHealths.Visible = true;
                    spiderPictureBox.Location = enemy.EnemyLocation;
                    spiderHealths.Location = spiderPictureBox.Location;
                    spiderHealths.Text = enemy.NumberOfHealths.ToString();
                }
            }
        }
        #endregion

        #region Pozycje Przedmiotów
        /// <summary>
        /// aktualizuje pozycje itemow specjalnych(zloto, książka)
        /// </summary>
        public void UpdateSpecialWeaponsPosition()
        {
            foreach (var specialWeapon in game.SpecialWeapons)
            {
                if (specialWeapon is Book)
                {
                    bookPictureBox.Visible = true;
                    bookPictureBox.Location = specialWeapon.Location;
                }
                if (specialWeapon is Gold)
                {
                    goldPictureBox.Visible = true;
                    goldPictureBox.Location = specialWeapon.Location;
                }
            }
        }
        #endregion

        #region Pozycje gracza
        /// <summary>
        /// aktualizuje pola opisujace gracza(życie, atak, obrona itp.)
        /// </summary>
        public void UpdateInformationAboutPlayer()
        {
            playerHealthLabel.Text = game.PlayerHealth.ToString();
            playerMoneyLabel.Text = game.PlayerMoney.ToString();
            playerExperienceLabel.Text = game.PlayerExperience.ToString();
            playerAttackLabel.Text = game.PlayerAttack.ToString();
            playerDefenceLabel.Text = game.PlayerDefence.ToString();
            numberOfPotionsLabel.Text = game.NumberOfPlayerPotions().ToString();
        }

        /// <summary>
        /// aktualizuje polozenia picture boxa gracza
        /// </summary>
        public void UpdatePlayerPosition()
        {
            newPoint = game.PlayerLocation;
            playerPictureBox.Location = newPoint;
        }
        #endregion

        #region Wyswietlanie pozostałego czasu
        /// <summary>
        /// aktualizacja wyświetlania czasu
        /// </summary>
        public void ActualizeTimeLabel()
        {
            roundTimeLabel.Text = roundTime.ToString();
        }
        #endregion

        #endregion

        #region Zarządzanie widocznościa/właściwościami obiektów sceny

        /// <summary>
        /// Metoda ukrywa kontrolki formularza na starcie gry
        /// </summary>
        public void HiddenItemsControls() 
        {
            playerPictureBox.Visible = false; //gracz
            bookPictureBox.Visible = false; //ksiega
            goldPictureBox.Visible = false; //zloto
            axPictureBox.Visible = false; //topor
            swordWoodenPictureBox.Visible = false; //drweniany miecz
            redPotionPictureBox.Visible = false; //czerwona mikstura
            bluePotionPictureBox.Visible = false; //niebieska mikstura
            shieldPictureBox.Visible = false; //tarcza
            bowPictureBox.Visible = false; //łuk
            numberOfPotionsLabel.Visible = false; //ilość potek
        }

        /// <summary>
        /// Metoda ukrywa picture boxy reprezentujące wrogów
        /// </summary>
        public void HiddenEnemiesPictureBoxes()
        {
            ghostPictureBox.Visible = false; //duch
            ghostHealths.Visible = false;
            skeletonPictureBox.Visible = false; //szkielet
            skeletonHealths.Visible = false;
            spiderPictureBox.Visible = false; //pajak
            spiderHealths.Visible = false;
            deathPictureBox.Visible = false; //smierc
            DeathHealths.Visible = false;
        }

        /// <summary>
        /// Metoda zmienia wartość border syle picture boxów ekwipunku na none
        /// </summary>
        public void ChangeBorderStylesToNone()
        {
            swordWoodenPictureBox.BorderStyle = BorderStyle.None;
            axPictureBox.BorderStyle = BorderStyle.None;
            bowPictureBox.BorderStyle = BorderStyle.None;
        }

        #endregion

        #region Obsługa poruszania/klawiszy pruszania(potworów i gracza)

        /// <summary>
        /// Przemieszcza przeciwników co pewien czas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enemyMoveTimer_Tick(object sender, EventArgs e)
        {
            roundTime -= 1;
            ActualizeTimeLabel();
            CheckTimeLeft();
            game.MoveEnemies();
            DoOnMove();
        }

        /// <summary>
        /// Metoda wywołuje metody potrzebne przy przemieszczaniu się obiektów, wykonywaniu kolejnych części gry
        /// </summary>
        private void DoOnMove()
        {
            UpdatePlayerPosition();
            CheckEnemyAlive();
            UpdateEnemiesPositions();
            game.CheckCollisions();
            UpdateInformationAboutPlayer();
            UpdateSpecialWeaponsPosition();
            ChceckAnyEnemyAlive();
            CheckPlayerAlive();
        }

        /// <summary>
        /// Obsługa klawisza poruszania do góry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveTopButton_Click(object sender, EventArgs e) //up
        {
            game.Move(Direction.Top);
            DoOnMove();
        }

        /// <summary>
        /// Obsługa klawisza poruszania w lewo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveLeftButton_Click(object sender, EventArgs e) //left
        {
            game.Move(Direction.Left);
            DoOnMove();
        }

        /// <summary>
        /// Obsługa klawisza poruszania w prawo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveRightButton_Click(object sender, EventArgs e) //right
        {
            game.Move(Direction.Right);
            DoOnMove();
        }

        /// <summary>
        /// Obsługa klawisza poruszania do dołu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveBottomButton_Click(object sender, EventArgs e) //bottom
        {
            game.Move(Direction.Bottom);
            DoOnMove();
        }
        #endregion

        #region Obsługa przycisku rozpoczęcia gry
        /// <summary>
        /// Obsługa klawisza rozpoczęcia gry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startGameButton_Click(object sender, EventArgs e)
        {
            if ((playerNameTextBox.Text != ""))
            {
                if (FindNick(playerNameTextBox.Text))
                {
                    InitializePlayer(playerNameTextBox.Text);
                    StartNewGame();
                    loginPanel.Visible = false;
                }
                else
                {
                    playerNameTextBox.Text = "Nick już istnieje";
                }
            }
            else
                playerNameTextBox.Text = "Musisz podać nick";
        }
        #endregion

        #region Obsługa przycisków sklepu
        /// <summary>
        /// Obsługa kliknięcia klawisza kupowania miecza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuySteelButton(object sender, EventArgs e)
        {
            weapon = new SwordSteel(200,20);
            if (game.BuyWeapon(weapon))
            {
                swordWoodenPictureBox.Visible = true;
                UpdateInformationAboutPlayer();
            }
        }

        /// <summary>
        /// Obsługa kliknięcia klawisza kupowania toporka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            weapon = new Ax(300, 25);
            if (game.BuyWeapon(weapon))
            {
                axPictureBox.Visible = true;
                UpdateInformationAboutPlayer();
            }
        }

        /// <summary>
        /// Obsługa kliknięcia klawisza kupowania tarczy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buyShieldButton_Click(object sender, EventArgs e)
        {
            weapon = new Shield(150, 15);
            if (game.BuyWeapon(weapon))
            {
                shieldPictureBox.Visible = true;
                UpdateInformationAboutPlayer();
            }
        }

        /// <summary>
        /// Obsługa kliknięcia klawisza kupowania łuku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buyBowButton_Click(object sender, EventArgs e)
        {
            weapon = new Bow(400, 40);
            if (game.BuyWeapon(weapon))
            {
                bowPictureBox.Visible = true;
                UpdateInformationAboutPlayer();
            }
        }

        /// <summary>
        /// Obsługa kliknięcia klawisza kupowania potki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buyPotionButton_Click(object sender, EventArgs e)
        {
            weapon = new RedPotion(50, 50);
            if (game.BuyWeapon(weapon))
            {
                redPotionPictureBox.Visible = true;
                numberOfPotionsLabel.Visible = true;
                UpdateInformationAboutPlayer();
            }
        }
        #endregion

        #region Obługa kliknięć picture boxów
        /// <summary>
        /// Obsługa kliknięcia picture boxa miecza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void swordWoodenPictureBox_Click(object sender, EventArgs e)
        {
            game.ChangePlayerWeapon(WeaponType.SwordSteel);
            UpdateInformationAboutPlayer();
            ChangeBorderStylesToNone();
            swordWoodenPictureBox.BorderStyle = BorderStyle.Fixed3D;
        }

        /// <summary>
        /// obsługa kliknięca picture boxa toporka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axPictureBox_Click(object sender, EventArgs e)
        {
            game.ChangePlayerWeapon(WeaponType.Ax);
            UpdateInformationAboutPlayer();
            ChangeBorderStylesToNone();
            axPictureBox.BorderStyle = BorderStyle.Fixed3D;
        }

        /// <summary>
        /// Obłusga kliknięcia picture boxa tarczy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shieldPictureBox_Click(object sender, EventArgs e)
        {
            game.ChangePlayerWeapon(WeaponType.Shield);
            UpdateInformationAboutPlayer();
            shieldPictureBox.BorderStyle = BorderStyle.Fixed3D;
        }

        /// <summary>
        /// Obsługa kliknięcia picture boxa łuku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bowPictureBox_Click(object sender, EventArgs e)
        {
            game.ChangePlayerWeapon(WeaponType.Bow);
            UpdateInformationAboutPlayer();
            ChangeBorderStylesToNone();
            bowPictureBox.BorderStyle = BorderStyle.Fixed3D;
        }

        /// <summary>
        /// Obsługa kliknięcia picture boxa czerwonej potki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void redPotionPictureBox_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numberOfPotionsLabel.Text) >= 0)
            {
                game.ChangePlayerWeapon(WeaponType.Potion);
            }
            else
            {
                MessageBox.Show("Nie masz żadnej potki!");
            }
            UpdateInformationAboutPlayer();
        }

        #endregion

        #region Obsługa przycisków powiązanych z panelem wyników graczy

        /// <summary>
        /// Obsługa klawisza służącego do wyświetlania panelu z wynikami graczy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showStatsButton_Click(object sender, EventArgs e)
        {
            WriteScores();
            statsPanel.Visible = true;
        }

        /// <summary>
        /// Obsługa klawisza  służącego do ukrycia panelu z wynikami graczy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeStatsPanel_Click(object sender, EventArgs e)
        {
            statsPanel.Visible = false;
        }

        /// <summary>
        /// Obsługa klawisza  służącego do wyświetlania wyników graczy od najlepszego
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sortFromTheBestButton_Click(object sender, EventArgs e)
        {
            WriteScoresFromTheBest();
        }

        /// <summary>
        /// Obsługa klawisza  służącego do wyświetlania wyników graczy od najgorszego
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortFromTheBaddestButton_Click(object sender, EventArgs e)
        {
            WriteScoresFromTheBaddest();
        }

        #endregion
    }
}
