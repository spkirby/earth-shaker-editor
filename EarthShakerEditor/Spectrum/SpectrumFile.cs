using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace EarthShakerEditor.Spectrum
{
    /// <summary>
    /// The exception that is thrown when a file is not recognised as a supported SpectrumFile.
    /// </summary>
    public class InvalidSpectrumFileException : Exception, ISerializable
    {
        public InvalidSpectrumFileException()
        {
        }

        public InvalidSpectrumFileException(string message)
            : base(message)
        {
        }

        public InvalidSpectrumFileException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public InvalidSpectrumFileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Abstract. Represents a file containing Spectrum emulation data.
    /// </summary>
    public abstract class SpectrumFile
    {
        // Properties
        public string Filename { get; set; }


        // Abstract properties
        public abstract string FileExtension { get; }


        // Constructors
        /// <summary>
        /// Initializes a new instance of the Spectrum.SpectrumFile class.
        /// </summary>
        public SpectrumFile()
        {
            Filename = "";
        }

        /// <summary>
        /// Initializes a new instance of the Spectrum.SpectrumFile class, using
        /// the specified filename.
        /// </summary>
        /// <param name="filename"></param>
        public SpectrumFile(string filename)
        {
            Filename = filename;
        }


        // Abstract methods
        /// <summary>
        /// Attempts to save this SpectrumFile's contents to the file specified in its Filename property.
        /// </summary>
        public abstract void Save();


        // Static methods
        /// <summary>
        /// Creates a Spectrum.SpectrumFile from the specified file.
        /// </summary>
        /// <param name="path">The path of the file to open</param>
        /// <returns>A SpectrumFile representing the physical file</returns>
        public static SpectrumFile Open(string path)
        {
            byte[] fileData;
            SpectrumFile specFile;
            string extension = Path.GetExtension(path).ToLower();

            using(FileStream input = File.OpenRead(path))
            {
                fileData = new byte[input.Length];
                input.Read(fileData, 0, fileData.Length);
            }

            if(SnaFile.IsValidSnaFile(fileData)) // SNA files are difficult to identify and should come last in the chain
                specFile = new SnaFile(path, fileData);
            else
                throw new InvalidSpectrumFileException("The file " + Path.GetFileName(path) + " is not in a supported Spectrum format.");

            return specFile;
        }
    }
}
