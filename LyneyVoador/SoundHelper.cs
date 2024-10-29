namespace LyneyVoador;

using Plugin.Maui.Audio;

public class SoundHelper
{
  //------------------------------------------------------------------------

  public static void Play(string lynettebaka)
  {
    Task.Run(async () =>
    {
      var audioFX = await FileSystem.OpenAppPackageFileAsync(lynettebaka);
      var audioPlayer = AudioManager.Current.CreatePlayer(audioFX);
      audioPlayer.Play();
    });
  }

  //------------------------------------------------------------------------

}