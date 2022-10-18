using System.Collections.Generic;
using ProjectStardust.Editor.PropertyDrawers;
using UnityEngine.InputSystem;

namespace ProjectStardust.Components
{
public class ActionMapControl : SceneSingletonBehaviour
{
	public static ActionMapControl Instance { get; private set; }

	public InputActionAsset ActionsAsset;

	[ActionMapPicker(nameof(ActionsAsset))]
	public string[] InitialEnabledMaps;

	private HashSet<string> _enabledMaps;

	private void Start()
	{
		_enabledMaps = new(InitialEnabledMaps);
		RefreshActionMaps();
	}

	public void EnableMap(string mapName)
	{
		_enabledMaps.Add(mapName);
		RefreshActionMaps();
	}

	public void DisableMap(string mapName)
	{
		_enabledMaps.Remove(mapName);
		RefreshActionMaps();
	}

	private void RefreshActionMaps()
	{
		foreach (InputActionMap map in ActionsAsset.actionMaps)
		{
			// With the input consumption system, the order in which action maps are enabled seem to make a difference.
			// So we always disable the maps before re-enabling them to ensure a consistent order.
			map.Disable();

			if (_enabledMaps.Contains(map.name))
			{
				map.Enable();
			}
		}
	}
}
}