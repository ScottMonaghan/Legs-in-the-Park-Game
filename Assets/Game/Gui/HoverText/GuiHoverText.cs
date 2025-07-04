using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using System.Linq;
using static GlobalScript;

public class GuiHoverText : GuiScript<GuiHoverText>
{


	void Update()
	{
		//basic default
		string hoverText = E.GetMouseOverDescription();
		IQuestClickable hoverObj = E.GetMouseOverClickable();
		
		if (hoverObj != null) {
			//inventory defaults
			if (I.Active != null && hoverObj.Description.Length > 0  && !E.Cursor.InventoryCursorOverridden) {
				hoverText = $"Use {I.Active.Description} on {hoverObj.Description}";
				if (hoverObj == I.Active) {
					hoverText = $"Use {I.Active.Description} on itself";
				}
				if (hoverObj.ClickableType == eQuestClickableType.Character)
				{
					if (((ICharacter)hoverObj).IsPlayer)
					{
						hoverText = $"Use {I.Active.Description} on yourself";
					}
					else
					{
						hoverText = $"Give {I.Active.Description} to {hoverObj.Description}";
					}
				}
			}
		
			//cursor defaults
			else if (hoverObj.Cursor == "Use" ) {
				hoverText = $"Use {E.GetMouseOverDescription()}";
			}
			else if (hoverObj.Cursor == "Look") {
				hoverText = $"Look at {E.GetMouseOverDescription()}";
			}
			else if (hoverObj.Cursor == "Talk")
			{
				hoverText = $"Talk to {E.GetMouseOverDescription()}";
			}
			else if (new[] { "Up", "Down", "Left", "Right" }.Contains(hoverObj.Cursor))
			{
				hoverText = $"Exit {hoverObj.Cursor}";
			}
		}
		
		//Global Overrides
		if (hoverObj == I.Grabber && I.Active == I.AstronautCard){
			hoverText = "Use Astronaut Card on Grabber";
		}
		if (hoverObj == I.AstronautCard && I.Active == I.Grabber){
			hoverText = "Use Grabber on Astronaut Card";
		}
		
		//BusStop Overrides
		if (E.GetCurrentRoom() == R.BusStop)
		{
		
			if (hoverObj == I.StickyShoe && hoverObj.Cursor == "Use" && I.Active == null)
			{
				hoverText = "Scrape the gum off the shoe";
			}
			if (hoverObj == R.BusStop.GetProp("Barrel") && hoverObj.Cursor == "Use" && I.Active == null)
			{
				//use custom overrides for barrel
				hoverText = E.GetMouseOverDescription();
			}
			if ((hoverObj==R.BusStop.GetHotspot("LeftCrosswalk") || hoverObj==R.BusStop.GetHotspot("Street"))&& hoverObj.Cursor == "Right")
			{
				hoverText = "Cross the street to the Legs";
			}
			if (hoverObj == R.BusStop.GetHotspot("RightCrosswalk") && hoverObj.Cursor == "Left")
			{
				hoverText = "Cross the street to the bus stop";
			}
			if (hoverObj == R.BusStop.GetHotspot("Legs") && hoverObj.Cursor == "Right")
			{
				hoverText = "Walk into the Legs";
			}
			if (I.Active == I.AstronautCard && hoverObj == R.BusStop.GetProp("Tree"))
			{
				hoverText = "Chop down the mightest tree on the block with Astronaut Card";
			}
			if (hoverObj == I.StickyShoe && I.Active == I.AstronautCard){
				hoverText = "Use Astronaut Card on Sticky Shoe";
			}
		
		}
		
		//LegsFountain Overrides
		if (E.GetCurrentRoom() == R.LegsFountain)
        {
			if(hoverObj == R.LegsFountain.GetProp("LegsFountainParkBench") && hoverObj.Cursor == "Use" && I.Active == null)
            {
				if (RoomLegsFountain.Script.m_player_on_bench)
				{
					hoverText = "Climb down";
				} else {
					hoverText = "Mess with the park bench";
				};
				//if (!RoomLegsFountain.Script.m_park_bench_movable)
				//{
				//	hoverText = "Sit down on the park bench";
				//} else if (RoomLegsFountain.Script.m_bench_position < 2 || (RoomLegsFountain.Script.m_hope_retrieved && !RoomLegsFountain.Script.m_sunbeam_on_bench))
    //            {
				//	hoverText = "Move the park bench";
				//} else
    //            {
				//	hoverText = "Climb the park bench";
    //            }
            }

			if(hoverObj == R.LegsFountain.GetProp("LegsFountainCard") && hoverObj.Cursor == "Use" && I.Active == null){
				hoverText = "Pick up the Astronaut Card";
            }
			if (hoverObj == R.LegsFountain.GetProp("LegsFountainParkBench") && I.Active == I.AbcGum)
            {
				hoverText = "Stick Gum on Bench";
            }

		}
		
		//finally display the text!
		Label("Text").Text = hoverText;
		Label("Text").Visible = !E.GetBlocked();
	}
}
