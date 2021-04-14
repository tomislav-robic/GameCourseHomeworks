using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface ICollectionHandler
{
    void PlayerDidCollectItem(GameObject item);
}

public class GameManager : MonoBehaviour, ICollectionHandler
{
    private int numberOfCollectables;
    private int numberOfItemsCollected = 0;
    public PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        player.collectionHandler = this;
        numberOfCollectables = GameObject.FindGameObjectsWithTag(Tags.Collectable).Length;
    }

    public void PlayerDidCollectItem(GameObject item)
    {
        Destroy(item);

        numberOfItemsCollected++;

        if (numberOfItemsCollected >= numberOfCollectables)
        {
            SceneManager.LoadScene(SceneNames.Level2);
        }
    }
}
