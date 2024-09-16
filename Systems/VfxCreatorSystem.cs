
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1OrganizerUI.Scripts.Pooling;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public class VfxCreatorSystem : EcsSignalListener<VfxSpritesPooler, VfxSpritesSignals.CommandCreateVfx>
    {
        private readonly AssetPool<VfxSpriteApi> _assetPooler = new();
        private VfxSpritesSettings _settings;

        protected override void Initialize()
        {
            GameShare.GetSharedObject(ref _settings);
            
            if (_settings.AddressableInfos == null || _settings.AddressableInfos.Length == 0) return;

            foreach (var addressableInfo in _settings.AddressableInfos) _assetPooler.Initialize(addressableInfo.name, addressableInfo.path);
        }

        protected override void OnSignal(VfxSpritesSignals.CommandCreateVfx data)
        {
            var newEntity = World.NewEntity();
            var packedEntity = World.PackEntity(newEntity);
            Pooler.ViewApiLoadingMark.Add(newEntity);
            
            _assetPooler.GetAndExecute(data.AssetName, data.AddressablePath, data.Position, api =>
            {
                if (packedEntity.Unpack(World, out var entity))
                {
                    ref var newApi = ref Pooler.ViewApi.Add(entity);
                    newApi.Value = api;
                    Pooler.ViewApiLoadingMark.Del(entity);
                    newApi.Value.Release = () => _assetPooler.Release(api); 
                    newApi.Value.PlayAnimation(_settings.DeltaTime());
                }
                else
                {
                    _assetPooler.Release(api);
                }
            });
        }
    }
}
