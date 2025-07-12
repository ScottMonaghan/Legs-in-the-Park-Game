using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomLegsFountain : RoomScript<RoomLegsFountain>
{
	public bool m_park_bench_movable = false;
	public int m_bench_position = 0;
	public bool m_sunbeam_on_bench = true;
	public bool m_card_on_bench = false;
	public bool m_card_on_ground = false;
	public bool m_card_in_sunbeam = false;
	public bool m_crow_with_card_in_sunbeam = false;
	public bool m_crow_on_bench = false;
	public bool m_crow_flew_away = false;
	public bool m_crow_guarding = true;
	public bool m_hope_retrieved = false;
	public bool m_crow_on_ground = false;
	public bool m_player_on_bench = false;
	public bool m_tried_grabber_from_ground = false;
	public bool m_crow_guarded = false;
	
	
	IEnumerator OnEnterRoomAfterFade()
	{
		if (R.FirstTimeVisited){
			//force-set inventory to allow play directly from room
		
			C.Plr.SetPosition(-140,-108);
			C.Plr.ClearInventory();
			I.AstronautCard.Add();
			Globals.m_lookedAtAstronautCard = true;
			I.AstronautCard.Description = "shiny, stiff, & sharp Astronaut card";
			((IQuestClickable)I.AstronautCard).Cursor = "Use";
			I.AbcGum.Add();
			Globals.m_lookedAtAbcGum = true;
			((IQuestClickable)I.AbcGum).Cursor = "Use";
			I.Grabber.Add();
			Globals.m_lookedAtGrabber = true;
			((IQuestClickable)I.Grabber).Cursor = "Use";
		}
		C.Narrator.Room = R.Current;
		C.Narrator.Position = new Vector2(0,0);
		Audio.StopMusic(1);
		Audio.PlayMusic("Frost Waltz",1);
		if (!Audio.IsPlaying("Birds")){
			 Audio.Play("Birds").FadeIn(5);
		}
		yield return C.Plr.WalkTo(Point("StartPoint"));
		yield return C.Plr.Say("Woh.");
		yield return C.Plr.Say("I never knew this was here.");
		yield return E.WaitSkip();
		yield return C.Plr.Say("This has got to be the place.");
		yield return C.Plr.Say("Looks like that statue is holding something sparkly.");
		
		yield return E.Break;
	}

	IEnumerator OnInteractPropLegsFountainSunbeamBench( IProp prop )
	{
		yield return E.HandleLookAt(Prop("LegsFountainSunbeamBench"));
	}

	IEnumerator OnLookAtPropLegsFountainSunbeamBench( IProp prop )
	{
		yield return C.Plr.Face(Prop("LegsFountainSunbeamBench"));
		yield return C.Plr.Say("A solitary sunbeam is breaking through the trees.");
		yield return E.WaitSkip();
		yield return C.Plr.Say("It's very poetic.");
		yield return E.WaitSkip();
		yield return C.Plr.Say("Or something.");
		yield return E.Break;
	}

	IEnumerator OnInteractPropLegsFountainParkBench( IProp prop )
	{
		if (!m_player_on_bench){
			D.LegsFountainBench.Start();
		} else {
			yield return E.WaitFor(ClimbDownBench);
		}
		yield return E.Break;
	}

	IEnumerator OnLookAtPropLegsFountainHope( IProp prop )
	{
		yield return C.Plr.Face(Prop("LegsFountainStatue"));
		if(!Region("GuardedRegion").ContainsCharacter(C.Player)){
			yield return C.Plr.Say("The statue is holding something. Let me see if I can get a closer look.");
		}
		if (!m_crow_guarding){
			yield return C.Plr.WalkTo(Prop("LegsFountainHope"));
			yield return C.Plr.Face(eFace.Right);
			yield return C.Plr.Say("The statue is holding something silvery and sparkly up there.");
			yield return C.Plr.Say("That's definitely gotta be the treasure!");
			if (m_bench_position < 2) {
				yield return C.Plr.Say("It's too high for me to reach though, and I don't think I can climb up there.");
			}
		} else {
			yield return C.Plr.WalkTo(Point("GuardedPoint"));
		}
		yield return E.Break;
	}

	IEnumerator OnEnterRegionGuardedRegion( IRegion region, ICharacter character )
	{
		if (m_crow_guarding){
			yield return E.WaitFor(CrowGuard);
		}
		yield return E.Break;
	}

	IEnumerator OnLookAtPropLegsFountainCrow( IProp prop )
	{
		yield return C.Plr.Face(Prop("LegsFountainCrow"));
		if (m_crow_guarding && !m_crow_guarded){
			yield return C.Plr.Say("Looks like crow up there.");
			yield return C.Plr.Say("Seems to be looking at the shiny thing.");
		} else if (m_crow_guarding){
			yield return C.Plr.Say("That bird is guarding that shiny thing.");
			yield return C.Plr.Say("I need to find a way to get by.");
		} else if (m_crow_on_bench){
			yield return C.Plr.Say("That crow is loving my shiny sunbeam card!");
			yield return C.Plr.Say("Now I should be able to check out the statue in peace.");
		} else if (!m_hope_retrieved) {
			yield return C.Plr.Say("Okay I think that dang bird is finally out of my way!");
			yield return C.Plr.Say("I hope I can get my Astronaut Card back when this is all over.");
		} else if (m_card_on_ground){
			yield return C.Plr.Say("Now I just to get my Astronaut Card back from this dang bird.");
		}
		yield return E.Break;
	}

	IEnumerator OnLookAtPropLegsFountainStatue( IProp prop )
	{
		if (!m_hope_retrieved){
			yield return E.HandleLookAt(Prop("LegsFountainHope"));
		} else {
			yield return C.Plr.Face(Prop("LegsFountainStatue"));
			yield return C.Plr.Say("You know?");
			yield return C.Plr.Say("There's something familiar about this statue.");
			yield return C.Plr.Say("But I can't quite put my thumb on it.");
		}
		yield return E.Break;
	}

	IEnumerator OnInteractPropLegsFountainCrow( IProp prop )
	{
		if (prop.Cursor == "Look"){
			yield return E.HandleLookAt(prop);
		}
		yield return E.Break;
	}

	IEnumerator OnInteractPropLegsFountainStatue( IProp prop )
	{
		if (prop.Cursor == "Look"){
			yield return E.HandleLookAt(prop);
		}
		yield return E.Break;
		
	}

	IEnumerator OnInteractPropLegsFountainHope( IProp prop )
	{
		if (prop.Cursor == "Look"){
			yield return E.HandleLookAt(prop);
		}
		yield return E.Break;
	}

	IEnumerator OnUseInvPropLegsFountainSunbeamBench( IProp prop, IInventory item )
	{
		Vector2 card_bench_position = new Vector2(0,0);
		Vector2 card_ground_position = new Vector2(6,-12);
		int card_bench_baseline = -99;
		int card_ground_baseline = -49;
		
		if (item == I.AstronautCard){
			Vector2 target_crow_position = Point("CrowStatuePoint");
			string place_animation = "";
			if (m_sunbeam_on_bench){
				target_crow_position = Point("CrowBenchPoint");
				m_card_on_bench = true;
				m_card_on_ground = false;
				m_crow_on_bench = true;
				place_animation="PlaceCardStanding";
				Prop("LegsFountainCard").Position = card_bench_position;
				Prop("LegsFountainCard").Baseline = card_bench_baseline;
			} else {
				m_crow_on_ground = true;
				target_crow_position = Point("CrowGrassPoint");
				m_card_on_ground = true;
				m_card_on_bench = false;
				m_crow_on_bench = false;
				Prop("LegsFountainCard").Baseline += 50;
				Prop("LegsFountainSunbeamBenchMotes").Baseline += 50;
				Prop("LegsFountainSunbeamBench").Baseline += 50;
				place_animation="PlaceCardCrouching";
				Prop("LegsFountainCard").Position = card_ground_position;
				Prop("LegsFountainCard").Baseline = card_ground_baseline;
			}
		
			yield return C.Plr.WalkTo(Prop("LegsFountainCard"),!m_sunbeam_on_bench);
			//E.FadeOut();
			//Narrator: (PLACEHOLDER)\n Elsa places the shiny astronaut card in the sunbeam. The crow flies down and lands next to it.
			yield return C.Plr.Face(eFace.Up);
			yield return C.Plr.Say("I'll just put this down right here.");
			yield return C.Plr.PlayAnimation(place_animation);
			yield return C.Plr.Face(eFace.Right);
			I.Active = null;
			C.Plr.RemoveInventory(I.AstronautCard);
			m_crow_guarding = false;
			m_card_in_sunbeam = true;
			Prop("LegsFountainCard").Animation = "LegsFountainCardShine";
			Prop("LegsFountainCard").Visible = true;
			Prop("LegsFountainCard").Clickable = true;
			yield return C.Plr.WalkTo(C.Plr.Position + new Vector2(-20,0));
			yield return C.Plr.Face(eFace.Right);
			yield return E.WaitSkip();
			yield return E.WaitFor( HandleCrowPosition );
			/*Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingLeft");
			yield return Prop("LegsFountainCrow").MoveTo(target_crow_position, 200,eEaseCurve.InOutSmooth);
			Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrow");
			m_crow_with_card_in_sunbeam = true;
			*/
		
			//E.FadeIn();
			yield return C.Plr.Say("Wow! that crow looks totally mesmerized.");
			yield return C.Plr.Say("I don't think it will bother me as long as it's distracted by my trusty Astronaut Card.");
		
		}
		
		if ((item==I.Grabber || item == I.GummyGrabber) && m_card_on_bench){
			yield return E.HandleInventory(Prop("LegsFountainCard"),item);
		}
		yield return E.Break;
	}

	IEnumerator OnUseInvPropLegsFountainHope( IProp prop, IInventory item )
	{
		if (item == I.AstronautCard && m_crow_guarding){
			yield return E.HandleInventory(Prop("LegsFountainCrow"), item);
		}
		if (item == I.Grabber){
			yield return C.Plr.WalkTo(Point("PointGroundReach"));
			if (!m_crow_guarding){
				if (!m_tried_grabber_from_ground && !m_player_on_bench){
					m_tried_grabber_from_ground = true;
					yield return C.Plr.Face(eFace.Down);
					yield return C.Plr.Say("AH HA! I was investing for the future...");
					yield return C.Plr.Say("AND THE FUTURE IS NOW!");
					yield return C.Plr.Face(eFace.UpRight);
					yield return E.WaitSkip(0.25f);
					Prop("GrabHopeNoGum").SetPosition(Prop("GrabHopeNoGum").Position[0]-24, Prop("GrabHopeNoGum").Position[1] - 52);
					C.Plr.Visible = false;
					Prop("GrabHopeNoGum").Visible = true;
					yield return Prop("GrabHopeNoGum").PlayAnimation("TakeOutGrabber");
					yield return Prop("GrabHopeNoGum").PlayAnimation("GrabHopeNoGum");
					yield return Prop("GrabHopeNoGum").PlayAnimation("GrabHopeNoGum");
					yield return Prop("GrabHopeNoGum").PlayAnimation("GrabHopeNoGum");
					yield return Prop("GrabHopeNoGum").PlayAnimation("PutAwayGrabber");
					Prop("GrabHopeNoGum").Visible = false;
					Prop("GrabHopeNoGum").SetPosition(Prop("GrabHopeNoGum").Position[0]+24, Prop("GrabHopeNoGum").Position[1] + 52);
					C.Plr.Visible = true;
					yield return C.Plr.Face(eFace.Down);
					yield return C.Plr.Say("DANG IT!");
					yield return C.Plr.Say("I really thought that was going to work.");
					yield return C.Plr.Say("Looks like I still need to get closer.");
					C.Plr.ActiveInventory = null;
					m_park_bench_movable = true;
				} else if (!m_player_on_bench){
					yield return C.Plr.Say("I need to get higher to reach.");
				} else {
					yield return C.Plr.Face(eFace.Down);
					yield return C.Plr.Say("and NOW I can get full return on my savvy investment!");
					yield return C.Plr.Face(eFace.UpRight);
					C.Plr.Visible = false;
					Prop("GrabHopeNoGum").Visible = true;
					yield return Prop("GrabHopeNoGum").PlayAnimation("TakeOutGrabber");
					yield return Prop("GrabHopeNoGum").PlayAnimation("GrabHopeNoGum");
					yield return Prop("GrabHopeNoGum").PlayAnimation("GrabHopeNoGum");
					yield return Prop("GrabHopeNoGum").PlayAnimation("GrabHopeNoGum");
					yield return Prop("GrabHopeNoGum").PlayAnimation("PutAwayGrabber");
					Prop("GrabHopeNoGum").Visible = false;
					C.Plr.Visible = true;
					/*
					yield return E.FadeOut();
					yield return C.Narrator.Say("(PLACEHOLDER)\n");
					yield return C.Narrator.Say("Elsa climbs up on the park bench...");
					yield return C.Narrator.Say("She takes out the Toothless Sue(tm) the T-Rex Grabber...");
					yield return C.Narrator.Say("She stands on her tippy toes and lifts its lizardy maw up to the shiny treasure...");
					yield return C.Narrator.Say("AND...");
					yield return C.Narrator.Say("IT REACHES!!");
					yield return C.Narrator.Say("AND...");
					yield return C.Narrator.Say("The grabber slides off like a claw machine at an arcade.");
					yield return E.FadeIn();
					*/
					Globals.m_ready_for_gummy_grabber = true;
					C.Plr.ActiveInventory = null;
					yield return C.Plr.Face(eFace.Down);
					yield return C.Plr.Say("Double DANG IT!");
					yield return C.Plr.Say("Without teeth, this grabber's got no grip!!");
					yield return C.Plr.Say("I'm going to need to find a way to make this work!");
					yield return C.Plr.Face(eFace.Right);
				}
			}
		}
		if (item == I.GummyGrabber){
			if (!m_crow_guarding && !m_hope_retrieved && m_player_on_bench){
				yield return C.Plr.Face(eFace.Down);
					yield return C.Plr.Say("This better be it or I'm getting my money back!");
					yield return C.Plr.Face(eFace.UpRight);
					C.Plr.Visible = false;
					Prop("GrabHopeNoGum").Visible = true;
					yield return Prop("GrabHopeNoGum").PlayAnimation("TakeOutGummyGrabber");
					Prop("LegsFountainHope").Visible=false;
					Prop("LegsFountainHope").Clickable = false;
					yield return Prop("GrabHopeNoGum").PlayAnimation("GrabHopeGum");
					Prop("GrabHopeNoGum").Visible = false;
					C.Plr.Visible = true;
					m_hope_retrieved = true;
					yield return C.Plr.Face(eFace.Down);
					yield return C.Plr.Say("FINALLY!!");
					yield return C.Plr.Say("I got it!");
					yield return E.FadeOut();
					Prop("HopeZoom").Visible = true;
					Prop("HopeZoom").Clickable = true;
					yield return E.FadeIn();
					yield return C.Plr.Say("It's a beautiful silver leaf.");
					yield return C.Plr.Say("It's warmer to the touch than I expected.");
					yield return E.WaitSkip();
					yield return E.FadeOut();
					Prop("HopeZoom").Visible = false;
					Prop("HopeZoom").Clickable = false;
					yield return E.FadeIn();
					yield return C.Plr.Say("Now I just need to get my Astronaut Card back and I can head back to Robin.");
					yield return C.Plr.Say("This was fun!");
					((IQuestClickable)I.SilverLeaf).Cursor = "Look";
					C.Plr.AddInventory(I.SilverLeaf);
					C.Plr.ActiveInventory = null;
		
			} else if (!m_player_on_bench){
				yield return C.Plr.Say("I need to get higher to reach.");
			}
		}
		yield return E.Break;
	}

	IEnumerator OnUseInvPropLegsFountainStatue( IProp prop, IInventory item )
	{
		if (item == I.Grabber || item == I.GummyGrabber){
			yield return E.HandleInventory(Prop("LegsFountainHope"), item);
		}
		if (item == I.AstronautCard && m_crow_guarding){
			yield return E.HandleInventory(Prop("LegsFountainCrow"), item);
		}
		if (item == I.AbcGum){
			yield return E.WaitFor(VandalizeWithGum);
		}
		yield return E.Break;
	}

	IEnumerator OnUseInvPropLegsFountainParkBench( IProp prop, IInventory item )
	{
		if(item == I.AstronautCard){
			if (m_sunbeam_on_bench){
				yield return E.HandleInventory(Prop("LegsFountainSunbeamBench"), item);
			} else {
				yield return C.Plr.Say("There's no reason to put my card down outside of the sunbeam.");
			}
		}
		if (item == I.AbcGum){
			/*
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("Really!?");
			yield return E.WaitSkip();
			yield return E.WaitSkip();
			yield return C.Plr.Say("I'm very disappointed.");
			*/
			yield return E.WaitFor(VandalizeWithGum);
		}
		if ((item==I.Grabber || item == I.GummyGrabber) && m_card_on_bench){
			yield return E.HandleInventory(Prop("LegsFountainCard"),item);
		}
		yield return E.Break;
	}

	public IEnumerator BenchForward()
	{
		yield return HandleBenchMove(1);
		/*
		if (m_bench_position == 1 && m_crow_guarding){
			yield return C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position,Point("PushPoint")[1],false);
			yield return C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position,Point("PushPoint")[1],true);
			yield return C.Plr.Face(eFace.Right);
			C.Plr.Visible = false;
			Prop("PushPlaceholder").Visible = true;
			yield return Prop("PushPlaceholder").PlayAnimation("PushBenchIn");
			Prop("PushPlaceholder").PlayAnimationBG("PushBenchLoop");
			yield return E.WaitSkip(0.25f);
			C.Plr.Visible = true;
			Prop("PushPlaceholder").Visible = false;
			yield return E.WaitFor( CrowGuard );
		} else if (
			(m_bench_position >= -2 && m_bench_position < 1)
			|| (m_bench_position == 1 && !m_crow_guarding)
		){
			int move_distance = 50;
			yield return C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position,Point("PushPoint")[1],false);
			yield return C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position,Point("PushPoint")[1],true);
			m_bench_position += 1;
			yield return C.Plr.Face(eFace.Right);
			if (m_bench_position == 0 && m_crow_on_ground){
				yield return C.Plr.Say("I'll push around the sunbeam so I don't get any more trouble from that crow.");
				m_bench_position = 2; //push all the way to save time.
				move_distance = 150;
			}
			C.Plr.Visible = false;
			Prop("PushPlaceholder").Visible = true;
			yield return Prop("PushPlaceholder").PlayAnimation("PushBenchIn");
			Prop("PushPlaceholder").PlayAnimationBG("PushBenchLoop");
		
			if (m_bench_position == 0 & !m_crow_on_ground) {
				m_sunbeam_on_bench = true;
			} else {
				m_sunbeam_on_bench = false;
			}
		
			Prop("LegsFountainParkBench").MoveToBG(new Vector2(50 * m_bench_position, Prop("LegsFountainParkBench").Position[1]), 25, eEaseCurve.InSmooth);
			if (m_card_on_bench){
				Prop("LegsFountainCard").MoveToBG(new Vector2(50 * m_bench_position, Prop("LegsFountainParkBench").Position[1]), 25, eEaseCurve.InSmooth);
				Prop("LegsFountainCard").Animation = "LegsFountainCard";
			}
			if (m_crow_on_bench){
				Prop("LegsFountainCrow").MoveToBG(new Vector2(Prop("LegsFountainCrow").Position[0] + move_distance, Prop("LegsFountainCrow").Position[1]), 25, eEaseCurve.InSmooth);
			}
			yield return Prop("PushPlaceholder").MoveTo(Prop("PushPlaceholder").Position[0] + move_distance, Prop("PushPlaceholder").Position[1], 25, eEaseCurve.InSmooth);
			yield return Prop("PushPlaceholder").PlayAnimation("PushBenchOut");
			if (m_crow_on_bench)
			{
				//Narrator: As soon as the bench is pushed out of the sunbeam the bird returns to its perch on the statute.
				//Narrator: Just like before, it won't let Elsa get any closer.
				Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingRight");
				yield return Prop("LegsFountainCrow").MoveTo(Point("CrowStatuePoint"), 200, eEaseCurve.InOutSmooth);
				Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrow");
				m_crow_on_bench = false;
				m_crow_with_card_in_sunbeam = false;
				m_crow_guarding = true;
			}
			if (m_card_on_bench) {
				Prop("LegsFountainCard").Position = Prop("LegsFountainParkBench").Position;
				if (m_sunbeam_on_bench){
					m_crow_on_bench = true;
					m_crow_guarding = false;
					m_crow_with_card_in_sunbeam = true;
					Prop("LegsFountainCard").Animation = "LegsFountainCardShine";
					Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingLeft");
					yield return Prop("LegsFountainCrow").MoveTo(Point("CrowBenchPoint"),200);
					Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrow");
				} else {
					Prop("LegsFountainCard").Animation = "LegsFountainCard";
				}
			}
			C.Plr.SetPosition(C.Plr.Position[0] + move_distance, C.Plr.Position[1]);
			C.Plr.Visible = true;
			Prop("PushPlaceholder").Visible = false;
			yield return C.Plr.WalkTo(C.Plr.Position[0], C.Plr.Position[1]-15);
			if (m_bench_position == 2) {
				Prop("LegsFountainParkBench").Baseline = 50;
				yield return C.Plr.Say("Phew!");
				yield return C.Plr.Say("I'm glad I ate my Wheaties this morning!");
			}
			yield return C.Plr.Face(eFace.Right);
		}
		*/
		yield return E.Break;
	}

	public IEnumerator BenchBack()
	{
		yield return HandleBenchMove(-1);
		/*
		if (m_bench_position > -2){
			yield return C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position, Point("PushPoint")[1], false);
			yield return C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position, Point("PushPoint")[1], true);
			m_bench_position -= 1;
			yield return C.Plr.Face(eFace.Right);
			C.Plr.Visible = false;
			Prop("PushPlaceholder").Visible = true;
			yield return Prop("PushPlaceholder").PlayAnimation("PushBenchIn");
			Prop("PushPlaceholder").PlayAnimationBG("PullBenchLoop");
			if (m_bench_position == 0)
			{
				m_sunbeam_on_bench = true;
			}
			else
			{
				m_sunbeam_on_bench = false;
			}
		
			Prop("LegsFountainParkBench").MoveToBG(new Vector2(50 * m_bench_position, Prop("LegsFountainParkBench").Position[1]), 25, eEaseCurve.InSmooth);
			if (m_card_on_bench)
			{
				Prop("LegsFountainCard").MoveToBG(new Vector2(50 * m_bench_position, Prop("LegsFountainParkBench").Position[1]), 25, eEaseCurve.InSmooth);
				Prop("LegsFountainCard").Animation = "LegsFountainCard";
			}
			if (m_crow_on_bench){
				Prop("LegsFountainCrow").MoveToBG(new Vector2(50 * m_bench_position + Prop("LegsFountainCrow").Position[0], Prop("LegsFountainCrow").Position[1]), 25, eEaseCurve.InSmooth);
			}
			yield return Prop("PushPlaceholder").MoveTo(Prop("PushPlaceholder").Position[0] - 50, Prop("PushPlaceholder").Position[1], 25, eEaseCurve.InSmooth);
			yield return Prop("PushPlaceholder").PlayAnimation("PushBenchOut");
			if (m_card_on_bench) {
				Prop("LegsFountainCard").Position = Prop("LegsFountainParkBench").Position;
				if (m_sunbeam_on_bench){
					m_crow_on_bench = true;
					m_crow_guarding = false;
					m_crow_with_card_in_sunbeam = true;
					Prop("LegsFountainCard").Animation = "LegsFountainCardShine";
					Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingLeft");
					yield return Prop("LegsFountainCrow").MoveTo(Point("CrowBenchPoint"),200);
					Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrow");
				} else {
					Prop("LegsFountainCard").Animation = "LegsFountainCard";
						}
				}
			if (m_crow_on_bench && !m_sunbeam_on_bench)
			{
				//Narrator: As soon as the bench is pushed out of the sunbeam the bird returns to its perch on the statute.
				//Narrator: Just like before, it won't let Elsa get any closer.
				Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingRight");
				yield return Prop("LegsFountainCrow").MoveTo(Point("CrowStatuePoint"), 200, eEaseCurve.InOutSmooth);
				Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrow");
				m_crow_on_bench = false;
				m_crow_with_card_in_sunbeam = false;
				m_crow_guarding = true;
			}
			C.Plr.SetPosition(C.Plr.Position[0] - 50, C.Plr.Position[1]);
			C.Plr.Visible = true;
			Prop("PushPlaceholder").Visible = false;
			yield return C.Plr.WalkTo(C.Plr.Position[0], C.Plr.Position[1]-15);
			yield return C.Plr.Face(eFace.Right);
		} else {
			yield return C.Plr.Say("I can't move the bench any further back.");
		}
		*/
		
		//if (m_bench_position > -2){
			//	yield return E.FadeOut();
			//	yield return C.Narrator.Say("(PLACEHOLDER)\n Elsa heaves the bench backward...");
			//	m_bench_position -= 1;
			//	Prop("LegsFountainParkBench").Position = new Vector2(50 * m_bench_position, 0);
			//	Prop("PushPlaceholder").SetPosition(Prop("PushPlaceholder").Position[0] - 50, Prop("PushPlaceholder").Position[1]);
			//} else {
			//	yield return C.Plr.Say("I don't want to move the bench any further back than that.");
			//}
		
			//if (m_crow_on_bench){
			//	m_crow_on_bench = false;
			//	m_crow_with_card_in_sunbeam = false;
			//	m_crow_guarding = true;
			//	Prop("LegsFountainCrow").Position = new Vector2(0,0);
			//}
		
			//if (m_bench_position == 0) {
			//	m_sunbeam_on_bench = true;
			//} else {
			//	m_sunbeam_on_bench = false;
			//}
		
			//if (m_card_on_bench) {
			//	Prop("LegsFountainCard").Position = Prop("LegsFountainParkBench").Position;
			//	if (m_sunbeam_on_bench){
			//		m_crow_on_bench = true;
			//		m_crow_guarding = false;
			//		m_crow_with_card_in_sunbeam = true;
			//		Prop("LegsFountainCrow").Position = Point("CrowBenchPoint");
			//		Prop("LegsFountainCard").Animation = "LegsFountainCardShine";
			//	} else {
			//		Prop("LegsFountainCard").Animation = "LegsFountainCard";
			//	}
			//}
			//yield return E.FadeIn();
			yield return E.Break;
	}

	public IEnumerator CrowGuard()
	{
		Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingLeft");
		Prop("LegsFountainCrow").MoveToBG(
		 C.Plr.Position + new Vector2(30,80),
		 200,
		 eEaseCurve.InOutSmooth
		);
		C.Plr.PlayAnimationBG("WalkBackFromCrow");
		yield return C.Plr.MoveTo(C.Plr.Position - new Vector2(60,0));//Point("ForceBackPoint"), true);
		C.Plr.AnimPrefix = "NoSmile";
		C.Plr.StopAnimation();
		Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingRight");
		yield return Prop("LegsFountainCrow").MoveTo(
			Point("CrowStatuePoint"),
			200,
			eEaseCurve.InOutSmooth
		);
		Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrow");
		yield return C.Plr.Face(eFace.Down);
		yield return C.Plr.Say("That bird isn't going to let me get any closer.");
		C.Plr.AnimPrefix = "";
		yield return C.Plr.Face(eFace.Right);
		m_crow_guarded = true;
		yield return E.Break;
	}

	IEnumerator OnInteractPropLegsFountainCard( IProp prop )
	{
		if (m_crow_with_card_in_sunbeam){
			yield return C.Plr.Say("I can't pick up the card with the crow there.");
		} else if (m_player_on_bench){
			yield return C.Plr.Say("I should climb down first");
			yield return E.HandleInteract(Prop("LegsFountainParkBench"));
		} else {
			yield return C.Plr.WalkTo(Prop("LegsFountainCard"));
			yield return C.Plr.Face(eFace.Up);
			yield return C.Plr.Say("I'll just put this back in my pocket for now.");
			Prop("LegsFountainCard").Visible = false;
			Prop("LegsFountainCard").Clickable = false;
			if (m_card_on_bench){
				yield return C.Plr.PlayAnimation("GetCardStanding");
			} else {
				yield return C.Plr.PlayAnimation("GetCardCrouching");
			}
			Prop("LegsFountainCard").Position = new Vector2(0,0);
			C.Plr.AddInventory(I.AstronautCard);
			m_card_on_bench = false;
			m_card_on_ground = false;
			m_card_in_sunbeam = false;
			m_crow_with_card_in_sunbeam = false;
			//reset card position
			yield return C.Plr.WalkTo(C.Plr.Position + new Vector2(-20,0));
			if (m_hope_retrieved){
				yield return C.Plr.Face(eFace.Down);
				yield return C.Plr.Say("I think I've got everything I need.");
				yield return C.Plr.Say("Now to head back to Robin and show him the treasure!");
				yield return E.HandleInteract(Hotspot("ExitLeft"));
			}
		}
		yield return E.Break;
	}

	IEnumerator OnUseInvPropLegsFountainCrow( IProp prop, IInventory item )
	{
		if (item == I.AstronautCard && m_crow_guarding){
			yield return C.Plr.Say("Maybe I can distract the crow with my shiny Astronaut Card.");
			yield return C.Plr.WalkTo(Point("GuardedPoint") - new Vector2(20,0));
			yield return E.WaitFor( CrowGuard );
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("It was worth a try.");
			yield return C.Plr.Say("My Astronaut Card is pretty shiny, but not as shiny as whatever is up there.");
			yield return C.Plr.Say("I'm going to need something shinier to distract that bird.");
			yield return C.Plr.Face(eFace.Right);
		}
		if (item == I.AbcGum){
			eFace _prevFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("I'm not feeding gum to the crow.");
			yield return C.Plr.Say("It's probably not good for it.");
			yield return C.Plr.Face(_prevFacing);
		}
		if (item == I.SilverLeaf){
			yield return C.Plr.Say("I just worked so hard to get this!");
			yield return C.Plr.Say("No way I'm giving it back to the crow!");
		}
		if (item == I.Grabber || item == I.GummyGrabber){
			yield return C.Plr.Say("Yes, the bird is annoying.");
			yield return C.Plr.Say("But I'm not going to attack it with the grabber.");
			yield return C.Plr.Say("That just wouldn't be right.");
		}
		
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotExitLeft( IHotspot hotspot )
	{
		if (!m_hope_retrieved){
			eFace _lastFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("I don't want to leave without the treasure!");
			yield return C.Plr.Face(_lastFacing);
		} else if (!C.Plr.HasInventory(I.AstronautCard)){
			eFace _lastFacing = C.Plr.Facing;
			yield return C.Plr.Face(eFace.Down);
			yield return C.Plr.Say("I can't leave without my Honorary Junior Astronaut Card!");
			yield return C.Plr.Say("That thing has gotten me out more than one jam!");
			yield return C.Plr.Face(_lastFacing);
		}
		
		else {
		yield return C.Plr.WalkTo(Point("LeavePoint"));
		yield return E.FadeOut();
		if (C.Robin.Room == null){
			C.Robin.Room = R.Legs1;
		}
		E.Set(eLegsProgress.GotHope);
		E.Set(eExitDirection.Left);
		R.Legs1.GetProp("Legs01Path").Animation = "Legs01PathEve";
		R.Legs2.GetProp("Legs01Path").Animation = "Legs01PathEve";
		yield return C.Plr.ChangeRoom(C.Robin.Room);
		/*
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
		*/
		}
		yield return E.Break;
	}

	void OnEnterRegionBGShadows( IRegion region, ICharacter character )
	{
	}

	IEnumerator OnInteractPropPushPlaceholder( IProp prop )
	{
		yield return C.Plr.WalkTo(Point("PushPoint"),true);
		yield return C.Plr.Face(eFace.Right);
		C.Plr.Visible = false;
		Prop("PushPlaceholder").Visible = true;
		yield return Prop("PushPlaceholder").PlayAnimation("PushBenchIn");
		Prop("PushPlaceholder").PlayAnimationBG("PushBenchLoop");
		Prop("LegsFountainParkBench").MoveToBG(new Vector2(Prop("LegsFountainParkBench").Position[0] + 50, Prop("LegsFountainParkBench").Position[1]), 25, eEaseCurve.InSmooth);
		yield return Prop("PushPlaceholder").MoveTo(Prop("PushPlaceholder").Position[0] + 50, Prop("PushPlaceholder").Position[1], 25, eEaseCurve.InSmooth);
		yield return Prop("PushPlaceholder").PlayAnimation("PushBenchOut");
		C.Plr.SetPosition(C.Plr.Position[0] + 50, C.Plr.Position[1]);
		C.Plr.Visible = true;
		Prop("PushPlaceholder").Visible = false;
		yield return C.Plr.WalkTo(C.Plr.Position[0], C.Plr.Position[1]-15);
		yield return E.Break;
	}

	IEnumerator OnWalkTo()
	{
		if (m_player_on_bench){
			yield return C.Plr.Say("I should climb down first");
			yield return E.WaitFor(ClimbDownBench);
		}
		yield return E.Break;
	}

	IEnumerator OnLookAtPropLegsFountainParkBench( IProp prop )
	{
		yield return C.Plr.WalkTo(Prop("LegsFountainParkBench"));
		yield return C.Plr.Face(Prop("LegsFountainParkBench"));
		yield return C.Plr.Say("It's a park bench.");
		yield return C.Plr.Say("Looks heavy.");
		yield return E.Break;
	}

	IEnumerator OnLookAtPropLegsFountainCard( IProp prop )
	{
		yield return C.Plr.WalkTo(Prop("LegsFountainCard"));
		yield return C.Plr.Face(Prop("LegsFountainCard"));
		if (m_card_in_sunbeam){
			yield return C.Plr.Say("In the sunbeam it's pretty dang sparkly.");
			yield return C.Plr.Say("The crow seems to love it!");
		} else if (m_card_on_bench){
			yield return C.Plr.Say("It's my astronaut card, on a bench.");
			yield return C.Plr.Say("What else can I say?");
		} else {
			yield return C.Plr.Say("Looks like the sunbeam faded and the crow left right at the exact time I got the silver leaf.");
			yield return C.Plr.Say("Probably just a coincidence.");
		}
		yield return E.Break;
	}

	public IEnumerator HandleBenchMove(int bench_position_change)
	{
		int old_bench_position = m_bench_position;
		int target_bench_position = m_bench_position + bench_position_change;
		int move_distance = bench_position_change * 50;
		
		if (old_bench_position == 1 && m_crow_guarding && bench_position_change > 0){
			// if we attempt to push from position 1 while crow is guarding, we're gonna have a bad time
			yield return C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position,Point("PushPoint")[1],false);
			yield return C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position,Point("PushPoint")[1],true);
			yield return C.Plr.Face(eFace.Right);
			C.Plr.Visible = false;
			Prop("PushPlaceholder").Visible = true;
			yield return Prop("PushPlaceholder").PlayAnimation("PushBenchIn");
			Prop("PushPlaceholder").PlayAnimationBG("PushBenchLoop");
			yield return E.WaitSkip(0.25f);
			C.Plr.Visible = true;
			Prop("PushPlaceholder").Visible = false;
			yield return E.WaitFor( CrowGuard );
		} else if (
			(target_bench_position >= -2 && target_bench_position <= 2)
		){
			yield return C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position,Point("PushPoint")[1],false);
			//C.Plr.WalkTo(Point("PushPoint")[0] + 50 * m_bench_position,Point("PushPoint")[1],true);
		
		
			yield return C.Plr.Face(eFace.Right);
			C.Plr.Visible = false;
			Prop("PushPlaceholder").Visible = true;
			yield return Prop("PushPlaceholder").PlayAnimation("PushBenchIn");
			if (bench_position_change > 0){
				Prop("PushPlaceholder").PlayAnimationBG("PushBenchLoop");
			} else {
				Prop("PushPlaceholder").PlayAnimationBG("PullBenchLoop");
			}
		
			Prop("LegsFountainParkBench").MoveToBG(new Vector2(Prop("LegsFountainParkBench").Position[0] + move_distance, Prop("LegsFountainParkBench").Position[1]), 25, eEaseCurve.InSmooth);
			if (m_card_on_bench){
				Prop("LegsFountainCard").MoveToBG(new Vector2(Prop("LegsFountainCard").Position[0] + move_distance, Prop("LegsFountainParkBench").Position[1]), 25, eEaseCurve.InSmooth);
				Prop("LegsFountainCard").Animation = "LegsFountainCard";
			}
		
			//if the crow is on the bench then move with the bench
			if (m_crow_on_bench){
				Prop("LegsFountainCrow").MoveToBG(new Vector2(Prop("LegsFountainCrow").Position[0] + move_distance, Prop("LegsFountainCrow").Position[1]), 25, eEaseCurve.InSmooth);
			}
		
			yield return Prop("PushPlaceholder").MoveTo(Prop("PushPlaceholder").Position[0] + move_distance, Prop("PushPlaceholder").Position[1], 25, eEaseCurve.InSmooth);
			yield return Prop("PushPlaceholder").PlayAnimation("PushBenchOut");
		
			//update bench position after all initial movement is complete
			m_bench_position = target_bench_position;
		
			if (m_bench_position == 0) {
				m_sunbeam_on_bench = true;
			} else {
				m_sunbeam_on_bench = false;
			}
		
			if (m_sunbeam_on_bench && m_card_on_bench) {
				m_card_in_sunbeam = true;
			} else if (!m_sunbeam_on_bench && m_card_on_ground){
				m_card_in_sunbeam = true;
			} else {
				m_card_in_sunbeam = false;
			}
		
			if (m_card_in_sunbeam){
				Prop("LegsFountainCard").Animation = "LegsFountainCardShine";
			} else {
				Prop("LegsFountainCard").Animation = "LegsFountainCard";
			}
		
			yield return E.WaitFor( HandleCrowPosition );
		
			C.Plr.SetPosition(C.Plr.Position[0] + move_distance, C.Plr.Position[1]);
			C.Plr.Visible = true;
			Prop("PushPlaceholder").Visible = false;
			if (m_bench_position == 2) {
				//Prop("LegsFountainParkBench").Baseline = 50;
				yield return C.Plr.Say("Phew!");
				yield return C.Plr.Say("I'm glad I ate my Wheaties this morning!");
			}
			yield return C.Plr.Face(eFace.Right);
			Region("BenchRegion_0").Walkable = true;
			Region("BenchRegion_1").Walkable = true;
			Region("BenchRegion_2").Walkable = true;
			Region("BenchRegion_neg1").Walkable = true;
			Region("BenchRegion_neg2").Walkable = true;
			yield return E.WaitSkip(0.25f);
			switch(m_bench_position){
				case -2:
					Region("BenchRegion_neg2").Walkable=false;
				break;
				case -1:
					Region("BenchRegion_neg1").Walkable=false;
				break;
				case 0:
					Region("BenchRegion_0").Walkable=false;
				break;
				case 1:
					Region("BenchRegion_1").Walkable=false;
				break;
				case 2:
					Region("BenchRegion_2").Walkable=false;
				break;
			}
		
		} else {
			yield return C.Plr.Say("I can't move it any farther than this.");
		}
		yield return E.Break;
	}

	void Update()
	{
		int max_position = 5;
		int min_position = -15;
		float park_bench_x = Prop("LegsFountainParkBench").Position[0];
		int base_baseline = -100;
		int shifted_baseline = -50;
		
		if (
			park_bench_x >= min_position && park_bench_x <= max_position
			&& Prop("LegsFountainSunbeamBench").Animation != "LegsFountainSunbeamBench"
		){
			Prop("LegsFountainSunbeamBench").Animation ="LegsFountainSunbeamBench";
			//Prop("LegsFountainSunbeamBench").Baseline = base_baseline;
			Prop("LegsFountainSunbeamBenchMotes").Animation="LegsFountainSunbeamBenchMotes";
			Prop("LegsFountainSunbeamBenchMotes").Baseline = base_baseline - 1;
			Prop("LegsFountainSunbeamGround").Clickable = false;
		
		} else if (
			(park_bench_x < min_position || park_bench_x > max_position)
			&& Prop("LegsFountainSunbeamBench").Animation != "LegsFountainSunbeamGround"
			){
			Prop("LegsFountainSunbeamBench").Animation ="LegsFountainSunbeamGround";
			//Prop("LegsFountainSunbeamBench").Baseline = shifted_baseline;
			Prop("LegsFountainSunbeamBenchMotes").Animation=("LegsFountainSunbeamGroundMotes");
			//Prop("LegsFountainSunbeamBenchMotes").Baseline = shifted_baseline - 1;
			Prop("LegsFountainSunbeamGround").Clickable = true;
		}
		
		if (park_bench_x > max_position){
			Prop("LegsFountainSunbeamBench").Baseline = shifted_baseline;
			Prop("LegsFountainSunbeamBenchMotes").Baseline = shifted_baseline - 1;
		} else {
			Prop("LegsFountainSunbeamBench").Baseline = base_baseline;
			Prop("LegsFountainSunbeamBenchMotes").Baseline = base_baseline - 1;
		}
		
	}

	IEnumerator OnLookAtPropLegsFountainSunbeamGround( IProp prop )
	{
		yield return E.HandleLookAt(Prop("LegsFountainSunbeamBench"));
		
		yield return E.Break;
	}

	IEnumerator OnLookAtPropPushPlaceholder( IProp prop )
	{

		yield return E.Break;
	}

	IEnumerator OnInteractPropLegsFountainSunbeamGround( IProp prop )
	{
		yield return E.HandleInteract(Prop("LegsFountainSunbeamBench"));
		yield return E.Break;
	}

	IEnumerator OnUseInvPropLegsFountainSunbeamGround( IProp prop, IInventory item )
	{
		yield return E.HandleInventory(Prop("LegsFountainSunbeamBench"),item);
		yield return E.Break;
	}

	public IEnumerator HandleCrowPosition()
	{
		if (!m_crow_flew_away){
			if (m_card_in_sunbeam){
					Vector2 crow_destination = new Vector2(0,0);
					if (m_card_on_bench){
						crow_destination = Point("CrowBenchPoint");
					} else {
					crow_destination = Point("CrowGrassPoint");
					}
					Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingLeft");
					m_crow_guarding = false;
					yield return Prop("LegsFountainCrow").MoveTo(crow_destination,200);
					Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrow");
					if (m_card_on_bench){
						m_crow_on_bench = true;
						m_crow_on_ground = false;
					} else {
						m_crow_on_ground = true;
						m_crow_on_bench=false;
					}
					m_crow_with_card_in_sunbeam = true;
			} else {
				if (!m_hope_retrieved){
					Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingRight");
					yield return Prop("LegsFountainCrow").MoveTo(Point("CrowStatuePoint"), 200, eEaseCurve.InOutSmooth);
					Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrow");
					m_crow_on_bench = false;
					m_crow_on_ground = false;
					m_crow_with_card_in_sunbeam = false;
					m_crow_guarding = true;
				} else {
					Vector2 crow_position = Prop("LegsFountainCrow").Position;
					Prop("LegsFountainCrow").PlayAnimationBG("LegsFountainCrowFlyingLeft");
					Prop("LegsFountainCrow").MoveToBG(new Vector2(crow_position[0]-300, crow_position[1] +300),200);
					Prop("LegsFountainCrow").Clickable = false;
					m_crow_on_bench = false;
					m_crow_on_ground = false;
					m_crow_with_card_in_sunbeam = false;
					m_crow_guarding = false;
					m_crow_flew_away = true;
			   }
			}
		}
		yield return E.Break;
	}

	public IEnumerator ClimbUpBench()
	{
		Vector2 base_climb_bench_position = new Vector2(56,-27);
		Vector2 bench_position_offset = new Vector2(50 * m_bench_position, 0);
		if (m_bench_position >= 1 && m_crow_guarding){
			yield return C.Plr.WalkTo(Point("GuardedPoint"));
		} else if (!m_crow_on_bench){
			yield return C.Plr.WalkTo(Point("PointBenchClimb") + bench_position_offset);
			C.Plr.Visible = false;
			Prop("ClimbBench").Position = base_climb_bench_position + bench_position_offset;
			Prop("ClimbBench").Visible = true;
			yield return Prop("ClimbBench").PlayAnimation("ClimbBench");
			C.Plr.SetPosition(Point("PointBenchReach") + bench_position_offset);
			C.Plr.Facing = eFace.UpLeft;
			C.Plr.Visible = true;
			Prop("ClimbBench").Visible = false;
			yield return C.Plr.Face(eFace.Down);
			m_player_on_bench = true;
		
			if (!m_hope_retrieved){
				if (m_bench_position==2){
					yield return C.Plr.Say("I bet I can reach from here!");
				} else {
					yield return C.Plr.Say("I can't reach anything from here.");
				}
			}
			C.Plr.Moveable = false;
		} else {
			yield return C.Plr.Say("I can't climb up with the crow on there.");
		}
		yield return E.Break;
	}

	public IEnumerator ClimbDownBench()
	{
		Vector2 base_climb_bench_position = new Vector2(56,-27);
		Vector2 bench_position_offset = new Vector2(50 * m_bench_position, 0);
		C.Plr.Moveable = true;
		yield return C.Plr.Face(eFace.UpLeft);
		Prop("ClimbBench").Position = base_climb_bench_position + bench_position_offset;
		Prop("ClimbBench").Visible = true;
		C.Plr.Visible = false;
		yield return Prop("ClimbBench").PlayAnimation("ClimbDownBench");
		C.Plr.Position = Point("PointBenchClimb") + bench_position_offset;
		C.Plr.Visible = true;
		Prop("ClimbBench").Visible = false;
		if (m_bench_position==2){
			yield return C.Plr.WalkTo(Point("OutFromStatuePoint"));
		}
		m_player_on_bench = false;
		yield return E.Break;
	}

	IEnumerator OnUseInvPropLegsFountainCard( IProp prop, IInventory item )
	{
		if (item == I.Grabber || item == I.GummyGrabber){
			yield return C.Plr.Say("I don't need the grabber to pick that up.");
			yield return C.Plr.Say("I can just use my hands.");
			yield return E.HandleInteract(Prop("LegsFountainCard"));
			}
		
		if (item == I.AbcGum){
			yield return E.HandleInventory(I.AbcGum,I.AstronautCard);
		}
		
		yield return E.Break;
	}

	public IEnumerator VandalizeWithGum()
	{
		yield return C.Plr.Face(eFace.Down);
		yield return C.Plr.Say("Really!?");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Plr.Say("I'm very disappointed.");
		
		yield return E.Break;
	}
}
