using Cosmos.Debug.Kernel;
using System;
using System.Collections;

namespace MOS.GUI
{
    public class PixelFont
    {
        public byte height { get; }
        public PixelFontCharacter[] characters { get; }

        private byte[] fileContent;
        private byte amountOfByteHeight;
        // TODO add conditionnal debug with a define
        public static Debugger debugger = new Debugger("PixFont", "DUMP");

        public PixelFont(string filePath)
        {
            try
            {
                debugger.Send("Starting pixfont parsing " + filePath);
                this.fileContent = Kernel.fileSystem.readFile(filePath);
                if (!this.checkMagic())
                {
                    throw new FormatException();
                }
                debugger.Send("Magic checked");
                this.height = this.getFontHeight();
                debugger.Send("Font height: " + height);
                this.amountOfByteHeight = (byte)(this.getFontAlignement() / 8);
                debugger.Send("Height bytes: " + this.amountOfByteHeight);
                this.characters = new PixelFontCharacter[this.getCharactersAmount()];
                debugger.Send("Character amount: " + characters.Length);
                for (uint i = 0, offset = 8; i < this.characters.Length; i++)
                {
                    ushort ascii = BitConverter.ToUInt16(fileContent, (int)offset);
                    offset += 2;
                    byte charWidth = fileContent[offset];
                    offset += 1;
                    int glyphDataLength = this.amountOfByteHeight * charWidth;
                    byte[] glyphData = subArray(fileContent, (int)offset, this.amountOfByteHeight * charWidth);
                    offset += (uint)glyphDataLength;
                    this.characters[i] = new PixelFontCharacter(this.height, charWidth, ascii, glyphData, this.amountOfByteHeight);
                }
            } catch (Exception e)
            {
                throw new PixelFontException("Cannot create PixelFont: " + e.Message);
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

        private byte[] subArray(byte[] data, int index, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        /// <summary>
        /// Represent a single character
        /// </summary>
        public class PixelFontCharacter
        {
            public byte width;
            public byte height;
            public ushort ascii;
            public byte[][] content;
            public byte bytesHeight;

            /// <summary>
            /// Creates a new character
            /// </summary>
            /// <param name="height">Height of the character</param>
            /// <param name="width">Width of the character</param>
            /// <param name="ascii">ASCII code of the character</param>
            /// <param name="fileContentSection">The section that represent only the glyph</param>
            /// <param name="bytesHeight">Number of bytes in a column (alignement)</param>
            public PixelFontCharacter(byte height, byte width, ushort ascii, byte[] fileContentSection, byte bytesHeight)
            {
                this.content = new byte[width][];// bytesHeight];
                for (uint i = 0; i < width; i++)
                {
                    this.content[i] = new byte[bytesHeight];
                    for (uint y = 0; y < bytesHeight; y++)
                    {
                        this.content[i][y] = fileContentSection[i * bytesHeight + y];
                    }
                }
                this.ascii = ascii;
                this.width = width;
                this.height = height;
                this.bytesHeight = bytesHeight;
            }


            public void Dump()
            {
                PixelFont.debugger.Send("------ CHARACTER " + ((char)this.ascii) + " ------");
                PixelFont.debugger.Send("Ascii code: " + this.ascii);
                PixelFont.debugger.Send("Width: " + this.width);
                PixelFont.debugger.Send("Height: " + this.height);
                PixelFont.debugger.Send("Bytes height: " + this.bytesHeight);
                PixelFont.debugger.Send("Hexadecimal glyph:");
                string hexGlyph = "";
                foreach (byte[] dline in this.content)
                {
                    hexGlyph += BitConverter.ToString(dline) + '|';
                }
                PixelFont.debugger.Send(hexGlyph);
                PixelFont.debugger.Send("Display (90° rotation):\n");
                string topPart = "+";
                for (uint i = 0; i < height; i++) { topPart += "-"; }
                topPart += "+";
                PixelFont.debugger.Send(topPart);
                for (uint i = 0; i < width; i++)
                {
                    string column = "|";
                    BitArray bits = new BitArray(this.content[i]);
                    for (int j = 0; j < this.bytesHeight; j ++)
                    {
                        for (int counter = 0; counter < 8; counter++)
                        {
                            if (j * 8 + counter >= height)
                            {
                                break;
                            }
                            column += (bits[j*8+counter] ? "1" : "0");
                        }
                    }
                    column += '|';
                    column = column.Replace('0', ' ');
                    column = column.Replace('1', 'X');
                    PixelFont.debugger.Send(column);
                }
                PixelFont.debugger.Send(topPart + "\n");
                PixelFont.debugger.Send("-------------------------");
            }
        }

        public class PixelFontException: Exception
        {
            public PixelFontException(string message): base(message) {}
        }
    }
}
