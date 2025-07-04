using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class InventorySilverLeaf : InventoryScript<InventorySilverLeaf>
{


	IEnumerator OnInteractInventory( IInventory thisItem )
	{
		if (((IQuestClickable)thisItem).Cursor == "Look"){
			yield return E.HandleLookAt(thisItem);
		}
		yield return E.Break;
	}

	IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		yield return C.Plr.Say("It's a beautiful silver leaf about the size of my palm.");
		yield return C.Plr.Say("It's surprisingly warm to the touch.");
		((IQuestClickable)I.SilverLeaf).Cursor = "Use";
		yield return E.Break;
	}
}