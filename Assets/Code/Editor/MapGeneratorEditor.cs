using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    private const string CellTemplateProperty = "_cellTemplate";
    private const string RowsProperty = "_rows";
    private const string ColumnsProperty = "_columns";
    private const string CellSizeProperty = "_cellSize";
    private const string SpacingProperty = "_spacing";
    private const string CellsProperty = "_cells";

    private static readonly Color LockedColor = new Color(1f, 0.55f, 0.55f);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty rows = serializedObject.FindProperty(RowsProperty);
        SerializedProperty columns = serializedObject.FindProperty(ColumnsProperty);
        SerializedProperty cells = serializedObject.FindProperty(CellsProperty);

        EditorGUILayout.PropertyField(serializedObject.FindProperty(CellTemplateProperty));
        EditorGUILayout.PropertyField(rows);
        EditorGUILayout.PropertyField(columns);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(CellSizeProperty));
        EditorGUILayout.PropertyField(serializedObject.FindProperty(SpacingProperty));

        int rowCount = Mathf.Max(1, rows.intValue);
        int columnCount = Mathf.Max(1, columns.intValue);

        if (EditorGUIUtility.editingTextField == false)
            SyncSize(cells, rowCount * columnCount);

        EditorGUILayout.Space(10f);
        EditorGUILayout.LabelField($"Lock matrix ({rowCount} x {columnCount})", EditorStyles.boldLabel);
        DrawMatrix(cells, rowCount, columnCount);
        DrawToolbar(cells);
        EditorGUILayout.HelpBox("true = Cell.Lock() after generation. Rows are drawn far -> near, as on screen.", MessageType.None);

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            Repaint();
    }

    private static void SyncSize(SerializedProperty cells, int size)
    {
        while (cells.arraySize < size)
        {
            cells.arraySize++;
            cells.GetArrayElementAtIndex(cells.arraySize - 1).boolValue = false;
        }

        if (cells.arraySize > size)
            cells.arraySize = size;
    }

    private static void DrawMatrix(SerializedProperty cells, int rowCount, int columnCount)
    {
        float width = Mathf.Clamp((EditorGUIUtility.currentViewWidth - 45f) / columnCount, 16f, 30f);
        Color defaultColor = GUI.backgroundColor;

        for (int row = rowCount - 1; row >= 0; row--)
        {
            EditorGUILayout.BeginHorizontal();

            for (int column = 0; column < columnCount; column++)
            {
                int index = row * columnCount + column;

                if (index >= cells.arraySize)
                    break;

                SerializedProperty cell = cells.GetArrayElementAtIndex(index);
                bool isLocked = cell.boolValue;

                GUI.backgroundColor = isLocked ? LockedColor : defaultColor;

                if (GUILayout.Button(isLocked ? "L" : string.Empty, EditorStyles.miniButton, GUILayout.Width(width), GUILayout.Height(22f)))
                    cell.boolValue = !isLocked;
            }

            GUI.backgroundColor = defaultColor;
            EditorGUILayout.EndHorizontal();
        }
    }

    private static void DrawToolbar(SerializedProperty cells)
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Clear"))
            SetAll(cells, false);

        if (GUILayout.Button("Fill"))
            SetAll(cells, true);

        EditorGUILayout.EndHorizontal();
    }

    private static void SetAll(SerializedProperty cells, bool value)
    {
        for (int i = 0; i < cells.arraySize; i++)
            cells.GetArrayElementAtIndex(i).boolValue = value;
    }
}
