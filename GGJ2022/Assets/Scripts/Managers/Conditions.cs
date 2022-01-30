using BonusLevel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Conditions : MonoBehaviour
{
    [Header("Window to show when game ends on any condition")]
    [SerializeField] private GameObject endWindow;

    private SwapCharacter checkDisabled;
    private FollowMouse setWinCondition;

    private void Start()
    {
        checkDisabled = GetComponent<SwapCharacter>();
        setWinCondition = GetComponent<FollowMouse>();
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
            setWinCondition.levelCompleted = true;
        }
    }

    public void BackToGame()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        SceneManager.LoadScene(currentLevel);
    }
}
