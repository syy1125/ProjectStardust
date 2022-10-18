using System;
using System.Collections.Generic;
using System.Linq;
using ProjectStardust.Components;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace ProjectStardust.WorldSurface
{
/// <summary>
/// Records all the available tiles, their ordering as it appears in the editor, and their associated tile assets.
/// </summary>
[CreateAssetMenu(fileName = "SurfaceTileDatabase", menuName = "Scriptable Objects/Surface Tile Database", order = 0)]
public class TileTypeDatabase : AbstractDatabaseObject<TileType, TileTypeDatabase.Row>
{
	[Serializable]
	public struct Row : IDatabaseRow<TileType>
	{
		public TileType PrimaryKey => TileType;

		public TileType TileType;
		public Tile TilemapTile;
	}

	public Row[] AvailableTileTypes;

	public override Row[] Rows => AvailableTileTypes;
}
}