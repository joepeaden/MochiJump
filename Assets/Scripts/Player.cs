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
	private LevelGenerator levelGenerator;

	[SerializeField]
	private UIManager ui;

	private float maxHeight;

	void Start()
	{
		inputHandler = GetComponent<InputHandler>();

		levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
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
		if(currentHeight > maxHeight)
		{
			// add difference to score
			score += (int) (currentHeight - maxHeight);

			maxHeight = currentHeight;


			ui.UpdateStats(score);
		}
	}

	public void GameOver()
	{
		ui.GameOver(score);
	}

	public void ResetGame()
	{
		// reset score to zero and update UI
		score = 0;
		ui.UpdateStats(score);

		// reset gameplay effects
		GetComponent<PlayerBoost>().BoostMeter = 0;
		GetComponent<PlayerBouncer>().Reset();

		ui.Reset();

		// reset all moving objects in scene
		MovingObject[] movingObjects = FindObjectsOfType<MovingObject>();
		foreach(MovingObject obj in movingObjects)
			obj.Reset();

		// regenerate level
		levelGenerator.ClearEnvironment();
		levelGenerator.GenerateEO(true);
	}

	public void AddPoints(int points)
    {
		score += points;
		ui.UpdateStats(score);
    }
}
