using UnityEngine;

public class SheepEntity : Entity
{
	private RollingCube viewCube;

	public SheepEntity (int zId, int zX, int zZ) : base (zId, zX, zZ)
	{
		var gameData = GameDataBase.Instance.GetData (0);
		var prefab = gameData.SheepPrefab;

		var obj = Object.Instantiate (prefab, new Vector3 (zX, 0.0f, zZ), Quaternion.identity);

		viewCube = obj.GetComponent <RollingCube>();
	}

	protected override void Destroy ()
	{
		Object.Destroy (viewCube);

		base.Destroy ();
	}
}