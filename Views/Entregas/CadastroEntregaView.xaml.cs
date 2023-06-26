using App_Entrega.ViewModels.Entregas;

namespace App_Entrega.Views.Entregas;

public partial class CadastroEntregaView : ContentPage
{
     private CadastroEntregaViewModel cadViewModel;
    public CadastroEntregaView()
    {
        InitializeComponent();

        cadViewModel = new CadastroEntregaViewModel();
        BindingContext = cadViewModel;
        Title = "NovaEntrega";
    }

}