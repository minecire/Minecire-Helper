

using Celeste.Mod;
using Celeste.Mod.MinecireHelper.Entities;
using Celeste.Mod.MinecireHelper.Triggers;
using Monocle;


using System;

namespace Celeste.Mod.MinecireHelper.Module
{
    public class MinecireHelperModule : EverestModule
    {

        // Only one alive module instance can exist at any given time.
        public static MinecireHelperModule Instance;

        public MinecireHelperModule()
        {
            Instance = this;
        }

        public override Type SessionType => typeof(MinecireHelperSession);
        public static MinecireHelperSession Session => (MinecireHelperSession)Instance._Session;

        // Set up any hooks, event handlers and your mod in general here.
        // Load runs before Celeste itself has initialized properly.
        public override void Load()
        {
            StrawberryHooks.Load();
            BronzeBerryCollectTrigger.Load();
        }

        // Optional, initialize anything after Celeste has initialized itself properly.
        public override void Initialize()
        {
        }

        // Optional, do anything requiring either the Celeste or mod content here.
        public override void LoadContent(bool firstLoad)
        {
        }

        // Unload the entirety of your mod's content. Free up any native resources.
        public override void Unload()
        {

            StrawberryHooks.Unload();
            BronzeBerryCollectTrigger.Unload();
        }

    }
}