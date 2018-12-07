﻿using System;
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
        public Brikks BrikksTheGame { get; set; }

        public SharpBrikksForm()
        {
            InitializeComponent();

            BrikksTheGame = new Brikks();
        }

        private void RollDiceButton_Click(object sender, EventArgs e)
        {
            RollDiceButton.Enabled = false;
            FixAllMarkedBoxes();
            PlaySound();
            DiceResultPictureBox.DiceRoll = this.BrikksTheGame.Roll();
            HighlightTetromino(DiceResultPictureBox.DiceRoll.D4, DiceResultPictureBox.DiceRoll.D6);
            RollDiceButton.Enabled = true;
        }

        private void HighLightColumn(Side side)
        {
            Column__one.BackColor = Color.LightGray;
            Column__two.BackColor = Color.LightGray;
            Column__three.BackColor = Color.LightGray;
            Column__four.BackColor = Color.LightGray;

            switch (side)
            {
                case Side.one:
                    Column__one.BackColor = Color.CornflowerBlue;
                    break;
                case Side.two:
                    Column__two.BackColor = Color.CornflowerBlue;
                    break;
                case Side.three:
                    Column__three.BackColor = Color.CornflowerBlue;
                    break;
                case Side.four:
                    Column__four.BackColor = Color.CornflowerBlue;
                    break;
            }
        }

        private void HighlightTetromino(Side D4, Side D6)
        {
            // highlight column
            HighLightColumn(D4);

            // set them all back
            foreach (var item in this.Controls)
            {
                if (item.GetType() == typeof(GroupBox) && ((GroupBox)item).Name.StartsWith("Column__"))
                {
                    foreach (var boxItem in ((GroupBox)item).Controls)
                    {
                        BrikksPictureBox box = (BrikksPictureBox)boxItem;
                        if (box.Name.StartsWith("HighlightBox__") && box.Visible)
                        {
                            box.Visible = false;
                        }
                    }                    
                }
            }
        
            BrikksPictureBox tetrominoBox = (BrikksPictureBox)this.Controls.Find($"HighlightBox__{D4}_{D6}", true)[0];
            tetrominoBox.Visible = true;
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
            //int line = int.Parse(box.Name.Substring(box.Name.IndexOf("__") + 2, box.Name.LastIndexOf('_') - box.Name.IndexOf("__") - 2));
            //((BrikksScoreLineLabel)this.Controls.Find($"Score__Line_{line}", false)[0]).Text = CalcScoreLine(line).ToString().PadLeft(2, ' ');
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

            DiceResultPictureBox.Image = null;
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
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static System.Random Local;

        public static System.Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}
