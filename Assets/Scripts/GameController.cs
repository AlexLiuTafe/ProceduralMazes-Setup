using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
	[SerializeField] private FpsMovement player;
	[SerializeField] private Text timeLabel;
	[SerializeField] private Text scoreLabel;

    private MazeConstructor generator;

	private DateTime startTime;
	private int timeLimit;
	private int reduceLimitBy;

	private int score;
	private bool goalReached;

    private void Start()
    {
        generator = GetComponent<MazeConstructor>();
		StartNewGame();
    }
	
	void StartNewGame()
	{
		timeLimit = 80;
		reduceLimitBy = 5;
		startTime = DateTime.Now;

		score = 0;
		scoreLabel.text = score.ToString();

		StartNewMaze();

	}

	void StartNewMaze()
	{
		generator.GenerateNewMaze(13, 15, OnStartTrigger, OnGoalTrigger);

		float x = generator.startCol * generator.hallWidth;
		float y = 1;
		float z = generator.startRow * generator.hallWidth;
		player.transform.position = new Vector3(x, y, z);

		goalReached = false;
		player.enabled = true;

		//restart timer
		timeLimit -= reduceLimitBy;
		startTime = DateTime.Now;
	}

	private void Update()
	{
		if(!player.enabled)
		{
			return;
		}

		int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
		int timeLeft = timeLimit - timeUsed;

		if(timeLeft > 0 )
		{
			timeLabel.text = timeLeft.ToString();
		}
		else
		{
			timeLabel.text = "TIME UP";
			player.enabled = false;

			Invoke("StartNewGame", 4);
		}
	}

	void OnGoalTrigger(GameObject trigger,GameObject other)
	{
		Debug.Log("Goal!");
		goalReached = true;

		score += 1;
		scoreLabel.text = score.ToString();

		Destroy(trigger);
	}
	void OnStartTrigger(GameObject trigger,GameObject other)
	{
		if(goalReached)
		{
			Debug.Log("FINISH!");
			player.enabled = false;

			Invoke("StartNewMaze", 4);
		}
	}
}
