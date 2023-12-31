using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TileCreator : MonoBehaviour {

    [SerializeField] Tilemap previewMap, playerMap;
    public static event Action<GameObject> tilePlaced;
    public static event Action<GameObject> tileRemoved;

    new Camera camera;

    Vector2 m_pos;
    Vector3Int currentGridPos;
    Vector3Int prevGridPos;
    Vector3 offset;

    TileBase currentTileBase;
    GameObject currentBuildable;
    bool blockIsEmpty;
    bool rampIsEmpty;
    bool jumpIsEmpty;
    bool directionIsEmpty;
    bool ghostIsEmpty;

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
        PlaybackControl.play += Shutdown;
        PlaybackControl.restart += Restart;
        PlayerController.Respawn += Restart;
        BuildableCounters.blockCountChange += BlockCheck;
        BuildableCounters.rampCountChange += RampCheck;
        BuildableCounters.jumpCountChange += JumpCheck;
        BuildableCounters.directionCountChange += DirectionCheck;
        BuildableCounters.wallsCountChange += GhostCheck;
    }

    private void OnDisable() {
        TileButton.tileButtonClicked -= HandleTileClick;
        PlaybackControl.play -= Shutdown;
        PlaybackControl.restart -= Shutdown;
        PlayerController.Respawn -= Restart;
        BuildableCounters.rampCountChange -= RampCheck;
        BuildableCounters.blockCountChange -= BlockCheck;
        BuildableCounters.jumpCountChange -= JumpCheck;
        BuildableCounters.directionCountChange -= DirectionCheck;
        BuildableCounters.wallsCountChange -= GhostCheck;
    }

    private void Update() {
        if(currentBuildable != null && isPlaying == false) {
            Vector3 pos = camera.ScreenToWorldPoint(m_pos);
            Vector3Int gridPos = previewMap.WorldToCell(m_pos);
            if(gridPos != currentGridPos) {
                prevGridPos = currentGridPos;
                currentGridPos = gridPos;
                UpdatePreview();
            }
        }

        if(Input.GetMouseButtonDown(0)) {
            if(currentBuildable != null && isPlaying == false && OnMap(m_pos)) {
                if(CastRay() == null) {
                    DrawItem();
                    tilePlaced?.Invoke(currentBuildable);
                } else {
                    return;
                }
            }
        }
        if(Input.GetMouseButtonDown(1)) {
            if(CastRay() != null && isPlaying == false) {
                GameObject clickedBlock = CastRay();
                PlaceableTile tileObject;
                if (clickedBlock.TryGetComponent<PlaceableTile>(out tileObject))
                {
                    if(tileObject.IsPlayerPlaced) {
                        Debug.Log(clickedBlock);
                        tileRemoved?.Invoke(clickedBlock);
                        DestroyItem(clickedBlock);
                    }
                }
            }
        }
        m_pos = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool OnMap(Vector3 position) {
        float width = transform.localScale.x;
        float height = transform.localScale.y;
        Vector2 diff = position - transform.position;
        return Mathf.Abs(diff.x) <= width / 2 && Mathf.Abs(diff.y) <= height / 2;
    }

    private void UpdatePreview() {
        previewMap.SetTile(prevGridPos, null);
        if(OnMap(m_pos)) {
            previewMap.SetTile(currentGridPos, currentTileBase);
        }
    }

    private void DrawItem() {
        GameObject item = InstantiateItem();
        if(item == null) {
            Debug.Log("empty item!!");
            return;
        }

        item.GetComponent<PlaceableTile>().IsPlayerPlaced = true;
    }

    private GameObject InstantiateItem() {
        if(currentBuildable.name == "Block" && !blockIsEmpty) {
            return Instantiate(currentBuildable, currentGridPos + offset, Quaternion.identity);
        }
        if(currentBuildable.name == "Ramp" && !rampIsEmpty) {
            return Instantiate(currentBuildable, currentGridPos + offset, Quaternion.identity);
        }
        if(currentBuildable.name == "JumpPad" && !jumpIsEmpty) {
            return Instantiate(currentBuildable, currentGridPos + offset, Quaternion.identity);
        }
        if(currentBuildable.name == "Flipper" && !directionIsEmpty) {
            return Instantiate(currentBuildable, currentGridPos + offset, Quaternion.identity);
        }
        if(currentBuildable.name == "GhostMode" && !ghostIsEmpty) {
            return Instantiate(currentBuildable, currentGridPos + offset, Quaternion.identity);
        }
        return null;
    }

    private void DestroyItem(GameObject clickedBlock) {
        Destroy(clickedBlock);
    }

    GameObject CastRay() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if(hit.collider != null) {
            return hit.collider.gameObject;
        } else {
            return null;
        }
    }

    private void HandleTileClick(GameObject buildablePrefab) {
        currentBuildable = buildablePrefab;
        currentTileBase = buildablePrefab.GetComponent<PlaceableTile>().tileBase;
    }

    void Shutdown() {
        isPlaying = true;
    }

    void Restart() {
        isPlaying = false;
    }

    void RampCheck(int total, int max) {
        if(total == 0) {
            rampIsEmpty = true;
        }
        if(total >= 1) {
            rampIsEmpty = false;
        }

    }

    void BlockCheck(int total, int max) {
        if(total == 0) {
            blockIsEmpty = true;
        }
        if(total >= 1) {
            blockIsEmpty = false;
        }
    }

    void JumpCheck(int total, int max)
    {
        if (total == 0)
        {
            jumpIsEmpty = true;
        }
        if (total >= 1)
        {
            jumpIsEmpty = false;
        }
    }

    void DirectionCheck(int total, int max)
    {
        if (total == 0)
        {
            directionIsEmpty = true;
        }
        if (total >= 1)
        {
            directionIsEmpty = false;
        }
    }

    void GhostCheck(int total, int max)
    {
        if (total == 0)
        {
            ghostIsEmpty = true;
        }
        if (total >= 1)
        {
            ghostIsEmpty = false;
        }
    }
}
