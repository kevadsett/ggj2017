using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPulser : MonoBehaviour {
	public AnimationCurve BounceAnimation;
	private float _timer;
	
	void Update () {
		_timer += Time.deltaTime;
		float newY = BounceAnimation.Evaluate (_timer);
		transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
	}
}
