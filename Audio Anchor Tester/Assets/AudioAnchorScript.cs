using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AudioAnchor - Audio Management and Playing Singleton
public class AudioAnchorScript : MonoBehaviour 
{
	public static AudioAnchorScript inst = null;	// the singleton instance

	void Awake () 
	{
		InitializeInstance();
	}

	// Initialize the instance of this singleton
	private void InitializeInstance()
	{
		// Delete the older instance if it exists
		if (inst == null) inst = this;
        else if (inst != this) Destroy(this.gameObject);
	}
}
