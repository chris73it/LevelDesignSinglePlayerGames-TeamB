using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour {

    // Temporary:
    public TileObject defaultBlock;

    [SerializeField] Tilemap previewMap, playerMap;

    Camera camera;
    
    Vector2 m_pos;
    Vector3Int currentGridPos;
    Vector3Int prevGridPos;

    TileBase tileBase;
    TileObject currentBlock;

    bool isPaused = true;

    private TileObject CurrentBlock {
        set {
            currentBlock = value;
            tileBase = currentBlock != null ? currentBlock.TileBase : null;
            UpdatePreview();
        }
    }

    private void Start () {
        PlaybackControl.play += Play;
        PlaybackControl.pause += Pause;
    }

    protected void Awake() {
        camera = Camera.main;
        CurrentBlock = defaultBlock;
    }

    private void Pause() {
        isPaused = true;
        previewMap.GetComponent<Renderer>().enabled = true;

    }

    private void Play() {
        isPaused = false;
        previewMap.GetComponent<Renderer>().enabled = false;
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

        if(isPaused) {
            if(Input.GetMouseButton(0)) {
                if(currentBlock != null)
                    DrawItem(tileBase);
            }
            if(Input.GetMouseButtonDown(1)) {
                DrawItem(null);
            }
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
