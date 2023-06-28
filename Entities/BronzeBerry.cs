
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;

namespace Celeste.Mod.MinecireHelper.Entities
{
    [CustomEntity("MinecireHelper/BronzeBerry")]
    [RegisterStrawberry(tracked: false, blocksCollection: true)]

    public class BronzeBerry : Strawberry
    {
        private static ParticleType P_BronzeGlow;
        private static ParticleType P_OrigGoldGlow;
        private static ParticleType P_BronzeGhostGlow;
        private static ParticleType P_OrigGhostGlow;

        private bool spawnedThroughGiveBronze = false;

        public BronzeBerry(EntityData data, Vector2 offset, EntityID gid) : base(data, offset, gid)
        {
            new DynData<Strawberry>(this)["Golden"] = true;

            if (P_BronzeGlow == null)
            {
                P_BronzeGlow = new ParticleType(P_Glow)
                {
                    Color = Calc.HexToColor("BABBC0"),
                    Color2 = Calc.HexToColor("6A8497")
                };
                P_BronzeGhostGlow = new ParticleType(P_Glow)
                {
                    Color = Calc.HexToColor("818E9E"),
                    Color2 = Calc.HexToColor("36585B")
                };
                P_OrigGoldGlow = P_GoldGlow;
                P_OrigGhostGlow = P_GhostGlow;
            }
        }
        public override void Added(Scene scene)
        {
            base.Added(scene);

            Session session = (scene as Level).Session;
            if (!spawnedThroughGiveBronze && ((session.FurthestSeenLevel != session.Level && session.Deaths != 0) ||
                (!SaveData.Instance.CheatMode && !SaveData.Instance.Areas_Safe[session.Area.ID].Modes[(int)session.Area.Mode].Completed)))
            {

                // we went in a further screen and die, or didn't complete the level once yet: don't have the berry spawn.
                RemoveSelf();
            }
        }

        public override void Update()
        {
            P_GoldGlow = P_BronzeGlow;
            P_GhostGlow = P_BronzeGhostGlow;
            base.Update();
            P_GoldGlow = P_OrigGoldGlow;
            P_GhostGlow = P_OrigGhostGlow;
        }

        [Command("give_bronze", "(Minecire Helper) gives you a bronze strawberry")]
        private static void cmdGiveBronze()
        {
            if (Engine.Scene is Level level)
            {
                Player player = level.Tracker.GetEntity<Player>();
                if (player != null)
                {
                    EntityData entityData = new EntityData();
                    entityData.Position = player.Position + new Vector2(0f, -16f);
                    entityData.ID = Calc.Random.Next();
                    entityData.Name = "MinecireHelper/BronzeBerry";
                    BronzeBerry bronzeBerry = new BronzeBerry(entityData, Vector2.Zero, new EntityID(level.Session.Level, entityData.ID));
                    bronzeBerry.spawnedThroughGiveBronze = true;
                    level.Add(bronzeBerry);
                }
            }
        }
    }
}