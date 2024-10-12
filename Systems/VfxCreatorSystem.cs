
using Exerussus._1EasyEcs.Scripts.Core;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public class VfxCreatorSystem : EcsSignalListener<VfxSpritesPooler, VfxSpritesSignals.CommandCreateVfx>
    {
        private VfxSpritesSettings _settings;


        protected override void OnSignal(VfxSpritesSignals.CommandCreateVfx data)
        {
            Pooler.CreateVfx(data.AssetName, data.AddressablePath, data.Position);
        }
    }
}
