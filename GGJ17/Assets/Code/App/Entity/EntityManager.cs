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
		foreach (Entity entity in Instance.mEntities)
		{
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
}