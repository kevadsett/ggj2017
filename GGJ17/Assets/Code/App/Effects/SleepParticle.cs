using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepParticle : MonoBehaviour
{
	[SerializeField]
	private float m_LifeTime = 1.5f;

	[SerializeField]
	private AnimationCurve m_Curve;


	private float mStartTime;


	private void Start()
	{
		mStartTime = Time.time;
	}

	private void Update()
	{
		float ratio = (Time.time - mStartTime) / m_LifeTime;

		if (ratio >= 1f)
		{
			GameObject.Destroy(gameObject);
		}

		var newPosition = transform.position;



		transform.position = newPosition;
	}
}