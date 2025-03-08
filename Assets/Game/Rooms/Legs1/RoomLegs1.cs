using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomLegs1 : RoomScript<RoomLegs1>
{
	
	IEnumerator OnEnterRegionExitNorth( IRegion region, ICharacter character )
	{

		yield return E.Break;
	}

	IEnumerator OnEnterRoomAfterFade()
	{
		if (!Audio.IsPlaying("FoxTaleWaltz")){
			Audio.PlayMusic("FoxTaleWaltz");
		}
		if (C.Plr.LastRoom.ScriptName == "BusStop"){
			C.Plr.Position = Point("ExitWest");
			E.DisableCancel();
			yield return C.Plr.WalkTo(Point("EnteranceStopWest"));
			if (Audio.IsPlaying("CitySounds")){
				Audio.StopAmbientSound(5);
			}
			if (Audio.IsPlaying("Birds")){
				 Audio.Stop("Birds",5);
			}
		} else if(C.Plr.LastRoom.ScriptName == "Legs1" || C.Plr.LastRoom.ScriptName == "Legs2") {
			if (E.Is(eExitDirection.North)){
				C.Plr.Position = Point("ExitSouth");
				Region("ExitSouth").Walkable = true;
				E.DisableCancel();
				yield return C.Plr.WalkTo(Point("EnteranceStopSouth"));
				Region("ExitSouth").Walkable = false;
			}
			else if (E.Is(eExitDirection.South)){
				Region("ExitNorth").Walkable = true;
				C.Plr.Position = Point("ExitNorth");
				E.DisableCancel();
				yield return C.Plr.WalkTo(Point("EnteranceStopNorth"));
				Region("ExitNorth").Walkable = false;
			}
			else if (E.Is(eExitDirection.East) || E.Is(eExitDirection.None)){
				C.Plr.Position = Point("ExitWest");
				E.DisableCancel();
				Region("ExitWest").Walkable = true;
				yield return C.Plr.WalkTo(Point("EnteranceStopWest"));
				Region("ExitWest").Walkable = false;
			}
			else if (E.Is(eExitDirection.West)){
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
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitWest( IHotspot hotspot )
	{
		Region("ExitWest").Walkable = true;
		yield return C.Plr.WalkTo(Point("ExitWest"));
		Region("ExitWest").Walkable = false;
		E.Set(eExitDirection.West);
		yield return C.Plr.ChangeRoom(R.Legs2);
		yield return E.Break;
	}

	IEnumerator OnEnterRegionExitWest( IRegion region, ICharacter character )
	{

		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitNorth( IHotspot hotspot )
	{
		Region("ExitNorth").Walkable = true;
		yield return C.Plr.WalkTo(Point("ExitNorth"));
		Region("ExitNorth").Walkable = false;
		E.Set(eExitDirection.North);
		yield return C.Plr.ChangeRoom(R.Legs2);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitEast( IHotspot hotspot )
	{
		Region("ExitEast").Walkable = true;
		yield return C.Plr.WalkTo(Point("ExitEast"));
		Region("ExitEast").Walkable = false;
		E.Set(eExitDirection.East);
		yield return C.Plr.ChangeRoom(R.Legs2);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitSouth( IHotspot hotspot )
	{
		Region("ExitSouth").Walkable = true;
		yield return C.Plr.WalkTo(Point("ExitSouth"));
		Region("ExitSouth").Walkable = false;
		E.Set(eExitDirection.South);
		yield return C.Plr.ChangeRoom(R.Legs2);
		yield return E.Break;
	}
}