using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerTools.Quest;

namespace PowerScript
{	
	// Shortcut access to SystemAudio.Get
	public class Audio : SystemAudio
	{
	}

	public static partial class C
	{
		// Access to specific characters (Auto-generated)
		public static ICharacter Dave           { get { return PowerQuest.Get.GetCharacter("Dave"); } }
		public static ICharacter Barney         { get { return PowerQuest.Get.GetCharacter("Barney"); } }
		public static ICharacter Elsa           { get { return PowerQuest.Get.GetCharacter("Elsa"); } }
		public static ICharacter LilElsa        { get { return PowerQuest.Get.GetCharacter("LilElsa"); } }
		public static ICharacter Scott          { get { return PowerQuest.Get.GetCharacter("Scott"); } }
		public static ICharacter Robin          { get { return PowerQuest.Get.GetCharacter("Robin"); } }
		public static ICharacter Narrator       { get { return PowerQuest.Get.GetCharacter("Narrator"); } }
		public static ICharacter Dan            { get { return PowerQuest.Get.GetCharacter("Dan"); } }
		// #CHARS# - Do not edit this line, it's used by the system to insert characters
	}

	public static partial class I
	{		
		// Access to specific Inventory (Auto-generated)
		public static IInventory Bucket         { get { return PowerQuest.Get.GetInventory("Bucket"); } }
		public static IInventory AstronautCard  { get { return PowerQuest.Get.GetInventory("AstronautCard"); } }
		public static IInventory StickyShoe     { get { return PowerQuest.Get.GetInventory("StickyShoe"); } }
		public static IInventory AbcGum         { get { return PowerQuest.Get.GetInventory("AbcGum"); } }
		// #INVENTORY# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	public static partial class G
	{
		// Access to specific gui (Auto-generated)
		public static IGui DialogTree     { get { return PowerQuest.Get.GetGui("DialogTree"); } }
		public static IGui SpeechBox      { get { return PowerQuest.Get.GetGui("SpeechBox"); } }
		public static IGui HoverText  { get { return PowerQuest.Get.GetGui("HoverText"); } }
		public static IGui DisplayBox    { get { return PowerQuest.Get.GetGui("DisplayBox"); } }
		public static IGui Prompt         { get { return PowerQuest.Get.GetGui("Prompt"); } }
		public static IGui Toolbar          { get { return PowerQuest.Get.GetGui("Toolbar"); } }
		public static IGui InventoryBar   { get { return PowerQuest.Get.GetGui("InventoryBar"); } }
		public static IGui Options        { get { return PowerQuest.Get.GetGui("Options"); } }
		public static IGui Save           { get { return PowerQuest.Get.GetGui("Save"); } }
		public static IGui BusStopEmotionBar { get { return PowerQuest.Get.GetGui("BusStopEmotionBar"); } }
		// #GUI# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	public static partial class R
	{
		// Access to specific room (Auto-generated)
		public static IRoom Title          { get { return PowerQuest.Get.GetRoom("Title"); } }
		public static IRoom Forest         { get { return PowerQuest.Get.GetRoom("Forest"); } }
		public static IRoom TestRoom       { get { return PowerQuest.Get.GetRoom("TestRoom"); } }
		public static IRoom BusStop        { get { return PowerQuest.Get.GetRoom("BusStop"); } }
		public static IRoom Intro          { get { return PowerQuest.Get.GetRoom("Intro"); } }
		public static IRoom Legs1          { get { return PowerQuest.Get.GetRoom("Legs1"); } }
		public static IRoom Legs2          { get { return PowerQuest.Get.GetRoom("Legs2"); } }
		// #ROOM# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	// Dialog
	public static partial class D
	{
		// Access to specific dialog trees (Auto-generated)
		public static IDialogTree ChatWithBarney       { get { return PowerQuest.Get.GetDialogTree("ChatWithBarney"); } }
		public static IDialogTree AskDadAboutBus       { get { return PowerQuest.Get.GetDialogTree("AskDadAboutBus"); } }
		public static IDialogTree InteractLegs         { get { return PowerQuest.Get.GetDialogTree("InteractLegs"); } }
		public static IDialogTree BusStopInteractTree  { get { return PowerQuest.Get.GetDialogTree("BusStopInteractTree"); } }
		public static IDialogTree BusStopInteractCrosswalk { get { return PowerQuest.Get.GetDialogTree("BusStopInteractCrosswalk"); } }
		public static IDialogTree InteractAstronautCard { get { return PowerQuest.Get.GetDialogTree("InteractAstronautCard"); } }
		public static IDialogTree InteractStickyShoe   { get { return PowerQuest.Get.GetDialogTree("InteractStickyShoe"); } }
		public static IDialogTree BusStopInteractBarrel { get { return PowerQuest.Get.GetDialogTree("BusStopInteractBarrel"); } }
		public static IDialogTree BusStopDialogDan     { get { return PowerQuest.Get.GetDialogTree("BusStopDialogDan"); } }
		// #DIALOG# - Do not edit this line, it's used by the system to insert rooms for easy access	    	    
	}


}
