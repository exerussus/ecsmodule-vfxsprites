
using Exerussus._1EasyEcs.Scripts.Core;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public static class VfxSpritesData
    {
        public struct ViewApi : IEcsComponent
        {
            public VfxSpriteApi Value;
        }

        public struct ViewApiLoadingMark : IEcsComponent
        {
            
        }
    }
}
