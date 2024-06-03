using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR

[RequireComponent(typeof(Tilemap))]
public class SpotLightCreator : MonoBehaviour
{
    [SerializeField] private GameObject lightPrefab;

    private Tilemap tilemap;

    public void Create()
    {
        DestroyOldLights();
        tilemap = GetComponent<Tilemap>();

        for (int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
        {
            for (int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
            {
                TileBase tileBase = tilemap.GetTile(new Vector3Int(x, y));
                if (tileBase != null)
                {
                    if (tilemap.GetTile(new Vector3Int(x, y + 1)) != null) continue;

                    Vector3 worldPos = tilemap.CellToWorld(new Vector3Int(x, y)) + new Vector3(0.5f, 0.5f);
                    Instantiate(lightPrefab, worldPos, Quaternion.identity, transform);
                }
            }
        }
    }

    public void DestroyOldLights()
    {
        var tempList = transform.Cast<Transform>().ToList();
        foreach (var child in tempList)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}

[CustomEditor(typeof(SpotLightCreator))]
public class SpotLightGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            var script = (SpotLightCreator)target;
            script.Create();
        }

        if (GUILayout.Button("Remove Shadows"))
        {
            var creator = (SpotLightCreator)target;
            creator.DestroyOldLights();
        }
        EditorGUILayout.EndHorizontal();
    }

}

#endif
