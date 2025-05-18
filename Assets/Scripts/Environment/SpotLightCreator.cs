using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR

[RequireComponent(typeof(Tilemap))]
public class SpotlightCreator : MonoBehaviour
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
                    if (tileBase.name == "torch")
                    {
                        Vector3 worldPos = tilemap.CellToWorld(new Vector3Int(x, y)) + new Vector3(0.5f, 0.5f);
                        Instantiate(lightPrefab, worldPos, Quaternion.identity, transform);
                    }
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

[CustomEditor(typeof(SpotlightCreator))]
public class SpotLightGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Lights"))
        {
            var script = (SpotlightCreator)target;
            script.Create();
        }

        if (GUILayout.Button("Destroy Lights"))
        {
            var creator = (SpotlightCreator)target;
            creator.DestroyOldLights();
        }
        EditorGUILayout.EndHorizontal();
    }

}

#endif
