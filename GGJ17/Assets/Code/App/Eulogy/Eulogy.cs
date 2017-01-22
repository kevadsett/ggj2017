﻿using System.Collections;
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


	public void SetData(EulogyData zData)
	{
		m_NameField.text = zData.Name;
		m_DateField.text = zData.Date;
		m_SummaryField.text = zData.Summary;
		m_FateField.text = "Buried At Sea";
	}
}