using UnityEngine;
using System.Collections;
using PowerScript;
using PowerTools.Quest;
using static GlobalScript;

public class RoomTitle : RoomScript<RoomTitle>
{
	public void OnEnterRoom()
	{
		
		// Hide the inventory in the title scene
		G.InventoryBar.Hide();
		
		// Later we could start some music here
		Audio.PlayMusic("KrampusWorkshop", 1);
		
		// Set version
	}

	public IEnumerator OnEnterRoomAfterFade()
	{
		
		// Start cutscene, so this can be skipped by pressing ESC
		E.StartCutscene();
		
		// Fade in the title prop
		Prop("Title").Visible = true;
		yield return Prop("Title").Fade(0,1,1.0f);
		
		// Wait a moment
		yield return E.Wait(0.5f);
		
		/*
		// Check if we have any save games. If so, turn on the "continue" prop.
		if (  E.GetSaveSlotData().Count > 0 )
		{
			// Enable the "Continue" prop and start it fading in
			Prop("Continue").Enable();
			Prop("Continue").FadeBG(0,1,1.0f);
		}
		*/
		
		// Turn on the "new game" prop and fade it in
		Prop("Prologue").Enable();
		Prop("Prologue").FadeBG(0,1,1.0f);
		Prop("Chapter1").Enable();
		Prop("Chapter1").FadeBG(0,1,1.0f);
		Prop("Chapter2").Enable();
		Prop("Chapter2").FadeBG(0,1,1.0f);
		Prop("Chapter3").Enable();
		Prop("Chapter3").FadeBG(0,1,1.0f);
		Prop("Chapter4").Enable();
		Prop("Chapter4").FadeBG(0,1,1.0f);
		
		// This is the point the game will skip to if ESC is pressed
		E.EndCutscene();
		
	}

	public IEnumerator OnInteractPropNew( Prop prop )
	{		
		// Turn on the inventory and info bar now that we're starting a game
		G.InventoryBar.Show();
		
		// Move the player to the room
		E.ChangeRoomBG(R.Intro);
		yield return E.ConsumeEvent;
	}

	public IEnumerator OnInteractPropContinue( Prop prop )
	{
		// Restore most recent save game
		E.RestoreLastSave();
		yield return E.ConsumeEvent;
	}


	IEnumerator OnInteractPropPrologue( IProp prop )
	{
		E.Restart(R.Intro);
		yield return E.Break;
	}

	IEnumerator OnInteractPropChapter1( IProp prop )
	{
		E.Restart(R.BusStop);
		yield return E.Break;
	}

	IEnumerator OnInteractPropChapter2( IProp prop )
	{
		E.Restart(R.Legs1);
		yield return E.Break;
	}

	IEnumerator OnExitRoom( IRoom oldRoom, IRoom newRoom )
	{
		// Hide the inventory in the title scene
		G.InventoryBar.Show();
		Audio.StopMusic(5);
		yield return E.Break;
	}

	IEnumerator OnInteractPropChapter4( IProp prop )
	{
		
		
		E.Restart(R.Legs1,"PlayFromGotHope");
		yield return E.Break;
	}

	IEnumerator OnInteractPropChapter3( IProp prop )
	{
		E.Restart(R.LegsFountain);
		yield return E.Break;
	}
}
