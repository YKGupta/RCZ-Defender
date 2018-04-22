using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float Force = 50f;
	public Camera cam;
	public GameObject EnemyDestroyedPrefab;
	public GameObject BulletImpactEffect;
	public GameObject EnemyDeadEffect;
	public AudioSource source;
	public AudioClip clip;
	public Text EnemyKilledText;
	public Text ScoreText;
	public Text HighScoreText;
	public GameObject I01;
	public GameObject I02;
	public GameObject I03;
	public GameObject I04;
	public GameObject I05;
	public static int Lives = 5;
	public static bool isDecreased = false;
	int Score = 0;
	int HighScore = 0;

	bool HasDied = false;

	void Start ()
	{
		Lives = 5;
		HighScore = PlayerPrefs.GetInt("Score", 0);
		HighScoreText.text = HighScore.ToString();
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			HighScore = 0;			
			HighScoreText.text = HighScore.ToString();
			PlayerPrefs.SetInt("Score", HighScore);
		}

		ScoreText.text = Score.ToString();

		if(Lives <= 0)
		{
			if(!HasDied)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
				Debug.Log("jsakbdskdsa");
			}
				
			HasDied = true;
		}

		if(Enemy.hasLostLife && !isDecreased)
		{
			if(I05.activeSelf)
			{
				I05.SetActive(false);
			}

			else if(!I05.activeSelf && I04.activeSelf)
			{
				I04.SetActive(false);
			}

			else if(!I04.activeSelf && I03.activeSelf)
			{
				I03.SetActive(false);
			}

			else if(!I03.activeSelf && I02.activeSelf)
			{
				I02.SetActive(false);
			}

			else if(!I02.activeSelf && I01.activeSelf)
			{
				I01.SetActive(false);
			}

			isDecreased = true;
		}

		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				if(hit.collider.tag == "UnShootable")
				{
					GameObject ImpactGO = (GameObject)Instantiate(BulletImpactEffect, hit.point, Quaternion.identity);
					Destroy(ImpactGO, 1f);
				}

				if(hit.collider.name != "Player" && hit.collider.tag != "UnShootable")
				{
					Score += 10;
					source.PlayOneShot(clip);
					GameObject EnemyGO = (GameObject)Instantiate(EnemyDeadEffect, hit.point, Quaternion.identity);
					Vector3 enemyPos = hit.transform.position;
					Destroy(hit.transform.gameObject);
					GameObject DestroyedGO = (GameObject)Instantiate(EnemyDestroyedPrefab, enemyPos, Quaternion.identity);
					DestroyedGO.GetComponentInChildren<Rigidbody>().AddForce(Force, Force, Force);
					EnemyKilledText.enabled = true;
					Destroy(EnemyGO, 1f);
					StartCoroutine(SetText(EnemyKilledText));
					Destroy(DestroyedGO, 3f);
				}				
			}			
		}

		if(Score > HighScore)
		{
			HighScore = Score;
			HighScoreText.text = HighScore.ToString();
			PlayerPrefs.SetInt("Score", HighScore);
		}
	}

	IEnumerator SetText (Text text)
	{
		yield return new WaitForSeconds(1f);
		text.enabled = false;
	}
}