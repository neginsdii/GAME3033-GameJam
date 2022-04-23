using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerCountDown : MonoBehaviour
{
	public TextMeshProUGUI textDisplay;
	public float gameTime;
	public float level2GameTime;
	public float timer;
	private bool stopTimer;

	private AudioSource audioSource;
	public AudioSource timeOut;

	public GameObject endGamePanel;
	public TextMeshProUGUI endgame;
	private void Start()
	{
		
		stopTimer = false;
		timer = gameTime;
		//audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{

			UpdateTimer();
	}

	private void UpdateTimer()
	{
		timer -= Time.deltaTime;
		int minutes = Mathf.FloorToInt(timer / 60);
		int seconds = Mathf.FloorToInt(timer - minutes * 60f);
		
		if (timer <= 0)
		{
			stopTimer = true;
			//if (!timeOut.isPlaying)
			//	timeOut.Play();
		

			timer = 0;
			endGamePanel.SetActive(true);
			endgame.SetText("Game Over");
			Time.timeScale = 0;
		}
		if (!stopTimer)
		{
			textDisplay.SetText("Time Remaining: "+seconds.ToString());
		}
	}

	public void AddTimer(float val)
	{
		timer += val;
	}
	
}
