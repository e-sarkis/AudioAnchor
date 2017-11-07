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
	public ClipIntensityPair[] ClipIntensityPairArray;	// For use in-editor
	// The true Dicitonary to be used in logic
	public Dictionary<AudioClip, float> ClipsToIntensities = new Dictionary<AudioClip, float>();


	void Awake () 
	{
		InitializeInstance();
		source = GetComponent<AudioSource>();
		PopulateDictionary();
	}


	// Play a sound via this GameObjects AudioSource via given AudioClip name
	public void PlaySound(string clipName)
    {
        foreach (AudioClip clip in ClipsToIntensities.Keys)
        {
            if (clipName == clip.name)
            {
                source.PlayOneShot(clip, ClipsToIntensities[clip]);
                return;
            }
        }
        Debug.LogError("AudioAnchor: " + clipName + " AudioClip name not found. Are you sure it's been added to the AudioAnchorScript component?");
    }


	// Populate ClipsToIntensities via ClipIntensityPairArray for use in logic
	private void PopulateDictionary()
	{
		for (int i = 0; i < ClipIntensityPairArray.Length; i++)
            ClipsToIntensities.Add(ClipIntensityPairArray[i].clip, ClipIntensityPairArray[i].intensity);
	}


	// Initialize the instance of this singleton
	private void InitializeInstance()
	{
		// Delete the older instance if it exists
		if (inst == null) inst = this;
        else if (inst != this) Destroy(this.gameObject);
	}
}
