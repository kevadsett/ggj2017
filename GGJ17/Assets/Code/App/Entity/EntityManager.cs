using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager
{
	private static EntityManager Instance;

	private List<Entity> mEntities;

	public EntityManager()
	{
		Instance = this;

		mEntities = new List<Entity>();
	}

	public static Entity GetEntityAtPosition(int zX, int zZ)
	{
		for (int i = 0; i < Instance.mEntities.Count; i++)
		{
			var entity = Instance.mEntities[i];

			if (entity.PosX == zX && entity.PosZ == zZ)
			{
				return entity;
			}
		}

		return null;
	}

	public static int GetSheepCount()
	{
		int count = 0;

		foreach (Entity entity in Instance.mEntities)
		{
			if (entity is SheepEntity)
			{
				count++;
			}
		}

		return count;
	}

	public static List<SheepData> DrownSheep()
	{
		List<Entity> drownedSheep = new List<Entity>();
		List<SheepData> sheepThatDrowned = new List<SheepData> ();

		foreach (Entity entity in Instance.mEntities)
		{
			var sheep = entity as SheepEntity;

			if (sheep != null)
			{
				if (!sheep.IsSafe())
				{
					drownedSheep.Add(sheep);
					sheepThatDrowned.Add (sheep.Data);
				}
			}
		}

		foreach (SheepEntity sheep in drownedSheep)
		{
			AudioPlayer.PlaySound("SheepDeath", Vector3.zero);
			sheep.Destroy();
		}
		return sheepThatDrowned;
	}

	public static void RegisterEntity(Entity zEntity)
	{
		Instance.mEntities.Add(zEntity);
	}

	public static void DeregisterEntity(Entity zEntity)
	{
		Instance.mEntities.Remove(zEntity);
	}

	public static void UpdateEntities(Game.eState zGameState)
	{
		for (int i = 0; i < Instance.mEntities.Count; i++)
		{
			Instance.mEntities[i].Update(zGameState);
		}
	}
}