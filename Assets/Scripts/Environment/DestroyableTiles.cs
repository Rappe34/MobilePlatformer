using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyableTiles : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject rubbleEffect;
    [SerializeField] private float destructionDelay;

    private Tilemap tilemap;
    Vector3 op;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 collidePos = collision.GetContact(0).point;
            Vector2 offsetPos = collidePos + (collidePos - (collision.transform.position + Vector3.up)).normalized * 0.5f;
            Vector3Int cellPos = grid.WorldToCell(offsetPos);

            StartCoroutine(DestroyTiles(cellPos));
        }
    }

    private IEnumerator DestroyTiles(Vector3Int pos)
    {
        List<Vector3Int> tilesToDestroy = GetConnectedTiles(pos);

        foreach (Vector3Int tile in tilesToDestroy)
        {
            if (tilemap.HasTile(tile))
            {
                tilemap.SetTile(tile, null);
                Instantiate(rubbleEffect, grid.CellToWorld(tile), Quaternion.identity);

                yield return new WaitForSeconds(destructionDelay);

                StartCoroutine(DestroyTiles(tile));
            }
        }
    }

    private List<Vector3Int> GetConnectedTiles(Vector3Int cellPos)
    {
        List<Vector3Int> tiles = new List<Vector3Int>
        {
            cellPos,
            cellPos + new Vector3Int(-1, 0, 0),
            cellPos + new Vector3Int(1, 0, 0),
            cellPos + new Vector3Int(0, 1, 0),
            cellPos + new Vector3Int(0, -1, 0)
        };

        tiles.RemoveAll(pos => !tilemap.HasTile(pos));

        return tiles;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(op, 0.1f);
    }
}
