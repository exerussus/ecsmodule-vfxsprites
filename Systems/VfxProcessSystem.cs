
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public class VfxProcessSystem : EasySystem<VfxSpritesPooler>
    {
        private EcsFilter _vfxSpritesFilter;
        
        protected override void Initialize()
        {
            _vfxSpritesFilter = World.Filter<VfxSpritesData.ViewApi>().Exc<VfxSpritesData.ViewApiLoadingMark>().End();
        }

        protected override void Update()
        {
            var deltaTime = DeltaTime;
            
            foreach (var entity in _vfxSpritesFilter)
            {
                ref var viewApi = ref Pooler.ViewApi.Get(entity);
                viewApi.Value.AnimationUpdate(deltaTime);
                if (viewApi.Value.IsDone)
                {
                    viewApi.Value.Release();
                    World.DelEntity(entity);
                }
            }
        }
    }
}
