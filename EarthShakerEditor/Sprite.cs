using System.Drawing;
using EarthShakerEditor.Spectrum;

namespace EarthShakerEditor
{
    /// <summary>
    /// Represents a Spectrum sprite. Sprite objects are immutable.
    /// </summary>
    public class Sprite
    {
        #region Public properties
        /// <summary>
        /// The size of the Sprite.
        /// </summary>
        public static Size Size
        {
            get { return size; }
        }
        
        /// <summary>
        /// Gets the display name of the Sprite.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the address of the Sprite's location in Spectrum memory.
        /// </summary>
        public int MemoryAddress { get; private set; }

        /// <summary>
        /// Gets the Spectrum colour attribute used by the Sprite.
        /// </summary>
        public SpectrumAttribute Attribute { get; private set; }

        /// <summary>
        /// Gets an Image representing the Sprite.
        /// </summary>
        public Image Image { get; private set; }
        #endregion

        #region Private variables
        private static Size size = new Size(16, 16);
        #endregion

        #region Constructors
        public Sprite(string name, int memoryAddress, Image image)
        {
            Name          = name;
            MemoryAddress = memoryAddress;
            Image         = image;
            Attribute     = null;
        }

        public Sprite(string name, int memoryAddress, Image image, SpectrumAttribute attribute)
            : this(name, memoryAddress, image)
        {
            Attribute = attribute;
        }
        #endregion

        #region Public methods
        public override bool Equals(object obj)
        {
            Sprite other = (obj as Sprite);
            return (other != null &&
                    other.Attribute.Equals(this.Attribute)    &&
                    other.MemoryAddress == this.MemoryAddress &&
                    other.Name          == this.Name );
        }

        public override int GetHashCode()
        {
            // MemoryAddress only goes up to 0xFFFF
            return (MemoryAddress + (0x10000 * Attribute.AttributeByte));
        }
        #endregion
    }
}