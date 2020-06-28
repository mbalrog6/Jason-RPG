using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JasonRPG.Inventory
{
    [CustomEditor(typeof(Item))]
    public class ItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var item = (Item) target;

            DrawIcon(item);
            DrawCrosshairDefinition(item);
            DrawActions(item);
            DrawStatMods(item);
        }

        private void DrawIcon(Item item)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Icon", GUILayout.Width(64), GUILayout.Height(64));

            if (item.Icon != null)
            {
                GUILayout.Box(item.Icon.texture, GUILayout.Width(64), GUILayout.Height(64));
            }
            else
            {
                EditorGUILayout.HelpBox("No Icon Sprite Selected", MessageType.Warning);
            }

            using (var property = serializedObject.FindProperty("icon"))
            {
                var sprite = (Sprite) EditorGUILayout.ObjectField(item.Icon, typeof(Sprite), false);
                property.objectReferenceValue = sprite;
                serializedObject.ApplyModifiedProperties();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawCrosshairDefinition(Item item)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Crosshair", GUILayout.Width(64), GUILayout.Height(64));

            if (item.CrosshairDefinition?.Sprite != null)
            {
                GUILayout.Box(item.CrosshairDefinition.Sprite.texture, GUILayout.Width(64), GUILayout.Height(64));
            }
            else
            {
                EditorGUILayout.HelpBox("No Crosshair Definition Selected", MessageType.Warning);
            }

            using (var property = serializedObject.FindProperty("crosshairDefinition"))
            {
                var crosshairDefinition = (CrosshairDefinition) EditorGUILayout.ObjectField(
                    item.CrosshairDefinition,
                    typeof(CrosshairDefinition),
                    false);
                property.objectReferenceValue = crosshairDefinition;
                serializedObject.ApplyModifiedProperties();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawActions(Item item)
        {
            using (var actionsProperty = serializedObject.FindProperty("actions"))
            {
                for (int i = 0; i < actionsProperty.arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("x", GUILayout.Width(20)))
                    {
                        actionsProperty.DeleteArrayElementAtIndex(i);
                        serializedObject.ApplyModifiedProperties();
                        break;
                    }

                    var action = actionsProperty.GetArrayElementAtIndex(i);
                    if (action != null)
                    {
                        var useModeProperty = action.FindPropertyRelative("UseMode");
                        var targetComponentProperty = action.FindPropertyRelative("TargetComponent");

                        useModeProperty.enumValueIndex = (int) (UseMode) EditorGUILayout.EnumPopup(
                            (UseMode) useModeProperty.enumValueIndex,
                            GUILayout.Width(80));

                        EditorGUILayout.PropertyField(targetComponentProperty, GUIContent.none, false);

                        serializedObject.ApplyModifiedProperties();
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Auto Assign Actions"))
                {
                    List<ItemComponent> assignedItemComponents = new List<ItemComponent>();

                    for (int i = 0; i < actionsProperty.arraySize; i++)
                    {
                        var action = actionsProperty.GetArrayElementAtIndex(i);
                        if (action != null)
                        {
                            var targetComponentProperty = action.FindPropertyRelative("TargetComponent");
                            var assignedItemComponent = targetComponentProperty.objectReferenceValue as ItemComponent;
                            assignedItemComponents.Add(assignedItemComponent);
                        }
                    }

                    foreach (var itemCompent in item.GetComponentsInChildren<ItemComponent>())
                    {
                        if (assignedItemComponents.Contains(itemCompent))
                        {
                            continue;
                        }

                        actionsProperty.InsertArrayElementAtIndex(actionsProperty.arraySize);
                        var action = actionsProperty.GetArrayElementAtIndex(actionsProperty.arraySize - 1);
                        var targetComponentProperty = action.FindPropertyRelative("TargetComponent");
                        targetComponentProperty.objectReferenceValue = itemCompent;
                        serializedObject.ApplyModifiedProperties();
                    }
                }
            }
        }

        private void DrawStatMods(Item item)
        {
            using (var statModsProperty = serializedObject.FindProperty("statMods"))
            {
                for (int i = 0; i < statModsProperty.arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("x", GUILayout.Width(20)))
                    {
                        statModsProperty.DeleteArrayElementAtIndex(i);
                        serializedObject.ApplyModifiedProperties();
                        break;
                    }

                    var statMod = statModsProperty.GetArrayElementAtIndex(i);
                    if (statMod != null)
                    {
                        var statTypeProperty = statMod.FindPropertyRelative("StatType");
                        var valueProperty = statMod.FindPropertyRelative("Value");

                        statTypeProperty.enumValueIndex = (int) (StatType) EditorGUILayout.EnumPopup(
                            (StatType) statTypeProperty.enumValueIndex,
                            GUILayout.Width(120));

                        EditorGUILayout.PropertyField(valueProperty, GUIContent.none, false);

                        serializedObject.ApplyModifiedProperties();
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("+ Add Stat"))
                {
                    statModsProperty.InsertArrayElementAtIndex(statModsProperty.arraySize);
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }
}