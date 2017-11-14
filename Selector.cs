using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sharpmon
{
    public class Selector
    {
        private List<string> Text;
        private int HiddenCounter;
        private int LastHiddenCounter;
        private int HiddenScroll;
        private string regex;
        private int[] cursorPosition;
        private int[] cursorLastPosition;
        public Selector()
        {
            this.HiddenCounter = 0;
            this.LastHiddenCounter = 0;
            this.HiddenScroll = 0;
            this.regex = @"\t(?=[^\t])";
            this.cursorPosition = new int[2]{0, 0};
            this.cursorLastPosition = new int[2]{0, 0};
        }
        public void SetText(string text, Player player = null, bool inPC = false, int page = 0, int pageMax = 0)
        {
            this.Text = new List<string>(text.Split('\n'));
            this.HiddenCounter = 0;
            this.LastHiddenCounter = 0;
            this.FirstDraw(player, inPC, page, pageMax);
        }
        public void ResetScroll()
        {
            this.HiddenScroll = 0;
        }
        public void ModifyHiddenCounter(ConsoleKey choice, out int hiddenCounter)
        {
            LastHiddenCounter = HiddenCounter;
            switch(choice)
            {
                case ConsoleKey.Z:
                    HiddenCounter = (((HiddenCounter - 1) % (this.Text.Count)) + this.Text.Count) % this.Text.Count;
                    hiddenCounter = HiddenCounter;
                    this.UpdateCursor();
                    break;
                case ConsoleKey.S:
                    HiddenCounter = (HiddenCounter + 1) % (this.Text.Count);
                    hiddenCounter = HiddenCounter;
                    this.UpdateCursor();
                    break;
                default:
                    hiddenCounter = HiddenCounter;
                    break;
            }
            
        }
        public bool ModifyHiddenScroll(ConsoleKey choice, int maxScroll, out int hiddenScroll)
        {
            switch(choice)
            {
                case ConsoleKey.Q:
                    HiddenScroll = (((HiddenScroll - 1) % maxScroll) + maxScroll) % maxScroll;
                    hiddenScroll = HiddenScroll;
                    return true;
                case ConsoleKey.D:
                    HiddenScroll = (HiddenScroll + 1) % maxScroll;
                    hiddenScroll = HiddenScroll;
                    return true;
                default:
                    hiddenScroll = HiddenScroll;
                    return false;
            }
            
        }
        public void FirstDraw(Player player = null, bool inPC = false, int page = 0, int pageMax = 0)
        {
            for(int i = 0; i < this.Text.Count; i++)
            {
                if(i == this.HiddenCounter)
                {
                    this.cursorPosition[0] = Console.CursorLeft + 7;
                    this.cursorPosition[1] = Console.CursorTop;
                    if(player != null && i < player.GetSharpmons().Count && !inPC)
                        this.DrawWithColor(i, player, true);
                    else if (inPC && (page*8)+i < (page*8)+player.GetSharpmonsInPC().Count%8)
                        this.DrawPCWithColor((page*8)+i, i, player, true);
                    else
                        Console.WriteLine(Regex.Replace(this.Text[i], this.regex, "      > "));
                }
                else
                {
                    if(player != null && i < player.GetSharpmons().Count && !inPC)
                        this.DrawWithColor(i, player);
                    else if (inPC && (page*8)+i < ((page+1 < pageMax) ? (page+1)*8: (page*8)+player.GetSharpmonsInPC().Count%8))
                        this.DrawPCWithColor((page*8)+i, i, player);
                    else
                        Console.WriteLine(Regex.Replace(this.Text[i], this.regex, "        "));
                }
                if(i == this.Text.Count - 1)
                {
                    this.cursorLastPosition[0] = Console.CursorLeft;
                    this.cursorLastPosition[1] = Console.CursorTop;
                }
            }
        }

        private void DrawWithColor(int index, Player player, bool selected = false)
        {
            player.GetSharpmons()[index].GetColorElementalType();
            if(selected)
                Console.WriteLine(Regex.Replace(this.Text[index], this.regex, "      > "));
            else
                Console.WriteLine(Regex.Replace(this.Text[index], this.regex, "        "));
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private void DrawPCWithColor(int sharpmonIndex, int index, Player player, bool selected = false)
        {
            player.GetSharpmonsInPC()[sharpmonIndex].GetColorElementalType();
            if(selected)
                Console.WriteLine(Regex.Replace(this.Text[index], this.regex, "      > "));
            else
                Console.WriteLine(Regex.Replace(this.Text[index], this.regex, "        "));
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private void UpdateCursor()
        {
            Console.SetCursorPosition(this.cursorPosition[0], this.cursorPosition[1] + this.LastHiddenCounter);
            Console.Write("\b ");
            Console.SetCursorPosition(this.cursorPosition[0], this.cursorPosition[1] + this.HiddenCounter);
            Console.Write("\b>");
        }
        public void PlaceAtLastPosition()
        {
            Console.SetCursorPosition(this.cursorLastPosition[0], this.cursorLastPosition[1]);
        }
    }
}