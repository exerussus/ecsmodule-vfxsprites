
using System;
using UnityEngine;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public class VfxSpritesSettings
    {
        public AddressableInfo[] AddressableInfos;
        public Func<float> DeltaTime = () => Time.fixedDeltaTime;
    }
    
    [Serializable]
    public struct AddressableInfo
    {
        public string name;
        public string path;
    }
}