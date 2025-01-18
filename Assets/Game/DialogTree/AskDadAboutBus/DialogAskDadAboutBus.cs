using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogAskDadAboutBus : DialogTreeScript<DialogAskDadAboutBus>
{
	public IEnumerator OnStart()
	{
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		yield return C.Plr.Face(eFace.Down);
		yield return C.Player.Say("What's a girl gotta do to get a ride around here?");
		yield return E.Break;
	}

	IEnumerator Option1( IDialogOption option )
	{
		yield return C.Player.Say("Whatcha doin?");
		yield return C.Scott.Say("Writing a quick work email.");
		yield return C.Player.Say("But it's SATURDAY!");
		yield return C.Scott.Say("I know. Almost done.");
		yield return E.Break;
	}

	IEnumerator Option5( IDialogOption option )
	{
		yield return C.Player.Say("Okay.");
		Stop();
		yield return E.Break;
	}

	IEnumerator Option2( IDialogOption option )
	{
		yield return C.Player.Say("Will the bus come soon?");
		yield return C.Scott.Say("It will get here when it gets here.");
		yield return E.Break;
	}

	IEnumerator Option3( IDialogOption option )
	{
		yield return C.Player.Say("How many more minutes until the bus gets here?");
		yield return C.Scott.Say("My phone says 5 more minutes.");
		yield return C.Player.Say("That's exactly what you said last time!");
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
		yield return E.Break;
	}

	IEnumerator Option10( IDialogOption option )
	{

		yield return E.Break;
	}
}