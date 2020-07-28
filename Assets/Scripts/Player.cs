using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Saving stuff:
// https://unity3d.com/learn/tutorials/topics/scripting/persistence-saving-and-loading-data

public class Player : MovingObject
{
	private InputHandler inputHandler;
	private int score = 0;
	private Text scoreText;
	private GameObject restartButton;
	private LevelGenerator levelGenerator;

	void Start()
	{
		inputHandler = GetComponent<InputHandler>();

		levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();

		// hopefully throwing these errors will help with debugging
		scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
		if(scoreText == null)
			Debug.LogError("Player -> Start: Score text not found");
       
        restartButton = GameObject.FindGameObjectWithTag("RestartButton");
		if(restartButton == null)
			Debug.LogError("Player -> Start: Restart button not found");
		restartButton.SetActive(false);
	}

	void Update()
	{
		// handle input
		Command command = inputHandler.HandleInput();
		if(command != null)
		{
			command.Execute(this.gameObject);
		}

		// update score
		int currentHeight = (int) GetComponent<Transform>().position.y;
		if(currentHeight > score)
		{
			score = currentHeight;
			UpdateStats();
		}
	}

	public void GameOver()
	{
		// enable restart game modal
		restartButton.SetActive(true);

		// set player back to original position to stop continual play after game over
		// ---- PROBLEM with this fix: if player dies early, will just respawn instantly and see it ----
		// Reset();

		// updating final score text
		Text finalScoreText = GameObject.FindGameObjectWithTag("FinalScoreText").GetComponent<Text>();
		if(finalScoreText == null)
		{
			Debug.LogError("Player -> GameOver: Final score text not found");
			return;
		}

		finalScoreText.text = "Final Score: " + score;
	}

	public void ResetGame()
	{
		// reset score to zero and update UI
		score = 0;
		UpdateStats();

		// reset gameplay effects
		GetComponent<PlayerBoost>().BoostMeter = 0;
		GetComponent<PlayerBouncer>().Reset();

		// turn off modal
		restartButton.SetActive(false);

		// reset all moving objects in scene
		MovingObject[] movingObjects = FindObjectsOfType<MovingObject>();
		foreach(MovingObject obj in movingObjects)
			obj.Reset();

		// regenerate level
		levelGenerator.ClearEnvironment();
		levelGenerator.GenerateEO(true);
	}

	private void UpdateStats()
	{
		scoreText.text = "Score: " + score;
	}
}
