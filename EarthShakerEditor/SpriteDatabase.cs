using System.Collections.Generic;
using System.Drawing;
using EarthShakerEditor.Spectrum;

namespace EarthShakerEditor
{
    /// <summary>
    /// Provides a collection of Sprites and static methods for getting Sprites
    /// from other data.
    /// </summary>
    public static class SpriteDatabase
    {
        #region Private variables
        /// <summary>
        /// A dictionary linking Spectrum memory addresses to Sprites.
        /// </summary>
        private static Dictionary<int, Sprite> addrToSprite = new Dictionary<int,Sprite>();

        /// <summary>
        /// Default Sprites for each Element.
        /// </summary>
        private static Sprite[] elementSprites =
        {
            SpriteDatabase.Space    , SpriteDatabase.Door      , SpriteDatabase.Rock        , SpriteDatabase.Player ,
            SpriteDatabase.Player   , SpriteDatabase.BrickWall , SpriteDatabase.Earth       , SpriteDatabase.Diamond,
            SpriteDatabase.JellyBean, SpriteDatabase.ForceField, SpriteDatabase.GravityStick, SpriteDatabase.Monitor,
            SpriteDatabase.Elixir   , SpriteDatabase.Teleport  , SpriteDatabase.Bubble      , SpriteDatabase.Fire
        };
        #endregion

        #region Public Sprites
        // Invalid
        public static readonly Sprite Invalid        = createSprite("Invalid"        , Address.Invalid       , Resources.Invalid, new Point(0, 0)    );

        // Walls and earth graphics
        public static readonly Sprite IceCube        = createSprite("Ice Cube"       , Address.IceCube       , Resources.Sprites, new Point(  0, 256));
        public static readonly Sprite Girder         = createSprite("Girder"         , Address.Girder        , Resources.Sprites, new Point( 16, 256));
        public static readonly Sprite JaggedRock     = createSprite("Jagged Rock"    , Address.JaggedRock    , Resources.Sprites, new Point( 32, 256));
        public static readonly Sprite MetalPlate     = createSprite("Metal Plate"    , Address.MetalPlate    , Resources.Sprites, new Point( 48, 256));
        public static readonly Sprite HoneycombWall  = createSprite("Honeycomb Wall" , Address.HoneycombWall , Resources.Sprites, new Point( 64, 256));
        public static readonly Sprite QuestionMark   = createSprite("Question Mark"  , Address.QuestionMark  , Resources.Sprites, new Point( 80, 256));
        public static readonly Sprite Clouds         = createSprite("Clouds"         , Address.Clouds        , Resources.Sprites, new Point( 96, 256));
        public static readonly Sprite VerticalTube   = createSprite("Vertical Tube"  , Address.VerticalTube  , Resources.Sprites, new Point(112, 256));
        public static readonly Sprite Hearts         = createSprite("Hearts"         , Address.Hearts        , Resources.Sprites, new Point(128, 256));
        public static readonly Sprite BlueStone      = createSprite("Blue Stone"     , Address.BlueStone     , Resources.Sprites, new Point(144, 256));
        public static readonly Sprite Moss           = createSprite("Moss"           , Address.Moss          , Resources.Sprites, new Point(160, 256));
        public static readonly Sprite WhiteTiles     = createSprite("White Tiles"    , Address.WhiteTiles    , Resources.Sprites, new Point(176, 256));
        public static readonly Sprite Brain          = createSprite("Brain"          , Address.Brain         , Resources.Sprites, new Point(192, 256));
        public static readonly Sprite HoneycombEarth = createSprite("Honeycomb Earth", Address.HoneycombEarth, Resources.Sprites, new Point(208, 256));
        public static readonly Sprite WetHellSoil    = createSprite("Wet Hell Soil"  , Address.WetHellSoil   , Resources.Sprites, new Point(224, 256));
        public static readonly Sprite Sparkles       = createSprite("Sparkles"       , Address.Sparkles      , Resources.Sprites, new Point(240, 256));

        // Recolourable graphics
        public static readonly Sprite Earth     = createSprite("Earth"               , Address.Earth         , Resources.Sprites, new Point( 96, 272));
        public static readonly Sprite BrickWall = createSprite("Brick Wall"          , Address.BrickWall     , Resources.Sprites, new Point( 80, 272));
        public static readonly Sprite Rock      = createSprite("Rock"                , Address.Rock          , Resources.Sprites, new Point( 32, 272));
        public static readonly Sprite Door      = createSprite("Door"                , Address.Door          , Resources.Sprites, new Point( 16, 272));
        
