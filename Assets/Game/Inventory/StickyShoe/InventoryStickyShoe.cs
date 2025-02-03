using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class InventoryStickyShoe : InventoryScript<InventoryStickyShoe>
{


	IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		yield return C.Player.Say("Looks like I stepped in some gum.");
		yield return C.Player.Say("It's really stuck on there.");
		yield return C.Plr.Face(_prevFacing);
		yield return E.Break;
	}

	IEnumerator OnInteractInventory( IInventory thisItem )
	{
		D.InteractStickyShoe.Start();
		yield return E.Break;
	}
}