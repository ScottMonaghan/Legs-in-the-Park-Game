using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class InventoryGrabber : InventoryScript<InventoryGrabber>
{


	IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		yield return C.Plr.Say("It's an Official Sue(tm) the T-Rex Grabber with no teeth.");
		yield return C.Plr.Say("It's got a tag on it that says");
		yield return C.Plr.Say("DEFECTIVE");
		yield return C.Plr.Say("DO NOT SELL");
		yield return C.Plr.Say("FOR DISPOSAL ONLY");
		yield return C.Plr.Face(_prevFacing);
		((IQuestClickable)I.Grabber).Cursor = "Use";
		Globals.m_lookedAtGrabber = true;
		yield return E.Break;
	}

	IEnumerator OnInteractInventory( IInventory thisItem )
	{
		if (!Globals.m_lookedAtGrabber){
			yield return E.HandleLookAt(I.Grabber);
		} else {
			I.Grabber.SetActive();
		}
		yield return E.Break;
	}
}