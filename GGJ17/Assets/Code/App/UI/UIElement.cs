using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
	[SerializeField]
	private Game.eState m_ActiveState;


	public void Enable(Game.eState zNewState)
	{
		gameObject.SetActive(zNewState == m_ActiveState);
	}
}