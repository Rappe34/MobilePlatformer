using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI killsText;

    public void ShowPanel(string timeString, string bestTimeString, string killsString)
    {
        gameObject.SetActive(true);

        timeText.text = timeString;
        bestTimeText.text = bestTimeString;
        killsText.text = killsString;
    }
}
