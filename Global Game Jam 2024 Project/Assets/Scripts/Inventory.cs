using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static class PlayerItems
    {
        public static string[] player1Items = new string[10]; // Adjust the size as needed
        public static string[] player2Items = new string[10];
    }

    // Example function to add an item for a specific player
    public static void AddItem(string item, int playerNumber)
    {
        if (playerNumber == 1)
        {
            AddItemToArray(item, PlayerItems.player1Items);
        }
        else if (playerNumber == 2)
        {
            AddItemToArray(item, PlayerItems.player2Items);
        }
        else
        {
            Debug.LogError("Invalid player number");
        }
    }

    // Helper function to add an item to the array
    private static void AddItemToArray(string item, string[] playerArray)
    {
        for (int i = 0; i < playerArray.Length; i++)
        {
            if (string.IsNullOrEmpty(playerArray[i]))
            {
                playerArray[i] = item;
                //Debug.Log("Item added: " + item);
                return;
            }
        }

        Debug.LogWarning("Player inventory is full, cannot add item: " + item);
    }

    // Example function to print player inventories
    public static void PrintPlayerInventories()
    {
        Debug.Log("Player 1 Items: " + string.Join(", ", PlayerItems.player1Items));
        Debug.Log("Player 2 Items: " + string.Join(", ", PlayerItems.player2Items));
    }

    // Ensure the script is not destroyed when loading a new scene
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        AddItem("PEPE PUNCHING", 1);
        AddItem("I like trains", 1);

        AddItem("NOOT NOOT", 2);
        AddItem("GOAT", 2);

        GameObject.Find("Player 1").GetComponent<SkillManager>().enabled = true;
        GameObject.Find("Player 2").GetComponent<SkillManager>().enabled = true;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            SceneManager.LoadScene("Phase_2");          
        }

        if (Input.GetKey(KeyCode.C))
        {
            PrintPlayerInventories();
        }
    }
}
