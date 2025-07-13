using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerScript;
using PowerTools.Quest;

///	Global Script: The home for your game specific logic
/**		
 * - The functions in this script are used in every room in your game.
 * - Add your own variables and functions in here and you can access them with `Globals.` (eg: `Globals.m_myCoolInteger`)
 * - If you've used Adventure Game Studio, this is equivalent to the Global Script in that
*/
public partial class GlobalScript : GlobalScriptBase<GlobalScript>
{	
	////////////////////////////////////////////////////////////////////////////////////
	// Global Game Variables
	
	/// Just an example of using an enum for game state.
	/// This can be accessed from other scripts, eg: `if ( E.Is(eProgress.DrankWater) )...`
	public enum eProgress
	{
		None,
		GotWater,
		DrankWater,
		WonGame
	};
	public enum eExitDirection {None,Up,Down,Left,Right};
	public eExitDirection m_lastExitDirection = eExitDirection.None;
	public eProgress m_progressExample = eProgress.None;
	
	/// Just an example of using a global variable that can be accessed in any room with `Globals.m_spokeToBarney`.
	/// All variables like this in Quest Scripts are automatically saved
	public bool m_spokeToBarney = false;
	
	//public eQuestClickableType _lastClickableType = eQuestClickableType.None; //I don't believe this is used SJM 2025-03-29
	
	//Global inventory variables
	public bool m_ready_for_gummy_grabber = false;
	public bool m_lookedAtAstronautCard = false;
	public bool m_lookedAtStickyShoe = false;
	public bool m_lookedAtAbcGum = false;
	public bool m_lookedAtGrabber = false;
	public bool m_lookedAtGummyGrabber = false;
	
	//Shared Global Variable for Emotion Meter
	public int m_emotion_level = 0;
	
	//Shared Global Variables for Legs rooms
	public eLegsProgress m_legsProgress = eLegsProgress.None;
	public enum eLegsProgress {
		None,
		BusStopMaxFrustrated,
		RobinHiding,
		SawRobinPeek1,
		SawRobinPeek2,
		SawRobinPeek3,
		SawRobinPeek4,
		ClickedRobin,
		MeetingRobin,
		GotTreasureHunt,
		CompletedTreasureHunt,
		GotHope,
		LegsEscapeAttempt1,
		LegsEscapeAttempt2,
		LegsEscapePanic,
	};
	public Vector2 m_legs_robin_hide_point = new Vector2(0,0);
	public Vector2 m_legs_robin_peek_point = new Vector2(0,0);
	public Vector2 m_legs_robin_meet_point = new Vector2(0,0);
	public Vector2 m_legs_elsa_meet_robin_point = new Vector2(0,0);
	
	//Legs treasure hunt path and tracker
	public eExitDirection[] m_treasure_hunt_path = {eExitDirection.None, eExitDirection.None, eExitDirection.None, eExitDirection.None };
	public int m_treasure_hunt_path_index = -1;
	
	public int m_dad_search_tracker = 0;
	public eExitDirection m_dad_search_direction = eExitDirection.None;
	
	////////////////////////////////////////////////////////////////////////////////////
	// Global Game Functions
	
	/// Called when game first starts
	public void OnGameStart()
	{     
		((IQuestClickable)I.AstronautCard).Cursor = "Look";
		((IQuestClickable)I.StickyShoe).Cursor = "Look";
		((IQuestClickable)I.AbcGum).Cursor = "Look";
		((IQuestClickable)I.Grabber).Cursor = "Look";
		((IQuestClickable)I.GummyGrabber).Cursor = "Look";
		
	} 

	/// Called after restoring a game. Use this if you need to update any references based on saved data.
	public void OnPostRestore(int version)
	{
	}

	/// Blocking script called whenever you enter a room, before fading in. Non-blocking functions only
	public void OnEnterRoom()
	{
	}

	/// Blocking script called whenever you enter a room, after fade in is complete
	public IEnumerator OnEnterRoomAfterFade()
	{
		yield return E.Break;
	}

	/// Blocking script called whenever you exit a room, as it fades out
	public IEnumerator OnExitRoom( IRoom oldRoom, IRoom newRoom )
	{
		yield return E.Break;
	} 

	/// Blocking script called every frame when nothing's blocking, you can call blocking functions in here that you'd like to occur anywhere in the game
	public IEnumerator UpdateBlocking()
	{
		// Add anything that should happen every frame when nothing's blocking the script here.
		yield return E.Break;
	}

	/// Called every frame. Non-blocking functions only
	public void Update()
	{
		// Add anything that should happen every frame here.
	}	

	/// Called every frame, even when paused. Non-blocking functions only
	public void UpdateNoPause()
	{	
		// Add anything that should happen every frame, even when paused, here.
		
		// Update keyboard/mouse shortcuts
		UpdateInput();
		
	}
 	
