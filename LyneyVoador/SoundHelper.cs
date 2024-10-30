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

    public static void Playdois(string fundo, bool loop = false)
  {
    Task.Run(async () =>
    {
      var audioFX = await FileSystem.OpenAppPackageFileAsync(fundo);
      var audioPlayer = AudioManager.Current.CreatePlayer(audioFX);
      audioPlayer.Loop = loop;
      audioPlayer.Play();
    });
  }


  //------------------------------------------------------------------------

}