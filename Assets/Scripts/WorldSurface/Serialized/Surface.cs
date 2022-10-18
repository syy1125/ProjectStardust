using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ProjectStardust.WorldSurface.Serialized
{
[Serializable]
public class Surface
{
	public int Width;
	public int Height;
	public TileType[] TileTypes;
	public int[] ResourceLevels;

	private Surface()
	{}

	public static Surface CreateDefault()
	{
		return new()
		{
			Width = 5,
			Height = 5,
			TileTypes = new TileType[25],
			ResourceLevels = new int[25],
		};
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int WrapIndex(int x, int y, int width, int height)
	{
		return x + y * width;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static (int X, int Y) UnwrapIndex(int i, int width, int height)
	{
		return (i % width, i / width);
	}

	public void Resize(int width, int height)
	{
		var tileTypes = new TileType[width * height];
		var resourceLevels = new int[width * height];
		int copyWidth = Mathf.Min(width, Width);

		for (int y = 0; y < height && y < Height; y++)
		{
			Array.Copy(TileTypes, y * Width, tileTypes, y * width, copyWidth);
			Array.Copy(ResourceLevels, y * Width, resourceLevels, y * width, copyWidth);
		}
	}

	public TileType GetTileTypeAt(int x, int y)
	{
		return TileTypes[WrapIndex(x, y, Width, Height)];
	}

	public void SetTileTypeAt(int x, int y, TileType tileType)
	{
		TileTypes[WrapIndex(x, y, Width, Height)] = tileType;
	}

	public int GetResourceLevelAt(int x, int y)
	{
		return ResourceLevels[WrapIndex(x, y, Width, Height)];
	}

	public void SetResourceLevelAt(int x, int y, int resourceLevel)
	{
		ResourceLevels[WrapIndex(x, y, Width, Height)] = resourceLevel;
	}
}
}