        private static readonly Sprite[,] recolouredBrickWalls = createSpecialSprites("Brick Wall", Address.BrickWall, Resources.Sprites, new Point(  0,   0));
        private static readonly Sprite[,] recolouredEarth      = createSpecialSprites("Earth"     , Address.Earth    , Resources.Sprites, new Point(128,   0));
        private static readonly Sprite[,] recolouredRocks      = createSpecialSprites("Rock"      , Address.Rock     , Resources.Sprites, new Point(  0, 128));
        private static readonly Sprite[,] recolouredDoors      = createSpecialSprites("Door"      , Address.Door     , Resources.Sprites, new Point(128, 128));

        // Other graphics
        public static readonly Sprite Space        = createSprite("Space"            , Address.Space         , Resources.Sprites, new Point(  0, 272));
        public static readonly Sprite Player       = createSprite("Player"           , Address.Player        , Resources.Sprites, new Point( 48, 272));
        public static readonly Sprite Diamond      = createSprite("Diamond"          , Address.Diamond       , Resources.Sprites, new Point(112, 272));
        public static readonly Sprite JellyBean    = createSprite("Jelly Bean"       , Address.JellyBean     , Resources.Sprites, new Point(128, 272));
        public static readonly Sprite ForceField   = createSprite("Force Field"      , Address.ForceField    , Resources.Sprites, new Point(144, 272));
        public static readonly Sprite GravityStick = createSprite("Gravity Stick"    , Address.GravityStick  , Resources.Sprites, new Point(160, 272));
        public static readonly Sprite Monitor      = createSprite("Monitor"          , Address.Monitor       , Resources.Sprites, new Point(176, 272));
        public static readonly Sprite Elixir       = createSprite("Elixir"           , Address.Elixir        , Resources.Sprites, new Point(192, 272));
        public static readonly Sprite Teleport     = createSprite("Teleport"         , Address.Teleport      , Resources.Sprites, new Point(208, 272));
        public static readonly Sprite Bubble       = createSprite("Bubble"           , Address.Bubble        , Resources.Sprites, new Point(224, 272));
        public static readonly Sprite Fire         = createSprite("Fire"             , Address.Fire          , Resources.Sprites, new Point(240, 272));
        #endregion

        #region Public methods
        /// <summary>
        /// Gets a sprite for an element of the given level. Deals with some of the
        /// weirder aspects of Earth Shaker's sprite system.
        /// </summary>
        public static Sprite GetSprite(Level level, Element element)
        {
            if(element == Element.Wall)
            {
                if(level.WallGraphicAddress == Address.Earth)
                    return GetSprite(level.WallGraphicAddress, level.EarthAttribute);
                else if(level.WallGraphicAddress == Address.Rock)
                    return GetSprite(level.WallGraphicAddress, level.RockAttribute);
                else if(level.WallGraphicAddress == Address.Door)
                    return GetSprite(level.WallGraphicAddress, level.DoorAttribute);
                else
                    return GetSprite(level.WallGraphicAddress, level.WallAttribute);
            }
            else if(element == Element.Earth)
            {
                if(level.EarthGraphicAddress == Address.BrickWall)
                    return GetSprite(level.EarthGraphicAddress, level.WallAttribute);
                else if(level.EarthGraphicAddress == Address.Rock)
                    return GetSprite(level.EarthGraphicAddress, level.RockAttribute);
                else if(level.EarthGraphicAddress == Address.Door)
                    return GetSprite(level.EarthGraphicAddress, level.DoorAttribute);
                else
                    return GetSprite(level.EarthGraphicAddress, level.EarthAttribute);
            }
            else if(element == Element.Rock)
            {
                return GetSprite(SpriteDatabase.Rock, level.RockAttribute);
            }
            else if(element == Element.Door)
            {
                return GetSprite(SpriteDatabase.Door, level.DoorAttribute);
            }
            else
            {
                return elementSprites[(int)element];
            }
        }

        /// <summary>
        /// Gets a sprite from a memory address, using a default attribute
        /// </summary>
        public static Sprite GetSprite(int memoryAddress)
        {
            return GetSprite(memoryAddress, new SpectrumAttribute(SpectrumColour.White));
        }

