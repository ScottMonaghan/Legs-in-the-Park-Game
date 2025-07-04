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
		public static IInventory Grabber        { get { return PowerQuest.Get.GetInventory("Grabber"); } }
		public static IInventory GummyGrabber   { get { return PowerQuest.Get.GetInventory("GummyGrabber"); } }
		public static IInventory SilverLeaf     { get { return PowerQuest.Get.GetInventory("SilverLeaf"); } }
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
		public static IRoom LegsFountain   { get { return PowerQuest.Get.GetRoom("LegsFountain"); } }
		// #ROOM# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	// Dialog
	public static partial class D
	{
		// Access to specific dialog trees (Auto-generated)
		public static IDialogTree ChatWithBarney       { get { return PowerQuest.Get.GetDialogTree("ChatWithBarney"); } }
		public static IDialogTree AskDadAboutBus       { get { return PowerQuest.Get.GetDialogTree("AskDadAboutBus"); } }
		public static IDialogTree BusStopDialogDan     { get { return PowerQuest.Get.GetDialogTree("BusStopDialogDan"); } }
		public static IDialogTree LegsMeetRobin        { get { return PowerQuest.Get.GetDialogTree("LegsMeetRobin"); } }
		public static IDialogTree LegsReadyForClues                { get { return PowerQuest.Get.GetDialogTree("LegsReadyForClues"); } }
		public static IDialogTree LegsFountainBench    { get { return PowerQuest.Get.GetDialogTree("LegsFountainBench"); } }
		// #DIALOG# - Do not edit this line, it's used by the system to insert rooms for easy access	    	    
	}


}
