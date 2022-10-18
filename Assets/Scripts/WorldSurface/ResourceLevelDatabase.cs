using System;
using ProjectStardust.Components;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace ProjectStardust.WorldSurface
{
[CreateAssetMenu(
	fileName = "ResourceLevelDatabase", menuName = "Scriptable Objects/Resource Level Database", order = 0
)]
public class ResourceLevelDatabase : AbstractDatabaseObject<int, ResourceLevelDatabase.Row>
{
	[Serializable]
	public struct Row : IDatabaseRow<int>
	{
		public int PrimaryKey => ResourceLevel;

		public int ResourceLevel;
		public Tile TilemapTile;
		public Tile PreviewTile;
	}

	public Row[] AvailableResourceLevels;

	public override Row[] Rows => AvailableResourceLevels;
}
}