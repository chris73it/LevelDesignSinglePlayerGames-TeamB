using UnityEngine;
using System; 

public class BuildableCounters : MonoBehaviour
{
    [SerializeField] LevelBuildableLimits levelLimits;
    [HideInInspector] public int blockCurrentTotal;
    [HideInInspector] public int blockLevelMax;
    [HideInInspector] public int rampCurrentTotal;
    [HideInInspector] public int rampLevelMax;
    [HideInInspector] public int jumpCurrentTotal;
    [HideInInspector] public int jumpLevelMax;
    [HideInInspector] public int directionCurrentTotal;
    [HideInInspector] public int directionLevelMax;
    [HideInInspector] public int wallsCurrentTotal;
    [HideInInspector] public int wallsLevelMax; 
    
    public static event Action<int, int> blockCountChange;
    public static event Action<int, int> rampCountChange;
    public static event Action<int, int> jumpCountChange;
    public static event Action<int, int> directionCountChange;
    public static event Action<int, int> wallsCountChange; 

    private void Start()
    {
        blockLevelMax = levelLimits.blockLimit;
        blockCurrentTotal = levelLimits.blockLimit;
        blockCountChange (blockCurrentTotal, blockLevelMax);
        rampCurrentTotal = levelLimits.rampLimit;
        rampLevelMax = levelLimits.rampLimit;
        rampCountChange(rampCurrentTotal, rampLevelMax);
        jumpLevelMax = levelLimits.jumpLimit;
        jumpCurrentTotal = levelLimits.jumpLimit;
        jumpCountChange(jumpCurrentTotal, jumpLevelMax);
        directionLevelMax = levelLimits.directionLimit;
        directionCurrentTotal = levelLimits.directionLimit;
        directionCountChange(directionCurrentTotal, directionLevelMax);
        wallsLevelMax = levelLimits.wallsLimit;
        wallsCurrentTotal = levelLimits.wallsLimit;
        wallsCountChange(wallsCurrentTotal, wallsLevelMax); 
    }

    private void OnEnable()
    {
        TileCreator.tilePlaced += TileCreator_Placed;
        TileCreator.tileRemoved += TileCreator_Removed; 
    }

    private void OnDisable()
    {
        TileCreator.tilePlaced -= TileCreator_Placed;
        TileCreator.tileRemoved -= TileCreator_Removed; 
    }

    void TileCreator_Placed(GameObject buildable)
    {
        if (IsBlock(buildable))
        {
            blockCurrentTotal -= 1;
            blockCountChange(blockCurrentTotal, blockLevelMax);
        }
        if (IsRamp(buildable))
        {
            rampCurrentTotal -= 1;
            rampCountChange(rampCurrentTotal, rampLevelMax);
        }
        if (IsJump(buildable))
        {
            jumpCurrentTotal -= 1;
            jumpCountChange(jumpCurrentTotal, jumpLevelMax);
        }
        if (IsFlip(buildable))
        {
            directionCurrentTotal -= 1;
            directionCountChange(directionCurrentTotal, directionLevelMax);
        }
        if (IsGhost(buildable))
        {
            wallsCurrentTotal -= 1;
            wallsCountChange(wallsCurrentTotal, wallsLevelMax);
        }
    }

    void TileCreator_Removed(GameObject buildable)
    {
        if (IsBlock(buildable))
        {
            if (blockCurrentTotal <= 0)
            {
                blockCurrentTotal = 0;
            }
            if (blockCurrentTotal >= blockLevelMax)
            {
                return;
            }
            blockCurrentTotal += 1;
            
            blockCountChange(blockCurrentTotal, blockLevelMax);
        }
        if (IsRamp(buildable))
        {
            if (rampCurrentTotal <= 0)
            {
                rampCurrentTotal = 0;
            }
            if (rampCurrentTotal >= rampLevelMax)
            {
                return;
            }
            rampCurrentTotal += 1;
           
            rampCountChange(rampCurrentTotal, rampLevelMax);
        }
        if (IsJump(buildable))
        {
            if (jumpCurrentTotal <= 0)
            {
                jumpCurrentTotal = 0;
            }
            if (jumpCurrentTotal >= rampLevelMax)
            {
                return;
            }
            jumpCurrentTotal += 1;

            jumpCountChange(jumpCurrentTotal, jumpLevelMax);
        }
        if (IsFlip(buildable))
        {
            if (directionCurrentTotal <= 0)
            {
                directionCurrentTotal = 0;
            }
            if (directionCurrentTotal >= directionLevelMax)
            {
                return;
            }
            directionCurrentTotal += 1;

            directionCountChange(directionCurrentTotal, directionLevelMax);
        }
        if (IsGhost(buildable))
        {
            if (wallsCurrentTotal <= 0)
            {
                wallsCurrentTotal = 0;
            }
            if (wallsCurrentTotal >= wallsLevelMax)
            {
                return;
            }
            wallsCurrentTotal += 1;

            wallsCountChange(wallsCurrentTotal, wallsLevelMax);
        }
    }
    private bool IsBlock(GameObject buildable) {
        return buildable.name == "Block" || buildable.name == "Block(Clone)";
    }
    private bool IsRamp(GameObject buildable) {
        return buildable.name == "Ramp" || buildable.name == "Ramp(Clone)";
    }
    private bool IsJump(GameObject buildable)
    {
        return buildable.name == "JumpPad" || buildable.name == "JumpPad(Clone)";
    }
    private bool IsFlip(GameObject buildable)
    {
        return buildable.name == "Flipper" || buildable.name == "Flipper(Clone)";
    }
    private bool IsGhost(GameObject buildable)
    {
        return buildable.name == "GhostMode" || buildable.name == "GhostMode(Clone)";
    }
}