        /// <summary>
        /// Gets a sprite from a memory address, using a custom attribute
        /// </summary>
        public static Sprite GetSprite(int memoryAddress, SpectrumAttribute attribute)
        {
            if(addrToSprite.ContainsKey(memoryAddress))
                return GetSprite(addrToSprite[memoryAddress], attribute);
            else
                return null;
        }

        public static Sprite GetSprite(Sprite baseSprite, SpectrumColour colour)
        {
            return GetSprite(baseSprite, new SpectrumAttribute(colour));
        }


        /// <summary>
        /// Gets a sprite from a base sprite and an attribute. Special sprites will be recoloured with
        /// the supplied attributes. Normal sprites will be returned as-is.
        /// </summary>
        public static Sprite GetSprite(Sprite baseSprite, SpectrumAttribute attribute)
        {
            if(baseSprite == BrickWall)
                return recolouredBrickWalls[(int)attribute.Background, (int)attribute.Foreground];
            else if(baseSprite == Earth)
                return recolouredEarth[(int)attribute.Background, (int)attribute.Foreground];
            else if(baseSprite == Rock)
                return recolouredRocks[(int)attribute.Background, (int)attribute.Foreground];
            else if(baseSprite == Door)
                return recolouredDoors[(int)attribute.Background, (int)attribute.Foreground];
            else
                return baseSprite;
        }
        #endregion

        #region Private methods
        private static Sprite createSprite(string name, int memoryAddress, Bitmap spriteSheet, Point offset)
        {
            Bitmap spriteBitmap = spriteSheet.Clone(new Rectangle(offset, Sprite.Size), spriteSheet.PixelFormat);
            Sprite sprite = new Sprite(name, memoryAddress, spriteBitmap);
            addrToSprite.Add(memoryAddress, sprite);
            return sprite;
        }

        private static Sprite[,] createSpecialSprites(string name, int memoryAddress, Bitmap spriteSheet, Point baseOffset)
        {
            int colourCount = (int)SpectrumColour.Last + 1;
            Point offset = baseOffset;
            SpectrumAttribute attribute = new SpectrumAttribute();
            Sprite[,] sprites = new Sprite[colourCount, colourCount];

            for(int y=0; y < colourCount; y++)
            {
                for(int x=0; x < colourCount; x++)
                {
                    Bitmap spriteBitmap = spriteSheet.Clone(new Rectangle(offset, Sprite.Size), spriteSheet.PixelFormat);
                    SpectrumAttribute attr = new SpectrumAttribute((SpectrumColour)x, (SpectrumColour)y, true, false);

                    sprites[y, x] = new Sprite(name, memoryAddress, spriteBitmap, attr);
                    offset.X += Sprite.Size.Width;
                }

                offset.X = baseOffset.X;
                offset.Y += Sprite.Size.Height;
            }

            return sprites;
        }
        #endregion

        #region Sprite address constants
        private class Address
        {
            public const int Invalid        = 0;
            public const int Space          = 60912;

            public const int IceCube        = 40156;
            public const int Girder         = 40192;
            public const int JaggedRock     = 40228;
            public const int MetalPlate     = 40264;
            public const int HoneycombWall  = 40300;
            public const int QuestionMark   = 60712;
            public const int Clouds         = 60748;
            public const int VerticalTube   = 60784;
            public const int Hearts         = 60820;
            public const int BlueStone      = 60856;
            public const int Moss           = 60928;
            public const int WhiteTiles     = 61184;
            public const int Brain          = 64452;
            public const int HoneycombEarth = 64416;
            public const int WetHellSoil    = 42064;
            public const int Sparkles       = 40336;

            public const int Earth          = 39760;
            public const int BrickWall      = 40120;
            public const int Rock           = 39724;
            public const int Door           = 39688;

            public const int Player         = 39868;
            public const int Diamond        = 39796;
            public const int JellyBean      = 42208;
            public const int ForceField     = 41632;
            public const int GravityStick   = 41920;
            public const int Monitor        = 40624;
            public const int Elixir         = 40480;
            public const int Teleport       = 40804;
            public const int Bubble         = 41236;
            public const int Fire           = 40660;
        }
        #endregion
    }
}
