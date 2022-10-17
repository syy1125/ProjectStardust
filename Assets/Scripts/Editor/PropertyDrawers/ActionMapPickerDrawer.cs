using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using System;
using UnityEditor;
#endif

namespace ProjectStardust.Editor.PropertyDrawers
{
public class ActionMapPickerAttribute : PropertyAttribute
{
	public readonly string ActionAssetName;

	public ActionMapPickerAttribute(string actionAssetName)
	{
		ActionAssetName = actionAssetName;
	}
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ActionMapPickerAttribute))]
public class ActionMapPickerDrawer : PropertyDrawer
{
	public override void OnGUI(
		Rect position, SerializedProperty property, GUIContent label
	)
	{
		if (property.propertyType != SerializedPropertyType.String) return;
		var actionAsset = property.serializedObject
			.FindProperty(((ActionMapPickerAttribute) attribute).ActionAssetName)
			.objectReferenceValue as InputActionAsset;
		if (actionAsset == null) return;

		string[] options = actionAsset.actionMaps.Select(map => map.name).ToArray();
		int index = Array.IndexOf(options, property.stringValue) + 1;

		EditorGUI.BeginChangeCheck();
		index = EditorGUI.Popup(
			position, label, index,
			options.Prepend("None").Select(option => new GUIContent(option)).ToArray()
		);
		if (EditorGUI.EndChangeCheck())
		{
			property.stringValue = index > 0 ? options[index - 1] : "";
			property.serializedObject.ApplyModifiedProperties();
			property.serializedObject.Update();
		}
	}

	public override float GetPropertyHeight(UnityEditor.SerializedProperty property, UnityEngine.GUIContent label)
	{
		return base.GetPropertyHeight(property, label);
	}
}
#endif
}