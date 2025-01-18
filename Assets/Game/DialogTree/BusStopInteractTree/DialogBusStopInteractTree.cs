using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogBusStopInteractTree : DialogTreeScript<DialogBusStopInteractTree>
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
		yield return C.Plr.Face(eFace.Down);
		yield return C.Player.Say("Not everything needs to have a use.");
		yield return C.Player.Say("Some things are just nice to look at.");
		yield return C.Plr.Face(Prop("Tree"));
		yield return E.WaitSkip(2.0f);
		yield return C.Player.Say("Nice.");
		Stop();
		yield return E.Break;
	}

	IEnumerator Option1( IDialogOption option )
	{
		yield return E.HandleLookAt(Prop("Tree"));
		Stop();
		yield return E.Break;
	}

	IEnumerator Option3( IDialogOption option )
	{
		yield return C.Player.Say("I'm all set with this tree.");
		Stop();
		yield return E.Break;
	}
}