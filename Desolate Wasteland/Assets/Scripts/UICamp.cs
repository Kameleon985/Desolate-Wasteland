using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICamp : MonoBehaviour
{
    public GameObject campView;
    public GameObject buildingView;
    public GameObject marketView;
    public GameObject labView;
    public static UICamp Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BuildingScreenOpen()
    {
        buildingView.gameObject.SetActive(true);
    }
    public void BuildingScreenClose()
    {
        buildingView.gameObject.SetActive(false);
    }

    public void ReturnToMap()
    {
        SceneManager.LoadScene("Map&UI");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
