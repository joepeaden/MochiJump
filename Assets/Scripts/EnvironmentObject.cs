using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
	[SerializeField]
	protected int boostValue = 1;
	[SerializeField]
	protected int pointValue;

	// delegate for updating level generator when platform is touched
	public delegate void Callback(int value);
	public Callback UpdateEOTouched;

	protected bool touched;

	protected GameObject parentGO;

	public AudioSource highping;
	public AudioSource lowping;

	public UIManager ui;

	public enum EOType
	{
		Platform,
		Bumper
	}
	public EOType type;

	[Header("Spawning Information")]
	[SerializeField]
	private float[] spawnChances;
	public float[] SpawnChances
    {
		get => spawnChances;
    }

	protected void Start()
	{
		// set up this way cause need platform to be solid but not bounce players off sides, for example
		parentGO = transform.parent.gameObject;
	}

	protected void PingFeedback(bool firstTime=false)
	{
		if (firstTime)
		{
			parentGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
			highping.Play();
			ui.EOPointNotification(pointValue);
		}
		else
        {
			lowping.Play();
        }
	}

	// tells if this object is active in the current level (based on if there's a spawn chance for it or not)
	public bool ActiveInLevel(int level)
    {
		if (level < spawnChances.Length)
			return true;
		return false;
    }
}
