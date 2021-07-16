using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private int level = 0;
    private int itemCount;
    private int keyCount;

    private Text itemText;
    private Text keyText;
    private Text infoText;

    private List<Vector3> level0Checkpoints = new List<Vector3>();
    private int level0CheckpointCounter;



    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        itemCount = 0;
        keyCount = 0;
        level0CheckpointCounter = 0;
        SetupCheckpoints();
    }

    void SetupCheckpoints()
    {
        level0Checkpoints.Add(new Vector3(0, 2, -5));
        level0Checkpoints.Add(new Vector3(-6, 0, 72));
    }

    public void l0CheckpointPassed()
    {
        if (level0CheckpointCounter < 1)
            level0CheckpointCounter++;
    }

    public Vector3 positionPlayer()
    {
        switch (this.level)
        {
            case 0:
                return level0Checkpoints[level0CheckpointCounter];

            default:
                return new Vector3(0, 0, 0);
        }

    }



    public void GameOver()
    {
        ResetItems();
        //enabled = false;
        SceneManager.LoadScene(level);

    }

    private void ResetItems()
    {
        itemCount = 0;
        keyCount = 0;
    }

    public void CompleteLevel()
    {
        level++;
        ResetItems();
        SceneManager.LoadScene(level);
    }

    public void IncreaseItemCount()
    {
        itemText = GameObject.Find("ItemText").GetComponent<Text>();

        itemCount++;
        itemText.text = "Item: " + itemCount.ToString();
        DisplayShortInfoText("You collected an item");
    }

    public void IncreaseKeyCount()
    {
        keyText = GameObject.Find("KeyText").GetComponent<Text>();

        keyCount++;
        keyText.text = "Key: " + keyCount.ToString();
        DisplayShortInfoText("You collected a key");

    }

    public int GetKeyCount()
    {
        return keyCount;
    }

    public void DisplayLongInfoText(string infoString)
    {
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        infoText.text = infoString;
        Invoke("HideInfoText", 10f); //TODO: Change to load next level

    }

    public void DisplayShortInfoText(string infoString)
    {
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        infoText.text = infoString;
        Invoke("HideInfoText", 3f); //TODO: Change to load next level

    }

    private void HideInfoText()
    {
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        infoText.text = "";
    }
}
