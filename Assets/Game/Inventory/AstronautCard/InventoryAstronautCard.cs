using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class InventoryAstronautCard : InventoryScript<InventoryAstronautCard>
{


	IEnumerator OnInteractInventory( IInventory thisItem )
	{
		//D.InteractAstronautCard.Start();
		if(!Globals.m_lookedAtAstronautCard){
			yield return E.HandleLookAt(I.AstronautCard);
		} else {
			I.AstronautCard.SetActive();
		}
		yield return E.Break;
	}

	IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		eFace _prevFacing = C.Elsa.Facing;
		yield return C.Elsa.Face(eFace.Down);
		yield return C.Elsa.Say("It's my Honorary Junior Astronaut card from the planetarium.");
		yield return C.Elsa.Say("It's laminated, shiny, stiff, and sharp.");
		yield return C.Elsa.Say("That's how you know it's official!");
		I.AstronautCard.Description = "shiny, stiff, & sharp Astronaut card";
		yield return C.Elsa.Face(_prevFacing);
		((IQuestClickable)I.AstronautCard).Cursor = "Use";
		Globals.m_lookedAtAstronautCard = true;
		yield return E.Break;
	}

	IEnumerator OnUseInvInventory( IInventory thisItem, IInventory item )
	{
		if (item == I.StickyShoe){
			yield return RoomBusStop.Script.ScrapeShoe();
		}
		yield return E.Break;
	}
}