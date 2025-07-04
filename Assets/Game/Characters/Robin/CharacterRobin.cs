using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class CharacterRobin : CharacterScript<CharacterRobin>
{


	IEnumerator OnInteract()
	{
		if(E.Before(eLegsProgress.ClickedRobin)){
			E.Set(eLegsProgress.ClickedRobin);
			yield return C.Player.Face(C.Robin);
			yield return C.Plr.Say("Hey I see you!");
			yield return C.Robin.WalkTo(Globals.m_legs_robin_meet_point);
			C.Plr.FaceBG(C.Robin);
			yield return C.Robin.Face(C.Plr);
			yield return C.Plr.WalkTo(Globals.m_legs_elsa_meet_robin_point);
			C.Robin.FaceBG(C.Plr);
			yield return C.Plr.Face(C.Robin);
			yield return C.Robin.Say("Hi");
			yield return C.Robin.Say("I'm Robin");
			yield return E.WaitSkip();
			yield return C.Plr.Say("Hi");
			yield return C.Plr.Say("I'm Elsa");
			yield return E.WaitSkip();
			yield return C.Plr.Say("like Queen Elsa \nbut I had my name first");
			yield return E.WaitSkip();
			yield return C.Robin.Say("YOU have the name of a queen?");
			yield return C.Robin.Say("And it was YOUR true name first!?");
			yield return E.WaitSkip();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("ummm...");
			yield return E.WaitSkip(1.5f);
			yield return C.Plr.Say("that's weird right?");
			yield return E.WaitSkip();
			yield return C.Plr.Face(C.Robin);
		} else {
			yield return C.Plr.WalkTo(Globals.m_legs_elsa_meet_robin_point);
			C.Robin.FaceBG(C.Plr);
			yield return C.Plr.Face(C.Robin);
		}
		
		D.LegsMeetRobin.Start();
		yield return E.Break;
	}
}