using ProjectStardust.WorldSurface;
using ProjectStardust.WorldSurface.Serialized;
using UnityEngine.Tilemaps;

namespace ProjectStardust.MapEditor.WorldEditor
{
public class TileTypeBrush : IPaintBrush
{
	public Tile PreviewTile { get; }
	private readonly TileType _tileType;

	public TileTypeBrush(TileType tileType, Tile previewTile)
	{
		_tileType = tileType;
		PreviewTile = previewTile;
	}

	public void Paint(Surface surface, int x, int y)
	{
		surface.SetTileTypeAt(x, y, _tileType);
	}

	public override bool Equals(object obj)
	{
		return obj is TileTypeBrush brush && brush._tileType == _tileType;
	}

	public override int GetHashCode()
	{
		return (int) _tileType;
	}
}
}