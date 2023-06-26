using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input; 
using App_Entrega.Service.Entregas;
using App_Entrega.Models;
using App_Entrega.ViewModels;

namespace App_Entrega.ViewModels.Entregas
{
    public class ListagemEntregaViewModel : BaseViewModel
    {
        private EntregaService pService;

        public ObservableCollection<Entrega> Entregas { get; set; }


        public ListagemEntregaViewModel()
        {

            string token = Preferences.Get("UsuarioToken ", string.Empty);
            pService = new EntregaService(token);
            Entregas = new ObservableCollection<Entrega>();
            _ = ObterEntregas();

            NovaEntrega = new Command(async () => { await ExibirCadastroEntrega(); });
            RemoverEntregaCommand = new Command<Entrega>(async (Entrega p) => { await RemoverEntrega(p); });
        }
        public ICommand NovaEntrega { get; }

        public ICommand RemoverEntregaCommand { get; }

        public async Task ObterEntregas()
        {
            try //Junto com o Cacth evitará que erros fechem o aplicativo
            {
                Entregas = await pService.GetEntregasAsync();
                OnPropertyChanged(nameof(Entregas)); //Informará a View que houve carregamento
            }
            catch (Exception ex)
            {
                //Captará o erro para exibir em tela
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes" + ex.InnerException, "Ok");
            }
        }
        public async Task ExibirCadastroEntrega()
        {
            try
            {
                await Shell.Current.GoToAsync("cadEntregaView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.
                    DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
        private Entrega entregaSelecionada;
        public Entrega EntregaSelecionada
        {
            get { return entregaSelecionada; }
            set
            {
                if (value != null)
                {
                    entregaSelecionada = value;
                    Shell.Current.GoToAsync($"cadEntregaView?pId={entregaSelecionada.Id}");
                }
            }
        }

        public async Task RemoverEntrega(Entrega p)
        {
            try
            {
                if (await Application.Current.MainPage.DisplayAlert("Confirmação", $"Confirma que quer remover a entrega de {p.IdMorador}?", "sim", "Não"))
                {
                    await pService.DeleteEntregaAsync(p.Id);

                    await Application.Current.MainPage.DisplayAlert("Mensagem", "Entrega removida com sucesso!", "Ok");

                    _ = ObterEntregas();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
