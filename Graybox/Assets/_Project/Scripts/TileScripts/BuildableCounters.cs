using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class BuildableCounters : MonoBehaviour
{
    [SerializeField] LevelBuildableLimits levelLimits;
    [HideInInspector] public int blockCurrentTotal;
    [HideInInspector] public int blockLevelMax;
    [HideInInspector] public int rampCurrentTotal;
    [HideInInspector] public int rampLevelMax;
    
    public static event Action<int, int> blockCountChange;
    public static event Action<int, int> rampCountChange;

    private void Start()
    {
        blockLevelMax = levelLimits.blockLimit;
        blockCurrentTotal = levelLimits.blockLimit;
        blockCountChange (blockCurrentTotal, blockLevelMax);
        rampCurrentTotal = levelLimits.rampLimit;
        rampLevelMax = levelLimits.rampLimit;
        rampCountChange(rampCurrentTotal, rampLevelMax);
        Debug.Log(rampCurrentTotal + rampLevelMax);
        Debug.Log(blockCurrentTotal + blockLevelMax);
        Debug.Log("test");
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
    }
    private bool IsBlock(GameObject buildable) {
        return buildable.name == "Block" || buildable.name == "Block(Clone)";
    }
    private bool IsRamp(GameObject buildable) {
        return buildable.name == "Ramp" || buildable.name == "Ramp(Clone)";
    }
}
