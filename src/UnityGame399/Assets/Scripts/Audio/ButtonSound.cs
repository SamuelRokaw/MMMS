using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlayBackGroundTrack(AudioClip clip)
    {
        SoundManager.Instance.playBackGroundTrack(clip);
    }

    public void SwitchBackGroundTrack(AudioClip clip)
    {
        SoundManager.Instance.switchBackGroundTrack(clip);
    }
    public void PlayPlayerSFX(AudioClip clip)
    {
        SoundManager.Instance.playPlayerSFX(clip);
    }
    public void PlayEnemySFX(AudioClip clip)
    {
        SoundManager.Instance.playEnemySFX(clip);
    }
    public void PlayOtherSFX(AudioClip clip)
    {
        SoundManager.Instance.playOtherSFX(clip);
    }

    public void SwitchBackGroundTrackWithFade(AudioClip clip)
    {
        SoundManager.Instance.switchBackGroundTrackWithFade(clip);
    }
}
