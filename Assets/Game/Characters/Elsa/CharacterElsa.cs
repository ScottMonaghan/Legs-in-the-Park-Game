using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class CharacterElsa : CharacterScript<CharacterElsa>
{


	IEnumerator OnInteract()
	{
		yield return E.HandleLookAt(C.Elsa);
		yield return E.Break;
	}

	IEnumerator OnUseInv( IInventory item )
	{
		if (C.Elsa.IsPlayer){
			eFace _prevFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			if (item == I.StickyShoe){
			  yield return C.Plr.Say("My shoe is already on");
			  yield return C.Plr.Say("The problem is that it's sticky with gum.");
			} else if (item == I.AbcGum) {
				yield return C.Plr.Say("If you think I'm going to chew that,");
				yield return E.WaitSkip();
				yield return C.Plr.Say("you are out of your mind.");
			} else if (item == I.Grabber) {
				yield return C.Plr.Say("If it had teeth it might make a great back-scratcher,");
				yield return E.WaitSkip();
				yield return C.Plr.Say("But it doesn't, so no.");
			} else if (item  == I.GummyGrabber){
				yield return C.Plr.Say("Now with the gum it's not even a good back scratcher.");
				yield return C.Plr.Say("It should be better at picking things up now though.");
			} else if (item == I.AstronautCard){
				if( !RoomBusStop.Script.m_got_abc_gum && C.Player.HasInventory(I.StickyShoe)){
				yield return E.WaitFor(RoomBusStop.Script.ScrapeShoe);
				} else {
					yield return C.Plr.Say("No thanks. The laminated edges are sharp. I might get scraped.");
				}
			} else {
			  yield return C.Plr.Say("I don't need to use that on myself right now.");
			}
			yield return C.Plr.Face(_prevFacing);
		}
		yield return E.Break;
	}

	IEnumerator OnLookAt()
	{
		if (C.Elsa.IsPlayer){
			eFace _prevFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("That's me!");
			yield return C.Plr.Face(_prevFacing);
		}
		yield return E.Break;
	}
}