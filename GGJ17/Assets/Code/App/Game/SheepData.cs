using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SheepData : ScriptableObject {
	private int _id;
	public int ID { get { return _id; } }
	public string Name;
	public string Dates;
	public string EulogyText;
	public string FinalText;

	public void SetID(int id)
	{
		_id = id;
	}
}
