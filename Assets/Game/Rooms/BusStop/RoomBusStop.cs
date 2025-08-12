using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBusStop : RoomScript<RoomBusStop>
{
	public enum eLocation {BusStop,Legs}
	public eLocation m_locationState = eLocation.BusStop;
	//public int m_emotion_level = 0;
	public bool m_allowCrosswalk = false;
	public int m_numberOfBuses = 2;
	public int m_minutesLeft = 5;
	public bool m_dan_carnival_barking = false;
	public bool m_dan_delay = false;
	public bool m_barrel_open = false;
	public bool m_dan_distracted = false;
	public bool m_price_of_grabber_revealed = false;
	public bool m_asked_dad_for_money = false;
	public bool m_talked_to_dan = false;
	public bool m_got_grabber = false;
	public bool m_got_abc_gum = false;
	float m_paused_timer = 0;
	public IEnumerator SetEmotionLevel(int new_emotion_level)
    {
		/*
		Globals.m_emotion_level = new_emotion_level;
		
		if (!G.BusStopEmotionBar.Visible){
			G.BusStopEmotionBar.Show();
			G.BusStopEmotionBar.GetControl("Meter").Visible = false;
			IImage meterBg = (IImage)(G.BusStopEmotionBar.GetControl("Bg"));
			meterBg.Alpha = 0;
			yield return meterBg.Fade(0,1,2);
			G.BusStopEmotionBar.GetControl("Meter").Visible = true;
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
				break;
			default:
				break;
		
		
		}
		*/
		E.WaitFor(() => Globals.SetEmotionLevel(new_emotion_level));
		if (Globals.m_emotion_level >= 5)
        {
			E.Set(eLegsProgress.BusStopMaxFrustrated);
			m_allowCrosswalk = true;

		}
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotStreet( IHotspot hotspot )
	{
			eFace _prevFacing = C.Player.Facing;
			yield return C.Player.Face(eFace.Down);
			yield return C.Player.Say("Jaywalk?");
			yield return E.WaitSkip();
			yield return C.Player.Say("What do you take me for?");
			yield return E.WaitSkip(0.25f);
			yield return C.Player.Say("Some kind of criminal!?");
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotLeftCrosswalk( IHotspot hotspot )
	{
		if (RoomBusStop.Script.m_allowCrosswalk){
			E.DisableCancel();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Player.Say("I'm just going to have to take matters into my own hands.");
			yield return E.WaitFor(Globals.EmotionBarFadeout);
			//G.BusStopEmotionBar.GetControl("Meter").Visible=false;
			//IImage meterBg = (IImage)G.BusStopEmotionBar.GetControl("Bg");
			//yield return meterBg.Fade(1,0,1);
			//G.BusStopEmotionBar.Hide();
			//meterBg.Alpha = 1;
			//G.BusStopEmotionBar.GetControl("Meter").Visible=true;
			//Globals.m_emotion_level = 0;
			yield return E.WaitSkip();
			yield return C.Plr.Face(C.Scott);
			yield return C.Player.Say("Dad, let's just go play in The Legs across the street like we used to!");
			yield return C.Scott.Say("Are you sure? I thought you wanted to go to the planetarium?");
			yield return C.Player.Say("I'm sure, let's go!");
			yield return C.Scott.Say("Okay kiddo!");
			yield return C.Plr.WalkTo(Point("LeftSideOfStreet"));
			yield return C.Plr.Face(eFace.UpRight);
			yield return E.WaitSkip();
			yield return C.Plr.Face(eFace.DownLeft);
			yield return E.WaitSkip();
			Region("Crosswalk").Walkable = true;
			C.Scott.AnimPrefix = "";
			C.Scott.WalkToBG(Point("RightSideOfStreet"));
			yield return C.Plr.WalkTo(Point("Gum"));
			yield return C.Plr.PlayAnimation("ElsaGum");
			C.Plr.StopAnimation();
			C.Plr.AnimPrefix="ElsaGum";
			yield return C.Player.Say("Well this is a sticky situation!");
			C.Plr.SetPosition(C.Plr.Position[0] + 44, C.Plr.Position[1]);
			C.Plr.AnimPrefix="Gum";
			I.StickyShoe.Add();
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.WalkTo(Point("RightSideOfStreet")[0] + 20, Point("RightSideOfStreet")[1]);
			C.Dan.AnimPrefix = "SUE";
			yield return C.Plr.Face(C.Scott);
			C.Scott.AnimPrefix = "Phone";
			yield return C.Scott.Face(C.Player);
			yield return C.Scott.Say("I just gotta finish this work email. I'll be right in.");
			yield return C.Player.Say("Okay!");
			RoomBusStop.Script.m_dan_carnival_barking = true;
			Region("Crosswalk").Walkable = false;
			Hotspot("LeftCrosswalk").Disable();
			Hotspot("RightCrosswalk").Enable();
			RoomBusStop.Script.m_locationState = RoomBusStop.eLocation.Legs;
			Hotspot("Street").Cursor = "Left";
		} else {
			yield return C.Player.Say("I don't want to cross the street. I might miss my bus to the planetarium!");
		}
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotRightCrosswalk( IHotspot hotspot )
	{
		yield return C.Player.Face(Hotspot("RightCrosswalk"));
		yield return C.Elsa.Say("I just got here... all the way to the other side.");
		yield return C.Elsa.Say("I can't turn back now.");
		yield return C.Elsa.Say("What would the chicken think!?");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotStreet( IHotspot hotspot )
	{
		yield return C.Plr.Face(Hotspot("Street"));
		yield return C.Player.Say("It's Roosevelt Road.");
		yield return C.Player.Say("Renamed from 12th Street for our mustachioed 26th president in 1919.");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotLeftCrosswalk( IHotspot hotspot )
	{
		yield return C.Player.Face(Hotspot("LeftCrosswalk"));
		yield return C.Player.Say("It's the safe and legal way to cross the street.");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotRightCrosswalk( IHotspot hotspot )
	{
		yield return C.Player.Face(Hotspot("RightCrosswalk"));
		yield return C.Player.Say("It's the crosswalk I just used to safely and legally cross the street.");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotLegs( IHotspot hotspot )
	{
		yield return C.Plr.Face(eFace.Right);
		yield return C.Player.Say("It's a forest of giant metal legs.");
		
		eFace _prevFacing = C.Plr.Facing;
		yield return E.WaitSkip();
		yield return C.Plr.Face(eFace.Down);
		yield return C.Player.Say("That's a totally normal thing to have in a city park right?");
		yield return E.WaitSkip();
		yield return C.Plr.Face(_prevFacing);
		Hotspot("Legs").Description = "Walk into the Legs";
		Hotspot("Legs").Cursor = "Right";
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotLegs( IHotspot hotspot )
	{
		//Player: This is fine.
		//...
		//C.Plr.WalkTo(Hotspot("Legs"));
		//E.FadeOut();
		if (Hotspot("Legs").LookCount == 0){
			yield return E.HandleLookAt(Hotspot("Legs"));
		} else {
			yield return C.Plr.Face(eFace.Right);
			bool _blockExit = false;
			bool _previousBlocks = false;
		
			eFace _prevFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
		
			if (! RoomBusStop.Script.m_got_abc_gum){
				_blockExit = true;
				_previousBlocks = true;
				yield return C.Plr.Say("I can't walk into public art with sticky gum on my shoe.");
			}
		
			if (!RoomBusStop.Script.m_talked_to_dan){
				_blockExit = true;
				if (_previousBlocks){
					yield return C.Plr.Say("Also");
					yield return E.WaitSkip();
				}
				yield return C.Plr.Say("I kinda wanna see what that guy's deal is first.");
				yield return C.Plr.Face(_prevFacing);
			} else if (!RoomBusStop.Script.m_got_grabber){
				_blockExit = true;
				yield return C.Plr.Face(eFace.Down);
				if (_previousBlocks){
					yield return C.Plr.Say("Also");
				}
				yield return C.Plr.Say("I really want one of those grabbers. It's important to invest in my future!");
				yield return C.Plr.Face(_prevFacing);
			}
			if(!_blockExit){
				yield return C.Player.Say("Off I go!");
				E.StartCutscene();
				m_dan_carnival_barking = false;
				yield return E.WaitSkip();
				yield return C.Plr.WalkTo(Hotspot("Legs"));
				Audio.StopAmbientSound(5);
				Audio.StopMusic(5);
				if (Audio.IsPlaying("Birds"))
				{
					Audio.Stop("Birds", 5);
				}
				yield return E.FadeOut(5);
				yield return E.ChangeRoom(R.Legs1);
			} else {
				yield return C.Plr.Face(_prevFacing);
			}
		
		}
		yield return E.Break;
	}

	IEnumerator OnLookAtPropTree( IProp prop )
	{
		yield return C.Plr.Face(Prop("Tree"));
		yield return C.Player.Say("That's a nice looking tree.");
		//main cursor turns to use after looking at the tree once
		Prop("Tree").Cursor = "Use";
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotBusSchedule( IHotspot hotspot )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
		yield return C.Player.Say("It's the CTA Bus schedule.");
		yield return E.WaitSkip(0.25f);
		yield return C.Player.Say($"{m_numberOfBuses} buses should have come by now!");
		m_numberOfBuses ++;
		yield return E.WaitSkip(0.25f);
		if (Hotspot("BusSchedule").FirstLook){
			yield return E.WaitFor(()=>SetEmotionLevel(Globals.m_emotion_level+1));
		}
		if (Globals.m_emotion_level >=5){
			yield return E.WaitFor(FullEmotionHint);
		}
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotBusSchedule( IHotspot hotspot )
	{
		yield return E.HandleLookAt(Hotspot("BusSchedule"));
		yield return E.Break;
	}

	void OnEnterRoom()
	{
		
		
		
	}

	IEnumerator OnLookAtCharacterScott( ICharacter character )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		yield return C.Plr.Say("That's my dad.");
		yield return C.Plr.Face(_prevFacing);
		yield return E.Break;
	}

	IEnumerator OnInteractCharacterScott( ICharacter character )
	{
		yield return C.WalkToClicked();
		yield return C.FaceClicked();
			yield return C.Player.Say("Hey dad.");
			yield return E.WaitSkip();
		if (E.Is(eLocation.BusStop)){
			yield return C.Scott.Say("Whats up kiddo?");
			D.AskDadAboutBus.Start();
		} else if (m_dan_distracted){
			yield return C.Scott.Say("Can you believe this guy?");
		} else if (m_price_of_grabber_revealed == true && ! m_got_grabber){
			yield return C.Plr.Say("Can I have $49.99 to buy an investment-grade toothless Sue(tm) the T-Rex grabber?");
			yield return E.WaitSkip();
			yield return C.Scott.Say("Nope.");
			m_asked_dad_for_money = true;
		} else if (E.Is(eLocation.Legs)){
			yield return C.Scott.Say("Just gotta finish this work email. Why don't you head into the Legs. I'll join you in a minute.");
		}
	}

	IEnumerator OnEnterRoomAfterFade()
	{
		if(R.Current.FirstTimeVisited) {
			C.Scott.AnimPrefix = "Phone";
			E.StartCutscene();
			yield return C.Narrator.ChangeRoom(R.Current);
			C.Narrator.SetPosition(-158,0);
			Prop("BlackScreen").Alpha = 1;
			yield return E.FadeIn();
			yield return C.Display("Chapter 1: At the Bus Stop");
			yield return C.Narrator.Say("Today is Saturday,");
			yield return C.Narrator.Say("the first day of autumn 2019 in Chicago.");
			yield return E.WaitSkip();
			Prop("BlackScreen").FadeBG(1,0,10);
			yield return C.Narrator.Say("Like Elsa & her dad have done many times before,");
			Audio.PlayMusic("FoxTaleWaltz");
			Audio.PlayAmbientSound("CitySounds",5);
			Audio.Play("Birds").FadeIn(5);
			yield return C.Narrator.Say("they wait for the 146 bus to the planetarium.");
			yield return C.Narrator.Say("But today,");
			yield return E.WaitSkip();
			yield return C.Narrator.Say("for some reason,");
			yield return E.WaitSkip();
			yield return C.Narrator.Say("the bus isn't coming.");
			E.EndCutscene();
		}
		
		else {
			if (!Audio.IsPlaying("FoxTaleWaltz")){
			   Audio.PlayMusic("FoxTaleWaltz");
			}
		
			if (!Audio.IsPlaying("CitySounds")){
				Audio.PlayAmbientSound("CitySounds",5);
			}
			if (!Audio.IsPlaying("Birds")){
				 Audio.Play("Birds").FadeIn(5);
			}
		}
		
		
		if (C.Plr.LastRoom != null && C.Plr.LastRoom.ScriptName == "Legs1"){
			C.Plr.SetPosition(Hotspot("Legs").WalkToPoint);
			E.DisableCancel();
			yield return C.Plr.WalkTo(Point("RightSideOfStreet")[0]+20, Point("RightSideOfStreet")[1]);
		}
		yield return E.Break;
	}

	public IEnumerator FullEmotionHint()
	{
		eFace _oldFacing = C.Player.Facing;
		yield return C.Player.Face(eFace.Down);
		yield return C.Player.Say("Maybe I should just give up and go play in the Legs accross the street.");
		yield return C.Player.Face(_oldFacing);
		yield return E.Break;
	}

	void Update()
    {
		if (m_dan_carnival_barking && (C.Elsa.Talking || C.Scott.Talking)){
			C.Dan.CancelSay();
			m_dan_delay = true;
			E.SetTimer("dan_delay", 1); //delay dan from starting talking for 1 second.
		}
		
		if(E.GetTimerExpired("dan_delay")){
			m_dan_delay = false;
		}
		
		
		if (m_dan_carnival_barking && !C.Dan.Talking && !m_dan_delay)
		{
			C.Dan.Face(eFace.Right);
			if (E.FirstOption(5, "danbarks"))
				C.Dan.SayBG("Don't miss the deal of a lifetime!");
			else if (E.NextOption)
				C.Dan.SayBG("Find your path to financial freedom today!");
			else if (E.NextOption)
				C.Dan.SayBG("Tired of being poor and/or having to work? I've got the answer for you!");
			else if (E.NextOption)
				C.Dan.SayBG("How does being rich sound to you? Talk to me to find out more!");
			else if (E.NextOption)
				C.Dan.SayBG("Sick of worrying about money? I've got the solution here!");
		}
		
	}

	void OnPostRestore( int version )
	{
		switch (Globals.m_emotion_level)
		{
			case 0:
				G.BusStopEmotionBar.GetControl("Bg").Anim = "Bg";
				break;
			case 1:
				G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient1";
				break;
			case 2:
				G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient2";
				break;
			case 3:
				G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient3";
				break;
			case 4:
				G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient4";
				break;
			case 5:
				G.BusStopEmotionBar.GetControl("Bg").Anim = "BgEmoteImpatient5";
				m_allowCrosswalk = true;
				break;
			default:
				break;
		}
		
	}

	IEnumerator OnLookAtPropBarrel( IProp prop )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		if(!m_barrel_open){
			yield return C.Plr.Say("It's a museum trash barrel.");
			yield return C.Plr.Say("But the museum is way on the other side of the park.");
			if (!m_dan_distracted){
				yield return C.Plr.Say("Also, that guy is standing weirdly close to it.");
			}
		} else {
			yield return C.Plr.Say("It's filled to the top with dozens of one-of-a-kind misprinted Sue(tm) the T-Rex grabbers.");
		}
		yield return C.Plr.Face(_prevFacing);
		if(Prop("Barrel").FirstLook){
			Prop("Barrel").Cursor = "Use";
			//need to override hovertext
			Prop("Barrel").Description = "Look inside the suspiciously-located museum trash barrel";
		}
		yield return E.Break;
	}

	IEnumerator OnInteractPropBarrel( IProp prop )
	{
		if (Prop("Barrel").LookCount==0){
			yield return E.HandleLookAt(Prop("Barrel"));
		} else if (! m_got_grabber){
			if(!RoomBusStop.Script.m_dan_distracted){
				m_dan_carnival_barking = false;
				yield return C.Dan.Face(eFace.Left);
				yield return C.Dan.Say("Sorry little lady!");
				yield return C.Dan.Say("That's official museum property!");
				yield return C.Dan.Say("That's for authorized personnel only!");
				yield return C.Dan.Say("Now pardon me while I get back to my hustle!");
				yield return C.Dan.Face(eFace.Right);
				m_dan_carnival_barking = true;
			} else {
				//pause timer
				m_paused_timer = E.GetTimer("DanDistracted");
				E.SetTimer("DanDistracted",0);
		
				if (!m_barrel_open){
					yield return C.Plr.WalkTo(Prop("Barrel"));
					yield return C.Plr.Face(eFace.UpRight);
					yield return C.Plr.PlayAnimation("ElsaOpenBarrel");
					C.Plr.StopAnimation();
					yield return E.WaitSkip(0.25f);
					RoomBusStop.Script.m_barrel_open = true;
					yield return C.Plr.Face(eFace.Down);
					yield return C.Plr.Say("It's filled to the top with dozens of one-of-a-kind misprinted Sue(tm) the T-Rex grabbers with missing teeth.");
					yield return C.Plr.Face(eFace.UpRight);
					Prop("Barrel").Description = "Take a toothless Official Sue(tm) the T-Rex grabber";
				} else {
					yield return C.Plr.Face(eFace.Down);
					yield return C.Plr.Say("It's not stealing to take from a trash barrel right?");
					yield return E.WaitSkip();
					yield return C.Plr.Say("It's RECYCLING!");
					yield return C.Plr.WalkTo(Prop("Barrel"));
					yield return C.Plr.Face(eFace.UpRight);
					yield return C.Plr.PlayAnimation("ElsaGetGrabber");
					C.Plr.StopAnimation();
					I.Grabber.Add();
					yield return E.WaitSkip(0.25f);
					yield return C.Plr.Face(eFace.Down);
					yield return C.Plr.Say("I'm glad I wore my shorts with pockets today!");
					RoomBusStop.Script.m_got_grabber = true;
					m_paused_timer = 1;
				}
				//unpause timer
				E.SetTimer("DanDistracted", m_paused_timer);
			}
		} else {
			eFace _prevFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("I don't need any more defective Official Sue(tm) the T-Rex grabbers.");
			yield return C.Plr.Face(_prevFacing);
		}
		yield return E.Break;
	}

	IEnumerator OnLookAtCharacterDan( ICharacter character )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		yield return C.Plr.Say("He looks kind of familiar.");
		yield return C.Plr.Say("He's standing very close to that trash barrel.");
		yield return E.Break;
	}

	IEnumerator OnInteractCharacterDan( ICharacter character )
	{
		if(!m_dan_distracted){
			D.BusStopDialogDan.Start();
		} else {
			yield return C.Plr.WalkTo(C.Dan.Position[0]+ 50, C.Dan.Position[1]);
			yield return C.Dan.Say("I'm doing business with your dad little lady. Can't be interrupted!");
		}
		yield return E.Break;
	}

	IEnumerator UpdateBlocking()
	{
		if (E.GetTimerExpired("DanDistracted")){
			C.Dan.StopAnimation();
			C.Scott.StopAnimation();
			C.Plr.WalkToBG(Point("DanHome")[0] - 30, Point("DanHome")[1]);
			yield return C.Dan.WalkTo(Point("DanHome"));
			C.Plr.FaceBG(eFace.Right);
			yield return C.Dan.Face(eFace.Left);
			yield return C.Dan.Say("Sorry little lady. Looks like your dad needs more convincing!");
			yield return C.Dan.Say("If he changes his mind, let me know!");
			yield return C.Dan.Say("Now pardon me while I get back to my hustle.");
			m_dan_distracted = false;
			m_dan_carnival_barking = true;
		
			}
		yield return E.Break;
	}

	IEnumerator OnInteractPropTree( IProp prop )
	{
		if (Prop("Tree").LookCount == 0){
			yield return E.HandleLookAt(Prop("Tree"));
		} else {
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("Not everything needs to have a use");
			yield return C.Plr.Say("Some things are just nice to look at");
			yield return C.Plr.WalkTo(Prop("Tree"));
			yield return C.Plr.Face(Prop("Tree"));
			yield return E.WaitSkip(2.0f);
			yield return C.Plr.Say("Nice");
			yield return E.WaitSkip();
			yield return C.Plr.Face(eFace.Down);
		}
		yield return E.Break;
	}

	public IEnumerator ScrapeShoe()
	{
			eFace _prevFacing = C.Plr.Facing;
			C.Plr.ActiveInventoryName = "";
			yield return C.Plr.Face(eFace.Down);
			yield return C.Player.Say("This should work to scrape this gum off!");
			yield return C.Elsa.PlayAnimation("ScrapeOffGum");
			C.Elsa.StopAnimation();
			C.Elsa.AnimPrefix="ScrapeOffGum";
			yield return C.Player.Say("Nice, ABC gum! Saving that for later!");
			yield return C.Elsa.PlayAnimation("PocketGum");
			C.Elsa.StopAnimation();
			C.Elsa.AnimPrefix="";
			I.StickyShoe.Remove();
			I.AbcGum.Add();
			RoomBusStop.Script.m_got_abc_gum = true;
			yield return C.Plr.Face(_prevFacing);
		yield return E.Break;
	}

	IEnumerator OnUseInvHotspotStreet( IHotspot hotspot, IInventory item )
	{
		if (item == I.AstronautCard){
			eFace _prevFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("If I throw my Honorary Junior Astronaut Card in the street, then I'd loose the HONOR...ary");
			yield return C.Plr.Say("Plus the edges are sharp enough to pop someone's tire");
			yield return C.Plr.Face(_prevFacing);
		}
		yield return E.Break;
	}

	IEnumerator OnUseInvPropTree( IProp prop, IInventory item )
	{
		if (item == I.AstronautCard){
		yield return C.Plr.WalkTo(Prop("Tree"));
		yield return C.Plr.Face(Prop("Tree"));
		yield return E.WaitSkip();
		yield return C.Plr.Face(eFace.Down);
		yield return E.WaitSkip();
		yield return C.Plr.Face(Prop("Tree"));
		yield return E.WaitSkip(1.0f);
		yield return C.Plr.Face(eFace.Down);
		yield return C.Plr.Say("Look, this card is sharp, but there's no way.");
		yield return C.Plr.Say("To chop this tree down I'll need something more appropriate");
		yield return E.WaitSkip();
		yield return C.Plr.Say("Like a herring");
		yield return E.WaitSkip();
		yield return C.Plr.Say("A red one");
		}
		yield return E.Break;
	}

	IEnumerator OnUseInvHotspotBusSchedule( IHotspot hotspot, IInventory item )
	{
		if (item == I.AstronautCard){
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("Maybe there's a completely nonsensical puzzle where I need to use my Astronaut Card on the CTA Bus Schedule.");
			yield return C.WalkToClicked();
			yield return C.FaceClicked();
			yield return E.WaitSkip();
			yield return C.Plr.Say("Let's see...");
			yield return E.WaitSkip();
			yield return C.Plr.Say("Wait for it...");
			yield return E.WaitSkip(1.5f);
			yield return C.Plr.Say("Still waiting...");
			yield return E.WaitSkip(1.5f);
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("Nope.");
			yield return C.Plr.Say("Was worth a shot though.");
			yield return C.Plr.Say("This IS a point-and-click adventure game after all, and there's always at least one horrible puzzle");
		
		}
		yield return E.Break;
	}

	IEnumerator OnUseInvHotspotLeftCrosswalk( IHotspot hotspot, IInventory item )
	{
		yield return E.HandleInventory(Hotspot("Street"),item);
		yield return E.Break;
	}
}
