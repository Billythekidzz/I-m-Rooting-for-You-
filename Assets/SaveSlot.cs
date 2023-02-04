using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SaveSlot : MonoBehaviour
{

    [SerializeField]
    Button saveButton;

    [SerializeField]
    Button loadButton;

    [SerializeField]
    TextMeshProUGUI label;

    [SerializeField]
    TextMeshProUGUI saveButtonText;

    [Range(0,2)]
    public int saveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSavePath();
        saveButton.onClick.AddListener(OnSaveButtonPressed);
        loadButton.onClick.AddListener(OnLoadButtonPressed);
    }

    private void OnLoadButtonPressed()
    {
        GameStateManager.Instance.LoadGameState(saveIndex);
    }

    private void OnSaveButtonPressed()
    {
        GameStateManager.Instance.SaveGameState(saveIndex);
        UpdateSavePath();
    }

    public void UpdateSavePath()
    {
        string lastSavedPath = GameStateManager.Instance.saveStates[saveIndex].lastSavedPath;
        label.text = lastSavedPath == Globals.INVALID_STRING ? "Empty File" : lastSavedPath;
        if(lastSavedPath == Globals.INVALID_STRING)
        {
            loadButton.gameObject.SetActive(false);
            saveButtonText.text = "Save";
        }
        else
        {
            loadButton.gameObject.SetActive(true);
            saveButtonText.text = "Overwrite";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
