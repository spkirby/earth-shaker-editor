using System.IO;

namespace EarthShakerEditor.Spectrum
{
    /// <summary>
    /// Represents a .sna (snapshot) file containing Spectrum emulation data.
    /// </summary>
    public class SnaFile : SpectrumFile
    {
        #region Public constants
        /// <summary>
        /// The default extension for this type of file.
        /// </summary>
        public const string Extension = ".sna";
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the default extension for this file.
        /// </summary>
        public override string FileExtension
        {
            get { return SnaFile.Extension; }
        }

        /// <summary>
        /// Gets or sets the SnaFile's raw data.
        /// </summary>
        public byte[] RawData
        {
            get { return (byte[])rawData.Clone();  }
            set
            {
                if(value.Length != SnaLength)
                    throw new InvalidDataException("Invalid snapshot data");

                rawData = (byte[])value.Clone();
            }
        }
        #endregion

        #region Note on the SNA format
        /* For future reference, the SNA format is as follows:
            byte           I;
            ushort         AltHL, AltDE, AltBC, AltAF;
            ushort         HL, DE, BC, IY, IX;
            byte           Interrupt;
            byte           R;
            ushort         AF, SP;
            byte           InterruptMode;
            SpectrumColour BorderColour;
            byte[]         Memory;
        */
        #endregion

        #region Private constants
        protected const int SnaLength    = 49179;
        protected const int MemoryStart  = 27;     // Memory dump starts at offset 27 of the snapshot
        protected const int MemoryLength = 49152;  // = 48KB
        #endregion

        #region Private variables
        protected byte[] rawData = new byte[SnaLength];
        #endregion

        #region Constructors
        public SnaFile(byte[] rawData)
            : base()
        {
            RawData = rawData;
        }

        public SnaFile(string filename, byte[] rawData)
        {
            Filename = filename;
            RawData  = rawData;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Writes the file to disk.
        /// </summary>
        public override void Save()
        {
            using(FileStream stream = File.Open(Filename, FileMode.Create))
            {
                stream.Write(rawData, 0, rawData.Length);
            }
        }

        /// <summary>
        /// Examines the specified binary data to verify if it is a SnaFile.
        /// </summary>
        /// <param name="data">The binary data to examine.</param>
        /// <returns>True if the data appears to be a SnaFile, false otherwise.</returns>
        public static bool IsValidSnaFile(byte[] data)
        {
            bool valid = false;

            // SNA files have very little identifying information, so these heuristics are quite rough
            if(data.Length == SnaLength)
            {
                int interruptMode = data[25]; // Saved Z80 interrupt mode must be 0, 1, or 2
                int borderColour  = data[26]; // Saved border colour must be 0-7
                
                if(interruptMode >= 0 && interruptMode <= 2 &&
                   borderColour  >= 0 && borderColour  <= 7)
                {
                    valid = true;
                }
            }

            return valid;
        }
        #endregion
    }
}
