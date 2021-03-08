using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsoulSharp;
using EnsoulSharp.SDK;
using EnsoulSharp.SDK.MenuUI;
using SharpDX;

namespace DeveloperSharp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			GameEvent.OnGameLoad += new GameEvent.OnGameLoadDelegate(Program.GameEventOnOnGameLoad);
		}

		public static AIHeroClient Player
		{
			get
			{
				return ObjectManager.Player;
			}
		}

		private static void GameEventOnOnGameLoad()
		{
			var menu = new Menu("xDreammsDeveloperSharp", "xDreamms DeveloperSharp", true);
			menu.Attach();
			GameObject.OnCreate += Program.GameObjectOnOnCreate;
			GameObject.OnDelete += Program.GameObjectOnOnDelete;
			MissileClient.OnCreate += Program.GameObjectOnOnMissileCreate;
			AIBaseClient.OnBuffAdd += Program.AiHeroClientOnOnBuffGain;
			AIBaseClient.OnDoCast += Program.AiHeroClientOnOnDoCast;
            MissileClient.OnCreate += Program.AiHeroClientOnOnMissileCreate;
			AIBaseClient.OnProcessSpellCast += Program.AiHeroClientOnOnProcessSpellCast;
			Drawing.OnDraw += Program.DrawingOnOnDraw;
			GameObject.OnCreate += Program.AiMinionClientOnOnCreate;
		}

		private static void GameObjectOnOnMissileCreate(GameObject sender, EventArgs args)
		{
			if (sender.Team == Program.Player.Team && sender.Position.Distance( Program.Player.Position) < 1000f)
			{
				Program.OnOnMissileCreateObjectName = "Object Name: " + sender.Name;
				Program.OnOnMissileCreateObjectBoundingRadius = "Bounding Radius: " + sender.BoundingRadius;
				Program.OnOnMissileCreateObjectType = "Object Type: " + sender.Type;
				Program.OnOnMissileCreateObjectPosition = "Position: " + sender.Position;
			}
		}

		private static void GameObjectOnOnDelete(GameObject sender, EventArgs args)
		{
			if (sender.Team == Program.Player.Team && sender.Position.Distance( Program.Player.Position) < 1000f)
			{
				Program.OnDeleteObjectName = "Object Name: " + sender.Name;
				Program.OnDeleteObjectBoundingRadius = "Bounding Radius: " + sender.BoundingRadius;
				Program.OnDeleteObjectType = "Object Type: " + sender.Type;
				Program.OnDeleteObjectPosition = "Position: " + sender.Position;
			}
		}

		private static void GameObjectOnOnCreate(GameObject sender, EventArgs args)
		{
			if (sender.Team == Program.Player.Team && sender.Position.Distance( Program.Player.Position) < 1000f)
			{
				Program.OnCreateObjectName = "Object Name: " + sender.Name;
				Program.OnCreateObjectBoundingRadius = "Bounding Radius: " + sender.BoundingRadius;
				Program.OnCreateObjectType = "Object Type: " + sender.Type;
				Program.OnCreateObjectPosition = "Position: " + sender.Position;
			}
		}

		private static void AiHeroClientOnOnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
		{
			if (!sender.IsMe)
			{
				Console.WriteLine("Hero Name: " + sender.CharacterName.ToString() + ", Spell Name: " + args.SData.Name.ToString());
			}
			else
			{
				Program.Slot = "SpellSlot: " + args.Slot;
				Program.CastTime = "CastTime: " + args.CastTime;
				//Program.Time = "Tİme: " + args.Time;
				Program.TotalTime = "Total Time: " + args.TotalTime;
				Program.CastRadius = "Cast Radius: " + args.SData.CastRadius;
				Program.CastRange = "Cast Range:  " + args.SData.CastRange;
				Program.LineWidth = "Line Width : " + args.SData.LineWidth;
				Program.LineDragLength = "Line Drag Length: " + args.SData.LineDragLength;
				Program.MissileSpeed = "Missile Speed:  " + args.SData.MissileSpeed;
				Program.Name = "Name : " + args.SData.Name;
				Program.CastType = "Cast Type:  " + args.SData.CastType;
				Program.CastConeAngle = "Cast Cone Angle: " + args.SData.CastConeAngle;
				Program.CastConeDistance = "Cast Cone Distance: " + args.SData.CastConeDistance;
			}
		}

		private static void AiHeroClientOnOnMissileCreate(GameObject sender, EventArgs args)
		{
			if (sender.IsAlly && sender.Position.Distance( Program.Player.Position) < 1000f)
			{
				Program.MissileName = "Missile Name: " + sender.Name;
				Program.MissilePosition = "Missile Position: " + sender.Position;
				Program.MissileBoundingRadius = "Missile Bounding Radius: " + sender.BoundingRadius;
				Program.MissileNetworkID = "Missile Network ID: " + sender.NetworkId;
				Program.MissileType = "Missile Type: " + sender.Type;
			}
		}

		private static void AiHeroClientOnOnDoCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
		{
			if (sender.IsMe)
			{
				Program.OnDoCastSpellName = "Spell Name: " + args.SData.Name;
				Program.OnDoCastSpellSpeed = "Spell Speed: " + args.SData.MissileSpeed;
				Program.OnDoCastSpellCastRadius = "Cast Radius: " + args.SData.CastRadius;
				Program.OnDoCastCastRange = "Cast Range: " + args.SData.CastRange;
				Program.OnDoCastTime = "Cast Time: " + args.CastTime;
				Program.OnDoCastWidth = "Line Width: " + args.SData.LineWidth;
			}
		}

		private static void AiHeroClientOnOnBuffGain(AIBaseClient sender, AIBaseClientBuffAddEventArgs args)
		{
			if (args.Buff.Name.Contains("ASSETS"))
			{
				return;
			}
			if (sender.IsMe)
			{
				Console.WriteLine("Hero Name: " + sender.CharacterName.ToString(), "My Buff Name: " + args.Buff.Name.ToString());
				Program.MyBuffName = "My Buff Name: " + args.Buff.Name;
				Program.MyBuffCount = "My Buff Count: " + args.Buff.Count;
				Program.MyBuffType = "My Buff Type: " + args.Buff.Type;
				return;
			}
			Console.WriteLine("Hero Name: " + sender.CharacterName.ToString(), "Enemy Buff Name: " + args.Buff.Name.ToString());
			Program.EnemyBuffName = "Enemy Buff Name: " + args.Buff.Name;
			Program.EnemyBuffCount = "Enemy Buff Count: " + args.Buff.Count;
			Program.EnemyBuffType = "Enemy Buff Type: " + args.Buff.Type;
		}

		private static void AiMinionClientOnOnCreate(GameObject sender, EventArgs args)
		{
			if (sender.Position.Distance( Program.Player.Position) < 1000f && sender.Team == Program.Player.Team && !sender.Name.ToLower().Contains("turret"))
			{
				Program.MinionName = "Minion Name: " + sender.Name;
				Program.MinionType = "Minion Type: " + sender.Type;
				Program.MinionPosition = "Minion Position: " + sender.Position;
			}
		}

		private static void DrawingOnOnDraw(EventArgs args)
		{
			Drawing.DrawText(1000, 205f, System.Drawing.Color.White, "OnBuffGain");
			Drawing.DrawText(1000, 215f, System.Drawing.Color.White, "=====================================");

			if (!string.IsNullOrWhiteSpace(Program.MyBuffName))
			{
				Drawing.DrawText(1000, 235, System.Drawing.Color.White, Program.MyBuffName);
			}
			if (!string.IsNullOrWhiteSpace(Program.MyBuffCount))
			{
				Drawing.DrawText(1000, 250, System.Drawing.Color.White, Program.MyBuffCount);
			}
			if (!string.IsNullOrWhiteSpace(Program.MyBuffType))
			{
				Drawing.DrawText(1000, 265, System.Drawing.Color.White, Program.MyBuffType);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffName))
			{
				Drawing.DrawText(1000, 290, System.Drawing.Color.White, Program.EnemyBuffName);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffCount))
			{
				Drawing.DrawText(1000, 305, System.Drawing.Color.White, Program.EnemyBuffCount);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 320, System.Drawing.Color.White, Program.EnemyBuffType);
			}
			Drawing.DrawText(1000, 340, System.Drawing.Color.White, "OnProcessSpellCast");
			Drawing.DrawText(1000, 350, System.Drawing.Color.White, "=====================================");
			if (!string.IsNullOrWhiteSpace(Program.Slot))
			{
				Drawing.DrawText(1000, 365, System.Drawing.Color.White, Program.Slot);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 380, System.Drawing.Color.White, Program.CastTime);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 395, System.Drawing.Color.White, Program.Time);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 410, System.Drawing.Color.White, Program.TotalTime);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 425, System.Drawing.Color.White, Program.CastRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 440, System.Drawing.Color.White, Program.CastRange);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 455, System.Drawing.Color.White, Program.LineWidth);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 470, System.Drawing.Color.White, Program.LineDragLength);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 485, System.Drawing.Color.White, Program.MissileSpeed);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 500, System.Drawing.Color.White, Program.Name);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 515, System.Drawing.Color.White, Program.CastType);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 530, System.Drawing.Color.White, Program.CastConeAngle);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(1000, 545, System.Drawing.Color.White, Program.CastConeDistance);
			}
			Drawing.DrawText(1000, 595, System.Drawing.Color.White, "OnMissileCreate");
			Drawing.DrawText(1000, 605, System.Drawing.Color.White, "=====================================");
			if (!string.IsNullOrWhiteSpace(Program.MissileName))
			{
				Drawing.DrawText(1000, 580, System.Drawing.Color.White, Program.MissileName);
			}
			if (!string.IsNullOrWhiteSpace(Program.MissilePosition))
			{
				Drawing.DrawText(1000, 585, System.Drawing.Color.White, Program.MissilePosition);
			}
			if (!string.IsNullOrWhiteSpace(Program.MissileBoundingRadius))
			{
				Drawing.DrawText(1000, 600, System.Drawing.Color.White, Program.MissileBoundingRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.MissileNetworkID))
			{
				Drawing.DrawText(1000, 615, System.Drawing.Color.White, Program.MissileNetworkID);
			}
			if (!string.IsNullOrWhiteSpace(Program.MissileType))
			{
				Drawing.DrawText(1000, 630, System.Drawing.Color.White, Program.MissileType);
			}
			Drawing.DrawText(1000, 650, System.Drawing.Color.White, "OnDoCast");
			Drawing.DrawText(1000, 660, System.Drawing.Color.White, "=====================================");

			if (!string.IsNullOrWhiteSpace(Program.OnDoCastSpellName))
			{
				Drawing.DrawText(1000, 675, System.Drawing.Color.White, Program.OnDoCastSpellName);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastSpellSpeed))
			{
				Drawing.DrawText(1000, 690, System.Drawing.Color.White, Program.OnDoCastSpellSpeed);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastSpellCastRadius))
			{
				Drawing.DrawText(1000, 705, System.Drawing.Color.White, Program.OnDoCastSpellCastRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastCastRange))
			{
				Drawing.DrawText(1000, 720, System.Drawing.Color.White, Program.OnDoCastCastRange);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastTime))
			{
				Drawing.DrawText(1000, 735, System.Drawing.Color.White, Program.OnDoCastTime);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastWidth))
			{
				Drawing.DrawText(1000, 750, System.Drawing.Color.White, Program.OnDoCastWidth);
			}
			Drawing.DrawText(1000, 770, System.Drawing.Color.White, "MinionCreate");
			Drawing.DrawText(1000, 780, System.Drawing.Color.White, "=====================================");

			if (!string.IsNullOrWhiteSpace(Program.MinionName))
			{
				Drawing.DrawText(1000, 800, System.Drawing.Color.White, Program.MinionName);
			}
			if (!string.IsNullOrWhiteSpace(Program.MinionType))
			{
				Drawing.DrawText(1000, 815, System.Drawing.Color.White, Program.MinionType);
			}
			if (!string.IsNullOrWhiteSpace(Program.MinionPosition))
			{
				Drawing.DrawText(1000, 830, System.Drawing.Color.White, Program.MinionPosition);
			}
			Drawing.DrawText(1300, 205f, System.Drawing.Color.White, "GameObjectOnCreate");
			Drawing.DrawText(1300, 215f, System.Drawing.Color.White, "=====================================");

			if (!string.IsNullOrWhiteSpace(Program.OnCreateObjectName))
			{
				Drawing.DrawText(1300, 235, System.Drawing.Color.White, Program.OnCreateObjectName);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnCreateObjectType))
			{
				Drawing.DrawText(1300, 250, System.Drawing.Color.White, Program.OnCreateObjectType);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnCreateObjectBoundingRadius))
			{
				Drawing.DrawText(1300, 265, System.Drawing.Color.White, Program.OnCreateObjectBoundingRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnCreateObjectPosition))
			{
				Drawing.DrawText(1300, 280, System.Drawing.Color.White, Program.OnCreateObjectPosition);
			}
			Drawing.DrawText(1300, 300, System.Drawing.Color.White, "GameObjectOnDelete");
			Drawing.DrawText(1300, 310, System.Drawing.Color.White, "=====================================");

			if (!string.IsNullOrWhiteSpace(Program.OnDeleteObjectName))
			{
				Drawing.DrawText(1300, 330, System.Drawing.Color.White, Program.OnDeleteObjectName);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDeleteObjectType))
			{
				Drawing.DrawText(1300, 345, System.Drawing.Color.White, Program.OnDeleteObjectType);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDeleteObjectBoundingRadius))
			{
				Drawing.DrawText(1300, 360, System.Drawing.Color.White, Program.OnDeleteObjectBoundingRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDeleteObjectPosition))
			{
				Drawing.DrawText(1300, 375, System.Drawing.Color.White, Program.OnDeleteObjectPosition);
			}
			Drawing.DrawText(1300, 395, System.Drawing.Color.White, "GameObjectOnMissileCreate");
			Drawing.DrawText(1300, 405, System.Drawing.Color.White, "=====================================");

			if (!string.IsNullOrWhiteSpace(Program.OnOnMissileCreateObjectName))
			{
				Drawing.DrawText(1300, 425, System.Drawing.Color.White, Program.OnOnMissileCreateObjectName);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnOnMissileCreateObjectType))
			{
				Drawing.DrawText(1300, 440, System.Drawing.Color.White, Program.OnOnMissileCreateObjectType);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnOnMissileCreateObjectBoundingRadius))
			{
				Drawing.DrawText(1300, 455, System.Drawing.Color.White, Program.OnOnMissileCreateObjectBoundingRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnOnMissileCreateObjectPosition))
			{
				Drawing.DrawText(1300, 470, System.Drawing.Color.White, Program.OnOnMissileCreateObjectPosition);
			}
		}
		public static void DrawText(Vector3 position, float addPosX, float addPosY, System.Drawing.Color Color, string text, bool checkValue)
		{
			if (checkValue)
			{
				Vector2 vector = Drawing.WorldToScreen(position);
				Drawing.DrawText(vector.X + addPosX, vector.Y + addPosY, Color, text);
			}
		}

		private static string OnOnMissileCreateObjectName;

		private static string OnOnMissileCreateObjectType;

		private static string OnOnMissileCreateObjectBoundingRadius;

		private static string OnOnMissileCreateObjectPosition;

		private static string OnDeleteObjectName;

		private static string OnDeleteObjectType;

		private static string OnDeleteObjectBoundingRadius;

		private static string OnDeleteObjectPosition;

		private static string OnCreateObjectName;

		private static string OnCreateObjectType;

		private static string OnCreateObjectBoundingRadius;

		private static string OnCreateObjectPosition;

		private static string Slot;

		private static string CastTime;

		private static string Time;

		private static string TotalTime;

		private static string CastRadius;

		private static string CastRange;

		private static string LineWidth;

		private static string LineDragLength;

		private static string MissileSpeed;

		private static string Name;

		private static string CastType;
		private static string CastConeAngle;
		private static string CastConeDistance;
		private static string MissileName;

		private static string MissilePosition;

		private static string MissileBoundingRadius;

		private static string MissileNetworkID;

		private static string MissileType;

		private static string OnDoCastSpellName;

		private static string OnDoCastSpellSpeed;

		private static string OnDoCastSpellCastRadius;

		private static string OnDoCastCastRange;

		private static string OnDoCastTime;

		private static string OnDoCastWidth;

		private static string MyBuffName;

		private static string MyBuffCount;

		private static string MyBuffType;

		private static string EnemyBuffName;

		private static string EnemyBuffCount;

		private static string EnemyBuffType;

		private static string MinionName;

		private static string MinionType;

		private static string MinionPosition;
	}
}
