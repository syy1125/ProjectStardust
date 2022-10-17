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
	public string[] EnabledMaps;

	private void Start()
	{
		HashSet<string> mapNames = new(EnabledMaps);

		foreach (InputActionMap map in ActionsAsset.actionMaps)
		{
			// With the input consumption system, the order in which action maps are enabled seem to make a difference.
			// So we always disable the maps before re-enabling them to ensure a consistent order.
			map.Disable();

			if (mapNames.Contains(map.name))
			{
				map.Enable();
			}
		}
	}
}
}