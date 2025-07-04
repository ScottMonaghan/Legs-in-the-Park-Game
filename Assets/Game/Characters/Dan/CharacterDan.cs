using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class CharacterDan : CharacterScript<CharacterDan>
{


	IEnumerator OnUseInv( IInventory item )
	{

		yield return E.Break;
	}
}