	/// Update keyboard and mouse shortcuts
	void UpdateInput()
	{	
		// Add any custom keyboard/mouse shortcuts here
		
		// Set up a debug key
		bool debugKeyHeld = E.IsDebugBuild && (Input.GetKey(KeyCode.BackQuote) || Input.GetKey(KeyCode.Backslash));
		
		if ( E.Paused == false )
		{
			// Skip cutscene if escape key released. (done on release, so that it can also be used to skip dialog while down)
			if ( Input.GetKeyUp(KeyCode.Escape) )
				E.SkipCutscene();
		
			// Skip dialog buttons
			if ( Input.GetMouseButtonDown(0) )
				E.SkipDialog(true); // Skip dialog with left click (if it's been up for long enough)
			if ( Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space) )
				E.SkipDialog(false); // Alternate skip buttons don't have the delay built in. Hold escape to skip through really quick
		}
		
		if ( E.GetBlocked() == false && E.Paused == false )
		{
			// Show menu gui
			if ( Input.GetKeyDown(KeyCode.F1) )
				G.Options.Show();
		
			// Quicksave
			if ( Input.GetKeyDown(KeyCode.F5) )
				E.Save(1, "Quicksave");
		
			// Quickload
			if (  Input.GetKeyDown(KeyCode.F7) )
				E.RestoreSave(1);
		
			// Restart
			if ( Input.GetKeyDown(KeyCode.F9) )
			{
				if ( debugKeyHeld ) // Holding ~ + F9 sets flag to restart at current room
					E.Restart( E.GetCurrentRoom(), E.GetCurrentRoom().Instance.m_debugStartFunction );
				else
					E.Restart();
			}
		}
		
		// Call through to gui system for keyboard navigation
		if ( Input.GetKey(KeyCode.UpArrow) )
			E.NavigateGui(eGuiNav.Up);
		if ( Input.GetKey(KeyCode.DownArrow) )
			E.NavigateGui(eGuiNav.Down);
		if ( Input.GetKey(KeyCode.RightArrow) )
			E.NavigateGui(eGuiNav.Right);
		if ( Input.GetKey(KeyCode.LeftArrow) )
			E.NavigateGui(eGuiNav.Left);
		
		if ( Input.GetKeyDown(KeyCode.Return) )
			E.NavigateGui(eGuiNav.Ok);
		if ( Input.GetKeyDown(KeyCode.Escape) )
			E.NavigateGui(eGuiNav.Cancel);
		
		
		// Debug keys
		if ( debugKeyHeld )
		{
			// Cheat to Give all items
			if ( Input.GetKeyDown(KeyCode.I) )
				PowerQuest.Get.GetInventoryItems_SaveFlagNotDirtied().ForEach(item=>item.Owned = true);
		
			// Cheats to speed up/slow down time
			if ( Input.GetKeyDown(KeyCode.PageDown) )
				Systems.Time.SetDebugTimeMultiplier( Systems.Time.GetDebugTimeMultiplier()*0.8f );
			if ( Input.GetKeyDown(KeyCode.PageUp) )
				Systems.Time.SetDebugTimeMultiplier( Systems.Time.GetDebugTimeMultiplier() + 0.2f );
			if ( Input.GetKeyDown(KeyCode.End) )
				Systems.Time.SetDebugTimeMultiplier( 1.0f );
		}
		
