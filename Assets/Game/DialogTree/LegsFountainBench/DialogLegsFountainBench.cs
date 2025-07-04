using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogLegsFountainBench : DialogTreeScript<DialogLegsFountainBench>
{
	public IEnumerator OnStart()
	{
		yield return E.WaitSkip(0.25f);
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		//set all options as unread since this isn't a real conversaion, but a hacked GUI
		foreach (DialogOption o in D.LegsFountainBench.Options)
        {
			o.Used = false;
        }
		yield return E.Break;
	}

	IEnumerator OptionExit( IDialogOption option )
	{
		Stop();
		yield return E.ConsumeEvent;
		yield return E.Break;
	}

	IEnumerator OptionForward( IDialogOption option )
	{
		yield return E.WaitFor(RoomLegsFountain.Script.BenchForward);
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionBack( IDialogOption option )
	{
		yield return E.WaitFor(RoomLegsFountain.Script.BenchBack);
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionClimb( IDialogOption option )
	{
		yield return E.WaitFor(RoomLegsFountain.Script.ClimbUpBench);
		Stop();
		yield return E.Break;
	}
}