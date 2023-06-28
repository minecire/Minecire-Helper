using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Monocle;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using System;
using System.Collections;
using System.Linq;

namespace Celeste.Mod.MinecireHelper.Entities
{
    /// <summary>
    /// This class sets up some hooks that will be useful for silver berries, speed berries and rainbow berries.
    /// They mod the following things:
    /// - strawberry sprite for silvers and rainbows
    /// - death sounds for silvers, speeds, and both at the same time
    /// - collect sounds for silvers and rainbows
    /// </summary>
    static class StrawberryHooks
    {

        internal static void Load()
        {
            IL.Celeste.Strawberry.Added += modStrawberrySprite;
            On.Celeste.Strawberry.CollectRoutine += onStrawberryCollectRoutine;

            // Any other mod blocking calls to Die to make Madeline invincible (like shadow dashes) should be able to also block the call to that hook on Die.
            // Otherwise, speed berries turn not golden and collect when Madeline is on the ground. This is bad.
            using (new DetourContext { Before = { "*" } })
            {
                On.Celeste.Player.Die += onPlayerDie;
            }
        }

        internal static void Unload()
        {
            IL.Celeste.Strawberry.Added -= modStrawberrySprite;
            On.Celeste.Strawberry.CollectRoutine -= onStrawberryCollectRoutine;

            On.Celeste.Player.Die -= onPlayerDie;
        }

        private static void modStrawberrySprite(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);

            // catch the moment where the sprite is added to the entity
            if (cursor.TryGotoNext(
                instr => instr.MatchLdarg(0),
                instr => instr.MatchLdfld<Strawberry>("sprite"),
                instr => instr.MatchCall<Entity>("Add")))
            {

                cursor.Index++;

                FieldReference strawberrySprite = cursor.Next.Operand as FieldReference;

                // mod the sprite
                Logger.Log("MinecireHelper/StrawberryHooks", $"Modding strawberry sprite at {cursor.Index} in IL for Strawberry.Added");

                cursor.Emit(OpCodes.Ldarg_0); // for stfld
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); // for the delegate call
                cursor.EmitDelegate<Func<Sprite, Strawberry, Sprite>>((orig, self) => {
                    // this method determines the strawberry sprite. "orig" is the original sprite, "self" is the strawberry.
                    if (self is BronzeBerry)
                    {
                        if (SaveData.Instance.CheckStrawberry(self.ID))
                        {
                            return GFX.SpriteBank.Create("MinecireHelper_ghostBronzeBerry");
                        }
                        return GFX.SpriteBank.Create("MinecireHelper_bronzeBerry");
                    }
                    return orig;
                });
                cursor.Emit(OpCodes.Stfld, strawberrySprite);
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldfld, strawberrySprite);
            }
        }



        private static PlayerDeadBody onPlayerDie(On.Celeste.Player.orig_Die orig, Player self, Vector2 direction, bool evenIfInvincible, bool registerDeathInStats)
        {
            bool hasBronze = false;

            // check if the player is actually going to die first.
            if (!self.Dead && (evenIfInvincible || !SaveData.Instance.Assists.Invincible) && self.StateMachine.State != Player.StReflectionFall)
            {
                hasBronze = self.Leader.Followers.Any(follower => follower.Entity is BronzeBerry);
            }

            PlayerDeadBody body = orig(self, direction, evenIfInvincible, registerDeathInStats);

            if (body != null)
            {
                DynData<PlayerDeadBody> data = new DynData<PlayerDeadBody>(body);
                data["hasBronze"] = hasBronze;
            }
            return body;
        }


        private static IEnumerator onStrawberryCollectRoutine(On.Celeste.Strawberry.orig_CollectRoutine orig, Strawberry self, int collectIndex)
        {
            Scene scene = self.Scene;

            IEnumerator origEnum = orig(self, collectIndex);
            while (origEnum.MoveNext())
            {
                yield return origEnum.Current;
            }

        }
    }
}