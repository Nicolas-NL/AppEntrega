using App_Entrega.Views.Entregas;
namespace App_Entrega;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("cadEntregaView", typeof(CadastroEntregaView));
    }
		
}
	