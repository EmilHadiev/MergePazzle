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
    private const string RotationsProperty = "_cellRotations";
    private const string SidesProperty = "_cellSides";

    private static readonly Color LockedColor = new Color(1f, 0.55f, 0.55f);
    private static readonly Color LeftColor = new Color(0.6f, 0.8f, 1f);
    private static readonly Color RightColor = new Color(0.65f, 1f, 0.65f);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty rows = serializedObject.FindProperty(RowsProperty);
        SerializedProperty columns = serializedObject.FindProperty(ColumnsProperty);
        SerializedProperty cells = serializedObject.FindProperty(CellsProperty);
        SerializedProperty rotations = serializedObject.FindProperty(RotationsProperty);
        SerializedProperty sides = serializedObject.FindProperty(SidesProperty);

        EditorGUILayout.PropertyField(serializedObject.FindProperty(CellTemplateProperty));
        EditorGUILayout.PropertyField(rows);
        EditorGUILayout.PropertyField(columns);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(CellSizeProperty));
        EditorGUILayout.PropertyField(serializedObject.FindProperty(SpacingProperty));

        int rowCount = Mathf.Max(1, rows.intValue);
        int columnCount = Mathf.Max(1, columns.intValue);
        int count = rowCount * columnCount;

        if (EditorGUIUtility.editingTextField == false)
        {
            SyncSize(cells, count);
            SyncSize(rotations, count);
            SyncSize(sides, count);
        }

        EditorGUILayout.Space(10f);
        EditorGUILayout.LabelField($"Lock matrix ({rowCount} x {columnCount})", EditorStyles.boldLabel);
        DrawLockMatrix(cells, rowCount, columnCount);
        DrawLockToolbar(cells);
        EditorGUILayout.HelpBox("true = Cell.Lock() after generation.", MessageType.None);

        EditorGUILayout.Space(10f);
        EditorGUILayout.LabelField($"Rotation matrix ({rowCount} x {columnCount})", EditorStyles.boldLabel);
        DrawRotationMatrix(rotations, rowCount, columnCount);
        DrawRotationToolbar(rotations);
        EditorGUILayout.HelpBox("Angles in degrees (negative allowed). Passed to Cell.SetRotation().", MessageType.None);

        EditorGUILayout.Space(10f);
        EditorGUILayout.LabelField($"Side matrix ({rowCount} x {columnCount})", EditorStyles.boldLabel);
        DrawSideMatrix(sides, rowCount, columnCount);
        DrawSideToolbar(sides);
        EditorGUILayout.HelpBox("L / R is passed to Cell.SetSide(RobotSide).", MessageType.None);

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            Repaint();
    }

    private static void SyncSize(SerializedProperty values, int size)
    {
        while (values.arraySize < size)
        {
            values.arraySize++;
            SetDefault(values.GetArrayElementAtIndex(values.arraySize - 1));
        }

        if (values.arraySize > size)
            values.arraySize = size;
    }

    private static void SetDefault(SerializedProperty element)
    {
        if (element.propertyType == SerializedPropertyType.Boolean)
            element.boolValue = false;
        else if (element.propertyType == SerializedPropertyType.Float)
            element.floatValue = 0f;
        else if (element.propertyType == SerializedPropertyType.Enum)
            element.enumValueIndex = 0;
    }

    private static void DrawLockMatrix(SerializedProperty cells, int rowCount, int columnCount)
    {
        float width = GetCellWidth(columnCount, 16f, 30f);
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

                if (GUILayout.Button(isLocked ? "X" : string.Empty, EditorStyles.miniButton, GUILayout.Width(width), GUILayout.Height(22f)))
                    cell.boolValue = !isLocked;
            }

            GUI.backgroundColor = defaultColor;
            EditorGUILayout.EndHorizontal();
        }
    }

    private static void DrawLockToolbar(SerializedProperty cells)
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Clear"))
            SetAll(cells, false);

        if (GUILayout.Button("Fill"))
            SetAll(cells, true);

        EditorGUILayout.EndHorizontal();
    }

    private static void DrawRotationMatrix(SerializedProperty rotations, int rowCount, int columnCount)
    {
        float width = GetCellWidth(columnCount, 34f, 46f);

        for (int row = rowCount - 1; row >= 0; row--)
        {
            EditorGUILayout.BeginHorizontal();

            for (int column = 0; column < columnCount; column++)
            {
                int index = row * columnCount + column;

                if (index >= rotations.arraySize)
                    break;

                SerializedProperty rotation = rotations.GetArrayElementAtIndex(index);

                rotation.floatValue = EditorGUILayout.FloatField(rotation.floatValue, GUILayout.Width(width));
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    private static void DrawRotationToolbar(SerializedProperty rotations)
    {
        if (GUILayout.Button("Reset rotations"))
            SetAll(rotations, 0f);
    }

    private static void DrawSideMatrix(SerializedProperty sides, int rowCount, int columnCount)
    {
        float width = GetCellWidth(columnCount, 22f, 34f);
        Color defaultColor = GUI.backgroundColor;

        for (int row = rowCount - 1; row >= 0; row--)
        {
            EditorGUILayout.BeginHorizontal();

            for (int column = 0; column < columnCount; column++)
            {
                int index = row * columnCount + column;

                if (index >= sides.arraySize)
                    break;

                SerializedProperty side = sides.GetArrayElementAtIndex(index);
                bool isLeft = side.enumValueIndex == (int)RobotSide.Left;

                GUI.backgroundColor = isLeft ? LeftColor : RightColor;

                if (GUILayout.Button(isLeft ? "L" : "R", EditorStyles.miniButton, GUILayout.Width(width), GUILayout.Height(22f)))
                    side.enumValueIndex = isLeft ? (int)RobotSide.Right : (int)RobotSide.Left;
            }

            GUI.backgroundColor = defaultColor;
            EditorGUILayout.EndHorizontal();
        }
    }

    private static void DrawSideToolbar(SerializedProperty sides)
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("All L"))
            SetAllSides(sides, RobotSide.Left);

        if (GUILayout.Button("All R"))
            SetAllSides(sides, RobotSide.Right);

        EditorGUILayout.EndHorizontal();
    }

    private static void SetAllSides(SerializedProperty sides, RobotSide side)
    {
        for (int i = 0; i < sides.arraySize; i++)
            sides.GetArrayElementAtIndex(i).enumValueIndex = (int)side;
    }

    private static float GetCellWidth(int columnCount, float min, float max)
    {
        return Mathf.Clamp((EditorGUIUtility.currentViewWidth - 45f) / columnCount, min, max);
    }

    private static void SetAll(SerializedProperty values, bool value)
    {
        for (int i = 0; i < values.arraySize; i++)
            values.GetArrayElementAtIndex(i).boolValue = value;
    }

    private static void SetAll(SerializedProperty values, float value)
    {
        for (int i = 0; i < values.arraySize; i++)
            values.GetArrayElementAtIndex(i).floatValue = value;
    }
}
