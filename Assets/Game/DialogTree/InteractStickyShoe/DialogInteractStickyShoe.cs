using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogInteractStickyShoe : DialogTreeScript<DialogInteractStickyShoe>
{
	public IEnumerator OnStart()
	{
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		yield return E.Break;
	}

	IEnumerator OptionUse( IDialogOption option )
	{
		I.StickyShoe.SetActive();
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionLookAt( IDialogOption option )
	{
		yield return E.HandleLookAt(I.StickyShoe);
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionExit( IDialogOption option )
	{
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionScrape( IDialogOption option )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		yield return C.Player.Say("Gross! I'm not touching that with my hands.");
		yield return C.Plr.Face(_prevFacing);
		yield return E.Break;
	}
}