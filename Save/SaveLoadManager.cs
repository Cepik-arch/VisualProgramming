using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public List<Block> blocksInUI;
    public List<DoorController> doorsInScene;

    //Static variable to generate block IDs
    private static int blockId = 0;

    private void Awake()
    {
        blockId = 0;

        doorsInScene = new List<DoorController>();

        //Find all DoorController components in the scene
        DoorController[] doorControllers = FindObjectsOfType<DoorController>();

        // Loop through each DoorController found
        foreach (DoorController doorController in doorControllers)
        {
            doorsInScene.Add(doorController);
        }

        blocksInUI = new List<Block>();

        //Find all DoorController components in the scene
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        // Loop through each game object found
        foreach (GameObject b in blocks)
        {
            Block block = b.GetComponent<Block>();

            if (block != null)
            {
                blocksInUI.Add(block);
            }
        }
    }

    public void SaveGame()
    {
        GameData gameData = new GameData();

        //Save player position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            gameData.playerPosition = new SerializableVector3(player.transform.position);
        }

        //Save block data
        List<BlockData> blockDataList = new List<BlockData>();
        foreach (Block block in blocksInUI)
        {
            BlockData blockData = block.GetBlockData();

            // Assign a unique ID to the block data
            blockData.blockID = blockId;
            blockId++;

            blockDataList.Add(blockData);
        }
        gameData.blockDataList = blockDataList;

        //Save door data
        List<DoorData> doorDataList = new List<DoorData>();
        foreach (DoorController door in doorsInScene)
        {
            DoorData doorData = door.GetDoorData();
            doorDataList.Add(doorData);
        }
        gameData.doorDataList = doorDataList;

        //Save active quest
        GameObject questTitle = GameObject.Find("QuestName");
        GameObject questDescription = GameObject.Find("QuestText");
        ActiveQuest activeQuest = new ActiveQuest()
        {
            title = questTitle.GetComponent<TMP_Text>().text,
            description = questDescription.GetComponent<TMP_Text>().text

        };
        gameData.activeQuest = activeQuest;

        //Save the populated GameData object
        SaveSystem.SaveGame(gameData);
    }

    public void LoadGame()
    {
        GameData savedData = LoadSystem.LoadGame();

        if (savedData != null)
        {
            //Restore player position
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (player != null)
                {
                    CharacterController controller = player.GetComponent<CharacterController>();
                    if (controller != null)
                    {
                        //Use CharacterController.Move to set player's position
                        controller.enabled = false;
                        controller.transform.position = savedData.playerPosition.ToVector3();
                        controller.enabled = true;
                    }
                }
            }

            //Restore door data
            foreach (DoorData doorData in savedData.doorDataList)
            {
                GameObject doorObject = GameObject.Find(doorData.doorName);
                if (doorObject != null)
                {
                    // Get the DoorController component from the found GameObject
                    DoorController doorController = doorObject.GetComponent<DoorController>();
                    if (doorController != null)
                    {
                        // Set the door data on the DoorController
                        doorController.SetDoorData(doorData);
                    }
                }
            }

            Debug.Log("Block Foreach");
            //Restore block data
            foreach (BlockData blockData in savedData.blockDataList)
            {
                Block block = FindBlockByBlockID(blockData.blockID);
                if (block != null)
                {
                    block.SetBlockData(blockData);
                }
            }

            //Restore active quest
            GameObject questTitle = GameObject.Find("QuestName");
            GameObject questDescription = GameObject.Find("QuestText");
            if(questTitle != null && questDescription != null)
            {
                questTitle.GetComponent<TMP_Text>().text = savedData.activeQuest.title;
                questDescription.GetComponent<TMP_Text>().text = savedData.activeQuest.description;
            }


        }
        else
        {
            Debug.Log("No saved game to load.");
        }
    }

    //Search for block with same ID
    private Block FindBlockByBlockID(int blockID)
    {
        foreach (Block block in blocksInUI)
        {
            BlockData blockData = block.GetBlockData();
            if (blockData.blockID == blockID)
            {
                return block;
            }
        }

        return null;
    }

}
