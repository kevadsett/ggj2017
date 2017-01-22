using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Particle;

	[SerializeField]
	private float m_AnimationTime = 1f;

	private float mLastCreationTime;


	private void Start()
	{
		mLastCreationTime = -m_AnimationTime;
	}

	private void Update()
	{
		if (Time.time > mLastCreationTime + m_AnimationTime)
		{
			mLastCreationTime = Time.time - Random.Range(0f, 0.12f);
			var newParticle = GameObject.Instantiate(m_Particle);
			newParticle.transform.position = transform.position;
		}
	}
}