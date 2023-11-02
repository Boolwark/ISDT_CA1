using DefaultNamespace.ProceduralTerrain;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DungeonGenerator dungeonGenerator = (DungeonGenerator)target;
        if (GUILayout.Button("Generate Dungeon"))
        {
            dungeonGenerator.Generate();
        }

        TerrainGenerator terrainGenerator = FindObjectOfType<TerrainGenerator>();
        if (terrainGenerator && GUILayout.Button("Generate Terrain"))
        {
            terrainGenerator.Generate();
        }
    }
}