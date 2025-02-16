using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonFloor", menuName = "Dungeon/Dungeon Floor")]
public class DungeonFloor : ScriptableObject
{
    [Header("Combat Levels")]
    public GameObject[] combatLevels;

    [Header("Boss Level")]
    public GameObject bossLevel;
}
