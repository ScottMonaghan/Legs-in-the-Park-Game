using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class InventoryAstronautCard : InventoryScript<InventoryAstronautCard>
{


	IEnumerator OnInteractInventory( IInventory thisItem )
	{
		D.InteractAstronautCard.Start();
		yield return E.Break;
	}

	IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		eFace _prevFacing = C.Elsa.Facing;
		yield return C.Elsa.Face(eFace.Down);
		yield return C.Elsa.Say("It's my Honorary Junior Astronaut card from the planetarium.");
		yield return C.Elsa.Say("It's laminated stiff and sharp.");
		yield return C.Elsa.Say("That's how you know it's official!");
		yield return C.Elsa.Face(_prevFacing);
		yield return E.Break;
	}

	IEnumerator OnUseInvInventory( IInventory thisItem, IInventory item )
	{
		eFace _prevFacing = C.Plr.Facing;
		
		if (item == I.StickyShoe){
			yield return C.Plr.Face(eFace.Down);
			yield return C.Player.Say("This should work to scrape this gum off!");
			yield return C.Elsa.PlayAnimation("ScrapeOffGum");
			C.Elsa.StopAnimation();
			yield return C.Player.Say("Nice, ABC gum! Saving that for later!");
			I.StickyShoe.Remove();
			I.AbcGum.Add();
			yield return C.Plr.Face(_prevFacing);
		} else if (item == I.AstronautCard) {
			yield return C.Plr.Face(eFace.Down);
			yield return C.Player.Say("Maybe I can use a glitch to make two of these!");
			yield return E.WaitSkip();
			yield return C.Player.Say("Nope");
			yield return C.Plr.Face(_prevFacing);
		}
		yield return E.Break;
	}
}