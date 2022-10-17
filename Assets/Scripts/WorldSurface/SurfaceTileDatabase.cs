using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProjectStardust.WorldSurface
{
/// <summary>
/// Records all the available tiles, their ordering as it appears in the editor, and their associated tile assets.
/// </summary>
[CreateAssetMenu(fileName = "SurfaceTileDatabase", menuName = "Scriptable Objects/Surface Tile Database", order = 0)]
public class SurfaceTileDatabase : ScriptableObject
{
	[Serializable]
	public struct Row
	{
		public TileType TileType;
		public Tile TilemapTile;
	}

	public Row[] AvailableTiles;

	private Dictionary<TileType, int> _tileTypeIndex;

	private void OnEnable()
	{
		_tileTypeIndex = AvailableTiles
			.Select((row, index) => (row, index))
			.ToDictionary(tuple => tuple.row.TileType, tuple => tuple.index);
	}

	public bool TryFind(TileType tileType, out Row row)
	{
		if (_tileTypeIndex.TryGetValue(tileType, out int index))
		{
			row = AvailableTiles[index];
			return true;
		}
		else
		{
			row = default;
			return false;
		}
	}
}
}