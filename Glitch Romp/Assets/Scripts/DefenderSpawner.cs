using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender = default;
    GameObject defenderParent;
    const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown()
    {
        if (!defender) { return; }
        AttemptToSpawnAt(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void AttemptToSpawnAt(Vector2 gridPos)
    {
        var eggsDisplay = FindObjectOfType<EggsDisplay>();
        int defenderCost = defender.GetEggsCost();
        if (eggsDisplay.HaveEnoughEggs(defenderCost))
        {
            SpawnDefender(gridPos);
            eggsDisplay.SpendEggs(defenderCost);
        }
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 worldPos)
    {
        float newX = Mathf.RoundToInt(worldPos.x);
        float newY = Mathf.RoundToInt(worldPos.y);
        Vector2 gridPos = new Vector2(newX, newY);
        return gridPos;
    }

    private void SpawnDefender(Vector2 roundedPos)
    {
        Defender newDefender = Instantiate
            (defender,
             roundedPos,
             Quaternion.identity) as Defender;
        newDefender.transform.parent = defenderParent.transform;
    }
}
