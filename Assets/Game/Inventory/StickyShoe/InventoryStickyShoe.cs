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
		((IQuestClickable)I.StickyShoe).Cursor = "Use";
		Globals.m_lookedAtStickyShoe = true;
		yield return C.Plr.Face(_prevFacing);
		yield return E.Break;
	}

	IEnumerator OnInteractInventory( IInventory thisItem )
	{
		//D.InteractStickyShoe.Start();
		if (!Globals.m_lookedAtStickyShoe){
			yield return E.HandleLookAt(I.StickyShoe);
		} else {
			eFace _prevFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("Gross. I'm not going to scrape that off with my hands!");
			yield return C.Plr.Face(_prevFacing);
		}
		yield return E.Break;
	}

	IEnumerator OnUseInvInventory( IInventory thisItem, IInventory item )
	{
		if (item==I.Grabber){
		 eFace prevFacing = C.Plr.Facing;
		 yield return C.Plr.Face(eFace.Down);
		 yield return C.Plr.Say("I don't want to get my grabber all nasty.");
		 yield return C.Plr.Say("It's a collector's item!");
		 yield return C.Plr.Face(prevFacing);
		}
		yield return E.Break;
	}
}