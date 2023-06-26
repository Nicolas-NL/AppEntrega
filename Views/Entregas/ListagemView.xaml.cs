using App_Entrega.Models;
using App_Entrega.ViewModels.Entregas;
namespace App_Entrega.Views.Entregas;

public partial class ListagemView : ContentPage
{
    ListagemEntregaViewModel viewModel;

    public ListagemView()
    {
        InitializeComponent();
        viewModel = new ListagemEntregaViewModel();
        BindingContext = viewModel;
        Title = "Entregas - App Entrega";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = viewModel.ObterEntregas();
    }
}