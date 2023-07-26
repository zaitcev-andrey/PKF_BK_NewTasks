using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _settingsPanel;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseMenuPanel.activeSelf || _settingsPanel.activeSelf)
            {
                ContinueOnCLick();
            }
            else
            {
                Time.timeScale = 0f;
                _pauseMenuPanel.SetActive(true);
            }
        }
    }

    public void ContinueOnCLick()
    {
        Time.timeScale = 1f;
        _pauseMenuPanel.SetActive(false);
        _settingsPanel.SetActive(false);
    }

    public void SettingsOnCLick()
    {
        _pauseMenuPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    public void ExitOnCLick()
    {
        Application.Quit();
    }
}
