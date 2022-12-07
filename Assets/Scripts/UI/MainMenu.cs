using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private GameObject sceneDetailPanel;    
    [SerializeField] private GameObject loadSceneButton;    
    [SerializeField] private Text sceneDetailText;
    [SerializeField] private Text sceneDetailTitle;

    
    [SerializeField] [TextArea(5, 10)] private string timeChallenge, sandboxMode, regular4polytopes, tesseractGrid, about;

    private MainMenuState state = MainMenuState.DEFAULT;

    public void OnTimeChallengeButtonClicked()
    {

        state = MainMenuState.TIME_CHALLENGE;
        loadSceneButton.SetActive(true);
        sceneDetailTitle.text = "Time challenge";
        sceneDetailText.text = timeChallenge;
        sceneDetailPanel.SetActive(true);
    }

    public void OnSandboxModeButtonClicked()
    {
        state = MainMenuState.SANDBOX_MODE;
        loadSceneButton.SetActive(true);
        sceneDetailTitle.text = "Sandbox mode";
        sceneDetailText.text = sandboxMode;
        sceneDetailPanel.SetActive(true);
    }

    public void OnRegularPolytopesButtonClicked()
    {
        state = MainMenuState.REGULAR_4POLYTOPES;
        loadSceneButton.SetActive(true);
        sceneDetailTitle.text = "Regular 4-polytopes";
        sceneDetailText.text = regular4polytopes;
        sceneDetailPanel.SetActive(true);
    }

    public void OnTesseractGridButtonClicked()
    {
        state = MainMenuState.TESSERACT_GRID;
        loadSceneButton.SetActive(true);
        sceneDetailTitle.text = "Tesseract grid";
        sceneDetailText.text = tesseractGrid;
        sceneDetailPanel.SetActive(true);
    }

    public void OnAboutButtonClicked()
    {
        state = MainMenuState.ABOUT;
        loadSceneButton.SetActive(false);
        sceneDetailTitle.text = "About 4DMayhem";
        sceneDetailText.text = about;
        sceneDetailPanel.SetActive(true);
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void OnLoadSceneButtonClicked()
    {
        switch (state)
        {
            case MainMenuState.TIME_CHALLENGE:
                Lzwp.sync.LoadScene("Gamification");
                break;
            case MainMenuState.SANDBOX_MODE:
                Lzwp.sync.LoadScene("PolytopeManipulation");
                break;
            case MainMenuState.REGULAR_4POLYTOPES:
                Lzwp.sync.LoadScene("RegularPolytopes");
                break;
            case MainMenuState.TESSERACT_GRID:
                Lzwp.sync.LoadScene("TesseractGrid");
                break;
        }
    }
}

public enum MainMenuState
{
    DEFAULT,
    TIME_CHALLENGE,
    SANDBOX_MODE,
    REGULAR_4POLYTOPES,
    TESSERACT_GRID,
    ABOUT
}
