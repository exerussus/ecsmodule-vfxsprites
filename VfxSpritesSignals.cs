
using UnityEngine;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public static class VfxSpritesSignals
    {
        public struct CommandCreateVfx
        {
            public Vector3 Position;
            public string AssetName;
            public string AddressablePath;
        }
    }
}
