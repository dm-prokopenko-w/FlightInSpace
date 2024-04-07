using System;
using Game;
using UnityEngine;

namespace AsteroidsSystem
{
	[CreateAssetMenu(fileName = "AsteroidsConfig", menuName = "Configs/AsteroidsConfig", order = 0)]
	public class AsteroidsConfig : Config
	{
		[Header("Seconds between asteroid spawns.")]
		[Range(0, 99)] public int SecondsBeforeSpawnMin = 2;
		[Range(1, 100)] public int SecondsBeforeSpawnMax = 3;
		
		[Header("Asteroid prefab.")]
		public Asteroid AsteroidPrefab;
		
		[Header("Asteroid speed.")]
		[Range(0, 4)] public float AsteroidSpeedMin = 2f;
		[Range(1, 5)] public float AsteroidSpeedMax = 3f;

		private void OnValidate()
		{
			if (SecondsBeforeSpawnMin >= SecondsBeforeSpawnMax)
			{
				SecondsBeforeSpawnMin = SecondsBeforeSpawnMax - 1;
			}
			
			if (AsteroidSpeedMin >= AsteroidSpeedMax)
			{
				AsteroidSpeedMin = AsteroidSpeedMax - 1;
			}
		}
	}
}
