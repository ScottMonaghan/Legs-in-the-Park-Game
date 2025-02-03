using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogInteractAstronautCard : DialogTreeScript<DialogInteractAstronautCard>
{
	public IEnumerator OnStart()
	{
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		yield return E.Break;
	}

	IEnumerator OptionLookAtCard( IDialogOption option )
	{
		yield return E.HandleLookAt(I.AstronautCard);
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionUseCard( IDialogOption option )
	{
		I.AstronautCard.SetActive();
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionExit( IDialogOption option )
	{
		Stop();
		yield return E.Break;
	}
}