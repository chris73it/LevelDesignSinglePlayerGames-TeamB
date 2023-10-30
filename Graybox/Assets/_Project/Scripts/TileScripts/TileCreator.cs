using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour {

    // Temporary:
    public TilePlacement defaultBlock;

    [SerializeField] Tilemap previewMap, playerMap;

    Camera camera;

    Vector2 m_pos;
    Vector3Int currentGridPos;
    Vector3Int prevGridPos;

    TileBase tileBase;
    TilePlacement currentBlock;

    private TilePlacement CurrentBlock {
        set {
            currentBlock = value;
            tileBase = currentBlock != null ? currentBlock.TileBase : null;
            UpdatePreview();
        }
    }

    protected void Awake() {
        camera = Camera.main;
        CurrentBlock = defaultBlock;
    }

    private void OnEnable() {
    }

    private void OnDisable() {
    }
    private void Update() {
        if(currentBlock != null) {
            Vector3 pos = camera.ScreenToWorldPoint(m_pos);
            Vector3Int gridPos = previewMap.WorldToCell(pos);
            if(gridPos != currentGridPos) {
                prevGridPos = currentGridPos;
                currentGridPos = gridPos;
                UpdatePreview();
            }
        }

        if(Input.GetMouseButtonDown(0)) {
            if(currentBlock != null)
                DrawItem(tileBase);
        }
        if(Input.GetMouseButtonDown(1)) {
            DrawItem(null);
        }
        m_pos = Input.mousePosition;
    }

    private void UpdatePreview() {
        previewMap.SetTile(prevGridPos, null);
        previewMap.SetTile(currentGridPos, tileBase);
    }

    private void DrawItem(TileBase item) {
        playerMap.SetTile(currentGridPos, item);
    }
}
