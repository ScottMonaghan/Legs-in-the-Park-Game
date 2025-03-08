using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogBusStopInteractBarrel : DialogTreeScript<DialogBusStopInteractBarrel>
{
	public IEnumerator OnStart()
	{
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		yield return E.Break;
	}

	IEnumerator OptionLookAt( IDialogOption option )
	{
		yield return E.HandleLookAt(Prop("Barrel"));
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionOpen( IDialogOption option )
	{
		if(!RoomBusStop.Script.m_dan_distracted){
			yield return C.Dan.Say("Sorry little lady!");
			yield return C.Dan.Say("That's official museum property!");
			yield return C.Dan.Say("That's for authorized personnel only!");
			yield return C.Dan.Say("Now why don't we talk about setting you on your path to financial freedom!");
			Stop();
			D.BusStopDialogDan.Start();
		} else {
		
		//TO DO add open animation
			RoomBusStop.Script.m_barrel_open = true;
			OptionOff("Open");
			yield return E.HandleLookAt(Prop("Barrel"));
			OptionOn("Take");
		}
		yield return E.Break;
	}

	IEnumerator OptionExit( IDialogOption option )
	{
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionTake( IDialogOption option )
	{

		yield return E.Break;
	}
}