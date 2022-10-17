using ProjectStardust.WorldSurface;
using ProjectStardust.WorldSurface.Serialized;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProjectStardust.MapEditor.WorldEditor
{
public class WorldEditor : MonoBehaviour
{
	[Header("References")]
	public SurfaceTileDatabase SurfaceTileDatabase;
	public Tilemap SurfaceTilemap;
	public Camera SurfaceCamera;

	private Surface _surface;

	private void Start()
	{
		_surface = Surface.CreateDefault();

		SurfaceTilemap.ClearAllTiles();

		for (int x = 0; x < _surface.Width; x++)
		{
			for (int y = 0; y < _surface.Height; y++)
			{
				if (SurfaceTileDatabase.TryFind(_surface.GetTileTypeAt(x, y), out var row))
				{
					SurfaceTilemap.SetTile(new(x, y, 0), row.TilemapTile);
				}
			}
		}

		SurfaceCamera.orthographicSize = Mathf.Min(_surface.Width, _surface.Height) / 2f;
		SurfaceCamera.transform.localPosition = new(_surface.Width / 2f, _surface.Height / 2f, -10);
	}
}
}