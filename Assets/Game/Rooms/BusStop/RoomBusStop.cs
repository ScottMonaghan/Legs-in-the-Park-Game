using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBusStop : RoomScript<RoomBusStop>
{
	public enum eLocation {BusStop,Legs}
	public eLocation m_locationState = eLocation.BusStop;
	public int m_emotion_level = 0;
	public bool m_allowCrosswalk = false;
	public int m_numberOfBuses = 2;
	public int m_minutesLeft = 5;
	public bool m_dan_carnival_barking = false;
	public bool m_barrel_open = false;
	public bool m_dan_distracted = false;
	public bool m_price_of_grabber_revealed = false;
	public bool m_asked_dad_for_money = false;
	
	public IEnumerator SetEmotionLevel(int new_emotion_level)
    {
		m_emotion_level = new_emotion_level;
		
		if (!G.BusStopEmotionBar.Visible){
			G.BusStopEmotionBar.Show();
			G.BusStopEmotionBar.GetControl("Impatience").Visible = false;
			IImage meterBg = (IImage)(G.BusStopEmotionBar.GetControl("Bg"));
			meterBg.Alpha = 0;
			yield return meterBg.Fade(0,1,2);
			G.BusStopEmotionBar.GetControl("Impatience").Visible = true;
		}
		switch (m_emotion_level)
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
				m_allowCrosswalk = true;
				break;
			default:
				break;
		
		
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
		yield return C.Player.Face(_prevFacing);
		
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotLeftCrosswalk( IHotspot hotspot )
	{
		D.BusStopInteractCrosswalk.Start();
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
		yield return C.Player.Say("Now, that's a safe and legal way to cross the street.");
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
		yield return C.FaceClicked();
		yield return C.Player.Say("It's a forest of giant metal legs.");
		
		eFace _prevFacing = C.Plr.Facing;
		yield return E.WaitSkip();
		yield return C.Plr.Face(eFace.Down);
		yield return C.Player.Say("That's a totally normal thing to have in a city park right?");
		yield return E.WaitSkip();
		yield return C.Plr.Face(_prevFacing);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotLegs( IHotspot hotspot )
	{
		//Player: This is fine.
		//...
		//C.Plr.WalkTo(Hotspot("Legs"));
		//E.FadeOut();
		yield return C.Plr.Face(Hotspot("Legs"));
		D.InteractLegs.Start();
		yield return E.Break;
	}

	IEnumerator OnLookAtPropTree( IProp prop )
	{
		yield return C.FaceClicked();
		yield return C.Player.Say("That's a nice looking tree.");
		yield return E.Break;
	}

	IEnumerator OnInteractPropTree( IProp prop )
	{
		yield return C.FaceClicked();
		D.BusStopInteractTree.Start();
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
			yield return SetEmotionLevel(m_emotion_level+1);
		}
		if (m_emotion_level >=5){
			yield return FullEmotionHint();
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
		//C.Scott.AnimPrefix = "Phone";
		
		
	}

	IEnumerator OnLookAtCharacterScott( ICharacter character )
	{

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
		}else if (m_price_of_grabber_revealed == true){
			yield return C.Plr.Say("Can I have $10 to buy an investment-grade toothless Sue(tm) the T-Rex grabber?");
			yield return E.WaitSkip();
			yield return C.Scott.Say("Nope.");
			m_asked_dad_for_money = true;
		}else if (E.Is(eLocation.Legs)){
			yield return C.Scott.Say("Just gotta finish this work email. I'll join you in a minute.");
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
			yield return C.Display("Chapter 1: Elsa");
			yield return C.Narrator.Say("Today is Saturday,");
			yield return C.Narrator.Say("the first day of autumn in Chicago.");
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
		if (m_dan_carnival_barking && !C.Dan.Talking)
		{
			if (E.FirstOption(5, "danbarks"))
				C.Dan.SayBG("Don't miss the deal of a lifetime!");
			else if (E.NextOption)
				C.Dan.SayBG("Find your path to financial freedom today!");
			else if (E.NextOption)
				C.Dan.SayBG("Tired of being poor and/or having to work? I've got the answer for you!");
			else if (E.NextOption)
				C.Dan.SayBG("How does being rich sound to you? Talk to me to find out more!");
			else if (E.NextOption)
				C.Dan.SayBG("Sick of worrying about money? I've to the solution here!");
		}
		
		if (E.GetTimerExpired("DanDistracted")){
			C.Display("Timer Expired");
			m_dan_distracted = false;
			C.Dan.StopAnimation();
			C.Scott.StopAnimation();
			C.Dan.WalkToBG(Point("DanHome"));
			m_dan_carnival_barking = true;
		}
		
	}

	void OnPostRestore( int version )
	{
		switch (m_emotion_level)
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
		Audio.Play("Birds").FadeIn(5);
		Audio.PlayAmbientSound("CitySounds",5);
		Audio.PlayMusic("FoxTaleWaltz");
		
	}

	IEnumerator OnLookAtPropBarrel( IProp prop )
	{
		eFace _prevFacing = C.Plr.Facing;
		yield return C.Plr.Face(eFace.Down);
		if(!m_barrel_open){
			yield return C.Plr.Say("It's a museum trash bin.");
			yield return C.Plr.Say("That guy is standing weirdly close to it.");
		} else {
			yield return C.Plr.Say("It's filled to the top with dozens of one-of-a-kind misprinted Sue(tm) the T-Rex grabbers.");
		}
		yield return C.Plr.Face(_prevFacing);
		yield return E.Break;
	}

	IEnumerator OnInteractPropBarrel( IProp prop )
	{
			D.BusStopInteractBarrel.Start();
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
		D.BusStopDialogDan.Start();
		yield return E.Break;
	}
}