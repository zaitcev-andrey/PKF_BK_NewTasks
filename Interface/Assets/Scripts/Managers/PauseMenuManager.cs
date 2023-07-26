using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    #region private Members
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _settingsPanel;
    #endregion

    #region private Methods
    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    #endregion

    #region public Methods
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
    #endregion
}
