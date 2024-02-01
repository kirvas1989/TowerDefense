using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject cautionGamePanel;
        [SerializeField] private Button continueButton;

        private void Start()
        {
            continueButton.interactable = FileHandler.HasFile(MapCompletion.Filename);
            cautionGamePanel.SetActive(false);
        }

        public void Caution()
        {
            if (FileHandler.HasFile(MapCompletion.Filename))
            {
                cautionGamePanel.SetActive(true); 
            }
            else
            {
                NewGame();
            }
        }
        
        public void NewGame()
        {
            FileHandler.Reset(MapCompletion.Filename);
            FileHandler.Reset(Upgrades.Filename);
            SceneManager.LoadScene(1);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit(); 
        }
    }
}