using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eulogy : MonoBehaviour
{
	[SerializeField]
	private Text m_NameField;

	[SerializeField]
	private Text m_DateField;

	[SerializeField]
	private Text m_SummaryField;

	[SerializeField]
	private Text m_FateField;


	public void SetData(SheepData zData)
	{
		m_NameField.text = zData.name;
		m_DateField.text = zData.Dates;
		m_SummaryField.text = zData.EulogyText;
		m_FateField.text = zData.FinalText;
	}
}