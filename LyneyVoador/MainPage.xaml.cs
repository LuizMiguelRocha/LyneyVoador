namespace LyneyVoador;

public partial class MainPage : ContentPage
{
	const int gravidade = 1;
	const int TempoEntreFrames = 25;
	bool estaMorto = false;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 20;
	const int ForcaPulo = 30;
	const int MaxTempoPulando = 3;
	bool EstaPulando = false;
	int TempoPulando = 0;
	const int AberturaMinima = 30;
	int score=0;

	void Inicializar()
	{
		imglyney.TranslationY = 0;
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
		if(TempoPulando >= MaxTempoPulando)
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
			AplicaGravidade();
			GerenciaCanos();
			if (VerificaColisao())
			{
				estaMorto = true;
				FrameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(TempoEntreFrames);
			if (EstaPulando)
				AplicaPulo();
			else
				AplicaGravidade();
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
			var alturaMax = -100;
			var alturaMin = -imgcanobaixo.HeightRequest;
			imgcanocima.TranslationY = Random.Shared.Next((int)alturaMin, (int)alturaMax);
			imgcanobaixo.TranslationY = imgcanocima.HeightRequest + imgcanocima.TranslationY + AberturaMinima;
			score++;
			labelScore.Text = "Cano: " + score.ToString("D3");
			fianlScore.Text = "Total de canos passados  " + score.ToString("");
		}
	}


	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			if (VerificaColisaoTeto() || VerificaColisaoChao())
			{
				return true;
			}
		}
		return false;
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

