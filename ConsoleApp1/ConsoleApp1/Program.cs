using Engine;
using System;
using System.Windows.Forms;

namespace ConsoleApp1
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConsoleEx.Create(128, 128);
            ConsoleEx.SetFont("Consolas", 8, 16);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            CharacterImage characterImage = CharacterImageReader.LoadFromImage(openFileDialog.FileName);

            while (true)
            {
                for (int y = 0; y < characterImage.Height; y++)
                {
                    for (int x = 0; x < characterImage.Width; x++)
                    {
                        ConsoleEx.Write($"{characterImage.CharacterPixels[x, y].Character}", characterImage.CharacterPixels[x, y].Color);
                    }
                    ConsoleEx.WriteLine();
                }
                ConsoleEx.Update();
            }
        }
    }
}
