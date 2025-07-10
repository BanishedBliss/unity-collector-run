using System.Text;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("HUD Text")]
        [SerializeField] private string scoreStaticText = "Score: ";
        [SerializeField] private string timerStaticText = "Time Left: ";
        [SerializeField] private string finalScoreText = "Score: ";
        [SerializeField] private string highScoreText = "HighScore: ";
        [Header("HUD Setup")]
        [SerializeField] private TextMeshProUGUI scoreTextElement;
        [SerializeField] private TextMeshProUGUI timerTextElement;
        [SerializeField] private TextMeshProUGUI finalScoreTextElement;
        [SerializeField] private TextMeshProUGUI highScoreTextElement;
        
        public void UpdateScore(int score)
        {
            scoreTextElement.text = $"{scoreStaticText}{score}";    
        }

        public void UpdateTimer(float secondsLeft)
        {
            timerTextElement.text = $"{timerStaticText}{GetTimeString(secondsLeft)}";
        }

        public void SetFinalScore(int score)
        {
            finalScoreTextElement.text = $"{finalScoreText}{score}";
        }

        public void SetHighScore(int highScore)
        {
            highScoreTextElement.text = $"{highScoreText}{highScore}";
        }

        private static string GetTimeString(float secondsLeft)
        {
            var sb = new StringBuilder();
            
            var totalMinutes = Mathf.FloorToInt(secondsLeft / 60);
            var totalSeconds = Mathf.FloorToInt(secondsLeft % 60);
            
            sb.Append(totalMinutes);
            sb.Append(":");
            sb.Append(totalSeconds);

            return sb.ToString();
        }
    }
}