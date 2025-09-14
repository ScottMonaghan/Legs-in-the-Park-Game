using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomLegs2 : RoomScript<RoomLegs2>
{

	IEnumerator OnEnterRoomAfterFade()
	{
		yield return E.WaitFor( Globals.LegsOnEnterRoomAfterFade );
		/*
		if (!Audio.IsPlaying("FoxTaleWaltz")){
			Audio.PlayMusic("FoxTaleWaltz");
		}
		if(C.Plr.LastRoom.ScriptName == "Legs1" || C.Plr.LastRoom.ScriptName == "Legs2") {
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
			} else {
				yield return C.Display($"UNEXPECTED EXIT DIRECTION: {Globals.m_lastExitDirection}.");
				C.Plr.Position = Point("ExitWest");
				E.DisableCancel();
				Region("ExitWest").Walkable = true;
				yield return C.Plr.WalkTo(Point("EnteranceStopWest"));
				Region("ExitWest").Walkable = false;
			}
		
		} else {
			yield return C.Display($"UNEXPECTED PREVIOUS ROOM: {C.Plr.LastRoom.ScriptName}.");
			C.Plr.Position = Point("ExitWest");
			E.DisableCancel();
			Region("ExitWest").Walkable = true;
			yield return C.Plr.WalkTo(Point("EnteranceStopWest"));
			Region("ExitWest").Walkable = false;
		}
		if (E.Reached(eLegsProgress.RobinHiding) && E.Before(eLegsProgress.ClickedRobin)){
			//setup Robin
			if(E.FirstOption(4)){
				Globals.m_legs_robin_hide_point = Point("RobinHide1");
				Globals.m_legs_robin_peek_point = Point("RobinPeek1");
			} else if (E.NextOption){
				Globals.m_legs_robin_hide_point = Point("RobinHide2");
				Globals.m_legs_robin_peek_point = Point("RobinPeek2");
			} else if (E.NextOption){
				Globals.m_legs_robin_hide_point = Point("RobinHide3");
				Globals.m_legs_robin_peek_point = Point("RobinPeek3");
			} else if (E.NextOption){
				Globals.m_legs_robin_hide_point = Point("RobinHide4");
				Globals.m_legs_robin_peek_point = Point("RobinPeek4");
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
			Globals.m_treasure_hunt_path_index =0;
		}
		*/
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitWest(IHotspot hotspot)
	{
		if (Globals.CheckDadHuntDirection(eExitDirection.Left))
		{
			Region("ExitWest").Walkable = true;
			yield return C.Plr.WalkTo(Point("ExitWest"));
			Region("ExitWest").Walkable = false;
			E.Set(eExitDirection.Left);
			yield return E.WaitFor(() => Globals.LegsChangeRoom());
		}
		else
		{
			yield return E.WaitFor(Globals.LegsKeepDirection);
		}
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitNorth(IHotspot hotspot)
	{
		if (Globals.CheckDadHuntDirection(eExitDirection.Up))
		{
			Region("ExitNorth").Walkable = true;
			yield return C.Plr.WalkTo(Point("ExitNorth"));
			Region("ExitNorth").Walkable = false;
			E.Set(eExitDirection.Up);
			yield return E.WaitFor(() => Globals.LegsChangeRoom());
		}
		else
		{
			yield return E.WaitFor(Globals.LegsKeepDirection);
		}
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitEast(IHotspot hotspot)
	{
		if (Globals.CheckDadHuntDirection(eExitDirection.Right))
		{
			Region("ExitEast").Walkable = true;
			yield return C.Plr.WalkTo(Point("ExitEast"));
			Region("ExitEast").Walkable = false;
			E.Set(eExitDirection.Right);
			yield return E.WaitFor(() => Globals.LegsChangeRoom());
		}
		else
		{
			yield return E.WaitFor(Globals.LegsKeepDirection);
		}
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitSouth(IHotspot hotspot)
	{
		if (Globals.CheckDadHuntDirection(eExitDirection.Down))
		{
			Region("ExitSouth").Walkable = true;
			yield return C.Plr.WalkTo(Point("ExitSouth"));
			Region("ExitSouth").Walkable = false;
			E.Set(eExitDirection.Down);
			yield return E.WaitFor(() => Globals.LegsChangeRoom());
		}
		else
		{
			yield return E.WaitFor(Globals.LegsKeepDirection);
		}
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

	IEnumerator OnExitRoom( IRoom oldRoom, IRoom newRoom )
	{
		/*
		if (E.Reached(eLegsProgress.GotTreasureHunt) && E.Before(eLegsProgress.CompletedTreasureHunt)){
			//Only show Robin if on the wrong path
			if(Globals.m_treasure_hunt_path_index < 0){
				C.Robin.Visible = true;
			} else {
				C.Robin.Visible = false;
				Globals.LegsDoTreasureHunt();
			}
		}
		*/
		yield return E.Break;
	}

	void OnEnterRoom()
	{
		Globals.LegsOnEnterRoom();
		
		/*if(E.Before(eLegsProgress.RobinHiding)){
			E.Set(eLegsProgress.RobinHiding);
		}*/
		/*
		if (E.Reached(eLegsProgress.GotTreasureHunt) && E.Before(eLegsProgress.CompletedTreasureHunt)){
			//Only show Robin if on the wrong path
			if(Globals.m_treasure_hunt_path_index < 0){
				C.Robin.Visible = true;
				C.Robin.Clickable = true;
			} else {
				C.Robin.Visible = false;
				C.Robin.Clickable = false;
			}
		}
		*/
	}

	void Update()
	{
		Globals.LegsOnUpdate();
	}

	[QuestPlayFromFunction]
	void PlayFromGotHope()
	{
		E.Set(eLegsProgress.GotHope);
		E.Set(eExitDirection.Left);
		E.DebugSetPreviousRoom(R.LegsFountain);
		Audio.PlayMusic("FoxTaleWaltz");
	}
}
