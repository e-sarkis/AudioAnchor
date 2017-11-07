using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnchorTesterScript : MonoBehaviour 
{
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.A)) AudioAnchorScript.inst.PlaySound("AA_Coin");
		if (Input.GetKeyDown(KeyCode.S)) AudioAnchorScript.inst.PlaySound("AA_Laser");
	}
}
