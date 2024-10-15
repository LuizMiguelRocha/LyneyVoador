namespace LyneyVoador;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	bool VerificaColisao()
	{
		if(!estaMorto)
		{
			if(VerificaColisaoTeto() || VerificaColisaoChao())
			{
				return true;
			}
		}
		return false;
	}

	bool VerificaColisaoTeto()
	{
		var minY = -alturaJanela/2;
		if(imglyney.TranslationY <= minY)
		return true;
		else
		return false;
	}

	bool VerificaColisaoChao()
	{
		var maxY = alturaJanela/2 - imgChao.HeightRequest;
		if (imglyney.TranslationY >= maxY)
		return true;
		else
		return false;
	}

	async Task Desenha()
	{
		while (!estaMorto)
		{
			AplicaGravidade();
			GerenciaCanos();
			if(VerificaColisao())
			{
				estaMorto=true;
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(TempoEntreFrames);
		}
	}
}

