using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horn : MonoBehaviour
{
	[SerializeField]
	private AnimationCurve m_Animation;

	[SerializeField]
	private float m_AnimationTime = 1f;

	private float mAnimateStartTime;
	private bool mIsAnimating = false;
	private static Horn Instance;


	private void Start()
	{
		Instance = this;
		m_AnimationTime = -m_AnimationTime;
	}

	public static void TriggerAnimation()
	{
		if (Instance.mIsAnimating)
			return;

		Instance.mAnimateStartTime = Time.time;
		Instance.mIsAnimating = true;
	}

	private void Update()
	{
		if (!mIsAnimating)
			return;

		float ratio = Mathf.Clamp((Time.time - mAnimateStartTime) / m_AnimationTime, 0f, 1f);
		float animValue = 1f + m_Animation.Evaluate(ratio);

		transform.localScale = new Vector3(animValue, animValue, animValue);

		if (ratio >= 1f)
		{
			mIsAnimating = false;
		}
	}
}