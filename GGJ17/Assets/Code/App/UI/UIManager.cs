using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	private static UIManager Instance;

	[SerializeField]
	private UIElement[] m_UIElements;

	[SerializeField]
	private Letterbox[] m_Letterboxes;


	private void Awake()
	{
		Instance = this;
	}

	public static void UpdateUI(Game.eState zGameState)
	{
		foreach (UIElement element in Instance.m_UIElements)
		{
			element.Enable(zGameState);
		}

		if (zGameState == Game.eState.Game)
		{
			Instance.UpdateLetterBoxes();
		}
	}

	private void UpdateLetterBoxes()
	{
		foreach (Letterbox letterBox in m_Letterboxes)
		{
			letterBox.StartAnimation();
		}
	}

	public void StartGame()
	{
		//go back to menu
		Game.StartGame();
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}