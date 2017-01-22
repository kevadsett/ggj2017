﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepParticle : MonoBehaviour
{
	[SerializeField]
	private float m_LifeTime = 1.5f;


	[SerializeField]
	private AnimationCurve m_CurveX;

	[SerializeField]
	private AnimationCurve m_CurveZ;


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

		newPosition.x += m_CurveX.Evaluate(ratio) * 0.25f;
		newPosition.z += m_CurveZ.Evaluate(ratio) * 0.5f;

		transform.position = newPosition;
	}
}