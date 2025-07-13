using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogAskDadAboutBus : DialogTreeScript<DialogAskDadAboutBus>
{
	public IEnumerator OnStart()
	{
		yield return C.Plr.WalkTo(C.Scott);
		yield return C.Plr.Face(C.Scott);
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		if (Globals.m_emotion_level >= 5){
			yield return E.WaitFor(RoomBusStop.Script.FullEmotionHint);
		} else if (Option(1).Used && Option(2).Used && Option(3).Used & Option(4).Used){
			eFace _oldFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			yield return C.Player.Say("What's a girl gotta do to get a ride around here?");
			yield return C.Plr.Face(_oldFacing);
		}
	}

	IEnumerator Option1( IDialogOption option )
	{
		yield return C.Player.Say("Whatcha doin?");
		yield return C.Scott.Say("Writing a quick work email.");
		yield return C.Player.Say("But it's SATURDAY!");
		yield return C.Scott.Say("I know. Almost done.");
		if (option.FirstUse)
		{
			yield return E.WaitFor(()=>RoomBusStop.Script.SetEmotionLevel(Globals.m_emotion_level+1));
		}
		option.Off();
		Stop();
		yield return E.Break;
	}

	IEnumerator Option5( IDialogOption option )
	{
		yield return C.Player.Say("Never mind.");
		yield return C.Scott.Say("Okay.");
		Stop();
		yield return E.Break;
	}

	IEnumerator Option2( IDialogOption option )
	{
		yield return C.Player.Say("Will the bus come soon?");
		yield return C.Scott.Say("It will get here when it gets here.");
		if (option.FirstUse)
		{
			yield return E.WaitFor(() => RoomBusStop.Script.SetEmotionLevel(Globals.m_emotion_level + 1));
		}
		option.Off();
		Stop();
		yield return E.Break;
	}

	IEnumerator Option3( IDialogOption option )
	{
		yield return C.Player.Say("How much longer until the bus gets here?");
		yield return C.Scott.Say($"My phone says {RoomBusStop.Script.m_minutesLeft} more minutes.");
		yield return C.Player.Say($"Last time it said {RoomBusStop.Script.m_minutesLeft - 1} minutes!");
		if (option.FirstUse)
		{
			yield return E.WaitFor(() => RoomBusStop.Script.SetEmotionLevel(Globals.m_emotion_level + 1));
		}
		
		RoomBusStop.Script.m_minutesLeft++;
		Stop();
		yield return E.Break;
	}

	IEnumerator Option4( IDialogOption option )
	{
		yield return C.Player.Say("Can we call an Uber?");
		yield return C.Scott.Say("Sure.");
		yield return C.Player.Say("Really!?");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Scott.Say("No.");
		yield return C.Player.Say("awww.");
		if (option.FirstUse)
		{
			yield return E.WaitFor(() => RoomBusStop.Script.SetEmotionLevel(Globals.m_emotion_level + 1));
		}
		option.Off();
		Stop();
		yield return E.Break;
	}

	IEnumerator Option10( IDialogOption option )
	{

		yield return E.Break;
	}
}