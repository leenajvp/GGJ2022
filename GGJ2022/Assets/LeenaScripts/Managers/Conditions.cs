using UnityEngine;
using UnityEngine.SceneManagement;
using BonusLevel;

public class Conditions : MonoBehaviour
{
    [SerializeField] private GameObject endWindow;
    private SwapCharacter checkDisabled;

    private void Start()
    {
        checkDisabled = GetComponent<SwapCharacter>();
        endWindow.SetActive(false);
    }

    private void Update()
    {
        if (checkDisabled.disabled)
        {
            endWindow.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            endWindow.SetActive(true);
            checkDisabled.disabled = true;
        }
    }

    public void BackToGame()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        SceneManager.LoadScene(currentLevel);
    }
}
