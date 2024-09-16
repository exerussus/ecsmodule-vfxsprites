
using System;
using Exerussus._1OrganizerUI.Scripts.Pooling;
using UnityEngine;

namespace ECS.Modules.Exerussus.VfxSprites
{
    public class VfxSpriteApi : MonoBehaviour, ILoadAsset
    {
        [SerializeField] private string assetName;
        [SerializeField] private VFXPack startInfo;
        [SerializeField] private VFXPack processInfo;
        [SerializeField] private VFXPack endInfo;

        public SpriteRenderer spriteRenderer;
        public string AssetName => assetName;
        public Action Release;
        
        private UpdateType _currentState;
        private int _maxCount;
        private int _currentSprite;
        private float _maxLifeTime;
        private float LifeTimer { get; set; }
        public bool IsDone { get; private set; }
        
        public void PlayAnimation(float deltaTime)
        {
            LifeTimer = 0;
            _maxLifeTime = 0;

            SetAllStates(deltaTime);

            if (!startInfo.enabled && !processInfo.enabled && !endInfo.enabled)
            {
                IsDone = true;
                return;
            }

            InitStart();
        }
        
        public void PlayAnimation(float deltaTime, float duration)
        {
            LifeTimer = 0;
            SetAllStates(deltaTime);
            _maxLifeTime = duration;

            if (!startInfo.enabled && !processInfo.enabled && !endInfo.enabled)
            {
                IsDone = true;
                return;
            }

            InitStart();
        }

        public void AnimationUpdate(float deltaTime)
        {
            LifeTimer += deltaTime;
            
            switch (_currentState)
            {
                case UpdateType.Start:
                    UpdateStart();
                    break;
                case UpdateType.Process:
                    UpdateProcess();
                    break;
                case UpdateType.End:
                    UpdateEnd();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (IsDone)
            {
                spriteRenderer.sprite = null;
                _currentSprite = 0;
            }
        }

        private void SetAllStates(float deltaTime)
        {
            if (startInfo.enabled && startInfo.Sprite is { Length: > 0 }) _maxLifeTime += startInfo.Sprite.Length * deltaTime;
            else startInfo.enabled = false;

            if (processInfo.enabled && processInfo.Sprite is { Length: > 0 }) _maxLifeTime += processInfo.Sprite.Length * deltaTime;
            else processInfo.enabled = false;

            if (!endInfo.enabled || endInfo.Sprite is not { Length: > 0 }) endInfo.enabled = false;
        }

        private void InitStart()
        {
            if (!startInfo.enabled)
            {
                InitProcess();
                return;
            }
            
            IsDone = false;
            _currentState = UpdateType.Start;
            _currentSprite = 0;
            _maxCount = startInfo.Sprite.Length;
        }

        private void InitProcess()
        {
            if (!processInfo.enabled)
            {
                InitEnd();
                return;
            }
            
            IsDone = false;
            _currentState = UpdateType.Process;
            _currentSprite = 0;
            _maxCount = processInfo.Sprite.Length;
        }

        private void InitEnd()
        {
            if (!endInfo.enabled)
            {
                IsDone = true;
                return;
            }
            
            IsDone = false;
            _currentState = UpdateType.End;
            _currentSprite = 0;
            _maxCount = endInfo.Sprite.Length;
        }
        
        private void UpdateStart()
        {
            if (_currentSprite >= _maxCount) InitProcess();
            else
            {
                spriteRenderer.sprite = startInfo.Sprite[_currentSprite];
                _currentSprite++;
            }
        }

        private void UpdateProcess()
        {
            if (LifeTimer > _maxLifeTime) InitEnd();
            else
            {
                spriteRenderer.sprite = processInfo.Sprite[_currentSprite];
                _currentSprite++;
                if (_currentSprite >= _maxCount) _currentSprite = 0;
            }
        }

        private void UpdateEnd()
        {
            if (_currentSprite >= _maxCount) IsDone = true;
            else
            {
                spriteRenderer.sprite = endInfo.Sprite[_currentSprite];
                _currentSprite++;
            }
        }

        private enum UpdateType
        {
            Start, Process, End
        }
        
        [Serializable]
        public class VFXPack
        {
            [SerializeField] public bool enabled;
            [SerializeField] private Sprite[] sprites;

            public Sprite[] Sprite => sprites;
        }
    }
}