using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPulser : MonoBehaviour {
	public AnimationCurve BounceAnimation;
	private float _timer;
	private float _offset;
	// Use this for initialization
	void Start () {
		_offset = Random.value / 2f;
	}
	
	// Update is called once per frame
	void Update () {
		_timer += Time.deltaTime;
		float newY = BounceAnimation.Evaluate (_timer);
		transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
	}
}
