using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class CharacterElsa : CharacterScript<CharacterElsa>
{


	IEnumerator OnInteract()
	{

		yield return E.Break;
	}

	IEnumerator OnUseInv( IInventory item )
	{

		yield return E.Break;
	}
}