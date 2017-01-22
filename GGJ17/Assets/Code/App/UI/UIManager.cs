using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager Instance;

	[SerializeField]
	private UIElement[] m_UIElements;

	[SerializeField]
	private Letterbox[] m_Letterboxes;

	[SerializeField]
	private Text m_Timer;


	private void Awake()
	{
		Instance = this;
	}

	public static void UpdateUI(Game.eState zGameState, float zTimer)
	{
		foreach (UIElement element in Instance.m_UIElements)
		{
			element.Enable(zGameState);
		}

		if (zGameState == Game.eState.Game)
		{
			Instance.UpdateLetterBoxes();
			Instance.UpdateTimer(zTimer);
		}
	}

	private void UpdateLetterBoxes()
	{
		foreach (Letterbox letterBox in m_Letterboxes)
		{
			letterBox.StartAnimation();
		}
	}

	private void UpdateTimer(float zTimer)
	{
		var prettyTimer = Mathf.Clamp(zTimer, 0f, float.MaxValue);
		m_Timer.text = "Wave incoming in " + prettyTimer.ToString("0.00");
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