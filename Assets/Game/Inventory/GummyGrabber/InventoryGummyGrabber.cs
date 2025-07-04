using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class InventoryGummyGrabber : InventoryScript<InventoryGummyGrabber>
{


	IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		yield return C.Plr.Say("It's an Official Sue(tm) the T-Rex Grabber with no teeth.");
		yield return C.Plr.Say("It's got sticky ABC gum where its teeth should be.");
		yield return C.Plr.Face(_prevFacing);
		((IQuestClickable)I.GummyGrabber).Cursor = "Use";
		Globals.m_lookedAtGummyGrabber = true;
		yield return E.Break;
	}

	IEnumerator OnInteractInventory( IInventory thisItem )
	{

		yield return E.Break;
	}
}