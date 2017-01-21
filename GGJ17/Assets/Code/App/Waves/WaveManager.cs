using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager {

	float lastWave, waveTime;
	float timeToNextWave = 5;
	bool wave = false;

	private List<Wave> waves;

	public WaveManager() {
		lastWave = Time.time;
		waves = new List<Wave> ();
	}

	public void Update () { 
		/*if (Time.time >= lastWave + timeToNextWave) {
			waves.Add (new Wave (1));
			lastWave = Time.time;
		}*/
		List<Wave> doneWaves = new List<Wave> ();
		foreach (Wave w in waves) {
			w.Update ();
			if (w.done) {
				doneWaves.Add (w);
			}
		}
		foreach (Wave w in doneWaves) {
			waves.Remove (w);
		}

	}

	void CreateWave() {
	}
}
