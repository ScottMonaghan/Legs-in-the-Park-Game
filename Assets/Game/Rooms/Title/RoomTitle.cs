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
		G.Toolbar.Hide();
		
		// Later we could start some music here
		Audio.PlayMusic("KrampusWorkshop", 1);
		
		// Set version
		Prop("Version").Instance.GetComponentInChildren<QuestText>().text = "v" + Application.version;
	}

	public IEnumerator OnEnterRoomAfterFade()
	{
		
		// Start cutscene, so this can be skipped by pressing ESC
		E.StartCutscene();
		
		/*
		// Fade in the title prop
		Prop("Title").Visible = true;
		yield return Prop("Title").Fade(0,1,1.0f);
		
		// Wait a moment
		yield return E.Wait(0.5f);
		*/
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
		yield return E.WaitSkip();
		yield return Prop("TitleBG").Fade(0,1,2.0f);
		Prop("TitleThe").FadeBG(0,1,1.0f);
		yield return Prop("TitleLegs").Fade(0,1,1.0f);
		yield return E.WaitSkip();
		Prop("TitlePark").FadeBG(0,1,1.0f);
		yield return Prop("TitleInThe").Fade(0,1,1.0f);
		yield return Prop("TitleBookOne").Fade(0,1,0.5f);
		yield return E.WaitSkip();
		Prop("NewGame").FadeBG(0,1,0.5f);
		Prop("Resume").FadeBG(0,1,0.5f);
		Prop("Credits").FadeBG(0,1,0.5f);
		Prop("Options").FadeBG(0,1,0.5f);
		yield return Prop("Quit").Fade(0,1,0.5f);
		Prop("NewGame").Clickable = true;
		Prop("Resume").Clickable = true;
		Prop("Credits").Clickable = true;
		Prop("Options").Clickable = true;
		Prop("Quit").Clickable = true;
		
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
		G.Toolbar.Show();
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

	IEnumerator OnInteractPropNewGame( IProp prop )
	{
		// Turn on the inventory and info bar now that we're starting a game
		G.InventoryBar.Show();
		
		// Move the player to the room
		E.ChangeRoomBG(R.Intro);
		yield return E.ConsumeEvent;
		yield return E.Break;
	}

	IEnumerator OnInteractPropResume( IProp prop )
	{
		GuiSave.Script.ShowRestore();
		yield return E.ConsumeEvent;
		yield return E.Break;
	}

	IEnumerator OnInteractPropOptions( IProp prop )
	{
		G.Options.Show();
		yield return E.ConsumeEvent;
		yield return E.Break;
	}

	IEnumerator OnInteractPropQuit( IProp prop )
	{
		GuiPrompt.Script.Show("Really Exit the Game?", "Yes", "Cancel", ()=>
		{
			Application.Quit();
		});
		yield return E.ConsumeEvent;
		yield return E.Break;
	}

	IEnumerator OnInteractPropCredits( IProp prop )
	{
		yield return E.WaitFor(Globals.RollCredits);
		yield return E.ConsumeEvent;
		yield return E.Break;
	}
}
