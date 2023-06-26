using App_Entrega.Views.Entregas;

namespace App_Entrega;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
