using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioAnchorTesterScript : MonoBehaviour 
{
	void Start() 
	{
		AudioAnchorScript.inst.PlayMusic("AA_Music");
	}
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.A)) AudioAnchorScript.inst.PlaySound("AA_Coin");
		if (Input.GetKeyDown(KeyCode.S)) AudioAnchorScript.inst.PlaySound("AA_Laser");
		if (Input.GetKeyDown(KeyCode.R))
		{
			AudioAnchorScript.inst.TransitionScenes();
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} 
	}
}
