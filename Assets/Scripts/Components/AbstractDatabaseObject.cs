using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectStardust.Components
{
public interface IDatabaseRow<out TKey>
{
	TKey PrimaryKey { get; }
}

public abstract class AbstractDatabaseObject<TKey, TRow> : ScriptableObject where TRow : IDatabaseRow<TKey>
{
	public abstract TRow[] Rows { get; }
	private Dictionary<TKey, int> _index;

	protected virtual void OnEnable()
	{
		_index = Rows
			.Select((row, index) => (row, index))
			.ToDictionary(tuple => tuple.row.PrimaryKey, tuple => tuple.index);
	}

	public bool TryGetRow(TKey key, out TRow row)
	{
		if (_index.TryGetValue(key, out int index))
		{
			row = Rows[index];
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