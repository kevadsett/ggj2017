using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	private static UIManager Instance;

	[SerializeField]
	private UIElement[] m_UIElements;


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
	}

	public void StartGame()
	{
		//send event to change game state
		Game.SetupGame();
	}

	public void GoToMenu()
	{
		//go back to menu
		Game.ShowMainMenu();
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}