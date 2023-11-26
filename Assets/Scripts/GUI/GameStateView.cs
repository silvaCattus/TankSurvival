using TMPro;
using UnityEngine;

public class GameStateView : MonoBehaviour
{
    [SerializeField] private TMP_Text stateText = null;
    [SerializeField] private TankBody tank = null;

    private void Start()
    {
        tank.died.AddListener(GameOver);
    }

    private void GameOver(Unit unit)
    {
        stateText.text = "Вы проиграли!";
        Time.timeScale = 0;
    }
}
