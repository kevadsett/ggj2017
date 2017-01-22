using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public float speed;
	public float amount;
	public AnimationCurve curve;

	private static Timer instance;
	private float timer;

	private void Awake ()
	{
		instance = this;
	}

	private void Update ()
	{
		timer += Time.deltaTime * speed;
		timer = Mathf.Clamp01 (timer);

		transform.localScale = Vector3.one * (1.0f + curve.Evaluate (timer) * amount);
	}

	public static void Tick ()
	{
		instance.timer = 0.0f;
	}
}