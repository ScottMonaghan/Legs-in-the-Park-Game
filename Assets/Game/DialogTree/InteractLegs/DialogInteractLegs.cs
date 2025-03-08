using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogInteractLegs : DialogTreeScript<DialogInteractLegs>
{
	public IEnumerator OnStart()
	{
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		yield return E.Break;
	}

	IEnumerator Option1( IDialogOption option )
	{
		yield return E.HandleLookAt(Hotspot("Legs"));
		yield return E.Break;
	}

	IEnumerator Option2( IDialogOption option )
	{
		Stop();
		yield return C.Player.Say("This is fine.");
		yield return E.WaitSkip();
		yield return C.Plr.WalkTo(Hotspot("Legs"));
		yield return C.Plr.ChangeRoom(R.Legs1);
		//E.FadeOut();
		//Display: Thanks for trying out this alpha demo of Legs in the Park!
		//Application.Quit();
		yield return E.Break;
	}

	IEnumerator Option3( IDialogOption option )
	{
		yield return C.Plr.Face(eFace.Down);
		yield return C.Player.Say("Fuhgeddaboudit!");
		Stop();
		yield return E.Break;
	}
}