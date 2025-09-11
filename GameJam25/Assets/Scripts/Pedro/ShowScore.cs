using UnityEngine;
using TMPro;
public class ShowScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainScoreText;
    [SerializeField] private TextMeshProUGUI[] scoreTypeTexts;
    void Start()
    {
        scoreTypeTexts = new TextMeshProUGUI[3];
    }
}
