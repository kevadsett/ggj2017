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