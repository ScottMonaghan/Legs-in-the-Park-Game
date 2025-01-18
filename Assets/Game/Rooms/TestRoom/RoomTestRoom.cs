using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomTestRoom : RoomScript<RoomTestRoom>
{


	IEnumerator OnWalkTo()
	{
		Vector2 _mouse_position = E.GetMousePosition();
		yield return C.Plr.WalkTo(_mouse_position);
		eFace _originalPlayerFacing = C.Plr.Facing;
		yield return C.Elsa.Face(C.Player);
		yield return C.Elsa.Say("Dad!");
		yield return C.Plr.Face(C.Elsa);
		yield return C.Elsa.Say("Wait up!");
		yield return C.Player.Say("I'm waiting.");
		yield return C.Elsa.WalkTo(C.Player);
		yield return C.Plr.Face(_originalPlayerFacing);
		yield return C.Elsa.Face(C.Player.Facing);
		
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotSeedyAlley( IHotspot hotspot )
	{
		Region("SeedyAlley").Walkable = true;
		yield return C.Plr.Face(Hotspot("SeedyAlley"));
		yield return E.WaitSkip(1.0f);
		yield return C.Plr.Face(eFace.Down);
		yield return C.Player.Say("That's a seedy alley!");
		yield return E.WaitSkip(1.0f);
		yield return C.Plr.Face(Hotspot("SeedyAlley"));
		yield return E.WaitSkip(1.0f);
		yield return C.Plr.Face(eFace.Down);
		yield return E.WaitSkip(1.0f);
		yield return C.Plr.Face(Hotspot("SeedyAlley"));
		yield return E.WaitSkip(1.0f);
		yield return C.Plr.Face(eFace.Down);
		yield return E.WaitSkip(1.0f);
		yield return C.Player.Say("I guess we're doing this.");
		yield return E.WaitSkip(1.0f);
		yield return C.Plr.WalkTo(Point("Alley"));
		yield return C.Elsa.Face(Hotspot("SeedyAlley"));
		yield return C.Elsa.Say("Dad! I don't wanna go in there.");
		yield return E.WaitSkip();
		yield return C.Elsa.Say("Oh jeez.");
		yield return C.Elsa.WalkTo(C.Player);
		
		yield return E.Break;
	}

	IEnumerator OnExitRegionSeedyAlley( IRegion region, ICharacter character )
	{
		Region("SeedyAlley").Walkable = false;
		yield return E.Break;
	}
}