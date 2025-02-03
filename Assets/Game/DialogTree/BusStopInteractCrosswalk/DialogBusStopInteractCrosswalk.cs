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
		Stop();
		if (RoomBusStop.Script.m_allowCrosswalk){
			E.DisableCancel();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Player.Say("I'm just going to have to take matters into my own hands.");
			G.BusStopEmotionBar.GetControl("Impatience").Visible=false;
			IImage meterBg = (IImage)G.BusStopEmotionBar.GetControl("Bg");
			yield return meterBg.Fade(1,0,1);
			G.BusStopEmotionBar.Hide();
			yield return E.WaitSkip();
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
			C.Scott.AnimPrefix = "";
			C.Scott.WalkToBG(Point("RightSideOfStreet"));
			yield return C.Plr.WalkTo(Point("Gum"));
			yield return C.Plr.PlayAnimation("ElsaGum");
			C.Plr.SetPosition(C.Plr.Position[0] + 44, C.Plr.Position[1]);
			C.Plr.StopAnimation();
			I.StickyShoe.Add();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Player.Say("Well this is a sticky situation!");
			yield return C.Plr.WalkTo(Point("RightSideOfStreet")[0] + 20, Point("RightSideOfStreet")[1]);
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
			yield return C.Player.Say("I don't want to cross the street. I might miss my bus to the planetarium!");
		}
		yield return E.Break;
	}

	IEnumerator Option1( IDialogOption option )
	{
		Stop();
		yield return E.HandleLookAt(Hotspot("LeftCrosswalk"));
		yield return E.Break;
	}

	IEnumerator Option3( IDialogOption option )
	{
		Stop();
		yield return C.Elsa.Say("I'm all set with this crosswalk for now.");
		yield return E.Break;
	}
}