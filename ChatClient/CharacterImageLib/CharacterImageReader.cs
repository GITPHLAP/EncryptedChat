﻿using ConsoleEngineCS.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterImageLib
{
    internal class CharacterImageReader
    {
        public static CharacterImage LoadFromImage(string imagePath)
        {
            Bitmap bitmap = new(imagePath);
            if (bitmap.Width > 24 || bitmap.Height > 24)
            {
                bitmap = ResizeImage(bitmap, new Size(32, 32));
            }
            CharacterImage characterImage = new CharacterImage();
            characterImage.Width = (byte)bitmap.Width;
            characterImage.Height = (byte)bitmap.Height;
            characterImage.CharacterPixels = new CharacterPixel[bitmap.Width * bitmap.Height];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    characterImage.CharacterPixels[y * characterImage.Width + x] = GetCharacterPixelFromColor(bitmap.GetPixel(x, y));
                }
            }
            return characterImage;
        }

        private static CharacterPixel GetCharacterPixelFromColor(Color color)
        {
            Dictionary<int, char> lumCharacters = new()
            {
                { 0, ' '  },
                { 32, '░' },
                { 64, '▒' },
                { 96, '▓' },
                { 128, '█' },
                { 160, '▓' },
                { 192, '▒' },
                { 224, '░' },
                { 255, ' ' },
            };

            CharacterPixel characterPixel = new();
            int lum = (color.R + color.G + color.B) / 3;
            foreach (int lumKey in lumCharacters.Keys)
            {
                if (lum <= lumKey)
                {
                    characterPixel.Character = lumCharacters[lumKey];
                    if (lum < 128)
                    {
                        characterPixel.Color = CColor.Silver;
                    }
                    else
                    {
                        characterPixel.Color = CColor.Get(CColor.White, CColor.Silver);
                    }
                    break;
                }
            }

            return characterPixel;
        }

        public static Bitmap ResizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }
    }
}
