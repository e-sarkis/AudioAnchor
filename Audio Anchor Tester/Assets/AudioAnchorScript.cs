using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AudioAnchor - Audio Management and Playing Singleton
public class AudioAnchorScript : MonoBehaviour 
{
	public static AudioAnchorScript inst = null;	// the singleton instance
	[HideInInspector] public AudioSource source;	// AudioSource component

	public bool playSoundEffectsBetweenScenes = true;
	public bool playMusicBetweenScenes = true;

	// In-editor dictionary pair AudioClip->float equivalent
	[System.Serializable]
    public struct ClipIntensityPair
    {
        public AudioClip clip;
        public float intensity;
    }
	public ClipIntensityPair[] ClipIntensityPairArray;
	public ClipIntensityPair[] MusicIntensityPairArray;


	void Awake () 
	{
		InitializeInstance();
		source = GetComponent<AudioSource>();

		// For music/audio transitions between scenes
		if (playSoundEffectsBetweenScenes || playMusicBetweenScenes) DontDestroyOnLoad(this.gameObject);
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
        Debug.LogError("AudioAnchor: " + clipName
						+ " AudioClip name not found. Are you sure it's been added to the AudioAnchorScript component Sounds array?");
    }

	// Play looping music via this GameObject's AudioSource via given AudioClip name
	public void PlayMusic(string musicName)
	{
		for (int i = 0; i < MusicIntensityPairArray.Length; i++)
		{
			if ( musicName == MusicIntensityPairArray[i].clip.name )
            {
				source.clip = MusicIntensityPairArray[i].clip;
				source.Play();
				return;
			}
			Debug.LogError("AudioAnchor: " + musicName
							+ " AudioClip name not found. Are you sure it's been added to the AudioAnchorScript component Music array?");
		}
	}


	// Initialize the instance of this singleton
	private void InitializeInstance()
	{
		// Delete the older instance if it exists
		if (inst == null) inst = this;
        else if (inst != this) Destroy(this.gameObject);
	}
}
