using Sce.Atf;

namespace Vitei.ATFExtensions
{
    /// <summary>
    /// Shared resources for ATF-based apps built at Vitei.
    /// </summary>
    public static class Resources
    {
        [ImageResource("vitei_16_KhE_icon.ico")]
        public static readonly string ViteiIconImage;
        [ImageResource("viteilogo.png")]
        public static readonly string ViteiLogo;

        /// <summary>
        /// Static constructor</summary>
        static Resources()
        {
            ResourceUtil.Register(typeof(Resources));
        }
    }
}
