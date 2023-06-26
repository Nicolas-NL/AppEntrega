
using System.Windows.Input;
using App_Entrega.Models;
using App_Entrega.Service.Entregas;

namespace App_Entrega.ViewModels.Entregas
{
    [QueryProperty("EntregaSelecionadaId", "pId")]
    public class CadastroEntregaViewModel: BaseViewModel
    {

        private EntregaService pService;



        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; set; }

        public CadastroEntregaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new EntregaService(token);

            SalvarCommand = new Command(async () => { await SalvarEntrega(); });
            CancelarCommand = new Command(async => CancelarCadastro());
        }

        private async void CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }



        private int id;
        private string remetente;
        private int idMorador;
        private int hora;
        private DateTime dataEntrega;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();//Informa mundaça de estado para a View
            }
        }



        public string Remetente
            {
                get => remetente;
                set
                {
                    remetente = value;
                    OnPropertyChanged();
                }
            }


            public int IdMorador
            {       
                get => idMorador;
                set
                {
                    idMorador = value;
                    OnPropertyChanged();
                }
            }
        public DateTime DataEntrega
        {
            get => dataEntrega;
            set
            {
                dataEntrega = value;
                OnPropertyChanged();
            }
        }
        public int Hora 
        {
            get => hora;
            set
            {
                hora = value;
                OnPropertyChanged();
            }
        }

        public async Task SalvarEntrega()
            {
            try
            {
                    Entrega model = new Entrega()
                    {
                       
                        Remetente = this.remetente,
                        DataEntrega = DateTime.Now.Date,
                        Hora = DateTime.Now.Hour,
                        IdMorador = this.idMorador,
                        Id = this.id,
                    };
                if(model.Id == 0)
                    await pService.PostEntregaAsync(model);
                
                else
                    await pService.PutEntregaAsync(model);
                
                await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");

                await Shell.Current.GoToAsync(".."); //Remove a página atual da pilha de páginas                 
                }
                catch (Exception ex)
            {
                    await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");
            }
            }
            public async void CarregarEntrega()
            {
                try
                {
                    Entrega p = await pService.GetEntregaAsync(int.Parse(entregaSelecionadaId));
                    this.Hora = p.Hora;
                    this.DataEntrega = p.DataEntrega;
                    this.Remetente = p.Remetente;
                    this.IdMorador = p.IdMorador;
                    this.Id = p.Id;

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        private string entregaSelecionadaId;
        public string EntregaSelecionadaId
        {
            set
            {
                if (value != null)
                {
                    entregaSelecionadaId = Uri.UnescapeDataString(value);
                    CarregarEntrega();
                }
            }
        }

    }
}
