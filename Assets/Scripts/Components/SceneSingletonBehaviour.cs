using System.Reflection;
using UnityEngine;

namespace ProjectStardust.Components
{
/// <summary>
/// A scene-level singleton. Expects and uses a static property named <c>Instance</c> on inheriting classes.
/// </summary>
public abstract class SceneSingletonBehaviour : MonoBehaviour
{
	private PropertyInfo _instance;

	protected virtual void Awake()
	{
		_instance = GetType().GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
		Debug.Assert(_instance != null, "_instance != null");

		if (_instance.GetValue(null) == null)
		{
			_instance.SetValue(null, this);
		}
		else
		{
			Debug.LogError($"Multiple instantiations of {GetType()}");
			Destroy(this);
		}
	}

	protected virtual void OnDestroy()
	{
		if (ReferenceEquals(_instance.GetValue(null), this))
		{
			_instance.SetValue(null, null);
		}
	}
}
}