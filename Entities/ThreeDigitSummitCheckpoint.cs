using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod;
using MonoMod.Cil;
using MonoMod.Utils;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Celeste.Mod.MinecireHelper.Entities
{
    [CustomEntity("MinecireHelper/ThreeDigitSummitCheckpoint = Generate3DigitSummitCheckpoint")]
    public class ThreeDigitSummitCheckpoint : Entity
    {



		// Token: 0x06000FAF RID: 4015 RVA: 0x00042560 File Offset: 0x00040760
		public override void Render()
		{
			List<MTexture> obj = Activated ? numbersActive : numbersEmpty;
			MTexture mtexture = baseActive;
			if (!Activated)
			{
				mtexture = (base.Scene.BetweenInterval(0.25f) ? baseEmpty : baseToggle);
			}
			mtexture.Draw(Position - new Vector2((float)(mtexture.Width / 2 + 1), (float)(mtexture.Height / 2)));
			List<MTexture> obj2 = obj;
			obj2[0].DrawJustified(Position + new Vector2(-1.5f, 1f), new Vector2(1.5f, 0f));
			obj2[1].DrawJustified(Position + new Vector2(-0.5f, 1f), new Vector2(0.5f, 0f));
			obj2[2].DrawJustified(Position + new Vector2(0.5f, 1f), new Vector2(-0.5f, 0f));
		}

		// Token: 0x04000B14 RID: 2836
		private const string Flag = "summit_checkpoint_3_digit_";

		// Token: 0x04000B15 RID: 2837
		public bool Activated;

		// Token: 0x04000B16 RID: 2838
		public readonly int Number;

		// Token: 0x04000B17 RID: 2839
		private string numberString;

		// Token: 0x04000B18 RID: 2840
		private Vector2 respawn;

		// Token: 0x04000B19 RID: 2841
		private MTexture baseEmpty;

		// Token: 0x04000B1A RID: 2842
		private MTexture baseToggle;

		// Token: 0x04000B1B RID: 2843
		private MTexture baseActive;

		// Token: 0x04000B1C RID: 2844
		private List<MTexture> numbersEmpty;

		// Token: 0x04000B1D RID: 2845
		private List<MTexture> numbersActive;

		// Token: 0x020004CF RID: 1231
		public class ConfettiRenderer : Entity
		{
			// Token: 0x06002414 RID: 9236 RVA: 0x000F1C94 File Offset: 0x000EFE94
			public ConfettiRenderer(Vector2 position) : base(position)
			{
				base.Depth = -10010;
				for (int i = 0; i < this.particles.Length; i++)
				{
					this.particles[i].Position = this.Position + new Vector2((float)Calc.Random.Range(-3, 3), (float)Calc.Random.Range(-3, 3));
					this.particles[i].Color = Calc.Random.Choose(ThreeDigitSummitCheckpoint.ConfettiRenderer.confettiColors);
					this.particles[i].Timer = Calc.Random.NextFloat();
					this.particles[i].Duration = (float)Calc.Random.Range(2, 4);
					this.particles[i].Alpha = 1f;
					float angleRadians = -1.5707964f + Calc.Random.Range(-0.5f, 0.5f);
					int num = Calc.Random.Range(140, 220);
					this.particles[i].Speed = Calc.AngleToVector(angleRadians, (float)num);
				}
			}

			public override void Update()
			{
				for (int i = 0; i < this.particles.Length; i++)
				{
					ThreeDigitSummitCheckpoint.ConfettiRenderer.Particle[] array = this.particles;
					int num = i;
					array[num].Position = array[num].Position + this.particles[i].Speed * Engine.DeltaTime;
					this.particles[i].Speed.X = Calc.Approach(this.particles[i].Speed.X, 0f, 80f * Engine.DeltaTime);
					this.particles[i].Speed.Y = Calc.Approach(this.particles[i].Speed.Y, 20f, 500f * Engine.DeltaTime);
					ThreeDigitSummitCheckpoint.ConfettiRenderer.Particle[] array2 = this.particles;
					int num2 = i;
					array2[num2].Timer = array2[num2].Timer + Engine.DeltaTime;
					ThreeDigitSummitCheckpoint.ConfettiRenderer.Particle[] array3 = this.particles;
					int num3 = i;
					array3[num3].Percent = array3[num3].Percent + Engine.DeltaTime / this.particles[i].Duration;
					this.particles[i].Alpha = Calc.ClampedMap(this.particles[i].Percent, 0.9f, 1f, 1f, 0f);
					if (this.particles[i].Speed.Y > 0f)
					{
						this.particles[i].Approach = Calc.Approach(this.particles[i].Approach, 5f, Engine.DeltaTime * 16f);
					}
				}
			}

			// Token: 0x06002416 RID: 9238 RVA: 0x000F1F8C File Offset: 0x000F018C
			public override void Render()
			{
				for (int i = 0; i < this.particles.Length; i++)
				{
					Vector2 vector = this.particles[i].Position;
					float num;
					if (this.particles[i].Speed.Y < 0f)
					{
						num = this.particles[i].Speed.Angle();
					}
					else
					{
						num = (float)Math.Sin((double)(this.particles[i].Timer * 4f)) * 1f;
						vector += Calc.AngleToVector(1.5707964f + num, this.particles[i].Approach);
					}
					GFX.Game["particles/confetti"].DrawCentered(vector + Vector2.UnitY, Color.Black * (this.particles[i].Alpha * 0.5f), 1f, num);
					GFX.Game["particles/confetti"].DrawCentered(vector, this.particles[i].Color * this.particles[i].Alpha, 1f, num);
				}
			}

			// Token: 0x040023B6 RID: 9142
			private static readonly Color[] confettiColors = new Color[]
			{
				Calc.HexToColor("fe2074"),
				Calc.HexToColor("205efe"),
				Calc.HexToColor("cefe20")
			};

			// Token: 0x040023B7 RID: 9143
			private ThreeDigitSummitCheckpoint.ConfettiRenderer.Particle[] particles = new ThreeDigitSummitCheckpoint.ConfettiRenderer.Particle[30];

			// Token: 0x0200078B RID: 1931
			private struct Particle
			{
				// Token: 0x04002F88 RID: 12168
				public Vector2 Position;

				// Token: 0x04002F89 RID: 12169
				public Color Color;

				// Token: 0x04002F8A RID: 12170
				public Vector2 Speed;

				// Token: 0x04002F8B RID: 12171
				public float Timer;

				// Token: 0x04002F8C RID: 12172
				public float Percent;

				// Token: 0x04002F8D RID: 12173
				public float Duration;

				// Token: 0x04002F8E RID: 12174
				public float Alpha;

				// Token: 0x04002F8F RID: 12175
				public float Approach;
			}
		}



		private static FieldInfo confettiColorsFieldInfo = typeof(ConfettiRenderer).GetField("confettiColors", BindingFlags.NonPublic | BindingFlags.Static);

        public static Entity Generate3DigitSummitCheckpoint(Level level, LevelData levelData, Vector2 offset, EntityData entityData)
        {
            // internally, the number will be the entity ID to ensure all "summit_checkpoint_{number}" session flags are unique.
            // we also add 100 to be sure not to conflict with vanilla checkpoints.
            entityData.Values["number"] = entityData.ID + 100;
            return new ThreeDigitSummitCheckpoint(entityData, offset);
        }

        private readonly Color[] confettiColors;
        private readonly string groupFlag;

        public ThreeDigitSummitCheckpoint(EntityData data, Vector2 offset) : base(data.Position + offset)
        {
            confettiColors = parseColors(data.Attr("confettiColors", "fe2074,205efe,cefe20"));
            groupFlag = data.Attr("groupFlag");

            DynData<ThreeDigitSummitCheckpoint> self = new DynData<ThreeDigitSummitCheckpoint>(this);

            string firstDigit = data.Attr("firstDigit");
            string secondDigit = data.Attr("secondDigit");
            string thirdDigit = data.Attr("thirdDigit");

            string directory = data.Attr("spriteDirectory", defaultValue: "MinecireHelper/summitcheckpoints_3digit");

            // reshuffle the loaded textures: the first wanted digit at index 0, the second one at index 1.
            // so, the string to display is always "01" no matter what
            self["numberString"] = "012";
            self["numbersEmpty"] = new List<MTexture>() {
                GFX.Game[$"{directory}/{firstDigit}/numberbg"],
                GFX.Game[$"{directory}/{secondDigit}/numberbg"],
                GFX.Game[$"{directory}/{thirdDigit}/numberbg"]
            };
            self["numbersActive"] = new List<MTexture>() {
                GFX.Game[$"{directory}/{firstDigit}/number"],
                GFX.Game[$"{directory}/{secondDigit}/number"],
                GFX.Game[$"{directory}/{thirdDigit}/number"]
            };

            // customize the background textures.
            self["baseEmpty"] = GFX.Game[$"{directory}/base00"];
            self["baseToggle"] = GFX.Game[$"{directory}/base01"];
            self["baseActive"] = GFX.Game[$"{directory}/base02"];

			this.Number = data.Int("number", 0);
			base.Collider = new Hitbox(32f, 32f, -16f, -8f);
			base.Depth = 8999;
		}

        private static Color[] parseColors(string input)
        {
            string[] colorsAsStrings = input.Split(',');
            Color[] colors = new Color[colorsAsStrings.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Calc.HexToColor(colorsAsStrings[i]);
            }
            return colors;
        }

        public override void Added(Scene scene)
        {
            base.Added(scene);

			if ((scene as Level).Session.GetFlag(Flag + this.Number))
			{
				this.Activated = true;
			}
			this.respawn = base.SceneAs<Level>().GetSpawnPoint(this.Position);

			// if another checkpoint in the group was activated, we should activate this checkpoint.
			if (!string.IsNullOrEmpty(groupFlag) && (scene as Level).Session.GetFlag(groupFlag))
            {
                Activated = true;
            }

		}

        public override void Awake(Scene scene)
        {
            bool wasActivated = Activated;

            base.Awake(scene);

			if (!this.Activated && base.CollideCheck<Player>())
			{
				this.Activated = true;
				Level level = base.Scene as Level;
				level.Session.SetFlag(Flag + this.Number, true);
				level.Session.RespawnPoint = new Vector2?(this.respawn);
			}

			if (!wasActivated && Activated && !string.IsNullOrEmpty(groupFlag))
            {
                // summit checkpoint was enabled due to spawning on it, enable the rest of the group without any effects.
                triggerGroupFlag(otherCheckpoint => otherCheckpoint.Activated = true);
            }
        }

        public override void Update()
        {
            bool wasActivated = Activated;

            if (!Activated && CollideCheck<Player>())
            {
                // player is potentially triggering the checkpoint => change the confetti colors!
                runWithModdedConfetti(confettiColors, () =>
                {
					if (!this.Activated)
					{
						Player player = base.CollideFirst<Player>();
						if (player != null && player.OnGround(1) && player.Speed.Y >= 0f)
						{
							Level level = base.Scene as Level;
							this.Activated = true;
							level.Session.SetFlag(Flag + this.Number, true);
							level.Session.RespawnPoint = new Vector2?(this.respawn);
							level.Session.UpdateLevelStartDashes();
							level.Session.HitCheckpoint = true;
							level.Displacement.AddBurst(this.Position, 0.5f, 4f, 24f, 0.5f, null, null);
							level.Add(new ConfettiRenderer(this.Position));
							Audio.Play("event:/game/07_summit/checkpoint_confetti", this.Position);
						}
					}
				});
            }
            else
            {
				// checkpoint can't be triggered, so don't mess with confetti to avoid useless reflection.
				if (!this.Activated)
				{
					Player player = base.CollideFirst<Player>();
					if (player != null && player.OnGround(1) && player.Speed.Y >= 0f)
					{
						Level level = base.Scene as Level;
						this.Activated = true;
						level.Session.SetFlag(Flag + this.Number, true);
						level.Session.RespawnPoint = new Vector2?(this.respawn);
						level.Session.UpdateLevelStartDashes();
						level.Session.HitCheckpoint = true;
						level.Displacement.AddBurst(this.Position, 0.5f, 4f, 24f, 0.5f, null, null);
						level.Add(new ConfettiRenderer(this.Position));
						Audio.Play("event:/game/07_summit/checkpoint_confetti", this.Position);
					}
				}

			}

            if (!wasActivated && Activated && !string.IsNullOrEmpty(groupFlag))
            {
                // enable entire group in a similar way, with the confetti effect.
                triggerGroupFlag(otherCheckpoint => {
                    otherCheckpoint.Activated = true;
                    otherCheckpoint.SceneAs<Level>().Displacement.AddBurst(otherCheckpoint.Position, 0.5f, 4f, 24f, 0.5f);

                    runWithModdedConfetti(otherCheckpoint.confettiColors, () => {
                        otherCheckpoint.SceneAs<Level>().Add(new ConfettiRenderer(otherCheckpoint.Position));
                    });

                    Audio.Play("event:/game/07_summit/checkpoint_confetti", otherCheckpoint.Position);
                });
            }
        }

        private static void runWithModdedConfetti(Color[] confettiColors, Action toRun)
        {
            Color[] vanillaConfetti = (Color[])confettiColorsFieldInfo.GetValue(null);
            confettiColorsFieldInfo.SetValue(null, confettiColors);

            toRun();

            confettiColorsFieldInfo.SetValue(null, vanillaConfetti);
        }

        private void triggerGroupFlag(Action<ThreeDigitSummitCheckpoint> actionOnEntireGroup)
        {
            // if this checkpoint was just activated, it should set the group flag...
            SceneAs<Level>().Session.SetFlag(groupFlag);

            // and look for other summit checkpoints that have the same group flag, in order to activate them.
            foreach (ThreeDigitSummitCheckpoint other in Scene.Tracker.GetEntities<ThreeDigitSummitCheckpoint>())
            {
                if (!other.Activated && other.groupFlag == groupFlag)
                {
                    actionOnEntireGroup(other);
                }
            }
        }
    }
}
