using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashAnimation : MonoBehaviour
{
	[SerializeField]
	private AnimationCurve m_AppearCurve;

	[SerializeField]
	private AnimationCurve m_HideCurve;

	[SerializeField]
	private float m_AnimationTime = 1f;


	private static SplashAnimation Instance;

	private Image mImage;
	private float mTimeStartedAnimation;
	private bool mAppearing;


	private void Start()
	{
		Instance = this;
		mTimeStartedAnimation = -m_AnimationTime;
		mImage = gameObject.GetComponent<Image>();
	}

	public static void Appear()
	{
		Instance.mAppearing = true;
		Instance.mTimeStartedAnimation = Time.time;
	}

	public static void Hide()
	{
		Instance.mAppearing = false;
		Instance.mTimeStartedAnimation = Time.time;
	}

	private void Update()
	{
		float ratio = (Time.time - mTimeStartedAnimation) / m_AnimationTime;

		if (mAppearing)
		{
			float animValue = m_AppearCurve.Evaluate(ratio);
			mImage.color = new Color(1, 1, 1, animValue);
		}
		else
		{
			float animValue = m_HideCurve.Evaluate(ratio);
			mImage.color = new Color(1, 1, 1, animValue);
		}
	}
}