using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DialogBusStopDialogDan : DialogTreeScript<DialogBusStopDialogDan>
{
	public IEnumerator OnStart()
	{
		RoomBusStop.Script.m_dan_carnival_barking = false;
		if(RoomBusStop.Script.m_asked_dad_for_money == true){
			OptionOn("DadSaidNo");
			OptionOn("DadSaidYes");
			RoomBusStop.Script.m_asked_dad_for_money = false;
		}
		yield return C.Plr.WalkTo(C.Dan);
		yield return C.Plr.Face(eFace.Right);
		yield return C.Dan.Face(eFace.Left);
		yield return C.Dan.Say("Hey there little lady. What can old Dan do for you?");
		yield return E.Break;
	}

	public IEnumerator OnStop()
	{
		yield return E.Break;
	}

	IEnumerator OptionFinancialFreedom( IDialogOption option )
	{
		RoomBusStop.Script.m_talked_to_dan = true;
		yield return C.Plr.Say("How can I obtain financial freedom?");
		
		yield return C.Dan.Say("That is an EXCELLENT question!");
		yield return C.Dan.Say("All you need is to invest in this");
		yield return C.Dan.Say("one-of-a-kind misprinted Official Sue(tm), the T-Rex grabber!");
		yield return C.Dan.Say("SURELY TO EVENTUALLY BE WORTH MILLIONS!!");
		
		yield return C.Dan.Say("Normally a specimen of this potential would sell for thousands");
		yield return C.Dan.Say("or even HUNDREDS of thousands of dollars!");
		yield return C.Dan.Say("But for you,");
		yield return C.Dan.Say("just for today,");
		yield return C.Dan.Say("I'd be willing to part with this beauty for just $49.99!");
		
		OptionOffForever("FinancialFreedom");
		OptionOn("Again");
		OptionOn("WhyBrokenToy");
		OptionOn("WhatMisprint");
		OptionOn("WhyTenDollars");
		OptionOn("NoMoney");
		RoomBusStop.Script.m_price_of_grabber_revealed = true;
		yield return E.Break;
	}

	IEnumerator OptionWhatMisprint( IDialogOption option )
	{
		yield return C.Plr.Say("What misprint?");
		
		yield return C.Dan.Say("If you look closely you'll see that this Official Sue(tm) the T-Rex Grabber");
		yield return C.Dan.Say("HAS");
		yield return C.Dan.Say("NO");
		yield return C.Dan.Say("TEETH!");
		yield return C.Dan.Say("It's a one-of-a-kind collector's dream!");

		OptionOffForever("WhatMisprint");
		yield return E.Break;
	}

	IEnumerator OptionWhyBrokenToy( IDialogOption option )
	{
		yield return C.Plr.Say("Why would a broken toy be worth millions?");
		yield return C.Dan.Say("Broken Toy!?");
		yield return C.Dan.Say("BROKEN");
		yield return C.Dan.Say("TOY!?");
		yield return E.WaitSkip();
		yield return C.Dan.Say("Listen kid, collectors go CRAZY for this sort of stuff!");
		yield return C.Dan.Say("Why, a stamp with an upside down plane sold for for over TWO MILLION DOLLARS!!");
		yield return E.WaitSkip();
		yield return C.Dan.Say("Why, I bet your dear old dad over there would be able to quit working altogether with prize like this!");
		
		OptionOffForever("WhyBrokenToy");
		yield return E.Break;
	}

	IEnumerator OptionHowMuch( IDialogOption option )
	{
		yield return C.Plr.Say("How much for the grabber?");
		yield return C.Dan.Say("Well normally a specimen of this potential would sell for thousands");
		yield return C.Dan.Say("or even HUNDREDS of thousands of dollars!");
		yield return C.Dan.Say("But for you,");
		yield return C.Dan.Say("just for today,");
		yield return C.Dan.Say("I'd be willing to part with this beauty for just $10!");
		OptionOffForever("HowMuch");
		OptionOn("WhyTenDollars");
		OptionOn("NoMoney");
		RoomBusStop.Script.m_price_of_grabber_revealed = true;
		yield return E.Break;
	}

	IEnumerator OptionExit( IDialogOption option )
	{
		yield return C.Plr.Say("Bye for now.");
		yield return C.Dan.Say("Talk to you later little lady!");
		yield return C.Dan.Say("Now pardon me while I return to my hustle.");
		Stop();
		yield return E.WaitSkip();
		RoomBusStop.Script.m_dan_carnival_barking = true;
		yield return E.Break;
	}

	IEnumerator OptionWhyTenDollars( IDialogOption option )
	{
		yield return C.Plr.Say("If that is going to be worth millions, why only sell it $49.99?");
		yield return C.Dan.Say("I could tell you were a smart one!");
		yield return C.Dan.Say("I've had a problem with a boat and need to achieve liquidity");
		yield return C.Dan.Say("F");
		yield return C.Dan.Say("A");
		yield return C.Dan.Say("S");
		yield return C.Dan.Say("T");
		yield return C.Dan.Say("My loss is your gain. You stepped on this sidewalk at the right time!");
		yield return C.Dan.Say("Don't miss out on this amazing opportunity!");
		Option("WhyTenDollars").OffForever();
		yield return E.Break;
	}

	IEnumerator OptionNoMoney( IDialogOption option )
	{
		yield return C.Plr.Say("I don't have any money!");
		yield return C.Dan.Say("Why don't you go ask your handsome dad over there?");
		yield return C.Dan.Say("I can tell he's got a soft spot for you.");
		OptionOffForever("NoMoney");
		yield return E.Break;
	}

	IEnumerator OptionFamiliar( IDialogOption option )
	{
		yield return C.Plr.Say("You look familiar, have we met?");
		yield return C.Dan.Say("I don't think so little lady. I get that a lot. I just have one of those faces!");
		OptionOff("Familiar");
		OptionOn("Familiar2");
		yield return E.Break;
	}

	IEnumerator OptionFamiliar2( IDialogOption option )
	{
		yield return C.Plr.Say("Are you sure we haven't met?");
		yield return C.Dan.Say("Yup, I'm sure.");
		yield return E.Break;
	}

	IEnumerator OptionWhyBarrel( IDialogOption option )
	{
		yield return C.Plr.Say("Why are you blocking that barrel?");
		yield return C.Dan.Say("I'm not. I just happen to be standing in front of it.");
		yield return C.Plr.Say("Can you move?");
		yield return C.Dan.Say("I'm very comfortable where I am thanks!");
		yield return E.Break;
	}

	IEnumerator OptionLookInBarrel( IDialogOption option )
	{
		yield return C.Plr.Say("Can I look in the barrel?");
		yield return C.Dan.Say("Sorry kiddo, no can do.");
		yield return C.Dan.Say("What kind of responsible adult would be if I let kids rummage around in the trash.");
		yield return C.Dan.Say("There could be dangerous items in there!");
		yield return E.Break;
	}

	IEnumerator OptionDadSaidNo( IDialogOption option )
	{
		yield return C.Plr.Say("My dad said no.");
		yield return C.Dan.Say("And you're giving up that easily?");
		yield return C.Dan.Say("This opportunity could change both of your lives!");
		yield return C.Dan.Say("Why don't you try again?");
		OptionOff("DadSaidNo");
		OptionOff("DadSaidYes");
		yield return E.Break;
	}

	IEnumerator OptionDadSaidYes( IDialogOption option )
	{
		yield return C.Plr.Say("My dad said yes!");
		yield return C.Plr.Say("You just need to walk over and he'll give you the money!");
		yield return C.Dan.Say("Well that's great! I'll be right back!");
		Stop();
		E.SetTimer("DanDistracted",10);
		RoomBusStop.Script.m_dan_distracted = true;
		yield return C.Dan.WalkTo(C.Scott.Position[0] + 30,C.Scott.Position[1]+5);
		C.Dan.PlayAnimationBG("SueTalk");
		C.Scott.PlayAnimationBG("Talk");
		OptionOff("DadSaidNo");
		OptionOff("DadSaidYes");
		yield return E.Break;
	}

	IEnumerator OptionAgain( IDialogOption option )
	{
		yield return C.Plr.Say("Let me get this straight...");
		yield return C.Plr.Say("You'll sell me that misprinted Official Sue(tm), the T-Rex grabber for $49.99");
		if(Option("WhatMisprint").Used){
		yield return C.Plr.Say("It was misprinted with no teeth.");
		}
		if(Option("WhyBrokenToy").Used){
		yield return C.Plr.Say("And since collectors love misprints it could be worth a millions of dollars some day.");
		}
		if(Option("WhyTenDollars").Used){
		yield return C.Plr.Say("It's so cheap because you need to sell it F-A-S-T.");
		}
		if(Option("NoMoney").Used){
		yield return C.Plr.Say("You suggested I should go ask my dad for the money.");
		}
		if(Option("DadSaidNo").Used){
		yield return C.Plr.Say("And even though my dad said no, you think I should go ask him again.");
		}
		yield return C.Plr.Say("Is that all right?");
		yield return C.Dan.Say("That sums it up perfectly little lady!");
		yield return C.Dan.Say("I couldn't have said it better myself!");
		yield return E.Break;
	}

	IEnumerator OptionMillions( IDialogOption option )
	{

		yield return E.Break;
	}
}
