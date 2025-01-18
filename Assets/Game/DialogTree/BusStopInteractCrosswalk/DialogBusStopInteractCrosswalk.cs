using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogBusStopInteractCrosswalk : DialogTreeScript<DialogBusStopInteractCrosswalk>
{
	public IEnumerator OnStart()
	{
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		yield return E.Break;
	}

	IEnumerator Option2( IDialogOption option )
	{
		if (RoomBusStop.Script.m_lookedAtSchedule){
			Stop();
			E.DisableCancel();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Player.Say("I'm just going to have to take matters into my own hands.");
			yield return C.Plr.Face(C.Scott);
			yield return C.Player.Say("Dad, let's just go play in The Legs across the street like we used to!");
			yield return C.Scott.Say("Are you sure? I thought you wanted to go to the planetarium?");
			yield return C.Player.Say("I'm sure, let's go!");
			yield return C.Scott.Say("Okay kiddo!");
			yield return C.Plr.WalkTo(Point("LeftSideOfStreet"));
			yield return C.Plr.Face(eFace.UpRight);
			yield return E.WaitSkip();
			yield return C.Plr.Face(eFace.DownLeft);
			yield return E.WaitSkip();
			Region("Crosswalk").Walkable = true;
			C.Plr.WalkToBG(Point("RightSideOfStreet")[0] + 20, Point("RightSideOfStreet")[1]);
			C.Scott.AnimPrefix = "";
			yield return C.Scott.WalkTo(Point("RightSideOfStreet"));
			yield return C.Plr.Face(C.Scott);
			C.Scott.AnimPrefix = "Phone";
			yield return C.Scott.Face(C.Player);
			yield return C.Scott.Say("I just gotta finish this work email. I'll be right in.");
			yield return C.Player.Say("Okay!");
		
			Region("Crosswalk").Walkable = false;
			Hotspot("LeftCrosswalk").Disable();
			Hotspot("RightCrosswalk").Enable();
			RoomBusStop.Script.m_locationState = RoomBusStop.eLocation.Legs;
		} else {
			yield return C.Elsa.Say("I don't want to cross the street. I might miss my bus to the planetarium!");
			Stop();
		}
		yield return E.Break;
	}

	IEnumerator Option1( IDialogOption option )
	{
		yield return E.HandleLookAt(Hotspot("LeftCrosswalk"));
		Stop();
		yield return E.Break;
	}

	IEnumerator Option3( IDialogOption option )
	{
		yield return C.Elsa.Say("I'm all set with this crosswalk for now.");
		Stop();
		yield return E.Break;
	}
}