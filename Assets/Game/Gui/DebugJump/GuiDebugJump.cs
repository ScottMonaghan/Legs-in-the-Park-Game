using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiDebugJump : GuiScript<GuiDebugJump>
{


	IEnumerator OnClickbtnCancel( IGuiControl control )
	{
		G.DebugJump.Hide();
		yield return E.Break;
	}

	IEnumerator OnClickNewButtonText( IGuiControl control )
	{

		yield return E.Break;
	}

	IEnumerator OnClickbtnPrologue( IGuiControl control )
	{
		G.DebugJump.Hide();
		E.Restart(R.Intro);
		yield return E.Break;
		
	}

	IEnumerator OnClickbtnAtTheBusStop( IGuiControl control )
	{
		G.DebugJump.Hide();
		E.Restart(R.BusStop);
		
		yield return E.Break;
	}

	IEnumerator OnClickbtnIntoTheLegs( IGuiControl control )
	{
		G.DebugJump.Hide();
		E.Restart(R.Legs1);
		
		yield return E.Break;
	}

	IEnumerator OnClickbtnAtTheStatue( IGuiControl control )
	{
		G.DebugJump.Hide();
		E.Restart(R.LegsFountain);
		
		yield return E.Break;
	}

	IEnumerator OnClickbtnReturnToTheLegs( IGuiControl control )
	{
		G.DebugJump.Hide();
		E.Restart(R.Legs1,"PlayFromGotHope");
		yield return E.Break;
	}
}