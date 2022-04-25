using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine;
using TMPro;

public class LeaderboardController : MonoBehaviour
{
    public TMP_InputField MemberID;
    public int ID, playerScore;

    [SerializeField]
    GameObject leaderboardBG;

    [SerializeField]
    TMP_Text[] entries;
    int maxScores = 10;

    public bool hasSubmitted;
    // Start is called before the first frame update
    void Start()
    {
        LootLockerSDKManager.StartGuestSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }
    public void SubmitScore()
    {
        if (!hasSubmitted)
        {
            LootLockerSDKManager.SubmitScore(MemberID.text, playerScore, ID, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Success");
                    hasSubmitted = true;
                }
                else
                {
                    Debug.Log("Failed");
                }
            });
        }
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(ID, maxScores, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;
                for (int i = 0; i < scores.Length; i++)
                {
                    entries[i].text = (scores[i].rank + ".   " + scores[i].member_id + ":   " + scores[i].score);
                }
            }
            else
            {
                Debug.Log("Failed");
            }
        });
        leaderboardBG.SetActive(true);
    }
}
