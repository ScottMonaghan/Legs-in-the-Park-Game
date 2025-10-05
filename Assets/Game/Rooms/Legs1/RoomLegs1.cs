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
		
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitWest( IHotspot hotspot )
	{
		if (Globals.CheckDadHuntDirection(eExitDirection.Left)){
			Region("ExitWest").Walkable = true;
			yield return C.Plr.WalkTo(Point("ExitWest"));
			Region("ExitWest").Walkable = false;
			E.Set(eExitDirection.Left);
			yield return E.WaitFor(()=> Globals.LegsChangeRoom() );
			} else {
				yield return E.WaitFor(Globals.LegsKeepDirection);
			}
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitNorth( IHotspot hotspot )
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

	IEnumerator OnInteractHotspotExitEast( IHotspot hotspot )
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

	IEnumerator OnInteractHotspotExitSouth( IHotspot hotspot )
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

	[QuestPlayFromFunction]
	void PlayFromGotHope()
	{
		E.Set(eLegsProgress.GotHope);
		E.Set(eExitDirection.Left);
		E.DebugSetPreviousRoom(R.LegsFountain);
		Audio.PlayMusic("FoxTaleWaltz");
	}
}
