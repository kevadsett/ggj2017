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


	public static ToiletWave Instance;


	private List<RollingCube> mCarriedSheep;
	private float mTimeStartedWave;
	private float mTimeToAnimate;
	private eState mWaveState;
	private bool mWaveResolved = false;

	private void Start()
	{
		mCarriedSheep = new List<RollingCube>();
		mTimeToAnimate = GameDataBase.Instance.GetData(0).TimeToAnimateWave * 0.5f;
		Instance = this;
		mWaveState = eState.Done;
	}

	public static void TriggerWave()
	{
		Instance.mTimeStartedWave = Time.time;
		Instance.mWaveState = eState.Incoming;
		Instance.mWaveResolved = false;
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

			foreach (RollingCube sheep in mCarriedSheep)
			{
				GameObject.Destroy(sheep.gameObject);
			}
			mCarriedSheep.Clear();
			break;
		case eState.Incoming:
		{
			var animValue = m_InCurve.Evaluate(ratio);
			var newPosition = (m_InPosition.position - m_OutPosition.position)*animValue + m_OutPosition.position;
			transform.position = newPosition;
			
			if (ratio >= 0.7 && !mWaveResolved)
			{
				mWaveResolved = true;
				Game.ResolveWave();
			}

			if (ratio >= 1f)
			{
				mWaveState = eState.Outgoing;
				mTimeStartedWave = Time.time;
			}
			break;
		}
		case eState.Outgoing:
		{
			var animValue = m_OutCurve.Evaluate(ratio);
			var newPosition = (m_OutPosition.position - m_InPosition.position)*animValue + m_InPosition.position;
			transform.position = newPosition;
			if (ratio >= 1f)
			{
				mWaveState = eState.Done;
				Game.FinishWave ();
			}
			break;
		}
		}
	}

	public void CarrySheep(RollingCube zSheep)
	{
		mCarriedSheep.Add(zSheep);
	}
}