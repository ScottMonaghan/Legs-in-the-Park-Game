using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class CharacterLilElsa : CharacterScript<CharacterLilElsa>
{


	IEnumerator OnInteract()
	{
		yield return C.LilElsa.WalkTo(C.Elsa);
		yield return E.Break;
	}
}