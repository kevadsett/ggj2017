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

	[SerializeField]
	private Image m_EulogyBackground;

	[SerializeField]
	private GameObject m_EulogyRoot;

	[SerializeField]
	private Text m_SheepScore;

	[SerializeField]
	private Image m_KeyboardInstruction;


	private bool m_isHidingKeyboardInstruction = false;
	private float m_keyboardInstructionHideStartTime;
	private AnimationCurve m_InstructionFadeCurve;

	private void Awake()
	{
		Instance = this;
	}

	public static void UpdateUI(Game.eState zGameState, float zTimer, bool hideKeyboardInstruction = false, AnimationCurve instructionUIFadeCurve = null)
	{
		foreach (UIElement element in Instance.m_UIElements)
		{
			element.Enable(zGameState);
		}

		if (zGameState == Game.eState.Game)
		{
			Instance.UpdateLetterBoxes();
			Instance.UpdateTimer(zTimer);
			Instance.UpdateScore();
			if (hideKeyboardInstruction)
			{
				if (!Instance.m_isHidingKeyboardInstruction)
				{
					Instance.m_keyboardInstructionHideStartTime = Time.time;
					Instance.m_isHidingKeyboardInstruction = true;
					Instance.m_InstructionFadeCurve = instructionUIFadeCurve;
				}
				Instance.HideKeyboardInstruction ();
			}
		}

		if (zGameState == Game.eState.GameEnd)
		{
			var bgColor = Instance.m_EulogyBackground.color;
			bgColor.a = Mathf.Lerp(bgColor.a, 1f, Time.deltaTime);
			Instance.m_EulogyBackground.color = bgColor;
			Instance.m_EulogyRoot.transform.Translate(Vector2.up * 60f * Time.deltaTime, Space.World);
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

	private void UpdateScore()
	{
		m_SheepScore.text = "Sheep Points: " + Score.Instance.CurrentScore;
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

	public void NextLevel()
	{
		Game.StartNextLevel ();
	}

	public void HideKeyboardInstruction()
	{
		Color colour = m_KeyboardInstruction.color;
		float timeSinceHideCall = Time.time - m_keyboardInstructionHideStartTime;
		float alphaValue = m_InstructionFadeCurve.Evaluate(timeSinceHideCall);
		m_KeyboardInstruction.color = new Color (colour.r, colour.g, colour.b, alphaValue);
	}
}