using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomLegs1 : RoomScript<RoomLegs1>
{

	IEnumerator OnEnterRoomAfterFade()
	{
		yield return E.WaitFor( Globals.LegsOnEnterRoomAfterFade );
		/*
		if (E.Before(eLegsProgress.RobinHiding)){
			//force-set inventory to allow play directly from room
		
			C.Plr.ClearInventory();
			I.AstronautCard.Add();
			Globals.m_lookedAtAstronautCard = true;
			((IQuestClickable)I.AstronautCard).Cursor = "Use";
			I.AbcGum.Add();
			Globals.m_lookedAtAbcGum = true;
			((IQuestClickable)I.AbcGum).Cursor = "Use";
			I.Grabber.Add();
			Globals.m_lookedAtGrabber = true;
			((IQuestClickable)I.Grabber).Cursor = "Use";
		
			//scene intro cut scene
			E.StartCutscene();
			yield return E.FadeIn();
			yield return C.Narrator.ChangeRoom(R.Current);
			C.Narrator.SetPosition(0,0);
			Prop("BlackScreen").Alpha = 1;
			yield return C.Narrator.Say("Before we continue, a quick story...");
			yield return E.WaitSkip(1.0f);
			yield return C.Narrator.Say("In the months before Elsa's birth,");
			yield return C.Narrator.Say("her parents carefully labored to choose a name that was...");
			yield return C.Narrator.Say("beautiful,");
			yield return C.Narrator.Say("beautiful,");
			yield return C.Narrator.Say("uncommon but classic,");
			yield return C.Narrator.Say("and most importantly,");
			yield return C.Narrator.Say("not associated with anything in popular culture.");
			yield return E.WaitSkip();
			yield return C.Narrator.Say("When Elsa was born,");
			yield return C.Narrator.Say("everyone agreed they had chosen very well.");
			yield return E.WaitSkip(1.0f);
			yield return C.Narrator.Say("Then of course,");
			yield return C.Narrator.Say("the movie happened.");
			yield return C.Narrator.Say("the movie happened.");
			yield return E.WaitSkip();
			yield return C.Narrator.Say("I'm sure you know the one.");
			yield return E.WaitSkip(1.5f);
			yield return C.Narrator.Say("And so it's been,");
			yield return C.Narrator.Say("that Elsa's introductions to other kids,");
			yield return C.Narrator.Say("are almost always some variation of:");
			yield return C.Narrator.Say("\"Hi my name's Elsa...\"");
			yield return C.Narrator.Say("and before the new kid can exclaim their incredulity,");
			yield return C.Narrator.Say("\"like Queen Elsa, but I had my name first.\"");
			yield return E.WaitSkip();
			yield return C.Narrator.Say("I tell you this not just because it is an amusing bit of color to flesh out Elsa's life,");
			yield return C.Narrator.Say("but because I want you to remember it for later,");
			yield return E.WaitSkip();
			yield return C.Narrator.Say("Names are important.");
			yield return E.WaitSkip(1.5f);
			yield return C.Narrator.Say("Names have power.");
			yield return C.Narrator.Say("Names have power.");
			yield return C.Narrator.Say("Names have power.");
			yield return E.WaitSkip(1.5f);
		
			yield return C.Display("Chapter 2: In The Legs");
			yield return E.WaitSkip();
			Prop("BlackScreen").FadeBG(1,0,10);
			if (!Audio.IsPlaying("FoxTaleWaltz")){
				Audio.PlayMusic("FoxTaleWaltz");
			}
			E.EndCutscene();
			C.Plr.Position = Point("ExitWest");
			E.DisableCancel();
			yield return C.Plr.WalkTo(Point("EnteranceStopWest"));
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("Quick work emails are never quick.");
			yield return C.Plr.Say("I should probably try to find some fun while I wait for my dad.");
			yield return C.Plr.Face(eFace.Right);
		} else if(C.Plr.LastRoom.ScriptName == "Legs1" || C.Plr.LastRoom.ScriptName == "Legs2") {
			if (E.Is(eExitDirection.Up)){
				C.Plr.Position = Point("ExitSouth");
				Region("ExitSouth").Walkable = true;
				E.DisableCancel();
				yield return C.Plr.WalkTo(Point("EnteranceStopSouth"));
				Region("ExitSouth").Walkable = false;
			}
			else if (E.Is(eExitDirection.Down)){
				Region("ExitNorth").Walkable = true;
				C.Plr.Position = Point("ExitNorth");
				E.DisableCancel();
				yield return C.Plr.WalkTo(Point("EnteranceStopNorth"));
				Region("ExitNorth").Walkable = false;
			}
			else if (E.Is(eExitDirection.Right) || E.Is(eExitDirection.None)){
				C.Plr.Position = Point("ExitWest");
				E.DisableCancel();
				Region("ExitWest").Walkable = true;
				yield return C.Plr.WalkTo(Point("EnteranceStopWest"));
				Region("ExitWest").Walkable = false;
			}
			else if (E.Is(eExitDirection.Left)){
				C.Plr.Position = Point("ExitEast");
				Region("ExitEast").Walkable = true;
				E.DisableCancel();
				yield return C.Plr.WalkTo(Point("EnteranceStopEast"));
				Region("ExitEast").Walkable = false;
			}
			else {
				yield return C.Display($"UNEXPECTED EXIT DIRECTION: {Globals.m_lastExitDirection}");
			}
		
		} else {
			yield return C.Display($"UNEXPECTED PREVIOUS ROOM: {C.Plr.LastRoom.ScriptName}.");
		}
		
		//Robin playing hide & seek
		if (E.Reached(eLegsProgress.RobinHiding) && E.Before(eLegsProgress.ClickedRobin)){
			//setup Robin
			if(E.FirstOption(2)){
				Globals.m_legs_robin_hide_point = Point("RobinHide1");
				Globals.m_legs_robin_peek_point = Point("RobinPeek1");
			} else if (E.NextOption){
				Globals.m_legs_robin_hide_point = Point("RobinHide2");
				Globals.m_legs_robin_peek_point = Point("RobinPeek2");
			}
			Globals.m_legs_robin_meet_point = Point("RobinMeet1");
			Globals.m_legs_elsa_meet_robin_point = Point("ElsaMeetRobin1");
			yield return E.WaitFor( Globals.LegsRobinPeek );
		}
		
		//Got lost on the treasure hunt
		if (
			E.Reached(eLegsProgress.GotTreasureHunt)
			&& E.Before(eLegsProgress.CompletedTreasureHunt)
			&& C.Robin.Room == R.Current
			&& Globals.m_treasure_hunt_path_index == -1
		){
			yield return C.Robin.Say("Looks like you're a little lost. Hee hee.");
			yield return C.Robin.Say("Can I do anything to help?");
			Globals.m_treasure_hunt_path_index=0;
		}
		*/
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitWest( IHotspot hotspot )
	{
		Region("ExitWest").Walkable = true;
		yield return C.Plr.WalkTo(Point("ExitWest"));
		Region("ExitWest").Walkable = false;
		E.Set(eExitDirection.Left);
		yield return Globals.LegsChangeRoom();
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitNorth( IHotspot hotspot )
	{
		Region("ExitNorth").Walkable = true;
		yield return C.Plr.WalkTo(Point("ExitNorth"));
		Region("ExitNorth").Walkable = false;
		E.Set(eExitDirection.Up);
		yield return Globals.LegsChangeRoom();
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitEast( IHotspot hotspot )
	{
		Region("ExitEast").Walkable = true;
		yield return C.Plr.WalkTo(Point("ExitEast"));
		Region("ExitEast").Walkable = false;
		E.Set(eExitDirection.Right);
		yield return Globals.LegsChangeRoom();
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitSouth( IHotspot hotspot )
	{
		Region("ExitSouth").Walkable = true;
		yield return C.Plr.WalkTo(Point("ExitSouth"));
		Region("ExitSouth").Walkable = false;
		E.Set(eExitDirection.Down);
		yield return Globals.LegsChangeRoom();
		yield return E.Break;
	}

	IEnumerator UpdateBlocking()
	{
		//can't use global routine here because it ends up endlessly blocking
		
		if(E.Reached(eLegsProgress.RobinHiding) && E.Before(eLegsProgress.ClickedRobin)) {
			if (E.GetTimerExpired("robin_peeking")){
				yield return C.Robin.WalkTo(Globals.m_legs_robin_hide_point,true);
				C.Robin.Clickable = false;
				C.Robin.Visible = false;
				yield return E.WaitFor( Globals.OnRobinHide );
			}
		}
		yield return E.Break;
	}

	void OnEnterRoom()
	{
		Globals.LegsOnEnterRoom();
		/*if (E.Reached(eLegsProgress.GotTreasureHunt) && E.Before(eLegsProgress.CompletedTreasureHunt)){
			//Only show Robin if on the wrong path
			if(Globals.m_treasure_hunt_path_index < 0){
				C.Robin.Visible = true;
				C.Robin.Clickable = true;
			} else {
				C.Robin.Visible = false;
				C.Robin.Clickable = false;
			}
		}*/
	}

	IEnumerator OnWalkTo()
	{
		yield return E.Break;
	}

	void Update()
	{
		/*
		if (E.Is(eLegsProgress.LookingForDad)){
			C.Robin.FaceBG(C.Player);
		}
		*/
		Globals.LegsOnUpdate();
		
	}
}
