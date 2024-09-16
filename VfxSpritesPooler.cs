
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public class VfxSpritesPooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            ViewApiLoadingMark = new PoolerModule<VfxSpritesData.ViewApiLoadingMark>(world);
            ViewApi = new PoolerModule<VfxSpritesData.ViewApi>(world);
        }

        public PoolerModule<VfxSpritesData.ViewApiLoadingMark> ViewApiLoadingMark;
        public PoolerModule<VfxSpritesData.ViewApi> ViewApi;
    }
}
