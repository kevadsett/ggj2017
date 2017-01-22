using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
	public static Score Instance;

	public int CurrentScore { get; private set; }


	public Score()
	{
		CurrentScore = 0;
		Instance = this;
	}

	public void AddScore(int zIncrement)
	{
		CurrentScore += zIncrement;
	}
}