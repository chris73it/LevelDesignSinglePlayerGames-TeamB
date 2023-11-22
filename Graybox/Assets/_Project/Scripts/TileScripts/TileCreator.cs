using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TileCreator : MonoBehaviour {

    // Temporary:
    //public TilePlacement defaultBlock;

    [SerializeField] Tilemap previewMap, playerMap;
    public static event Action<GameObject> tilePlaced;
    public static event Action<GameObject> tileRemoved;

    new Camera camera;

    // bool emptyClip = false;

    Vector2 m_pos;
    Vector3Int currentGridPos;
    Vector3Int prevGridPos;
    Vector3 offset;

    TileBase currentTileBase;
    GameObject currentBuildable;
    bool blockIsEmpty;
    bool rampIsEmpty;

    private bool isPlaying = false;

    private GameObject CurrentBuildable {
        set {
            currentBuildable = value;
            UpdatePreview();
        }
    }

    protected void Awake() {
        camera = Camera.main;
        CurrentBuildable = null;
        offset.x = 0.5f;
        offset.y = 0.5f;
    }

    private void OnEnable() {
        TileButton.tileButtonClicked += HandleTileClick;
        //TileButton.noMoreTiles += Empty;
        //TileButton.tilesReplinished += Restock;
        PlaybackControl.play += Shutdown;
        PlaybackControl.restart += Restart;
        BuildableCounters.blockCountChange += BlockCheck;
        BuildableCounters.rampCountChange += RampCheck;
    }

    private void OnDisable() {
        TileButton.tileButtonClicked -= HandleTileClick;
        //TileButton.noMoreTiles -= Empty;
        //TileButton.tilesReplinished -= Restock;
        PlaybackControl.play -= Shutdown;
        PlaybackControl.restart -= Shutdown;
        BuildableCounters.rampCountChange += RampCheck;
        BuildableCounters.blockCountChange += BlockCheck;
    }

    private void Update() {
        if(currentBuildable != null && isPlaying == false) {
            Vector3 pos = camera.ScreenToWorldPoint(m_pos);
            Vector3Int gridPos = previewMap.WorldToCell(pos);
            if(gridPos != currentGridPos) {
                prevGridPos = currentGridPos;
                currentGridPos = gridPos;
                UpdatePreview();
            }
        }

        if(Input.GetMouseButtonDown(0)) {
            if(currentBuildable != null && isPlaying == false)
            {
                if (CastRay() == null)
                {
                    DrawItem();
                    tilePlaced?.Invoke(currentBuildable);
                } else {
                    return; 
                }
            }
        }
        if(Input.GetMouseButtonDown(1)) {
            if (CastRay() != null && isPlaying == false)
            { 
                DestroyItem(CastRay());
                tileRemoved?.Invoke(currentBuildable);
            }
        }
        m_pos = Input.mousePosition;
    }

    private void UpdatePreview() {
        previewMap.SetTile(prevGridPos, null);
        previewMap.SetTile(currentGridPos, currentTileBase);
    }

    private void DrawItem() {
        Debug.Log(blockIsEmpty);
        if (currentBuildable.name == "Block" && !blockIsEmpty)
        {
            Instantiate(currentBuildable, currentGridPos + offset, Quaternion.identity);
        }
        if (currentBuildable.name == "Ramp" && !rampIsEmpty)
        {
            Instantiate(currentBuildable, currentGridPos + offset, Quaternion.identity);
        }
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

    private void HandleTileClick(GameObject buildablePrefab)
    {
        currentBuildable = buildablePrefab;
        currentTileBase = buildablePrefab.GetComponent<PlaceableTile>().tileBase;
    }

    void Shutdown()
    {
        isPlaying = true;
    }

    void Restart()
    {
        isPlaying = false;
    }

    void RampCheck(int total, int max)
    {
        if (total == 0)
        {
            rampIsEmpty = true; 
        }
        if (total >= 1)
        {
            rampIsEmpty = false;
        }
        
    }

    void BlockCheck(int total, int max)
    {
        if (total == 0)
        {
            blockIsEmpty = true;
        }
        if (total >= 1)
        {
            blockIsEmpty = false;
        }
    }
}
