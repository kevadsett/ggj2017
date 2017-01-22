using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEulogy : MonoBehaviour
{
	private static ResetEulogy Instance;


	private void Awake()
	{
		Instance = this;
	}

	public static void ResetPosition()
	{
		RectTransform rect = Instance.transform as RectTransform;
		rect.localPosition = new Vector3(0, -400, 0);

		MusicPlayer.Play (1);
	}
}