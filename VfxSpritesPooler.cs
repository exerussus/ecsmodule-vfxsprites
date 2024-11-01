
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1OrganizerUI.Scripts.Pooling;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public class VfxSpritesPooler : IGroupPooler
    {
        public virtual void PreInitialize(VfxSpritesSettings settings)
        {
            Settings = settings;
        }
        
        public void Initialize(EcsWorld world)
        {
            World = world;
            ViewApiLoadingMark = new PoolerModule<VfxSpritesData.ViewApiLoadingMark>(world);
            ViewApi = new PoolerModule<VfxSpritesData.ViewApi>(world);        
            
            if (Settings.AddressableInfos == null || Settings.AddressableInfos.Length == 0) return;
            foreach (var addressableInfo in Settings.AddressableInfos) _assetPooler.Initialize(addressableInfo.name, addressableInfo.path);
        }
        
        public EcsWorld World { get; private set; }
        public VfxSpritesSettings Settings { get; private set; }
        private readonly AssetPool<VfxSpriteApi> _assetPooler = new();
        public PoolerModule<VfxSpritesData.ViewApiLoadingMark> ViewApiLoadingMark;
        public PoolerModule<VfxSpritesData.ViewApi> ViewApi;

        public void CreateVfx(string assetName, string addressablePath, Vector3 position)
        {
            var newEntity = World.NewEntity();
            var packedEntity = World.PackEntity(newEntity);
            ViewApiLoadingMark.Add(newEntity);
            
            _assetPooler.GetAndExecute(assetName, addressablePath, position, api =>
            {
                if (packedEntity.Unpack(World, out var entity))
                {
                    ref var newApi = ref ViewApi.Add(entity);
                    newApi.Value = api;
                    ViewApiLoadingMark.Del(entity);
                    newApi.Value.Release = () => _assetPooler.Release(api); 
                    newApi.Value.PlayAnimation(Settings.DeltaTime());
                }
                else
                {
                    _assetPooler.Release(api);
                }
            });
        }
    }
}
