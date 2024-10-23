namespace LyneyVoador;

public partial class MainPage : ContentPage
{
	const int gravidade = 5;
	const int TempoEntreFrames = 25;
	bool estaMorto = false;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 5;
	const int ForcaPulo = 30;
	const int MaxTempoPulando = 3;
	bool EstaPulando = false;
	int TempoPulando = 0;
	const int AberturaMinima = 200;
	int score = 0;

	void Inicializar()
	{
		imglyney.TranslationX = 0;
		imglyney.TranslationY = 0;
		imgcanocima.TranslationX = -larguraJanela;
		imgcanobaixo.TranslationX = -larguraJanela;
		score = 0;
		GerenciaCanos();
	}



	void Oi(object s, TappedEventArgs e)
	{
		FrameGameOver.IsVisible = false;
		estaMorto = false;
		Inicializar();
		Desenha();
	}

	public MainPage()
	{
		InitializeComponent();
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela = w;
		alturaJanela = h;
	}

	void AplicaPulo()
	{
		imglyney.TranslationY -= ForcaPulo;
		TempoPulando++;
		if (TempoPulando >= MaxTempoPulando)
		{
			EstaPulando = false;
			TempoPulando = 0;
		}
	}

	void OnGridClicked(object s, TappedEventArgs a)
	{
		EstaPulando = true;
	}

	void AplicaGravidade()
	{
		imglyney.TranslationY += gravidade;
	}
	public async void Desenha()
	{
		while (!estaMorto)
		{
			if (EstaPulando)
				AplicaPulo();
			else
				AplicaGravidade();

			GerenciaCanos();

			if (VerificaColisao())
			{
				estaMorto = true;
				FrameGameOver.IsVisible = true;
				break;
			}

			await Task.Delay(TempoEntreFrames);
		}
	}

	void GerenciaCanos()
	{
		imgcanocima.TranslationX -= velocidade;
		imgcanobaixo.TranslationX -= velocidade;
		if (imgcanobaixo.TranslationX < -larguraJanela)
		{
			imgcanobaixo.TranslationX = 0;
			imgcanocima.TranslationX = 0;
			var alturaMax = -300;
			var alturaMin = -imgcanobaixo.HeightRequest;
			imgcanocima.TranslationY = Random.Shared.Next((int)alturaMin, (int)alturaMax);
			imgcanobaixo.TranslationY = imgcanocima.HeightRequest + imgcanocima.TranslationY + AberturaMinima;
			score++;
			if (score % 2 == 0)
				velocidade++;
			labelScore.Text = "Cano: " + score.ToString("D3");
			fianlScore.Text = "Total de canos passados  " + score.ToString("D3");
		}
	}


	bool VerificaColisao()
	{
		return VerificaColisaoTeto() ||
			VerificaColisaoChao() ||
			VerificaColisaoCanoCima() ||
			VerificaColisaoCanoBaixo();
	}

	bool VerificaColisaoCanoCima()
	{
		var posHimglyney = (larguraJanela / 2) - (imglyney.WidthRequest / 2);
		var posVimglyney = (alturaJanela / 2) - (imglyney.HeightRequest / 2) + imglyney.TranslationY;
		if (posHimglyney >= Math.Abs(imgcanocima.TranslationX) - imgcanocima.WidthRequest &&
		   posHimglyney <= Math.Abs(imgcanocima.TranslationX) + imgcanocima.WidthRequest &&
		   posVimglyney <= imgcanocima.HeightRequest + imgcanocima.TranslationY)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool VerificaColisaoCanoBaixo()
	{
		var posHimglyney = (larguraJanela / 2) - (imglyney.WidthRequest / 2);
		var posVimglyney = (alturaJanela / 2) + (imglyney.HeightRequest / 2) + imglyney.TranslationY;
		var yMaxCano = imgcanocima.HeightRequest + imgcanocima.TranslationY + AberturaMinima;
		if (posHimglyney >= Math.Abs(imgcanobaixo.TranslationX) - imgcanobaixo.WidthRequest &&
		   posHimglyney <= Math.Abs(imgcanobaixo.TranslationX) + imgcanobaixo.WidthRequest &&
		   posVimglyney >= yMaxCano)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool VerificaColisaoTeto()
	{
		var minY = -alturaJanela / 2;
		if (imglyney.TranslationY <= minY)
			return true;
		else
			return false;
	}

	bool VerificaColisaoChao()
	{
		var maxY = alturaJanela / 2 - imgChao.HeightRequest;
		if (imglyney.TranslationY >= maxY)
			return true;
		else
			return false;
	}


}

