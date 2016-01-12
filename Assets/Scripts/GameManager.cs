using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GUIStyle ButtonStyle;
	public GUIStyle TimerStyle;
	public GUIStyle TextStyle;
	public GUIStyle TextStyle2;

	public Color normalColor;
	public Color hcColor;

	public Texture plusTwoTexture;

	int counter = 0;
	int comparator = 0;

	bool outOfTime = false;
	float currentTime = 10.00f;
	string timeText;

	bool hardcore = false;

	int highscore = 0;
	int hcHighscore = 0;

	// Use this for initialization
	void Start () {
		Camera.main.backgroundColor = normalColor;
	}
	
	// Update is called once per frame
	void Update () {

		if (highscore < counter)
		{
			highscore = counter;
		}
	
		if (!outOfTime)
		{
			currentTime -= Time.deltaTime;
			timeText = string.Format ("{0:0.0}", currentTime);

			if (hardcore == false)
			{
				if (comparator >= 25)
				{
					currentTime += 2f;
					comparator = 0;
				}

				Camera.main.backgroundColor = normalColor;
			} else 
			{
				Camera.main.backgroundColor = hcColor;

				if (hcHighscore < counter)
				{
					hcHighscore = counter;
				}
			}
		}

		if (currentTime < 0)
		{
			outOfTime = true;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Reinitialize();
		}

		if (Input.GetKeyDown (KeyCode.Tab))
		{
			HardcoreMode ();
			Reinitialize();
		}
	}

	void Reinitialize()
	{
		outOfTime = false;
		currentTime = 10;
		counter = 0;
		comparator = 0;
	}

	void HardcoreMode()
	{
		hardcore = !hardcore;
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect ((Screen.width-500)/2, (Screen.height+100)/2, 500, 100), counter.ToString (), ButtonStyle) && !outOfTime)
		{
			counter++;
			comparator++;
		}

		GUI.Label (new Rect (Screen.width/2-50, 100, 100, 100), timeText, TimerStyle);

		GUI.Label (new Rect (50, Screen.height / 2 + 230, 170, 100), "Press TAB to toggle between HARDCORE and NORMAL.");
		GUI.Label (new Rect (Screen.width/2 - 70, Screen.height/2 + 230, 170, 100), "Press SPACE to play again with the same settings.");
		GUI.Label (new Rect (Screen.width/2 + 200, Screen.height/2 + 230, 170, 100), "Press ESCAPE to reset.");

		GUI.Label (new Rect (40, 40, 200, 200), "HIGHSCORE: " + highscore.ToString (), TextStyle2);
		GUI.Label (new Rect (35, 65, 200, 200), "HARDSCORE: " + hcHighscore.ToString (), TextStyle2);

		if (hardcore)
		{
			GUI.Label (new Rect (Screen.width/2 + 200, 40, 200, 200), "HARDCORE", TextStyle);
		}
	}
}
