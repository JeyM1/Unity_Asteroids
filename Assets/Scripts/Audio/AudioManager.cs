using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
	static GameObject audioSourceObject = null;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();


    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize()
    {
		if (!audioSourceObject)
		{
			GameObject audioSourcePrefab = (GameObject)Resources.Load(@"Prefabs/Audio/MainAudioSource");
			audioSourceObject = GameObject.Instantiate(audioSourcePrefab, Vector3.zero, Quaternion.identity);
			audioSource = audioSourceObject.GetComponent<AudioSource>();
			audioClips.Add(AudioClipName.ExplosionSmall,
				Resources.Load<AudioClip>(@"Audio/ExplosionSmall"));
			audioClips.Add(AudioClipName.ExplosionBig,
				Resources.Load<AudioClip>(@"Audio/ExplosionBig"));
			audioClips.Add(AudioClipName.Fire1,
				Resources.Load<AudioClip>(@"Audio/Fire1"));

		}

	}

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
		if (!GameManagerSys.isGamePaused)
		{
			audioSource.PlayOneShot(audioClips[name]);
		}
	}
}
