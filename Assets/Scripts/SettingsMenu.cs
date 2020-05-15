using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField]
	AudioMixer audioMixer = null;

	[SerializeField]
	Slider slider = null;

    public void SetVolume(float val)
	{
		audioMixer.SetFloat("MainVolume", val);
	}

	void Start()
	{
		float val = 0;
		audioMixer.GetFloat("MainVolume", out val);
		slider.value = val;
	}
}
