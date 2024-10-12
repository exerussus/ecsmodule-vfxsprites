
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public class VfxSpritesGroup : EcsGroup<VfxSpritesPooler>
    {
        public VfxSpritesSettings Settings = new();
        
        protected override void OnBeforePoolInitializing(EcsWorld world, VfxSpritesPooler pooler)
        {
            Pooler.PreInitialize(Settings);
        }

        protected override void SetInitSystems(IEcsSystems initSystems)
        {
            initSystems.Add(new VfxCreatorSystem());
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new VfxProcessSystem());
        }

        protected override void SetSharingData(EcsWorld world, GameShare gameShare)
        {
            gameShare.AddSharedObject(Settings);
        }
    }
}
