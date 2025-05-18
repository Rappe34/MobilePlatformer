using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    public void ShowPanel(string timeString)
    {
        gameObject.SetActive(true);

        timeText.text = timeString;
    }
}
