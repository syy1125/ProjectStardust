using System;
using ProjectStardust.WorldSurface;
using ProjectStardust.WorldSurface.Serialized;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace ProjectStardust.MapEditor.WorldEditor
{
public class WorldEditor : MonoBehaviour
{
	[Header("Assets")]
	public InputActionReference CursorAction;
	public InputActionReference[] TileTypeActions;
	public InputActionReference[] ResourceLevelActions;

	public TileTypeDatabase TileTypeDatabase;
	public ResourceLevelDatabase ResourceLevelDatabase;
	public GameObject SurfaceTileButtonPrefab;

	[Header("References")]
	public Transform SurfaceTileButtonsParent;
	public Tilemap SurfaceTilemap;
	public Tilemap ResourceLevelTilemap;
	public Camera SurfaceCamera;

	private Surface _surface;
	private IPaintBrush _brush;

	private void Start()
	{
		_surface = Surface.CreateDefault();
		_brush = CursorBrush.Instance;

		SurfaceTilemap.ClearAllTiles();

		for (int x = 0; x < _surface.Width; x++)
		{
			for (int y = 0; y < _surface.Height; y++)
			{
				UpdateTilemapFromSurface(x, y);
			}
		}

		SetupBrushButton(CursorBrush.Instance, out var selectCursor);

		if (CursorAction.action != null)
		{
			CursorAction.action.performed += _ => selectCursor();
		}

		for (int i = 0; i < TileTypeDatabase.Rows.Length; i++)
		{
			var row = TileTypeDatabase.Rows[i];
			var brush = new TileTypeBrush(row.TileType, row.TilemapTile);

			SetupBrushButton(brush, out var selectBrush);

			if (i < TileTypeActions.Length && TileTypeActions[i].action != null)
			{
				TileTypeActions[i].action.performed += _ => selectBrush();
			}
		}

		for (int i = 0; i < ResourceLevelDatabase.Rows.Length; i++)
		{
			var row = ResourceLevelDatabase.Rows[i];
			var brush = new ResourceLevelBrush(row.ResourceLevel, row.PreviewTile);

			SetupBrushButton(brush, out var selectBrush);

			if (i < ResourceLevelActions.Length && ResourceLevelActions[i].action != null)
			{
				ResourceLevelActions[i].action.performed += _ => selectBrush();
			}
		}

		SurfaceCamera.orthographicSize = Mathf.Min(_surface.Width, _surface.Height) / 2f;
		SurfaceCamera.transform.localPosition = new(_surface.Width / 2f, _surface.Height / 2f, -10);
	}

	private void SetupBrushButton(IPaintBrush brush, out Action selectBrush)
	{
		void SelectBrush()
		{
			if (brush.Equals(_brush)) return;
			_brush = brush;
		}

		var button = Instantiate(SurfaceTileButtonPrefab, SurfaceTileButtonsParent);
		button.GetComponent<Button>().onClick.AddListener(SelectBrush);

		if (brush.PreviewTile != null)
		{
			button.GetComponent<Image>().sprite = brush.PreviewTile.sprite;
		}
		else
		{
			button.GetComponent<Image>().enabled = false;
		}

		selectBrush = SelectBrush;
	}

	private void UpdateTilemapFromSurface(int x, int y)
	{
		if (TileTypeDatabase.TryGetRow(_surface.GetTileTypeAt(x, y), out var tileTypeRow))
		{
			SurfaceTilemap.SetTile(new(x, y, 0), tileTypeRow.TilemapTile);
		}

		if (ResourceLevelDatabase.TryGetRow(_surface.GetResourceLevelAt(x, y), out var resourceLevelRow))
		{
			ResourceLevelTilemap.SetTile(new(x, y, 0), resourceLevelRow.TilemapTile);
		}
	}
}
}