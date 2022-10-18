using ProjectStardust.WorldSurface.Serialized;
using UnityEngine.Tilemaps;

namespace ProjectStardust.MapEditor.WorldEditor
{

public interface IPaintBrush
{
	Tile PreviewTile { get; }
	void Paint(Surface surface, int x, int y);
}
}