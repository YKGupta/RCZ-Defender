using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public string MainSceneNum;
	public GameObject LoadingScreen;
	public Text HighScoreText;
	public Text TimesPlayedText;
	public AudioSource source;
	public AudioClip clip;
	int PlayedTimes = 0;

	void Start ()
	{
		HighScoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
		PlayedTimes = PlayerPrefs.GetInt("Times", 0);
		if(PlayedTimes <= 1)
		{
			TimesPlayedText.text = PlayedTimes.ToString() + " - Time Played!";
		}
		else if(PlayedTimes > 1)
		{
			TimesPlayedText.text = PlayedTimes.ToString() + " - Times Played!";
		}
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			PlayedTimes = 0;	
			TimesPlayedText.text = PlayedTimes.ToString() + " - Time Played!";		
			PlayerPrefs.SetInt("Times", PlayedTimes);
		}
	}

	public void PlayGame ()
	{		
		PlayedTimes++;
		source.PlayOneShot(clip);
		PlayerPrefs.SetInt("Times", PlayedTimes);
		SceneManager.LoadSceneAsync(MainSceneNum);
        LoadingScreen.SetActive(true);

	}

	public void QuitGame ()
	{
		source.PlayOneShot(clip);
		Application.Quit();
	}
}