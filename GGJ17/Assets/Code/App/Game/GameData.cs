using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameData : ScriptableObject {
	public Vector2 GridDimensions;
	public GameObject SheepPrefab;
	public GameObject DogPrefab;
	public float DogMoveDuration;
	public float FreeMoveThreshold;
	public float SheepMoveDuration;
	public int SheepObediencePercent;
	public float WaveSpeed;
	public float TimeToAnimateWave;
	public float RestartTimerMultiplier;
}
