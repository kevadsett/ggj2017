using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogView : MonoBehaviour {
	public AnimationCurve SpeedCurve;
	public AnimationCurve HeightCurve;

	public float CharacterBounceHeight;

	private Vector3 _lastPos;
	private Vector3 _newPos;
	private Quaternion _lastRot;
	private Quaternion _newRot;
	private float _timer;

	public void InitAtPoint (Vector3 point)
	{
		_lastPos = _newPos = point;
		_timer = 0.0f;
	}

	public void MoveToPoint(Vector3 point)
	{
		_lastPos = transform.localPosition;
		_newPos = point;
		_timer = 0.0f;

		_lastRot = transform.localRotation;
		_newRot = Quaternion.LookRotation( Vector3.Cross (_lastPos - _newPos, Vector3.up));
	}

	void Update () {
		_timer += Time.deltaTime / GameDataBase.Instance.GetData().DogMoveDuration;
		_timer = Mathf.Clamp01 (_timer);

		float v = SpeedCurve.Evaluate (_timer);
		float bounce = CharacterBounceHeight * HeightCurve.Evaluate (v);

		transform.localPosition = Vector3.Lerp (_lastPos, _newPos, v);
		transform.localPosition += new Vector3 (0.0f, bounce, 0.0f);

		transform.localRotation = Quaternion.Slerp (_lastRot, _newRot, v);
	}
}
