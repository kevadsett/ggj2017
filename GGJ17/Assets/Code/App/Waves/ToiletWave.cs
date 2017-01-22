using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletWave : MonoBehaviour
{
	[SerializeField]
	private AnimationCurve m_InCurve;

	[SerializeField]
	private AnimationCurve m_OutCurve;

	[SerializeField]
	private Transform m_OutPosition;

	[SerializeField]
	private Transform m_InPosition;


	private enum eState
	{
		Incoming,
		Outgoing,
		Done,
	}


	private static ToiletWave Instance;

	private float mTimeStartedWave;
	private float mTimeToAnimate = 2f;
	private eState mWaveState;


	private void Start()
	{
		Instance = this;
		mWaveState = eState.Done;
	}

	public static void TriggerWave()
	{
		Instance.mTimeStartedWave = Time.time;
		Instance.mWaveState = eState.Incoming;
	}

	public static bool IsDone()
	{
		return (Instance.mWaveState == eState.Done);
	}

	private void Update()
	{
		float ratio = Mathf.Clamp((Time.time - mTimeStartedWave) / mTimeToAnimate, 0f, 1f);

		switch (mWaveState)
		{
		case eState.Done:
			transform.position = m_OutPosition.position;
			break;
		case eState.Incoming:
		{
			var animValue = m_InCurve.Evaluate(ratio);
			var newPosition = (m_InPosition.position - m_OutPosition.position)*animValue + m_OutPosition.position;
			transform.position = newPosition;

			if (ratio >= 1f)
			{
				mWaveState = eState.Outgoing;
				mTimeStartedWave = Time.time;
				//flush the sheep here
			}
			break;
		}
		case eState.Outgoing:
		{
			var animValue = m_OutCurve.Evaluate(ratio);
			var newPosition = (m_OutPosition.position - m_InPosition.position)*animValue + m_InPosition.position;
			transform.position = newPosition;
			break;
		}
		}
	}
}