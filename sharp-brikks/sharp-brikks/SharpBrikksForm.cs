using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// http://soundbible.com/182-Shake-And-Roll-Dice.html

namespace sharpbrikks
{
    public partial class SharpBrikksForm : Form
    {
        [Obsolete]
        Die ColorDie = null;

        [Obsolete]
        Die NumberDie = null;

        public BrikksRandom RandoloManolo { get; set; }

        public Brikks BrikksTheGame { get; set; }

        public SharpBrikksForm()
        {
            InitializeComponent();

            Console.WriteLine(DateTime.Now);
            BrikksTheGame = new Brikks();
            Console.WriteLine(DateTime.Now);
            BrikksTheGame.GetRoll();
            Console.WriteLine(DateTime.Now);
        }

        private void RollDiceButton_Click(object sender, EventArgs e)
        {
            RollDiceButton.Enabled = false;
            FixAllMarkedBoxes();
            PlaySound();
            RollDice();
            ShowChoices();
            RollDiceButton.Enabled = true;
        }

        private void ShowChoices()
        {
            /*
             * Tetromino__1__2_0    Tetromino__1__2_1    Tetromino__1__2_2
             * Tetromino__1__1_0    Tetromino__1__1_1    Tetromino__1__1_2
             * Tetromino__1__0_0    Tetromino__1__0_1    Tetromino__1__0_2
             */

            List<BrikksPictureBox> boxes = new List<BrikksPictureBox>();

            // clean all
            foreach (var item in this.Controls)
            {
                if (item.GetType() == typeof(BrikksPictureBox))
                {
                    BrikksPictureBox box = (BrikksPictureBox)item;
                    if (box.Name.StartsWith("Tetromino__"))
                    {
                        box.BackColor = Color.Gray;
                    }
                }
            }

            if (DiceResult.D4Result == Side.one)
            {
                switch (DiceResult.D6Result)
                {
                    case Side.white:
                        break;
                    default:
                        break;
                }
            }

            foreach (var item in boxes)
            {
                var color = Color.Gray;

                switch (DiceResult.D6Result)
                {
                    case Side.red:
                        color = Color.Red;
                        break;
                    case Side.blue:
                        color = Color.Blue;
                        break;
                    case Side.black:
                        color = Color.Black;
                        break;
                    case Side.white:
                        color = Color.White;
                        break;
                    case Side.yellow:
                        color = Color.Yellow;
                        break;
                    case Side.green:
                        color = Color.Green;
                        break;
                }

                item.BackColor = color;
            }
        }

        private void RollDice()
        {
            DiceResult.D6Result = ColorDie.Roll(this.RandoloManolo).Side;
            DiceResult.D4Result = NumberDie.Roll(this.RandoloManolo).Side;
            DiceResult.Image = (Image)Properties.Resources.ResourceManager.GetObject($"{DiceResult.D6Result.ToString()}_{DiceResult.D4Result.ToString()}");
        }

        private void PlaySound()
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.roll_the_dice);
            simpleSound.Play();
        }

        private void FixAllMarkedBoxes()
        {
            foreach (var item in this.Controls)
            {
                if (item.GetType() == typeof(BrikksPictureBox))
                {
                    BrikksPictureBox box = (BrikksPictureBox)item;
                    if (box.Name.StartsWith("Block__") && box.IsMarked && !box.IsFixed)
                    {
                        box.IsFixed = true;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateBlockEvents();
            CreateDice();

            //RandoloManolo = new BrikksRandom(Properties.Settings.Default.RandomOrgApiKey);
        }

        private void CreateBlockEvents()
        {
            foreach (var item in this.Controls)
            {
                if (item.GetType() == typeof(BrikksPictureBox) && ((BrikksPictureBox)item).Name.StartsWith("Block__"))
                {
                    ((BrikksPictureBox)item).Click += MarkBlockClick;
                }
                else if (item.GetType() == typeof(BrikksBombButton))
                {
                    ((BrikksBombButton)item).Click += UseBombClick;
                }
            }
        }

        private void CreateDice()
        {
            // color die
            this.ColorDie = (new Die(new DieSide(SideType.color, Side.red), new DieSide(SideType.color, Side.black), new DieSide(SideType.color, Side.yellow), new DieSide(SideType.color, Side.blue), new DieSide(SideType.color, Side.green), new DieSide(SideType.color, Side.white)));
            // d4
            this.NumberDie = (new Die(new DieSide(SideType.color, Side.one), new DieSide(SideType.color, Side.two), new DieSide(SideType.color, Side.three), new DieSide(SideType.color, Side.four)));
        }

        private void MarkBlockClick(object sender, EventArgs e)
        {
            var box = (BrikksPictureBox)sender;

            if (!box.IsFixed)
            {
                if (box.IsMarked)
                {
                    box.IsMarked = false;
                    box.Image = null;
                }
                else
                {
                    box.IsMarked = true;
                    box.Image = Properties.Resources.cross_mark_small;
                }
            }

            // calculate score on the fly
            int line = int.Parse(box.Name.Substring(box.Name.IndexOf("__") + 2, box.Name.LastIndexOf('_') - box.Name.IndexOf("__") - 2));
            ((BrikksScoreLineLabel)this.Controls.Find($"Score__Line_{line}", false)[0]).Text = CalcScoreLine(line).ToString().PadLeft(2, ' ');
        }

        private void UseBombClick(object sender, EventArgs e)
        {
            UseBomb((BrikksBombButton)sender);
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            foreach (var item in this.Controls)
            {
                // reset blocks
                if (item.GetType() == typeof(BrikksPictureBox))
                {
                    BrikksPictureBox box = (BrikksPictureBox)item;
                    if (box.Name.StartsWith("Block__"))
                    {
                        box.Image = box.OriginalImage;
                        box.IsMarked = false;
                        box.IsFixed = false;
                    }
                }
                // reset bombs
                else if (item.GetType() == typeof(BrikksBombButton))
                {
                    BrikksBombButton button = (BrikksBombButton)item;
                    button.BombUsed = false;
                    button.Enabled = true;
                    button.Text = button.BombValue.ToString();
                }
                // reset score lines
                else if (item.GetType() == typeof(BrikksScoreLineLabel))
                {
                    BrikksScoreLineLabel label = (BrikksScoreLineLabel)item;
                    label.Text = "__";
                }
            }




            DiceResult.Image = null;
        }

        private void UseBomb(BrikksBombButton button)
        {
            if (!button.BombUsed)
            {
                button.BombUsed = true;
                button.Image = Properties.Resources.cross_mark_small;
                button.Text = string.Empty;
            }
        }

        private void CalculateScoreButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 11; i++)
            {
                ((BrikksScoreLineLabel)this.Controls.Find($"Score__Line_{i}", false)[0]).Text = CalcScoreLine(i).ToString().PadLeft(2, ' ');
            }
        }

        private int CalcScoreLine(int lineNumber)
        {
            int marks = 0;
            int points = 0;
            int multi = 1;

            for (int i = 0; i < 10; i++)
            {
                marks = marks + (((BrikksPictureBox)this.Controls.Find($"Block__{lineNumber}_{i}", false)[0]).IsMarked ? 1 : 0);
            }

            if (lineNumber > 4) { multi *= 2; }
            if (lineNumber > 9) { multi *= 2; }

            if (marks > 7) { points++; }
            if (marks > 8) { points++; }
            if (marks > 9) { points += 3; }

            return points * multi;
        }

        private void UploadPlayButton_Click(object sender, EventArgs e)
        {

        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static System.Random Local;

        public static System.Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
