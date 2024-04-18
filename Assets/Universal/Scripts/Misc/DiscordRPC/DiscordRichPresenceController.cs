using System;
using UnityEngine;

public class DiscordRichPresenceController : MonoBehaviour
{

    public string largeImage, smallImage, state, smallImageText;
    public bool enableRichPresence;
    
    private Discord.Discord richPresence;
    private readonly long clientId = 1090862646993096745;
    private long startTime;
    private readonly string details = string.Empty;
    private string largeImageText;
    
    private void Start()
    {
        richPresence = new Discord.Discord(clientId, (UInt64)Discord.CreateFlags.NoRequireDiscord);
        startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        SetStatus();
        largeImageText = $"Playing on Version {Application.version}";
    }

    private void Update()
    {
        try
        {
            if (enableRichPresence)
            {
                richPresence.RunCallbacks();
            }
        }
        catch
        {
            gameObject.GetComponent<DiscordRichPresenceController>().enabled = false;
        }
    }
    private void LateUpdate()
    {
        SetStatus();
    }

    private void OnApplicationQuit()
    {
        DisposeRPC();
    }

    private void SetStatus()
    {
        try
        {
            var activityManager = richPresence.GetActivityManager();
            var activity = new Discord.Activity
            {
                State = state,
                Details = details,
                Assets =
            {
                LargeImage = largeImage,
                SmallImage = smallImage,
                LargeText = largeImageText,
                SmallText = smallImageText
            },
                Timestamps =
            {
                Start = startTime
            }
            };
            
            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res == Discord.Result.Ok)
                {
                    // Debug.Log("Connected to Discord!");
                    return;
                }
                Debug.LogWarning("Rich presence failed to connect to Discord.");
            });
        }
        catch
        {
            gameObject.GetComponent<DiscordRichPresenceController>().enabled = false;
        }
    }

    public void DisposeRPC()
    {
        richPresence.Dispose();
    }
}