		// This one makes holding '.' while debugging speed the game up and skip over text.
		if ( E.IsDebugBuild && Input.GetKeyDown(KeyCode.Period) )
			Systems.Time.SetDebugTimeMultiplier(4);
		else if ( E.IsDebugBuild && Input.GetKeyUp(KeyCode.Period) )
			Systems.Time.SetDebugTimeMultiplier(1);
		if ( E.IsDebugBuild && Input.GetKey(KeyCode.Period) )
			E.SkipDialog(false);
		
	}

	/// Blocking script called whenever the player clicks anywwere. This function is called before any other click interaction. If this function blocks, it will stop any other interaction from happening.
	public IEnumerator OnAnyClick()
	{
		yield return E.Break;
	}

	/// Blocking script called whenever the player tries to walk somewhere. Even if `C.Player.Moveable` is set to false.

	/// Called when the mouse is clicked in the game screen. Use this to customise your game interface by calling E.ProcessClick() with the verb that should be used. By default this is set up for a 2 click interface
	public void OnMouseClick( bool leftClick, bool rightClick )
	{
		bool mouseOverSomething = E.GetMouseOverClickable() != null;
		
		// Check if should clear inventory
		if ( C.Plr.HasActiveInventory && ( rightClick || (mouseOverSomething == false && leftClick ) || Cursor.NoneCursorActive ) )
		{
			// Clear inventory on Right click, or left click on empty space, or on hotspot with cursor set to "None"
			I.Active = null;
		}
		else if ( Cursor.NoneCursorActive ) // Checks if cursor is set to "None"
		{
			// Special case for clickables with cursor set to "None"- Don't do anything
		}
		else if ( E.GetMouseOverType() == eQuestClickableType.Gui )  // Checks if clicked on a gui
		{
			// Clicked on a gui - Don't do anything
		}
		else if ( leftClick ) // Checks if player left clicked
		{
			if ( mouseOverSomething ) // Check if they clicked on anything
			{
				if ( C.Plr.HasActiveInventory && Cursor.InventoryCursorOverridden == false )
				{
					// Left click with active inventory, use the inventory item
					E.ProcessClick( eQuestVerb.Inventory );
				}
				//else if ( E.GetMouseOverType() == eQuestClickableType.Inventory )
				//{
					// Left clicked on inventory item, make it the active item. Remove this "if statement" if you want to be able to "use" items by clicking on them
					//I.Active = (IInventory)E.GetMouseOverClickable();
				//}
				else
				{
					// Left click on item, so use it
					E.ProcessClick(eQuestVerb.Use);
				}
			}
			else  // They've clicked empty space
			{
				// Left click empty space, so walk
				E.ProcessClick( eQuestVerb.Walk );
			}
		}
		else if ( rightClick )
		{
			// If right clicked something, look at it (if 'look' enabled in PowerQuest Settings)
			if ( mouseOverSomething )
				E.ProcessClick( eQuestVerb.Look );
		}
	}

	////////////////////////////////////////////////////////////////////////////////////
	// Unhandled interactions

	/// Called when player interacted with something that had not specific "interact" script
	public IEnumerator UnhandledInteract(IQuestClickable mouseOver)
	{		
		// This function is called when the player interacts with something that doesn't have a response
		
		if ( mouseOver.ClickableType == eQuestClickableType.Inventory )
		{
			// If clicking an inventory item, select it as the active inventory
			E.ActiveInventory = (IInventory)mouseOver;
		}
		else
		{
			// This bit of logic cycles between three options. The '% 3' makes it cycle between 3 options.
			int option = E.Occurrence("unhandledInteract") % 3;
			if ( option == 0 )
				yield return C.Plr.Say("You can't use that");
			else if ( option == 1 )
				yield return C.Plr.Say("That doesn't work");
			else if ( option == 2 )
				yield return C.Plr.Say("Nothing happened");
		}
	}

	/// Called when player looked at something that had not specific "Look at" script
	public IEnumerator UnhandledLookAt(IQuestClickable mouseOver)
	{
		// This function is called when the player looks at something that doesn't have a response
		
		// In the title screen we don't want any response when looking at things, so we 'return' to stop the script
		if ( R.Current.ScriptName == "Title")
			yield break;
		
		// This bit of logic randomly chooses between three options
		int option = Random.Range(0,3);
		if ( option == 0 )
			yield return C.Plr.Say("It's nothing interesting");
		else if ( option == 1 )
			yield return C.Plr.Say("You don't see anything");
		else if ( option == 2 ) // in this one we do some fancy manipulation to include the name of what was clicked
			yield return C.Plr.Say($"The {mouseOver.Description.ToLower()} isn't very interesting");
	}

	/// Called when player used one inventory item on another that doesn't have a response
	public IEnumerator UnhandledUseInvInv(Inventory invA, Inventory invB)
	{
		// Called when player used one inventory item on another that doesn't have a response
		//Defaults
		yield return C.Plr.Say($"I don't know how to make the {invA.Description} work with the {invB.Description}");
	}

	/// Called when player used inventory on something that didn't have a response
	public IEnumerator UnhandledUseInv(IQuestClickable mouseOver, Inventory item)
	{		
		// This function is called when the uses an item on things that don't have a response
		
		//Bus Stop Overrides
		if (mouseOver == C.Scott){
			yield return C.Scott.Say("No thanks.");
		} else if (mouseOver == C.Dan){
			if (! RoomBusStop.Script.m_dan_distracted){
			RoomBusStop.Script.m_dan_carnival_barking = false;
			yield return C.Dan.Face(eFace.Left);
			yield return C.Dan.Say("That's quite a generous offer little lady, but I'm going to have to decline.");
			yield return C.Dan.Say("Now pardon me while I get back to my hustle.");
			yield return C.Dan.Face(eFace.Right);
			RoomBusStop.Script.m_dan_carnival_barking = true;
		} else {
			yield return E.HandleInteract(C.Dan);
		}
		//Defaults
		} else if (mouseOver != null) {
			yield return C.Plr.Say($"I don't know how to make the {I.Active.Description} work with the {mouseOver.Description}");
		} else {
			yield return C.Plr.Say($"I don't know to to make the {I.Active.Description} work with that.");
		}
	}

	public IEnumerator SetEmotionLevel(int new_emotion_level)
	{
		Globals.m_emotion_level = new_emotion_level;
		
		if (!G.BusStopEmotionBar.Visible){
			G.BusStopEmotionBar.Show();
			/*G.BusStopEmotionBar.GetControl("Meter").Visible = false;
			IImage meterBg = (IImage)(G.BusStopEmotionBar.GetControl("Bg"));
			meterBg.Alpha = 0;
			yield return meterBg.Fade(0,1,2);
			G.BusStopEmotionBar.GetControl("Meter").Visible = true;*/
		}
		switch (Globals.m_emotion_level)
		{
			case 0:
				yield return G.BusStopEmotionBar.GetControl("Bg").Anim = "Bg";
				break;
			case 1:
				yield return G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient1";
				break;
			case 2:
				yield return G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient2";
				break;
			case 3:
				yield return G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient3";
				break;
			case 4:
				yield return G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient4";
				break;
			case 5:
				yield return G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient5";
				//m_allowCrosswalk = true;
				break;
			default:
				break;
		
		
		}
		
		yield return E.Break;
	}

	public void LegsOnEnterRoom()
    {
		if (E.Reached(eLegsProgress.GotTreasureHunt) && E.Before(eLegsProgress.CompletedTreasureHunt))
		{
			//Only show Robin if on the wrong path
			if (Globals.m_treasure_hunt_path_index < 0)
			{
				C.Robin.Visible = true;
				C.Robin.Clickable = true;
			}
			else
			{
				C.Robin.Visible = false;
				C.Robin.Clickable = false;
			}
		} else if (E.Reached(eLegsProgress.GotHope) && E.Before(eLegsProgress.LegsEscapeAttempt1)){
			R.Legs1.GetProp("Legs01Path").Animation = "Legs01PathEve";
			R.Legs2.GetProp("Legs01Path").Animation = "Legs01PathEve";
			if (C.Robin.Room == null)
			{
				C.Robin.Room = R.Current;
			}
		
			if (!I.AstronautCard.Owned){
				I.AstronautCard.Add();
			}
		
			if (!I.GummyGrabber.Owned){
				I.GummyGrabber.Add();
			}
		
			if (!I.SilverLeaf.Owned){
				I.SilverLeaf.Add();
			}
		
		
		
			if (m_legs_robin_meet_point == new Vector2(0,0)){
				m_legs_robin_meet_point = Point("RobinMeet1");
				m_legs_elsa_meet_robin_point = Point("ElsaMeetRobin1");
				C.Robin.Position = m_legs_robin_meet_point;
				C.Robin.Facing = eFace.Right;
			}
			C.Robin.Visible = true;
			C.Robin.Clickable = true;
		} else if (E.Reached(eLegsProgress.LegsEscapeAttempt1) && E.Before(eLegsProgress.LegsEscapeAttempt2)){
		
		}
		
	}

	public IEnumerator OnRobinHide()
	{
		eFace _prevFacing = C.Plr.Facing;
		if(E.Before(eLegsProgress.SawRobinPeek1)){
			E.Set(eLegsProgress.SawRobinPeek1);
		} else if(E.Before(eLegsProgress.SawRobinPeek2)){
			yield return C.Plr.Face(C.Robin);
			yield return E.WaitSkip();
			yield return C.Plr.Face(eFace.Down);
			yield return E.WaitSkip();
			yield return C.Plr.Face(C.Robin);
			yield return E.WaitSkip();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("I'm not crazy...");
			yield return C.Plr.Say("You saw that right?");
			E.Set(eLegsProgress.SawRobinPeek2);
		} else if (E.Before(eLegsProgress.SawRobinPeek3)){
			yield return C.Plr.Face(C.Robin);
			yield return E.WaitSkip();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("Okay, that was definitely a kid.");
			E.Set(eLegsProgress.SawRobinPeek3);
		} else if (E.Before(eLegsProgress.SawRobinPeek4)){
			yield return C.Plr.Face(C.Robin);
			yield return E.WaitSkip();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("This is getting ridiculous.");
			E.Set(eLegsProgress.SawRobinPeek4);
		} else {
			yield return C.Plr.Face(C.Robin);
			yield return E.WaitSkip();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("I'm gonna catch that kid if it's the last thing I do.");
		}
		
		
		yield return C.Plr.Face(_prevFacing);
		
		yield return E.Break;
	}

	public IEnumerator LegsRobinPeek()
	{
		C.Robin.Visible = false;
		C.Robin.Clickable = false;
		C.Robin.SetPosition(-9999,-9999);
		yield return C.Robin.ChangeRoom(R.Current);
		
		C.Robin.SetPosition(Globals.m_legs_robin_hide_point);
		C.Robin.Visible = true;
		yield return C.Robin.WalkTo(Globals.m_legs_robin_peek_point,true);
		C.Robin.Clickable = true;
		E.SetTimer("robin_peeking",1);
		yield return E.Break;
	}

	public void LegsResetTreasureHunt()
	{
		m_treasure_hunt_path[0] = eExitDirection.Down;
		m_treasure_hunt_path[1] = eExitDirection.Up;
		m_treasure_hunt_path[2] = eExitDirection.Left;
		m_treasure_hunt_path[3] = eExitDirection.Right;
		m_treasure_hunt_path.Shuffle();
	}

	public IEnumerator LegsRobinRecitePoem()
	{

		/*
		It will be up to your skills to locate your quarry,
		It will come down to your skills to locate your quarry,
		It will be left to your skills to locate your quarry,
		You must have the right skills to locate your quarry,
		
		But don't make up a lie to tell your own story.
		But don't write down a lie to tell your own story.
		But if left to a liar you won't tell your own story.
		But to get the truth right you must tell your own story.
		
		You think me up to no good? Then please be my guest,
		Look down on my intentions? Then please be my guest,
		Still left with mistrust? Then please be my guest,
		Unsure if I'm right? Then please be my guest,
		
		If you're fed up with my poem then be off on your quest!
		If my poem gets you down then be off on your quest!
		If there's nothing else left then be off on your quest!
		If you're ready right now then be off on your quest!
		*/
		
		/*
		if (m_treasure_hunt_path.Count == 0){
			LegsResetTreasureHunt();
		}
		*/
		
		if (m_treasure_hunt_path[0] == eExitDirection.Up){
		yield return C.Robin.Say(" It will be up to your skills\n to locate your quarry,");
		}
		else if (m_treasure_hunt_path[0] == eExitDirection.Down){
		yield return C.Robin.Say("It will come down to your skills\n to locate your quarry,");
		}
		else if (m_treasure_hunt_path[0] == eExitDirection.Left){
		yield return C.Robin.Say(" It will be left to your skills\n to locate your quarry,");
		}
		else if (m_treasure_hunt_path[0] == eExitDirection.Right){
		yield return C.Robin.Say(" You must have the right skills\n to locate your quarry,");
		}
		
		if (m_treasure_hunt_path[1] == eExitDirection.Up){
		yield return C.Robin.Say("But don't make up a lie\n to tell your own story.");
		}
		else if (m_treasure_hunt_path[1] == eExitDirection.Down){
		yield return C.Robin.Say("But don't write down a lie\n to tell your own story.");
		}
		else if (m_treasure_hunt_path[1] == eExitDirection.Left){
		yield return C.Robin.Say("But if left to a liar\n you won't tell your own story.");
		}
		else if (m_treasure_hunt_path[1] == eExitDirection.Right){
		yield return C.Robin.Say("But to get the truth right\n you must tell your own story.");
		}
		
		if (m_treasure_hunt_path[2] == eExitDirection.Up){
		yield return C.Robin.Say("Think you're up to the task?\n Then please be my guest,");
		}
		else if (m_treasure_hunt_path[2] == eExitDirection.Down){
		yield return C.Robin.Say("Got these clues down?\n Then please be my guest,");
		}
		else if (m_treasure_hunt_path[2] == eExitDirection.Left){
		yield return C.Robin.Say("Left with no questions?\n Then please be my guest,");
		}
		else if (m_treasure_hunt_path[2] == eExitDirection.Right){
		yield return C.Robin.Say("Think you've got it all right?\n Then please be my guest,");
		}
		
		if (m_treasure_hunt_path[3] == eExitDirection.Up){
		yield return C.Robin.Say("If you're fed up with my poem\n then be off on your quest!");
		}
		else if (m_treasure_hunt_path[3] == eExitDirection.Down){
		yield return C.Robin.Say("If you've got these clues down\n then be off on your quest!");
		}
		else if (m_treasure_hunt_path[3] == eExitDirection.Left){
		yield return C.Robin.Say("If there's nothing else left\n then be off on your quest!");
		}
		else if (m_treasure_hunt_path[3] == eExitDirection.Right){
		yield return C.Robin.Say("If you're ready right now\n then be off on your quest!");
		}
		
		
		yield return E.Break;
	}

	public void LegsDoTreasureHunt()
	{
		if (m_treasure_hunt_path_index >= 0 && m_lastExitDirection == m_treasure_hunt_path[m_treasure_hunt_path_index]){
			//progress the treasure hunt!
			if (m_treasure_hunt_path[m_treasure_hunt_path_index] == m_treasure_hunt_path.LastOrDefault()){
				//Successfully completed the treasure hunt path!
				E.Set(eLegsProgress.CompletedTreasureHunt);
			} else {
				m_treasure_hunt_path_index++;
			}
		} else {
			//we are off the path!
			m_treasure_hunt_path_index = -1;
			D.LegsMeetRobin.OptionOn("NewClues");
		}
	}

	public IEnumerator LegsChangeRoom()
	{
		if (E.Reached(eLegsProgress.GotTreasureHunt) && E.Before(eLegsProgress.CompletedTreasureHunt)){
			LegsDoTreasureHunt();
		}
		//if finished treasure hunt path send to Legs fountain
		if (E.Is(eLegsProgress.CompletedTreasureHunt)){
			yield return C.Plr.ChangeRoom(R.LegsFountain);
		} else {
		//otherwise send to the other legs room
			//kick off Robin hiding on first room change
			if(!E.Reached(eLegsProgress.RobinHiding))
            {
				E.Set(eLegsProgress.RobinHiding);
            }
			if (R.Current == R.Legs1){
				yield return C.Plr.ChangeRoom(R.Legs2);
			} else {
				yield return C.Plr.ChangeRoom(R.Legs1);
			}
		}
		
		yield return E.Break;
	}

	public IEnumerator LegsOnEnterRoomAfterFade()
	{
		if (E.Before(eLegsProgress.RobinHiding))
		{
			yield return E.FadeOut();
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
			//E.FadeIn();
			yield return C.Narrator.ChangeRoom(R.Current);
			C.Narrator.SetPosition(0, 0);
			//Prop("BlackScreen").Alpha = 1;
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
			//Prop("BlackScreen").FadeBG(1,0,10);
			yield return E.FadeIn();
			if (!Audio.IsPlaying("FoxTaleWaltz"))
			{
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
			yield return E.ConsumeEvent;
		}
		else if (C.Plr.LastRoom.ScriptName == "Legs1" || C.Plr.LastRoom.ScriptName == "Legs2" || C.Plr.LastRoom.ScriptName == "LegsFountain")
		{
			IRegion enterance_region = Region("ExitWest");
			Vector2 starting_position = Point("ExitWest");
			Vector2 enterance_walkto = Point("EnteranceStopWest");
			Vector2 follow_offset = new Vector2(0,0);
			if (E.Is(eExitDirection.Up))
			{
				starting_position = Point("ExitSouth");
				enterance_region = Region("ExitSouth");
				enterance_walkto = Point("EnteranceStopSouth");
				follow_offset = new Vector2(-40,0);
			}
			else if (E.Is(eExitDirection.Down))
			{
				starting_position = Point("ExitNorth");
				enterance_region = Region("ExitNorth");
				enterance_walkto = Point("EnteranceStopNorth");
				follow_offset = new Vector2(-40,0);
			}
			else if (E.Is(eExitDirection.Right) || E.Is(eExitDirection.None))
			{
				starting_position = Point("ExitWest");
				enterance_region = Region("ExitWest");
				enterance_walkto = Point("EnteranceStopWest");
				follow_offset = new Vector2(-40,0);
			}
			else if (E.Is(eExitDirection.Left))
			{
				starting_position = Point("ExitEast");
				enterance_region = Region("ExitEast");
				enterance_walkto = Point("EnteranceStopEast");
				follow_offset = new Vector2(40,0);
			}
			else
			{
				yield return C.Display($"UNEXPECTED EXIT DIRECTION: {Globals.m_lastExitDirection}");
			}
			C.Plr.Position = starting_position;
			enterance_region.Walkable = true;
			E.DisableCancel();
			yield return E.FadeIn();
			yield return C.Plr.WalkTo(enterance_walkto);
			if (E.Reached(eLegsProgress.LegsEscapeAttempt1) && E.Before(eLegsProgress.LegsEscapePanic)){
				C.Robin.Visible = false;
				C.Robin.Clickable = false;
				yield return C.Robin.ChangeRoom(R.Current);
				C.Robin.Position = starting_position;
				C.Robin.Visible = true;
				C.Robin.Clickable = true;
				C.Robin.WalkToBG(C.Player.Position + follow_offset,C.Plr.Facing);
			}
			enterance_region.Walkable = false;
		}
		else
		{
			yield return C.Display($"UNEXPECTED PREVIOUS ROOM: {C.Plr.LastRoom.ScriptName}.");
		}
		
		//Robin playing hide & seek
		if (E.Reached(eLegsProgress.RobinHiding) && E.Before(eLegsProgress.ClickedRobin))
		{
			//setup Robin
			if (R.Current == R.Legs1)
			{
				if (E.FirstOption(2))
				{
					Globals.m_legs_robin_hide_point = Point("RobinHide1");
					Globals.m_legs_robin_peek_point = Point("RobinPeek1");
				}
				else if (E.NextOption)
				{
					Globals.m_legs_robin_hide_point = Point("RobinHide2");
					Globals.m_legs_robin_peek_point = Point("RobinPeek2");
				}
				Globals.m_legs_robin_meet_point = Point("RobinMeet1");
				Globals.m_legs_elsa_meet_robin_point = Point("ElsaMeetRobin1");
			}
			else
			{
				if (E.FirstOption(4))
				{
					Globals.m_legs_robin_hide_point = Point("RobinHide1");
					Globals.m_legs_robin_peek_point = Point("RobinPeek1");
				}
				else if (E.NextOption)
				{
					Globals.m_legs_robin_hide_point = Point("RobinHide2");
					Globals.m_legs_robin_peek_point = Point("RobinPeek2");
				}
				else if (E.NextOption)
				{
					Globals.m_legs_robin_hide_point = Point("RobinHide3");
					Globals.m_legs_robin_peek_point = Point("RobinPeek3");
				}
				else if (E.NextOption)
				{
					Globals.m_legs_robin_hide_point = Point("RobinHide4");
					Globals.m_legs_robin_peek_point = Point("RobinPeek4");
				}
				Globals.m_legs_robin_meet_point = Point("RobinMeet1");
				Globals.m_legs_elsa_meet_robin_point = Point("ElsaMeetRobin1");
			}
			yield return E.WaitFor( Globals.LegsRobinPeek );
		
		}
		
		//Got lost on the treasure hunt
		else if (
			E.Reached(eLegsProgress.GotTreasureHunt)
			&& E.Before(eLegsProgress.CompletedTreasureHunt)
			&& C.Robin.Room == R.Current
			&& Globals.m_treasure_hunt_path_index == -1
		)
		{
			yield return C.Robin.Face(C.Player);
			yield return C.Robin.Say("Looks like you're a little lost. Hee hee.");
			yield return C.Robin.Say("Can I do anything to help?");
			Globals.m_treasure_hunt_path_index = 0;
		}
		
		//Completed treasure hunt!
		else if (E.Reached(eLegsProgress.GotHope) && E.Before(eLegsProgress.LegsEscapeAttempt1))
		{
			E.StartCutscene();
			yield return C.Plr.WalkTo(m_legs_elsa_meet_robin_point);
			yield return C.Robin.Say(" Need some more clues? Hee hee.");
			yield return C.Plr.Say("Nope. I got it mission accomplished!");
			yield return C.Plr.Say("A silver leaf right?");
			yield return C.Robin.Say(" Wait. What!?");
			yield return C.Robin.Say(" You got it!?");
			yield return C.Plr.Say("Yup. Right here.");
			yield return C.Plr.Say("Let me show you then I gotta get back to my dad.");
			yield return E.FadeOut();
			C.Narrator.Room = R.Current;
			C.Narrator.SetPosition(0,0);
			yield return C.Narrator.Say("(PLACEHOLDER)");
			yield return C.Narrator.Say("Elsa reaches into her pocket and shifts around looking for the silver leaf.");
			yield return C.Plr.Say("Elsa: Wow it's REALLY warm now.");
			yield return C.Narrator.Say("Elsa pulls out the leaf to show Robin");
			yield return C.Narrator.Say("But it melts into her hand leaving a glowing outline");
			yield return C.Plr.Say("Elsa: WHAT THE HECK!");
			yield return C.Robin.Say("Robin: It shouldn't do that! You shouldn't even have been able to find it.");
			E.EndCutscene();
			yield return E.FadeIn();
			yield return C.Plr.Say("Elsa: I NEED TO GET BACK TO MY DAD RIGHT NOW!");
			yield return C.Robin.Say("Okay.");
			G.BusStopEmotionBar.GetControl("Meter").Text = "Fear";
			yield return E.WaitFor(()=> SetEmotionLevel(m_emotion_level + 1) );
			C.Plr.WalkSpeed = new Vector2(100,100);
			E.Set(eLegsProgress.LegsEscapeAttempt1);
		} else if (E.Reached(eLegsProgress.LegsEscapeAttempt1) && E.Before(eLegsProgress.LegsEscapeAttempt2)) {
			m_dad_search_tracker++;
			if (m_dad_search_tracker == 1)
			{
				yield return C.Plr.Say("All I need to do is retrace my steps back");
			}
			else if (m_dad_search_tracker == 2)
			{
				yield return C.Plr.Say("I THINK this is the way");
			}
			else if (m_dad_search_tracker == 3)
			{
				yield return C.Plr.Say("Is this the way?");
			}
			else if (m_dad_search_tracker >= 4)
			{
				yield return C.Plr.Say("ARRRRGH!");
				yield return E.WaitFor(()=> SetEmotionLevel(m_emotion_level + 1) );
				E.Set(eLegsProgress.LegsEscapeAttempt2);
				m_dad_search_tracker = 0;
				yield return C.Plr.Say("Okay Elsa, you're not thinking straight.");
				yield return C.Plr.Say("The park isn't very big all I need to do is walk in a straight line");
				yield return C.Plr.Say("then I'll get to the street and walk back to my dad there.");
		
			}
		} else if (E.Reached(eLegsProgress.LegsEscapeAttempt2) && E.Before(eLegsProgress.LegsEscapePanic)){
			if (m_lastExitDirection != m_dad_search_direction){
				m_dad_search_tracker = 1;
				m_dad_search_direction = m_lastExitDirection;
			} else {
				m_dad_search_tracker++;
			}
			if (m_dad_search_tracker == 1){
				yield return C.Plr.Say("Just gotta keep heading this way, and I'll be out in no time.");
			}
			else if (m_dad_search_tracker == 2) {
				yield return C.Plr.Say("I'll be out soon.");
			}
			else if (m_dad_search_tracker == 3) {
				yield return C.Plr.Say("I must be walking through the long part.");
				yield return C.Plr.Say("That's okay it's still less than a city block.");
			}
			else if (m_dad_search_tracker == 4) {
				yield return C.Plr.Say("How am I not there yet!?");
				yield return C.Plr.Say("I MUST be close.");
				yield return E.WaitFor(()=> SetEmotionLevel(m_emotion_level+1) );
			}
			else if (m_dad_search_tracker == 5) {
				yield return C.Plr.Say("THIS IS RIDICULOUS!");
				yield return C.Plr.Say("Must be straight ahead.");
				yield return E.WaitFor(()=> SetEmotionLevel(m_emotion_level+1) );
			}
			else if (m_dad_search_tracker >= 6) {
				yield return E.WaitFor(()=> SetEmotionLevel(m_emotion_level+1) );
				E.Set(eLegsProgress.LegsEscapePanic);
				yield return E.FadeOut();
				C.Narrator.Room = R.Current;
				C.Narrator.SetPosition(0,0);
				C.Robin.SetTextPosition(new Vector2(0,10));
				C.Elsa.SetTextPosition(new Vector2(0,10));
				yield return C.Narrator.Say("PLACEHOLDER");
				yield return C.Narrator.Say("Elsa starts too breath too heavy like she sometimes does when she is about to panic.");
				yield return C.Narrator.Say("With rising concern, Robin asks,");
				yield return C.Robin.Say("What's wrong?");
				yield return C.Narrator.Say("Elsa half-sobs, half-screams through labored breaths,");
				yield return C.Player.Say("I CAN'T...");
				yield return C.Player.Say("FIND...");
				yield return C.Player.Say("MY DAD!");
				yield return C.Narrator.Say("THE EARTH SHAKES AND THE WIND BLOWS");
				yield return C.Narrator.Say("Robin nervously replies,");
				yield return C.Robin.Say("Don't worry, I'll help you. I can help you find your dad.");
				yield return C.Narrator.Say("THE QUAKING AND WIND DIE DOWN");
				yield return C.Narrator.Say("With a small voice tinged with hope, Elsa asks,");
				yield return C.Player.Say("you can?");
				yield return C.Robin.Say("Sure! But one question first,");
				yield return E.WaitSkip();
				yield return C.Robin.Say("What...\nis a dad?");
				yield return C.Robin.Say("What...\nis a dad?");
				yield return C.Robin.Say("What...\nis a dad?");
				G.BusStopEmotionBar.Hide();
				yield return E.WaitFor( EndDemo );
			}
		}
		
		
		yield return E.Break;
	}

	public IEnumerator LegsOnUpdateBlocking()
	{
		if (E.Reached(eLegsProgress.RobinHiding) && E.Before(eLegsProgress.ClickedRobin))
		{
			if (E.GetTimerExpired("robin_peeking"))
			{
				yield return C.Robin.WalkTo(Globals.m_legs_robin_hide_point, true);
				C.Robin.Clickable = false;
				C.Robin.Visible = false;
				yield return E.WaitFor( Globals.OnRobinHide );
			}
		}
		yield return E.Break;
	}

	public void LegsOnUpdate()
	{
		if (E.Is(eLegsProgress.LegsEscapeAttempt1)){
			C.Robin.FaceBG(C.Player);
		}
	}

	public IEnumerator LegsKeepDirection()
	{
		yield return C.Plr.Say("I can't go that way, I need to keep going the same direction so I can get out of here!");
		yield return E.Break;
	}

	public bool CheckDadHuntDirection(eExitDirection direction)
	{
		if (E.Is(eLegsProgress.LegsEscapeAttempt2) && m_dad_search_direction != eExitDirection.None && direction != m_dad_search_direction){
			return false;
		}
		return true;
	}

	public IEnumerator EndDemo()
	{
		yield return E.Display(
			"You've reached the end of this alpha demo!\n"
			+ "Thanks for playing!\n"
			+ "\n"
			+ "Please report any bugs to scott.monaghan@gmail.com\n"
		);
		yield return E.Display(
			"Music Credit:\n"
			+ "Krampus Workshop,\n"
			+ "Night Vigil,\n"
			+ "Fox Tale Waltz Part 1 Instrumental\n"
			+ "Frost Waltz\n"
			+ "Kevin MacLeod (incompetech.com)\n"
			+ "Licensed under Creative Commons: By Attribution 4.0 License\n"
			+ "http://creativecommons.org/licenses/by/4.0/"
		);
		E.Restart(R.Title);
		yield return E.Break;
	}
}
