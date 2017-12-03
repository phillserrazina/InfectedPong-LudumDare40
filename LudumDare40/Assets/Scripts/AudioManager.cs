using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public GameObject soundButtonOn;
	public GameObject soundButtonOff;

	private bool audioIsOn = true;

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake ()
	{

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	void Start()
	{
		audioIsOn = intToBool(PlayerPrefs.GetInt ("audioIsOn"));

		if(SceneManager.GetActiveScene().name == "MenuScene")
			Play ("MenuTheme");
		if(SceneManager.GetActiveScene().name == "GameScene")
			Play ("MainTheme");
		if(SceneManager.GetActiveScene().name == "HowToPlayScene")
			Play ("MainTheme");
		if(SceneManager.GetActiveScene().name == "GameOverScene")
			Play ("MenuTheme");
	}

	void Update()
	{
		if(SceneManager.GetActiveScene().name == "MenuScene" || SceneManager.GetActiveScene().name == "GameScene") 
		{
			if (audioIsOn) {
				AudioListener.pause = false;

				soundButtonOn.SetActive (true);
				soundButtonOff.SetActive (false);
			} else {
				AudioListener.pause = true;

				soundButtonOn.SetActive (false);
				soundButtonOff.SetActive (true);
			}
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void MuteAudio()
	{
		if (audioIsOn == true) 
		{
			audioIsOn = false;
		}
		else 
		{
			audioIsOn = true;
		}
	}

	int boolToInt(bool val)
	{
		if (val)
			return 1;
		else
			return 0;
	}


	bool intToBool(int val)
	{
		if (val != 0)
			return true;
		else
			return false;
	}


	void OnDestroy()
	{
		PlayerPrefs.SetInt ("audioIsOn", boolToInt(audioIsOn));
	}

}