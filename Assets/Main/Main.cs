using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Main : MonoBehaviour {
    public GameObject LiveFill;
    public GameObject LiveNumberText;
	public GameObject GameoverPanel;
	public GameObject LevelClearPanel;
    public Slider ProgressSlider;
	public static int lives = 3;
    private GameObject car;

    public static float distance;
    private GameObject finishLine;
    public static float currentDistance;

    // Use this for initialization
    void Start () {
        finishLine = GameObject.FindWithTag("Finish");
        distance = finishLine.transform.position.z;
        car = GameObject.FindWithTag("Player");
        ProgressSlider.maxValue = distance;
    }

    public void NextLevel()
    {
        Car.gameover = false;
        Main.lives = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	public void Restart () {
		Car.gameover = false;
		Main.lives = 3;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void DisplayLives()
	{
        LiveNumberText.GetComponent<TextMeshProUGUI>().SetText((Main.lives).ToString());
		if (Car.gameover == false && Car.levelclear == false) {
			if (lives == 3) {
                LiveFill.GetComponent<Image>().fillAmount = 1f;
			} else if (lives == 2) {
                LiveFill.GetComponent<Image>().fillAmount = 0.66f;
			} else if (lives == 1) {
                LiveFill.GetComponent<Image>().fillAmount = 0.33f;

			}

		} else {
            LiveNumberText.SetActive(false);
            LiveFill.GetComponent<Image>().fillAmount = 0f;
		}
	}
		

	void Update () {
        DisplayLives ();
        if (Car.levelclear == false && Car.gameover == false)
        {
            currentDistance = Vector3.Distance(finishLine.transform.position, car.transform.position);
            ProgressSlider.value = currentDistance;
        }
    }
}
