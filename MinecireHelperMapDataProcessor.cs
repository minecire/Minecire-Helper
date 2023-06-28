using System;
using System.Collections.Generic;
namespace Celeste.Mod.MinecireHelper
{
    class MinecireHelperMapDataProcessor : EverestMapDataProcessor
    {

        // the structure here is: SilverBerries[LevelSet][SID] = ID of the silver berry in that map.
        // so, to check if all silvers in a levelset have been unlocked, go through all entries in SilverBerries[levelset].
        public static Dictionary<string, Dictionary<string, EntityID>> BronzeBerries = new Dictionary<string, Dictionary<string, EntityID>>();

        private string levelName;

        public static HashSet<string> MapsWithBronzeBerries = new HashSet<string>();

        public override Dictionary<string, Action<BinaryPacker.Element>> Init()
        {
            return new Dictionary<string, Action<BinaryPacker.Element>> {
                {
                    "level", level => {
                        // be sure to write the level name down.
                        levelName = level.Attr("name").Split(':')[0];
                        if (levelName.StartsWith("lvl_")) {
                            levelName = levelName.Substring(4);
                        }
                    }
                },
                {
                    "entity:MinecireHelper/BronzeBerry", bronzeBerry => {
                        if (!BronzeBerries.TryGetValue(AreaKey.GetLevelSet(), out Dictionary<string, EntityID> allBronzesInLevelSet)) {
                            allBronzesInLevelSet = new Dictionary<string, EntityID>();
                            BronzeBerries.Add(AreaKey.GetLevelSet(), allBronzesInLevelSet);
                        }
                        allBronzesInLevelSet[AreaKey.GetSID()] = new EntityID(levelName, bronzeBerry.AttrInt("id"));
                        MapsWithBronzeBerries.Add(AreaKey.GetSID());
                    }
                }
            };
        }


        public override void Reset()
        {
            if (BronzeBerries.ContainsKey(AreaKey.GetLevelSet()))
            {
                BronzeBerries[AreaKey.GetLevelSet()].Remove(AreaKey.GetSID());
            }
            MapsWithBronzeBerries.Remove(AreaKey.GetSID());
        }

        public override void End()
        {
            // nothing to do here
        }
    }
}