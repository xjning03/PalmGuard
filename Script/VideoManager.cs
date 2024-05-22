using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPlayerObject;

    private void Start()
    {
        // Ensure the Video Player is initially inactive
        videoPlayerObject.SetActive(false);

        DialogManager.Instance.OnHideDialog += PlayVideo;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnHideDialog event to prevent memory leaks
        DialogManager.Instance.OnHideDialog -= PlayVideo;
    }

    public void PlayVideo()
    {
        // Activate the Video Player and start playing the video
        videoPlayerObject.SetActive(true);
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        // Stop the video and deactivate the Video Player
        videoPlayer.Stop();
        videoPlayerObject.SetActive(false);
    }
}
