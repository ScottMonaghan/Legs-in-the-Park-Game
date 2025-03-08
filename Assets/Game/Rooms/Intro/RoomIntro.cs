using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomIntro : RoomScript<RoomIntro>
{


	IEnumerator OnEnterRoomAfterFade()
	{
		E.StartCutscene();
		C.Narrator.Room = R.Intro;
		C.Narrator.SetPosition(0,0);
		yield return C.Display("Prologue: The Legs");
		yield return E.WaitSkip(1.0f);
		yield return E.WaitSkip(1.0f);
		yield return C.Narrator.Say("Before we begin...");
		yield return E.WaitSkip();
		yield return C.Narrator.Say("you need to understand...");
		yield return E.WaitSkip();
		Audio.PlayMusic("Night Vigil");
		yield return C.Narrator.Say("The Legs have always been there.");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		Prop("IntroFG").FadeBG(0,1,3);
		Prop("IntroLegs").FadeBG(0,1,3);
		yield return Prop("IntroPrimordialBG").Fade(0,1,3);
		Audio.PlayAmbientSound("Eruption",3);
		yield return C.Narrator.Say("When the boiling young world was ruled by ancient beasts...");
		yield return C.Narrator.Say("not the famous giant lizard-birds...");
		Prop("IntroPrimordialMonsters").FadeBG(0,1,2);
		yield return C.Narrator.Say("but other, older, more terrible things...");
		yield return C.Narrator.Say("The Legs were there.");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		Prop("IntroPrimordialMonsters").FadeBG(1,0,3);
		yield return Prop("IntroFirstMammalBG").Fade(0,1,3);
		Audio.PlayAmbientSound("SpringForest",3);
		Prop("IntroPrimordialBG").Visible=false;
		Prop("IntroPrimordialMonsters").Visible=false;
		yield return C.Narrator.Say("When the old volcanoes had been tamed into grassy hills...");
		Prop("IntroFirstMammalCritter").Visible = true;
		yield return C.Narrator.Say("and the first furry critters poked out of their deep burrows...");
		yield return C.Narrator.Say("The Legs were there.");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		Prop("IntroIceAgeMammoths").Visible = true;
		Prop("IntroIceAgeMammoths").FadeBG(0,1,3);
		yield return Prop("IntroIceAgeBG").Fade(0,1,3);
		Audio.PlayAmbientSound("HowlingWind",3);
		Prop("IntroFirstMammalBG").Visible=false;
		Prop("IntroFirstMammalCritter").Visible=false;
		yield return C.Narrator.Say("When the sun grew dim...");
		yield return C.Narrator.Say("and the Great Ice marched down from the north...");
		yield return C.Narrator.Say("devouring everything in its path...");
		yield return C.Narrator.Say("The Legs were there.");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		Prop("IntroThawSky").Visible = true;
		Prop("IntroThawSun").Visible = true;
		Prop("IntroThawBG").Visible = true;
		Prop("IntroThawGrass").Visible = true;
		Prop("IntroThawWaves").Visible = true;
		Prop("IntroIceAgeMammoths").FadeBG(1,0,3);
		yield return Prop("IntroIceAgeBG").Fade(1,0,3);
		Audio.PlayAmbientSound("LakeWaves",3);
		yield return C.Narrator.Say("When the sun finally returned...");
		yield return C.Narrator.Say("and the Great Ice retreated back to its stronghold at the top of the world...");
		yield return C.Narrator.Say("leaving behind a lake as wide and as deep and as long as a sea...");
		yield return C.Narrator.Say("at the edge of the shore...");
		yield return C.Narrator.Say("The Legs were there.");
		Audio.StopAmbientSound(5);
		Audio.StopMusic(5);
		yield return E.FadeOut(5);
		yield return C.Narrator.Say("But enough of the past.");
		yield return C.Narrator.Say("Let us speak of today.");
		E.EndCutscene();
		yield return C.Plr.ChangeRoom(R.BusStop);
		
		yield return E.Break;
	}
}
