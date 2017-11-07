using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AudioAnchor - Audio Management and Playing Singleton
public class AudioAnchorScript : MonoBehaviour 
{
	public static AudioAnchorScript inst = null;	// the singleton instance
	[HideInInspector] public AudioSource source;	// AudioSource component

	// In-editor dictionary pair AudioClip->float equivalent
	[System.Serializable]
    public struct ClipIntensityPair
    {
        public AudioClip clip;
        public float intensity;
    }
	public ClipIntensityPair[] ClipIntensityPairArray;


	void Awake () 
	{
		InitializeInstance();
		source = GetComponent<AudioSource>();
	}


	// Play a sound via this GameObjects AudioSource via given AudioClip name
	public void PlaySound(string clipName)
    {
		for (int i = 0; i < ClipIntensityPairArray.Length; i++)
		{
			if ( clipName == ClipIntensityPairArray[i].clip.name )
            {
                source.PlayOneShot(ClipIntensityPairArray[i].clip, ClipIntensityPairArray[i].intensity);
                return;
            }
			
		}
        Debug.LogError("AudioAnchor: " + clipName + " AudioClip name not found. Are you sure it's been added to the AudioAnchorScript component?");
    }


	// Initialize the instance of this singleton
	private void InitializeInstance()
	{
		// Delete the older instance if it exists
		if (inst == null) inst = this;
        else if (inst != this) Destroy(this.gameObject);
	}
}
