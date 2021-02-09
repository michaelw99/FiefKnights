using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializableTypes;

[System.Serializable]
public class RoomSave
{
    // need to record
    // player spawn position
    public int playerSpawnIndex;
    public SVector3[] playerSpawnLocations;
    // monster positions / spawn timers?
    // treasure positions / still exist
    // breakable walls positions / still exist
}
