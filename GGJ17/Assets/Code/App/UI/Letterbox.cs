using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letterbox : MonoBehaviour
{
	[SerializeField]
	private bool m_GoUp;

	[SerializeField]
	private float m_AnimateTime = 1f;

	[SerializeField]
	private AnimationCurve m_Curve;

	private float mAnimateStartTime;
	private RectTransform mRect;
	private bool mShouldAnimate = false;
	private bool mHasAnimated = false;

	private void Start()
	{
		mRect = transform as RectTransform;
	}

	public void StartAnimation()
	{
		if (mHasAnimated)
			return;

		mHasAnimated = true;
		mAnimateStartTime = Time.time;
		mShouldAnimate = true;
	}

	private void Update()
	{
		if (!mShouldAnimate)
			return;

		float ratio = Mathf.Clamp((Time.time - mAnimateStartTime) / m_AnimateTime, 0f, 1f);

		float moveAmount = 150f;
		float moveTop = 0;
		float moveBottom = 0;
		float animValue = m_Curve.Evaluate(ratio);

		if (m_GoUp)
		{
			moveTop = animValue * moveAmount;
			moveBottom = animValue * moveAmount;
		}
		else
		{
			moveTop = -animValue * moveAmount;
			moveBottom = -animValue * moveAmount;
		}

		mRect.offsetMin = new Vector2(mRect.offsetMin.x, moveBottom);
		mRect.offsetMax = new Vector2(mRect.offsetMax.x, moveTop);
	}
}