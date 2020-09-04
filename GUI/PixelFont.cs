

using System;

namespace MOS.GUI
{
    public class PixelFont
    {
        public byte height { get; }
        public PixelFontCharacter[] characters { get; }

        private byte[] fileContent;
        private byte amountOfByteHeight;

        public PixelFont(string filePath)
        {
            this.fileContent = Kernel.fileSystem.readFile(filePath);
            if (!this.checkMagic())
            {
                throw new FormatException();
            }
            this.height = this.getFontHeight();
            this.amountOfByteHeight = (byte)(this.getFontAlignement() / 8);
            this.characters = new PixelFontCharacter[this.getCharactersAmount()];
            for (uint i = 0; i < this.characters.Length; i++)
            {
                // TODO create all the characters
            }
        }

        /// <summary>
        /// Checks if the magic number is valid
        /// </summary>
        /// <returns>true if valid</returns>
        private bool checkMagic()
        {
            return this.fileContent[0] == 0xF8 && this.fileContent[1] == 0xCA;
        }

        /// <summary>
        /// Get the font height
        /// </summary>
        /// <returns>Font height</returns>
        private byte getFontHeight()
        {
            return this.fileContent[2];
        }

        /// <summary>
        /// Get the font bit alignement (8 bit multiple)
        /// </summary>
        /// <returns>Font bit alignement</returns>
        private byte getFontAlignement()
        {
            return this.fileContent[3];
        }

        /// <summary>
        /// Get the amount of characters
        /// </summary>
        /// <returns>Amout of characters</returns>
        private uint getCharactersAmount()
        {
            return (uint)BitConverter.ToInt32(this.fileContent, 4);
        }

        /// <summary>
        /// Represent a single character
        /// </summary>
        public class PixelFontCharacter
        {
            public byte width { get; }
            public ushort ascii { get; }
            public byte[,] content { get; }

            /// <summary>
            /// Creates a new character
            /// </summary>
            /// <param name="height">Height of the character</param>
            /// <param name="width">Width of the character</param>
            /// <param name="ascii">ASCII code of the character</param>
            /// <param name="fileContentSection">The section that represent only the glyph</param>
            public PixelFontCharacter(byte height, byte width, ushort ascii, byte[] fileContentSection)
            {
                this.content = new byte[width, height];
                this.ascii = ascii;
                // TODO Add glyph conversion
            }

        }
    }
}
