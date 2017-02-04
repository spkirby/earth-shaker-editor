using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthShakerEditor.Spectrum
{
    #region SpectrumColour enumeration
    /// <summary>
    /// Specifies constants for the eight basic Spectrum colours.
    /// </summary>
    public enum SpectrumColour
    {
        Black   = 0,
        Blue    = 1,
        Red     = 2,
        Magenta = 3,
        Green   = 4,
        Cyan    = 5,
        Yellow  = 6,
        White   = 7,
        Last    = 7  // Last colour, used in for loops
    }
    #endregion

    /// <summary>
    /// Represents a Spectrum colour attribute.
    /// </summary>
    public class SpectrumAttribute
    {
        #region Note on Spectrum attributes
        // A Spectrum attribute is stored in a single byte, in the following format:
        //
        //       Bit:  |  7|  6|  5|  4|  3|  2|  1|  0|
        //     Value:  |128| 64| 32| 16|  8|  4|  2|  1|
        // Attribute:  |Fl.|Br.| Background| Foreground|
        #endregion

        #region Private members
        private static readonly string[] colourNames =
        {
            "Black", "Blue", "Red", "Magenta", "Green", "Cyan", "Yellow", "White"
        };
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the foreground colour of this attribute.
        /// </summary>
        public SpectrumColour Foreground { get; set; }

        /// <summary>
        /// Gets or sets the background colour of this attribute.
        /// </summary>
        public SpectrumColour Background { get; set; }

        /// <summary>
        /// Gets or sets the Bright flag of this attribute.
        /// </summary>
        public bool Bright { get; set; }

        /// <summary>
        /// Gets or sets the Flash flag of this attribute;
        /// </summary>
        public bool Flash  { get; set; }

        /// <summary>
        /// Gets or sets the attribute as a Spectrum attribute byte.
        /// </summary>
        public byte AttributeByte
        {
            get
            {
                return (byte)((int)Foreground + (int)Background * 8 + (Bright ? 64 : 0) + (Flash ? 128 : 0));
            }

            set
            {
                Foreground = (SpectrumColour)(value & 0x07);
                Background = (SpectrumColour)((value & 0x38) >> 3);
                Bright     = ((value & 0x40) != 0);
                Flash      = ((value & 0x80) != 0);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Spectrum.SpectrumAttribute class.
        /// </summary>
        public SpectrumAttribute()
        {
            Foreground = SpectrumColour.Black;
            Background = SpectrumColour.Black;
            Bright     = false;
            Flash      = false;
        }

        /// <summary>
        /// Initializes a new instance of the Spectrum.SpectrumAttribute class using the
        /// specified attribute byte.
        /// </summary>
        /// <param name="attribute"></param>
        public SpectrumAttribute(byte attribute)
        {
            AttributeByte = attribute;
        }

        /// <summary>
        /// Initializes a new instance of the Spectrum.SpectrumAttribute class with the
        /// specified foreground colour.
        /// </summary>
        /// <param name="foreground"></param>
        public SpectrumAttribute(SpectrumColour foreground)
            : this()
        {
            Foreground = foreground;
        }

        /// <summary>
        /// Initializes a new instance of the Spectrum.SpectrumAttribute class with the
        /// specified foreground and background colours.
        /// </summary>
        /// <param name="foreground"></param>
        /// <param name="background"></param>
        public SpectrumAttribute(SpectrumColour foreground, SpectrumColour background)
            : this(foreground)
        {
            Background = background;
        }

        /// <summary>
        /// Initializes a new instance of the Spectrum.SpectrumAttribute class with the
        /// specified foreground, background, and bright values.
        /// </summary>
        /// <param name="foreground"></param>
        /// <param name="background"></param>
        /// <param name="bright"></param>
        public SpectrumAttribute(SpectrumColour foreground, SpectrumColour background, bool bright)
            : this(foreground, background)
        {
            Bright = bright;
        }

        /// <summary>
        /// Initializes a new instance of the Spectrum.SpectrumAttribute class with the
        /// specified foreground, background, bright, and flash values.
        /// </summary>
        /// <param name="foreground"></param>
        /// <param name="background"></param>
        /// <param name="bright"></param>
        /// <param name="flash"></param>
        public SpectrumAttribute(SpectrumColour foreground, SpectrumColour background, bool bright, bool flash)
            : this(foreground, background, bright)
        {
            Flash = flash;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the name of the specified SpectrumColour.
        /// </summary>
        /// <param name="colour">The SpectrumColour to get the name of.</param>
        /// <returns>A string representing the name of the specified SpectrumColour.</returns>
        public static string GetColourName(SpectrumColour colour)
        {
            return colourNames[(int)colour];
        }

        public override string ToString()
        {
            return AttributeByte.ToString();
        }

        public override bool Equals(object obj)
        {
            SpectrumAttribute other = (obj as SpectrumAttribute);
            return (other != null && other.AttributeByte == this.AttributeByte);
        }

        public override int GetHashCode()
        {
            return AttributeByte;
        }
        #endregion
    }
}
