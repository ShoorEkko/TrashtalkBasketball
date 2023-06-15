using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine.Analytics;
using Unity.Services.Core.Environments;

using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
     public static AnalyticsManager _instance;
    public static AnalyticsManager Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Analytics Sample
    async void Start()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName("production");
        await UnityServices.InitializeAsync(options);
        await AnalyticsService.Instance.CheckForRequiredConsents();
        Debug.Log($"Started UGS Analytics Sample with user ID: {AnalyticsService.Instance.GetAnalyticsUserID()}");
    }

   

    #region Progression Events
    public void OnLevelComplete(int currentLevel, int score)
    {
        var parameters = new Dictionary<string, object>
            {
                { "game_level", currentLevel},
                { "game_score",score}
                
            };
        AnalyticsService.Instance.CustomData("level_completed", parameters);
        Debug.Log($"Level Completed: {currentLevel}");
    }
    #endregion

    #region Normal Events
    public void OnReplayGame()
    {
        AnalyticsService.Instance.CustomData("replay_game");
        Debug.Log($"Replay Game");
    }
    #endregion
}
