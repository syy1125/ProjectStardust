using ProjectStardust.WorldSurface.Serialized;
using UnityEngine.Tilemaps;

namespace ProjectStardust.MapEditor.WorldEditor
{
public class ResourceLevelBrush : IPaintBrush
{
	public Tile PreviewTile { get; }
	private readonly int _resourceLevel;

	public ResourceLevelBrush(int resourceLevel, Tile previewTile)
	{
		_resourceLevel = resourceLevel;
		PreviewTile = previewTile;
	}

	public void Paint(Surface surface, int x, int y)
	{
		surface.SetResourceLevelAt(x, y, _resourceLevel);
	}

	public override bool Equals(object obj)
	{
		return obj is ResourceLevelBrush brush && brush._resourceLevel == _resourceLevel;
	}

	public override int GetHashCode()
	{
		return _resourceLevel;
	}
}
}