using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour {
	public GameObject car;
	public float speed = 5f;
	public float multiply = 0f;
	public static bool gameover = false;
	public static bool levelclear = false;
	public ParticleSystem shower;
	public int leftorright;
	public Animator animator;
	public Main main;
    public AudioSource tireScreech;
    public AudioSource carCruise;
    public AudioSource carExplode;
	bool temp = false;

    public Text GameoverProgress;
    public GameObject GameoverProgressFill;

    private float progressPercentage;

    void LevelClear (){
		if (levelclear&&(temp==false)) {
			main.LevelClearPanel.SetActive (true);
			main.LevelClearPanel.GetComponent<Animator> ().Play ("Transition");
			temp = true;
		}			
	}

	void Gameover ()
	{
		if ((Main.lives == 0) && (gameover == false)) {
            GameoverProgressFill.GetComponent<Image>().fillAmount = 1-(Main.currentDistance/Main.distance);
            progressPercentage = 100-((Main.currentDistance / Main.distance) * 100);
            GameoverProgress.text = (int)progressPercentage + "%";
            gameover = true;
            carCruise.Stop();
            carExplode.Play();
			shower.Play ();
			car.GetComponent<Rigidbody> ().AddForce (0, 5, 20, ForceMode.Impulse);
			main.GameoverPanel.SetActive (true);
			main.GameoverPanel.GetComponent<Animator>().Play ("Transition");
			// col.gameObject.GetComponent<Rigidbody> ().AddForce (0, 4, 0, ForceMode.Impulse);
		}
	}

	void Turnleft () {
		if (multiply <= -1f)
			return;
		multiply -= 0.1f;

	}

	void Turnright () {
		if (multiply >= 1f)
			return;
		multiply += 0.1f;
	}

	void Movement() {
		car.transform.position = new Vector3 (0 + 4 * (multiply), -0.17f, car.transform.position.z + (speed*Time.deltaTime));
		if ((multiply <= -1f) || (multiply >= 1f) || ((multiply <= 0.0001f) && (multiply >= -0.0001f))) {
			if ((Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.Q))&&(multiply>-1)) {
                tireScreech.Play();
                leftorright = 0;
				animator.Play ("CarLeftMovement");
				Turnleft ();
			}
			if ((Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D))&&(multiply<1)) {
                tireScreech.Play();
				animator.Play ("CarRightMovement");
				Turnright ();
				leftorright = 1;
			}
		} else {
			if (leftorright == 1)
				Turnright ();
			if (leftorright == 0)
				Turnleft ();
		}
	}

	void Start () {
		Car.levelclear = false;
		Car.gameover = false;
	}

	void Update () {
		LevelClear ();
		Gameover ();
		if (gameover == false && Pause.isPaused == false) {
			Movement ();
		}
	}

}
