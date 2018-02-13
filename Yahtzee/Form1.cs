using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Yahtzee
{
    public partial class Yahtzee : Form
    {
        #region Declaration
        Image[] diceimages; //declare dice image array.
        int[] dice;//Declare dice array for storing values.
        int[] DS; //Declare DiceSorted array.
        Random rand;//Declare rand.
        int[] LockedDice;//Declare locked dice array for storing values.
        int[] Score;//Declare Score array for scoring.
        int[] CalculatedScore;//Declare Calculated scoring array .
        int RollCount;//Declare Rollcount to disable roll button after three rolls.
        bool BonusYahtzeeTrue = false;//Declare bonusYahtzee to be false.
        bool InfiniRoll;//Declare infiniRoll for later on.
        ToolTip ToolTip1;//Declare Tolltip for showing scores on hover over.
        int EndGameProgress = 0;//Stores progress of game to calculate when game over.
        #endregion

        public Yahtzee()
        {
            InitializeComponent();
        }

        private void MenuRules_Click(object sender, EventArgs e)//Function to handle help form.
        {
            HelpForm HF = new HelpForm();//Initializes help form.
            HF.Show();//Shows help form.
        }

        private void MenuExit_Click(object sender, EventArgs e)//Function for when game exit is clicked.
        {
            this.Close();//Closes game.
        }

        private void btnRollDice_Click(object sender, EventArgs e)//Function for RollButtonClick
        {
            RollDice();//Call rolldice function.
        }

        private void Yahtzee_Load(object sender, EventArgs e)//Function loaded at start of application.
        {
            diceimages = new Image[7];//Initializes Image array for dice.
            //For space images
            diceimages[0] = Properties.Resources.die_face_blank;//Blank dice image.
            diceimages[1] = Properties.Resources.space_1;//Dice image 1
            diceimages[2] = Properties.Resources.space_2;//Dice image 2
            diceimages[3] = Properties.Resources.space_3;//Dice image 3
            diceimages[4] = Properties.Resources.space_4;//Dice image 4
            diceimages[5] = Properties.Resources.space_5;//Dice image 5
            diceimages[6] = Properties.Resources.space_6;//Dice image 6

            dice = new int[5] { 0, 0, 0, 0, 0 };//Initializes Dice array for storing dice values.
            DS = new int[5] { 0, 0, 0, 0, 0 };//initialize DiceSorted array
            LockedDice = new int[5] { 0, 0, 0, 0, 0 };//Initialize Locked dice array, 1 == Locked.
            Score = new int[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};//initialize Score array for actual game score.
            //Score = new int[18] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; //TEST.
            CalculatedScore = new int[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//Initialize Calculated score array.

            #region ScoreAssigns
            /*----------------------
             * 0 = Ones
             * 1 = Twos  
             * 2 = Threes
             * 3 = Fours
             * 4 = Fives
             * 5 = Sixes
             * ---------------------
             * 6 = Sum
             * 7 = Bonus
             * ---------------------
             * 8 = Three of a kind
             * 9 = Four of a kind
             * 10 = Full House
             * 11 = Small Straight
             * 12 = Large Straight
             * 13 = Chance
             * 14 = Yahtzee
             * 15 = Bonus Yahtzee
             * ---------------------
             * 16 = Lower Total
             * 17 = Total score
             */
            #endregion
            rand = new Random();//initialize rand.
            ScoreCalculation();//Calculate score.


            ToolTip1 = new ToolTip();
            //Set delays for tool tip.
            ToolTip1.AutomaticDelay = 100;
            //Force Tool Tip to remain even if form not active.
            ToolTip1.ShowAlways = true;
        }
        private void RollDice()//Function to call when rolling dice.
        {
            RollCount++;//Count up roll dice.
            for(int i =0; i<= 4; i++)//For loop between 0 and 4 on each dice to calculate its random value.
            {
                if (LockedDice[i] == 0)//If dice isn't locked:
                    dice[i] = rand.Next(1, 7);//Then assign a random value between 1 and 6.
            }
            ImageUpdate();//Update images after dice numbers generated.

        }

        private void ImageUpdate()//Function to update images.
        {
            if (LockedDice[0] == 0)//IF Dice 1 isn't locked
            {
                imgDiceRolled1.Image = diceimages[dice[0]];//Dice Rolled 1 = Image dice[0].
                imgDiceLocked1.Image = diceimages[0];//Dice Locked 1 = BlankImage
            }
            else//Else Dice 1 must be locked
            {
                imgDiceLocked1.Image = diceimages[dice[0]];//Dice Locked 1 = Image dice[0].
                imgDiceRolled1.Image = diceimages[0];//Dice Rolled 1 = BlankImage
            }
            if (LockedDice[1] == 0)//IF Dice 2 isn't locked
            {
                imgDiceRolled2.Image = diceimages[dice[1]];//Dice Rolled 2 = Image dice[1].
                imgDiceLocked2.Image = diceimages[0];//Dice Locked 2 = BlankImage
            }
            else//Else Dice 2 must be locked
            {
                imgDiceLocked2.Image = diceimages[dice[1]];//Dice Locked 2 = Image dice[1].
                imgDiceRolled2.Image = diceimages[0];//Dice Rolled 2 = BlankImage
            }
            if (LockedDice[2] == 0)//IF Dice 3 isn't locked
            {
                imgDiceRolled3.Image = diceimages[dice[2]];//Dice Rolled 3 = Image dice[2].
                imgDiceLocked3.Image = diceimages[0];//Dice Locked 3 = BlankImage
            }
            else//Else Dice 3 must be locked
            {
                imgDiceLocked3.Image = diceimages[dice[2]];//Dice Locked 3 = Image dice[2].
                imgDiceRolled3.Image = diceimages[0];//Dice Rolled 3 = BlankImage
            }
            if (LockedDice[3] == 0)//IF Dice 4 isn't locked
            {
                imgDiceRolled4.Image = diceimages[dice[3]];//Dice Rolled 4 = Image dice[3].
                imgDiceLocked4.Image = diceimages[0];//Dice Locked 4 = BlankImage
            }
            else//Else Dice 4 must be locked
            {
                imgDiceLocked4.Image = diceimages[dice[3]];//Dice Locked 4 = Image dice[3].
                imgDiceRolled4.Image = diceimages[0];//Dice Rolled 4 = BlankImage
            }
            if (LockedDice[4] == 0)//IF Dice 5 isn't locked
            {
                imgDiceRolled5.Image = diceimages[dice[4]];//Dice Rolled 5 = Image dice[4].
                imgDiceLocked5.Image = diceimages[0];//Dice Locked 5 = BlankImage
            }
            else//Else Dice 5 must be locked
            {
                imgDiceLocked5.Image = diceimages[dice[4]];//Dice Locked 5 = Image dice[4].
                imgDiceRolled5.Image = diceimages[0];//Dice Rolled 5 = BlankImage
            }


            ScoreCalculation();//Calculate Score after images updated.

            //Update tooltips:
            ToolTip1.SetToolTip(btnOnes, CalculatedScore[0].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnTwos, CalculatedScore[1].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnThrees, CalculatedScore[2].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnFours, CalculatedScore[3].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnFives, CalculatedScore[4].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnSixes, CalculatedScore[5].ToString());//Update tooltip

            ToolTip1.SetToolTip(btnThreeOfAKind, CalculatedScore[8].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnFourOfAKind, CalculatedScore[9].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnFullHouse, CalculatedScore[10].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnSmallStraight, CalculatedScore[11].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnLargeStraight, CalculatedScore[12].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnChance, CalculatedScore[13].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnYahtzee, CalculatedScore[14].ToString());//Update tooltip
            ToolTip1.SetToolTip(btnBonusYahtzee, CalculatedScore[15].ToString());//Update tooltip
        }

        private void btnDiceLocked1(object sender, MouseEventArgs e)//Button 1 dice locked function.
        {
            LockedDice[0] = 0;//Unlocks dice 1.
            ImageUpdate();//Updates images.
        }
        private void btnDiceLocked2(object sender, MouseEventArgs e)//Button 2 dice locked function.
        {
            LockedDice[1] = 0;//Unlocks dice 2.
            ImageUpdate();//Updates images.
        }
        private void btnDiceLocked3(object sender, MouseEventArgs e)//Button 3 dice locked function.
        {
            LockedDice[2] = 0;//Unlocks dice 3.
            ImageUpdate();//Updates images.
        }
        private void btnDiceLocked4(object sender, MouseEventArgs e)//Button 4 dice locked function.
        {
            LockedDice[3] = 0;//Unlocks dice 4.
            ImageUpdate();//Updates images.
        }
        private void btnDiceLocked5(object sender, MouseEventArgs e)//Button 5 dice locked function.
        {
            LockedDice[4] = 0;//Unlocks dice 5.
            ImageUpdate();//Updates images.
        }
        private void btnDiceRolled1(object sender, MouseEventArgs e)//Button 1 dice Rolled function.
        {
            LockedDice[0] = 1;//Lock dice 1.
            ImageUpdate();//Updates images.
        }
        private void btnDiceRolled2(object sender, MouseEventArgs e)//Button 2 dice Rolled function.
        {
            LockedDice[1] = 1;//Lock dice 2.
            ImageUpdate();//Updates images.
        }
        private void btnDiceRolled3(object sender, MouseEventArgs e)//Button 3 dice Rolled function.
        {
            LockedDice[2] = 1;//Lock dice 3.
            ImageUpdate();//Updates images.
        }
        private void btnDiceRolled4(object sender, MouseEventArgs e)//Button 4 dice Rolled function.
        {
            LockedDice[3] = 1;//Lock dice 4.
            ImageUpdate();//Updates images.
        }
        private void btnDiceRolled5(object sender, MouseEventArgs e)//Button 5 dice Rolled function.
        {
            LockedDice[4] = 1;//Lock dice 5.
            ImageUpdate();//Updates images.
        }

        private void ScoreCalculation()
        {
            //sort dice
            for (int a = 0; a < 5; a++)//Assigns Dice values to DiceSorted(DS) array.
            {
                DS[a] = dice[a];//DiceSorted = dice.
            }
            Array.Sort(DS);//Sort DS array.

            if (RollCount >= 3 && InfiniRoll == false)//If roll count is equal to 3 and infiniroll disabled.
            {
                    btnRollDice.Enabled = false;//disable roll button.
            }
            btnRollDice.Text = "Launch Rockets! " + Environment.NewLine + RollCount + " of 3";// display how many rolls you have left on the roll button.
            //
            //Upper House.
            //
            CalculateUpperDice();//Function to calculate Upper count of...
            CalculateUpperHouseTotal();//Function to calculate Upper house total, bonus, Lower total and game total.
            //
            //Lower House.
            //
            CalculateFullHouse();//Function to calculate FullHouse.
            CalculateThreeofaKind();//Function to calculate Three of a kind.
            CalculateFourofaKind();//Function to calculate  Four of a kind.
            CalculateSmallStraight();//Function to calculate Small Straight.
            CalculateLargeStraight();//Function to calculate Large Straight.
            CalculateChance();//Function to calculate Chance.
            CalculateYahtzee();//Function to calculate Yahtzee
            CalculateYahtzeeBonus();//Function to calculate Bonus Yahtzee.

            if((EndGameProgress == 13 && Score[14] == 0) || EndGameProgress == 14)// If all buttons have been pressed and yahtzee has not been recieved then bonus yahtzee doesn't matter. OR All buttons have been pressed including bonus yahtzee.
            {
                EndGameForm EndForm = new EndGameForm(Score[17].ToString());//Initialize form and send score.
                EndForm.Show();//Show Endgame form.
                this.Visible = false;//Hides this form.
                NewGame();//resets scores for new game If returning for new game.
            }
            
        }
        private void CalculateUpperDice()//Routine to calculate Upper dice(Count of ones to count of sixes).
        {
            for (int i = 1; i <= 6; i++)//For loop between 1 and 6.
            {
                int sum = 0;// Sum = 0.
                for (int j = 0; j <= 4; j++)//For loop between 0 and 4.
                {
                    if(dice[j] == i)// IF dice j == i then:
                    {
                        sum += dice[j];//sum = sum + dice j.
                    }
                }
                CalculatedScore[i - 1] = sum;//Score = sum.
            }
        }
        private void CalculateUpperHouseTotal()//Routine to calculate Upper house total/Upper Bonus/Lower Total/Total.
        {
            //UPPERHOUSE TOTAL BEGIN:
            int Score1 = 0;//Score1 = o.
            for (int i = 0; i <= 5; i++)//for loop between 0 and 5.
            {
                Score1 += Score[i];// Score1 = Score1 + Score i.
            }
            lblSumScore.Text = Score1.ToString();//LABLE upper total = Score1 as a string.
            Score[6] = Score1;//Score 6 = Score1
            //UPPERHOUSE TOTAL END


            //UPPERHOUSE BONUS BEGIN:
            if (Score[6] >= 63)//If Score 6 greater than or equal to 63:
            {
                lblBonusScore.Text = "35";//Lable BonusScore displays 35.
                Score[7] = 35;// Score 7 = 35.
            }
            //UPPERHOUSE BONUS END


            //LOWERHOUSE TOTAL BEGIN:
            int score2 = 0;//score2 =0
            for(int k = 8; k <= 15; k++)//For loop between 8 and 15.
            {
                score2 += Score[k];//score2 = score2 + Score k.
            }
            Score[16] = score2;//lScore 16 = score2.
            //LOWERHOUSE TOTAL END


            //GAME TOTAL BEGIN:
             Score[17] = Score[6] + Score[7] + Score[16];// Total score = Upper Total + Upper Bonus +Lower Total.
            lblTotalScore.Text = Score[17].ToString();//Lable total score displays totalScore1.
        }
        private void CalculateThreeofaKind() //Routine to calculate Three of a Kind.
        {
            int sum = 0;//Reset count
            bool ThreeofaKind = false;//Reset count
            for (int i = 1; i <= 6; i++) //Set dice value to look for.
            {
                int count = 0;
                for (int j = 0; j <= 4; j++) //Search through dice array to find set dice value.
                {
                    if (dice[j] == i) //If dice value found.
                    {
                        count++; //Plus 1 to the count
                    }
                    if (count > 2) //if three or more of the same dice found.
                    {
                        ThreeofaKind = true; //three of a kind is true.
                    }
                }
            }
            if (ThreeofaKind == true) //If three of a kind found add up dice.
            {
                for (int k = 0; k < 5; k++)//for loop.
                {
                    sum += dice[k];//adds dice k value to sum.
                }
                CalculatedScore[8] = sum;// sets array to sum.
            }
            else //else set score to 0.
            {
                CalculatedScore[8] = 0;//Score = 0.
            }
        }
        private void CalculateFourofaKind() //Routine to calculate Four of a Kind.
        {
            int sum = 0;//sum = 0.
            bool FourOfAKind = false;//Fourofakind = FALSE.
            for (int i=1; i <= 6; i++)//for loop between 1 AND 6.
            {
                int count = 0;//Count = 0.
                for(int j= 0; j<=4; j++)//For loop between 0 AND 4
                {
                    if(dice[j] == i)//if dice J == i
                    {
                        count++;//add 1 to count.
                    }
                    if (count > 3)//If count is greater than 3.
                    {
                        FourOfAKind = true;//Four of a kind = True.
                    }
                }
            }
            if(FourOfAKind == true)//If four of a kind == True:
            {
                for(int k =0; k < 5; k++)//for loop between 0 and 5.
                {
                    sum += dice[k];//Sum = sum + dice k.
                }
                CalculatedScore[9] = sum;//score 9 = sum.
            }
            else//Do this if IF satement not met.
            {
                CalculatedScore[9] = 0;//Score 9 = 0.
            }
        }
        private void CalculateFullHouse() //Routine to calculate Full House.
        {
            CalculatedScore[10] = 0;//sets score to 0.
            if ((((DS[0] == DS[1]) && (DS[1] == DS[2])) && (DS[3] == DS[4]) && (DS[2] != DS[3])) ||((DS[0] == DS[1]) && ((DS[2] == DS[3]) && (DS[3] == DS[4])) && (DS[1] != DS[2])))//Checks to see if statement is true.
            {
                CalculatedScore[10] = 25;// If True Score = 25.
            }
        }
        private void CalculateSmallStraight()//Routine to calculate Short Straight.
        {
            CalculatedScore[11] = 0;//Score = 0.

            if (((DS[4] == (DS[3] + 1)) && (DS[4] == (DS[2] + 2)) && (DS[4] == (DS[1] + 3))) ||//Checks to see if condition is true.
                ((DS[4] == (DS[3] + 1)) && (DS[4] == (DS[2] + 2)) && (DS[4] == (DS[0] + 3))) ||
                ((DS[4] == (DS[3] + 1)) && (DS[4] == (DS[1] + 2)) && (DS[4] == (DS[0] + 3))) ||
                ((DS[4] == (DS[2] + 1)) && (DS[4] == (DS[1] + 2)) && (DS[4] == (DS[0] + 3))) ||
                ((DS[3] == (DS[2] + 1)) && (DS[3] == (DS[1] + 2)) && (DS[3] == (DS[0] + 3))))
            {
                CalculatedScore[11] = 30;//If true Score = 30
            }
        }
        private void CalculateLargeStraight()//Routine to calculate Large Straight.
        {
            CalculatedScore[12] = 0;//Score = 0.
            if (((DS[0] == 1) && (DS[1] == 2) && (DS[2] == 3) && (DS[3] == 4) && (DS[4] == 5)) ||//Checks to see if condition is true.
                ((DS[0] == 2) && (DS[1] == 3) && (DS[2] == 4) && (DS[3] == 5) && (DS[4] == 6)))
            {
                CalculatedScore[12] = 40;//If true Score = 40.
            }
        }
        private void CalculateChance()//Routine to calculate chance.
        {
            CalculatedScore[13] = 0;//Score 13 = 0.
            int sum = 0;//sum = 0.
            for(int i = 0; i <= 4; i++)//for loop between 0 and 4.
            {
                sum += dice[i];//sum == sum + dice value.
            }
            CalculatedScore[13] = sum;//Score 13 = sum.
        }
        private void CalculateYahtzee()//Routine to calculate Yahtzee.
        {
            CalculatedScore[14] = 0;//Score 14 = 0.
            for (int i = 1; i <= 6; i++)//For loop between 1 and 6.
            {
                int Count = 0;//count = 0.
                for (int j = 0; j < 5; j++)//for loop between 0 and 5.
                {
                    if (dice[j] == i)// if dice = j.
                        Count++;//Adds 1 to count.

                    if (Count > 4)//If count is greater then 4
                        CalculatedScore[14] = 50;//Score 14 = 50.
                }
            }
        }
        private void CalculateYahtzeeBonus()//Routine to calculate Bonus Yahtzee.
        {
            CalculatedScore[15] = 0;//score 15 = 0.
            for (int i = 1; i <= 6; i++)//for loop between 1 and 6.
            {
                int Count = 0;// count = 0.
                for (int j = 0; j < 5; j++)//For loop between 0 and 5.
                {
                    if (dice[j] == i)// if dice j = i.
                        Count++;//add 1 to Count

                    if ((Count > 4) && (Score[14] > 0))// If count is greater than 4 AND Score 14 is Greater than 0.
                    {
                        CalculatedScore[15] = 100;//Score 15 = 100.
                    }
                    if((Score[14] > 0) && (BonusYahtzeeTrue == false))//Enables button IF Yahtzee has been recieved AND BonusYahtze has not been recieved.
                    {
                        btnBonusYahtzee.Enabled = true;//Enables button
                    }
                    else
                        btnBonusYahtzee.Enabled = false;//Else disable button.
                }
            }
        }

        //Lower House Buttons:
        private void btnFourOfAKind_Click(object sender, EventArgs e)//Button Four of a kind Click function.
        {
            Score[9] = CalculatedScore[9]; //Score 9 = CalcScore 9.
            btnFourOfAKind.Enabled = false;//Disable button.
            lblFourOfAKindScore.Text = Score[9].ToString();// Display Score 9 on Four of a kind.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnThreeOfAKind_Click(object sender, EventArgs e)//Button Three of a kind Click function.
        {
            Score[8] = CalculatedScore[8];//Score 8 = CalcScore 8.
            btnThreeOfAKind.Enabled = false;//Disable button.
            lblThreeOfAKindScore.Text = Score[8].ToString();// Display Score 8 on Three of a kind.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnFullHouse_Click(object sender, EventArgs e)//Button FullHouse Click function.
        {
            Score[10] = CalculatedScore[10];//Score 10 = CalcScore 10.
            btnFullHouse.Enabled = false;//Disable button.
            lblFullHouseScore.Text = Score[10].ToString();// Display Score 10 on Full House.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnSmallStraight_Click(object sender, EventArgs e)//Button SmallStraight Click function.
        {
            Score[11] = CalculatedScore[11];//Score 11 = CalcScore 11.
            btnSmallStraight.Enabled = false;//Disable button.
            lblSmallStraightScore.Text = Score[11].ToString();// Display Score 11 on SmallStraight.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnLargeStraight_Click(object sender, EventArgs e)//Button LargeStraight Click function.
        {
            Score[12] = CalculatedScore[12];//Score 12 = CalcScore 12.
            btnLargeStraight.Enabled = false;//Disable button.
            lblLargeStraightScore.Text = Score[12].ToString();// Display Score 12 on LargeStraight.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnChance_Click(object sender, EventArgs e)//Button Chance Click function.
        {
            Score[13] = CalculatedScore[13];//Score 13 = CalcScore 13.
            btnChance.Enabled = false;//Disable button.
            lblChanceScore.Text = Score[13].ToString();// Display Score 13 on Chance.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnYahtzee_Click(object sender, EventArgs e)//Button Yahtzee Click function.
        {
            Score[14] = CalculatedScore[14];//Score 14 = CalcScore 14.
            btnYahtzee.Enabled = false;//Disable button.
            lblYahtzeeScore.Text = Score[14].ToString();// Display Score 14 on Yahtzee.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnBonusYahtzee_Click(object sender, EventArgs e)//Button BonusYahtzee Click function.
        {
            Score[15] = CalculatedScore[15];//Score 15 = CalcScore 15.
            btnBonusYahtzee.Enabled = false;//Disable button.
            lblBonusYahtzeeScore.Text = Score[15].ToString();// Display Score 15 on BonusYahtzee.
            BonusYahtzeeTrue = true;//Bonus Yahtzee has been recieved and can be disabled now.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }

        //Upper House Buttons:
        private void btnOnes_Click(object sender, EventArgs e)//Button Ones Click function.
        {
            Score[0] = CalculatedScore[0];//Score 0 = CalcScore 0.
            btnOnes.Enabled = false;//Disable button.
            lblOnesScore.Text = Score[0].ToString();// Display Score 0 on Ones.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnTwos_Click(object sender, EventArgs e)//Button Twos Click function.
        {
            Score[1] = CalculatedScore[1];//Score 1 = CalcScore 1.
            btnTwos.Enabled = false;//Disable button.
            lblTwosScore.Text = Score[1].ToString();// Display Score 1 on Twos.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnThrees_Click(object sender, EventArgs e)//Button Threes Click function.
        {
            Score[2] = CalculatedScore[2];//Score 2 = CalcScore 2.
            btnThrees.Enabled = false;//Disable button.
            lblThreesScore.Text = Score[2].ToString();// Display Score 2 on Threes.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnFours_Click(object sender, EventArgs e)//Button Fours Click function.
        {
            Score[3] = CalculatedScore[3];//Score 3 = CalcScore 3.
            btnFours.Enabled = false;//Disable button.
            lblFoursScore.Text = Score[3].ToString();// Display Score 3 on Fours.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnFives_Click(object sender, EventArgs e)//Button Fives Click function.
        {
            Score[4] = CalculatedScore[4];//Score 4 = CalcScore 4.
            btnFives.Enabled = false;//Disable button.
            lblFivesScore.Text = Score[4].ToString();// Display Score 4 on Fives.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }
        private void btnSixes_Click(object sender, EventArgs e)//Button Sixes Click function.
        {
            Score[5] = CalculatedScore[5];//Score 5 = CalcScore 5.
            btnSixes.Enabled = false;//Disable button.
            lblSixesScore.Text = Score[5].ToString();// Display Score 5 on Sixes.
            DiceReset();//Call dice reset function.
            EndGameProgress++;//add 1 onto the end game progress.
            ScoreCalculation();//Calculate score to see if game over.
        }


        private void DiceReset()//Dice Reset Function
        {
            RollCount = 0;//Set roll count to 0.
            btnRollDice.Enabled = true;//Enables roll button.
            dice = new int[5] { 0, 0, 0, 0, 0 };//Set dice values to 0.
            LockedDice = new int[5] { 0, 0, 0, 0, 0 };//Unlock dice for next roll.
            ImageUpdate();//Update images to clear them.
        }

        private void MenuNewGame_Click(object sender, EventArgs e)//void to call when rest button clicked.
        {
            NewGame();//Call newgame function
        }

        private void NewGame()//Void resets entire game.
        {
            EndGameProgress = 0;
            Score = new int[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//All Score's = 0.
            DiceReset();//Dice reset.
            ImageUpdate();//Images updated

            //Enable buttons.
            btnOnes.Enabled = true;//Enable button.
            lblOnesScore.Text = Score[0].ToString();//Update label.

            btnTwos.Enabled = true;//Enable button.
            lblTwosScore.Text = Score[1].ToString();//Update label.

            btnThrees.Enabled = true;//Enable button.
            lblThreesScore.Text = Score[2].ToString();//Update label.

            btnFours.Enabled = true;//Enable button.
            lblFoursScore.Text = Score[3].ToString();//Update label.

            btnFives.Enabled = true;//Enable button.
            lblFivesScore.Text = Score[4].ToString();//Update label.

            btnSixes.Enabled = true;//Enable button.
            lblSixesScore.Text = Score[5].ToString();//Update label.



            btnThreeOfAKind.Enabled = true;//Enable button.
            lblThreeOfAKindScore.Text = Score[8].ToString();//Update label.

            btnFourOfAKind.Enabled = true;//Enable button.
            lblFourOfAKindScore.Text = Score[9].ToString();//Update label.

            btnFullHouse.Enabled = true;//Enable button.
            lblFullHouseScore.Text = Score[10].ToString();//Update label.

            btnSmallStraight.Enabled = true;//Enable button.
            lblSmallStraightScore.Text = Score[11].ToString();//Update label.

            btnLargeStraight.Enabled = true;//Enable button.
            lblLargeStraightScore.Text = Score[12].ToString();//Update label.

            btnChance.Enabled = true;//Enable button.
            lblChanceScore.Text = Score[13].ToString();//Update label.

            btnYahtzee.Enabled = true;//Enable button.
            lblYahtzeeScore.Text = Score[14].ToString();//Update label.

            btnBonusYahtzee.Enabled = true;//Enable button.
            lblBonusYahtzeeScore.Text = Score[15].ToString();//Update label.
        }


        private void FormKeyPressDown(object sender, KeyEventArgs e)//If key is pressed on form.
        {
            if(e.KeyCode == Keys.L)//Checks to see if key was L.
            {
                InfiniRoll = true;//InfiniRoll becomes true and dice can be rolled any number of times.
                btnRollDice.Enabled = true;//enables Dice Roll Button.
                ScoreCalculation();//Update ScoreCalculation
            }
        }

        private void FormKeyPressUp(object sender, KeyEventArgs e)//If key released on form.
        {
            if (e.KeyCode == Keys.L)//Checks to see if key was L.
            {
                InfiniRoll = false;//InfiniRoll becomes true and dice can be rolled any number of times.
                ScoreCalculation();//Update Score to disable image.
                Console.Beep();//Beeps to alert player
            }
        }
    }
}