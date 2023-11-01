using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TileCreator : MonoBehaviour {

    // Temporary:
    //public TilePlacement defaultBlock;

    [SerializeField] Tilemap previewMap, playerMap;
    public static event Action tilePlaced;
    public static event Action tileRemoved;

    new Camera camera;

    bool emptyClip = false;

    Vector2 m_pos;
    Vector3Int currentGridPos;
    Vector3Int prevGridPos;

    TileBase currentTileBase;
    GameObject currentBlock;

    private GameObject CurrentBlock {
        set {
            currentBlock = value;
            UpdatePreview();
        }
    }

    protected void Awake() {
        camera = Camera.main;
        CurrentBlock = null;
    }

    private void OnEnable() {
        TileButton.tileButtonClicked += HandleTileClick;
        TileButton.noMoreTiles += Empty;
        TileButton.tilesReplinished += Restock;
    }

    private void OnDisable() {
        TileButton.tileButtonClicked -= HandleTileClick;
        TileButton.noMoreTiles -= Empty;
        TileButton.tilesReplinished -= Restock;
    }

    private void Update() {
        if(currentBlock != null  && emptyClip == false) {
            Vector3 pos = camera.ScreenToWorldPoint(m_pos);
            Vector3Int gridPos = previewMap.WorldToCell(pos);
            if(gridPos != currentGridPos) {
                prevGridPos = currentGridPos;
                currentGridPos = gridPos;
                UpdatePreview();
            }
        }

        if(Input.GetMouseButtonDown(0)) {
            if(currentBlock != null && emptyClip == false)
            {
                if (CastRay() == null)
                {
                    DrawItem();
                    tilePlaced?.Invoke();
                } else {
                    return; 
                }
            }
        }
        if(Input.GetMouseButtonDown(1)) {
            if (CastRay() != null)
            {
                DestroyItem(CastRay());
                tileRemoved?.Invoke();
            }
        }
        m_pos = Input.mousePosition;
    }

    private void UpdatePreview() {
        previewMap.SetTile(prevGridPos, null);
        previewMap.SetTile(currentGridPos, currentTileBase);
    }

    private void DrawItem() {
        //playerMap.SetTile(currentGridPos, item);
        Instantiate(currentBlock, currentGridPos, Quaternion.identity); 
    }

    private void DestroyItem(GameObject clickedBlock)
    {
        Destroy(clickedBlock);
    }

    GameObject CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

        private void HandleTileClick(GameObject blockPrefab)
    {
        currentBlock = blockPrefab;
        currentTileBase = blockPrefab.GetComponent<PlaceableTile>().tileBase;
        Debug.Log("message went through");
    }

    private void Empty()
    {
        emptyClip = true;
    }

    private void Restock()
    {
        emptyClip = false;
    }
}
