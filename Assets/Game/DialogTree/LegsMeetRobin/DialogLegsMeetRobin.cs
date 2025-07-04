using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogLegsMeetRobin : DialogTreeScript<DialogLegsMeetRobin>
{

	public IEnumerator OnStop()
	{
		yield return E.Break;
	}

	IEnumerator OptionExit( IDialogOption option )
	{
		yield return C.Plr.Say("Bye for now!");
		Stop();
		yield return E.Break;
	}

	IEnumerator OptionWhat( IDialogOption option )
	{
		if (Option("What").FirstUse) {
		yield return C.Plr.Say("What do you mean TRUE name?");
		} else {
		yield return C.Plr.Say("What was that about TRUE names again?");
		}
		yield return C.Robin.Say("True names have POWER!");
		yield return C.Robin.Say("A queen's name especially!");
		if (Option("What").FirstUse) {
		
		yield return C.Plr.Face(eFace.Down);
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Plr.Say("O");
		yield return C.Plr.Say("kayyyy.");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Plr.Face(C.Robin);
		} else {
		yield return C.Plr.Face(eFace.Down);
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Plr.Say("Oh.");
		yield return C.Plr.Say("Riiiiiiiight.");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Plr.Face(C.Robin);
		}
		Option("What").Description="What was that about TRUE names again?";
		yield return E.Break;
	}

	IEnumerator OptionQueenElsa( IDialogOption option )
	{
		yield return C.Plr.Say("You've never heard of Queen Elsa?");
		yield return C.Robin.Say("I don't know anything about your queens.");
		Option("Movie").On();
		Option("QueenElsa").OffForever();
		yield return E.Break;
	}

	IEnumerator OptionMovie( IDialogOption option )
	{
		yield return C.Plr.Say("Queen Elsa is from a movie.");
		yield return E.WaitSkip();
		yield return C.Robin.Say("Amoovie? Is that the name of her kingdom?");
		yield return C.Plr.Face(eFace.Down);
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Plr.Say("Okay.");
		yield return C.Plr.Say("This kid is weird.");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Plr.Say("There are some kids at my school whose parents won't let them watch any movies or TV.");
		yield return C.Plr.Say("I asked my dad why and he said...");
		yield return C.Scott.Say("Because they're hippies--or CEOs' kids. One or the other.");
		yield return E.WaitSkip();
		yield return C.Plr.Say("This kid must be a hippie CEO.");
		yield return C.Plr.Say("I won't hold it against him.");
		yield return C.Plr.Say("You can't choose your parents.");
		yield return C.Plr.Face(C.Robin);
		Option("Movie").OffForever();
		yield return E.Break;
	}

	IEnumerator OptionPlay( IDialogOption option )
	{
		yield return C.Player.Say("Want to play?");
		
		yield return C.Robin.Say("Yes!");
		yield return C.Robin.Say("Let's play treasure hunt!");
		
		yield return C.Player.Say("What's that?");
		
		yield return C.Robin.Say("I give you clues and you need to find some treasure!");
		
		yield return C.Player.Say("That sounds fun!");
		yield return C.Player.Say("Do I need to close my eyes while you go hide something?");
		
		yield return C.Robin.Say("What you seek is already hidden!");
		
		yield return C.Player.Say("You hid it before we met?");
		
		yield return C.Robin.Say("It was hidden long ago!");
		
		yield return C.Plr.Face(eFace.Down);
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Player.Say("Hmm...");
		yield return E.WaitSkip();
		yield return E.WaitSkip();
		yield return C.Plr.Face(C.Robin);
		Option("Play").OffForever();
		Option("CluesAgain").On();
		Globals.LegsResetTreasureHunt();
		E.Set(eLegsProgress.GotTreasureHunt);
		yield return C.Robin.Say("Are you ready for your clues?");
		Goto(D.LegsReadyForClues);
		yield return E.Break;
	}

	IEnumerator OptionCluesAgain( IDialogOption option )
	{
		yield return C.Elsa.Say("Can you tell me those clues again?");
		yield return C.Robin.Say("Of course, are you ready?");
		Goto(D.LegsReadyForClues);
		
		yield return E.Break;
	}

	IEnumerator OptionNewClues( IDialogOption option )
	{
		yield return C.Elsa.Say("I'm having a hard time with those clues. Can you give me some new clues?");
		
		yield return C.Robin.Say("Certainly.");
		
		yield return C.Robin.Say("Ready for new clues?");
		
		Globals.LegsResetTreasureHunt();
		Goto(D.LegsReadyForClues);
		
		yield return E.Break;
	}
}
