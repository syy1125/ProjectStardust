using ProjectStardust.WorldSurface.Serialized;
using UnityEngine.Tilemaps;

namespace ProjectStardust.MapEditor.WorldEditor
{
public class CursorBrush : IPaintBrush
{
	public static CursorBrush Instance { get; }

	public Tile PreviewTile => null;

	static CursorBrush()
	{
		Instance = new();
	}

	private CursorBrush()
	{}

	public void Paint(Surface surface, int x, int y)
	{}

	public override bool Equals(object obj)
	{
		return obj == Instance;
	}
}
}