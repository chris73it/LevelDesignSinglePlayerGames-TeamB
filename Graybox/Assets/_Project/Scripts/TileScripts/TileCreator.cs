using PlasticPipe.PlasticProtocol.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileCreator : Singleton<TileCreator>
{

    // Temporary:
    public TilePlacement defaultBlock;

    [SerializeField] Tilemap previewMap, levelMap;
    TileInputs playerInput;

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

    protected override void Awake() {
        base.Awake();
        playerInput = new TileInputs();
        camera = Camera.main;
        CurrentBlock = defaultBlock;
    }

    private void OnEnable() {
        playerInput.Enable();
        playerInput.EditorPlacement.MousePosition.performed += OnMouseMove;
        playerInput.EditorPlacement.Mouse1.performed += OnMouse1;
        playerInput.EditorPlacement.Mouse2.performed += OnMouse2;
    }

    private void OnDisable() {
        playerInput.Disable();
        playerInput.EditorPlacement.MousePosition.performed -= OnMouseMove;
        playerInput.EditorPlacement.Mouse1.performed -= OnMouse1;
        playerInput.EditorPlacement.Mouse2.performed -= OnMouse2;
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
    }

    private void UpdatePreview() {
        previewMap.SetTile(prevGridPos, null);
        previewMap.SetTile(currentGridPos, tileBase);
    }

    private void DrawItem(TileBase item) {
        levelMap.SetTile(currentGridPos, item);
    }

    private void OnMouseMove(InputAction.CallbackContext context) {
        m_pos = context.ReadValue<Vector2>();
    }

    private void OnMouse1(InputAction.CallbackContext context) {
        if(currentBlock != null)
            DrawItem(tileBase);
    }

    private void OnMouse2(InputAction.CallbackContext context) {
        DrawItem(null);
    }
}
