using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letterbox : MonoBehaviour
{
	[SerializeField]
	private bool m_GoUp;

	[SerializeField]
	private float m_AnimateTime = 1f;

	private float mAnimateStartTime;
	private RectTransform mRect;

	private void Start()
	{
		mRect = transform as RectTransform;
	}

	public void Animate()
	{
		mAnimateStartTime = Time.time;
	}

	private void Update()
	{
		float ratio = Mathf.Clamp((Time.time - mAnimateStartTime) / m_AnimateTime, 0f, 1f);

		float moveValue = 0;
		if (m_GoUp)
		{
			moveValue = ratio * 150f;
		}
		else
		{
			moveValue = -ratio * 150f;
		}

		Rect newRect = mRect.rect;

		newRect.yMin = moveValue;
		newRect.yMax = moveValue;

		mRect.offsetMax = new Vector2(0, moveValue);
		//mRect.rect = newRect;
	}
}