using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterImageLib
{
    internal class CharacterImage
    {
        public byte Width { get; set; }
        public byte Height { get; set; }
        public CharacterPixel[] CharacterPixels { get; set; }

        public override string ToString()
        {
            string str = "";
            str += (char)Width;
            str += (char)Height;
            foreach (CharacterPixel characterPixel in CharacterPixels)
            {
                str += characterPixel.Character;
                str += (char)characterPixel.Color;
            }
            return str;
        }

        public static CharacterImage FromString(string str)
        {
            CharacterImage characterImage = new CharacterImage();
            characterImage.Width = (byte)str[0];
            characterImage.Height = (byte)str[1];
            characterImage.CharacterPixels = new CharacterPixel[characterImage.Width * characterImage.Height];
            for (int i = 2; i < str.Length; i += 2)
            {
                characterImage.CharacterPixels[i] = new CharacterPixel
                {
                    Character = str[i],
                    Color = (byte)str[i + 1],
                };
            }
            return characterImage;
        }
    }
}
