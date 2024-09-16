Модуль для 1EasyEcs.   
Создает vfx и пулирует по вызову.

````csharp
     public static class VfxSpritesSignals
    {
        public struct CommandCreateVfx
        {
            public Vector3 Position;
            public string AssetName;
            public string AddressablePath;
        }
    }
````

`AssetName` указывается в **`VfxSpriteApi`**, а `AddressablePath` - в Addressables.   
Необходимо подготовить префаб с компонентом `VfxSpriteApi` и сделать его Addressable.

Зависимости:    
[Ecs-Lite](https://github.com/Leopotam/ecslite.git)  
[1EasyEcs](https://github.com/exerussus/1EasyEcs.git)  
[1Extensions](https://github.com/exerussus/1Extensions.git)  
[Organizer-UI](https://github.com/exerussus/1organizer-ui.git)  
[Addressables](https://docs.unity3d.com/Manual/com.unity.addressables.html)