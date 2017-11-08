using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// AudioAnchor - Audio Management and Playing Singleton
public class AudioAnchorScript : MonoBehaviour 
{
	public static AudioAnchorScript inst = null;		// The singleton instance
	[HideInInspector] public AudioSource sfxSource;		// SFX AudioSource component
	[HideInInspector] public AudioSource musicSource;	// Music AudioSource

	public bool playSoundEffectsBetweenScenes = true;
	public bool playMusicBetweenScenes = true;

	// In-editor dictionary pair AudioClip->float equivalent
	[System.Serializable]
    public struct ClipIntensityPair
    {
        public AudioClip clip;
        public float intensity;
    }
	public ClipIntensityPair[] SFXIntensityPairArray;	// Sound FX
	public ClipIntensityPair[] MusicIntensityPairArray;	// Musical Tracks


	void Awake () 
	{
		InitializeSingletonInstance();

		sfxSource = gameObject.AddComponent<AudioSource>();
		sfxSource.loop = true;
		sfxSource.playOnAwake = false;
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.loop = true;
		musicSource.playOnAwake = false;		
	}


	// Performs functions required when transitioning between scenes.
	// MUST BE CALLED BEFORE CHANGING SCENES OR AUDIO WILL MAY PERFORM INCORRECTLY
	public void TransitionScenes()
	{
		if (!playSoundEffectsBetweenScenes)
		{
			// Stop the source sound effects
			sfxSource.enabled = false;
			sfxSource.enabled = true;
		}
		if (!playMusicBetweenScenes)
		{
			// Stop the source music
			musicSource.enabled = false;
			musicSource.enabled = true;
		}
    }


	// Play a sound via this GameObjects AudioSource via given AudioClip name
	public void PlaySound(string clipName)
    {
		for (int i = 0; i < SFXIntensityPairArray.Length; i++)
		{
			if ( clipName == SFXIntensityPairArray[i].clip.name )
            {
                sfxSource.PlayOneShot(SFXIntensityPairArray[i].clip, SFXIntensityPairArray[i].intensity);
                return;
            }
		}
        Debug.LogError("AudioAnchor: " + clipName
						+ " AudioClip name not found. Are you sure it's been added to the AudioAnchorScript component SFX array?");
    }


	// Play looping music via this GameObject's AudioSource via given AudioClip name
	public void PlayMusic(string musicName)
	{
		for (int i = 0; i < MusicIntensityPairArray.Length; i++)
		{
			if ( musicName == MusicIntensityPairArray[i].clip.name )
            {
				if (musicSource.isPlaying) return;
				musicSource.clip = MusicIntensityPairArray[i].clip;
				musicSource.volume = MusicIntensityPairArray[i].intensity;
				musicSource.Play();
				return;
			}
			Debug.LogError("AudioAnchor: " + musicName
							+ " AudioClip name not found. Are you sure it's been added to the AudioAnchorScript component Music array?");
		}
	}


	// Attempt initialization of the instance of this singleton
	private void InitializeSingletonInstance()
	{
		// Delete the older instance if it exists and isn't this
		if (inst == null)
		{
			inst = this;
			DontDestroyOnLoad(this.gameObject);
		} 
        else if (inst != this) Destroy(this.gameObject);
	}
}
