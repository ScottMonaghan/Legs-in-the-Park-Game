using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class InventoryAbcGum : InventoryScript<InventoryAbcGum>
{


	IEnumerator OnUseInvInventory( IInventory thisItem, IInventory item )
	{
		if (item == I.AstronautCard){
			eFace _prevFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("I already got gum on there when I used it to scrape my shoe");
			yield return C.Plr.Say("I don't want to ruin my official honorary astronaut card");
			yield return C.Plr.Say("Otherwise, how would I get to honorary space?");
			yield return C.Plr.Face(_prevFacing);
		}
		if (item == I.Grabber){
			if (Globals.m_ready_for_gummy_grabber){
				yield return C.Plr.Face(eFace.Down);
				yield return C.Plr.Say("I hope this doesn't ruin the collector's value!");
				C.Plr.AnimPrefix="GumGrabber";
				yield return C.Plr.PlayAnimation("ElsaGumGrabber");
				I.AbcGum.Remove();
				I.Grabber.Remove();
				I.GummyGrabber.Add();
				((IQuestClickable)I.GummyGrabber).Cursor = "Use";
				yield return C.Plr.Say("Its definitely sticky now.");
				C.Plr.AnimPrefix="";
				yield return C.Plr.PlayAnimation("GumGrabberAway");
			} else {
				eFace _prevFacing = C.Plr.Facing;
				yield return C.Plr.Face(eFace.Down);
				yield return C.Plr.Say("That would make my grabber all sticky");
				yield return C.Plr.Say("and probably lower its value SIGNIFICANTLY!");
				yield return C.Plr.Say("I don't want to do that right now.");
				yield return C.Plr.Face(_prevFacing);
			}
			C.Plr.ActiveInventoryName = "";
		}
		
		yield return E.Break;
	}

	IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		yield return C.Plr.Say("It's the ABC gum I scraped off my shoe. It's really sticky.");
		yield return C.Plr.Face(_prevFacing);
		((IQuestClickable)I.AbcGum).Cursor = "Use";
		Globals.m_lookedAtAbcGum = true;
		yield return E.Break;
	}

	IEnumerator OnInteractInventory( IInventory thisItem )
	{
		//D.InventoryInteractABCGum.Start();
		if (!Globals.m_lookedAtAbcGum){
			yield return E.HandleLookAt(I.AbcGum);
		} else {
			I.AbcGum.SetActive();
		}
		yield return E.Break;
	}
}