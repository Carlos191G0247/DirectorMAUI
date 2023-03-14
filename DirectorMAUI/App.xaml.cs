using DirectorMAUI.Views;

namespace DirectorMAUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new Views.IniciarSesionView());
	}
}
