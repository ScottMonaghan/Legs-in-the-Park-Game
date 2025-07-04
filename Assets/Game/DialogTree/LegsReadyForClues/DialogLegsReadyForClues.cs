using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogLegsReadyForClues : DialogTreeScript<DialogLegsReadyForClues>
{
	public IEnumerator OnStart()
	{
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		yield return E.Break;
	}

	IEnumerator OptionYes( IDialogOption option )
	{
		
		yield return C.Player.Say("Yes, I'm ready.");
		
		yield return C.Robin.Say("Okay, pay close attention...");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return Globals.LegsRobinRecitePoem();
		Globals.m_treasure_hunt_path_index = 0;
		GotoPrevious();
		yield return E.Break;
	}
}
