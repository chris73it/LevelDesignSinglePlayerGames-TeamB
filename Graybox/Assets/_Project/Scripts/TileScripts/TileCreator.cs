using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour {

    // Temporary:
    public TileObject defaultBlock;

    [SerializeField] Tilemap previewMap, playerMap;

    Camera camera;
    
    Vector3 m_pos;
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
            Vector3Int gridPos = previewMap.WorldToCell(m_pos);
            if(gridPos != currentGridPos) {
                prevGridPos = currentGridPos;
                currentGridPos = gridPos;
                UpdatePreview();
            }
        }

        if(isPaused && OnMap(m_pos)) {
            if(Input.GetMouseButton(0)) {
                if(currentBlock != null)
                    DrawItem(tileBase);
            }
            if(Input.GetMouseButtonDown(1)) {
                DrawItem(null);
            }
        }
        m_pos = camera.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void UpdatePreview() {
        previewMap.SetTile(prevGridPos, null);
        if(OnMap(m_pos)) {
            previewMap.SetTile(currentGridPos, tileBase);
        }
    }

    private bool OnMap(Vector3 position) {
        float width = transform.localScale.x / 2;
        float height = transform.localScale.y / 2;
        Vector2 diff = position - transform.position;
        return Mathf.Abs(diff.x) <= width && Mathf.Abs(diff.y) <= height;
    }

    private void DrawItem(TileBase item) {
        playerMap.SetTile(currentGridPos, item);
    }
}
