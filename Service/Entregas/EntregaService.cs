using App_Entrega.Models;
using App_Entrega.ViewModels.Entregas;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Entrega.Service.Entregas
{
    public class EntregaService: Request
    {
        
        private readonly Request _request;

        private const string apiUrlBase = "http://nin2.somee.com/AppEntrega/Entregas";
        private string _token;
        public EntregaService(string token)
        {

            _request = new Request();
            _token = token;

        }
        public async Task<ObservableCollection<Entrega>> GetEntregasAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Entrega> listaEntregas = await
      _request.GetAsync<ObservableCollection<Models.Entrega>>(apiUrlBase + urlComplementar, _token);
            return listaEntregas;
        }
        public async Task<Entrega> GetEntregaAsync(int entregaId)
        {
            string urlComplementar = string.Format("/{0}", entregaId);
            var entrega = await _request.GetAsync<Models.Entrega>(apiUrlBase + urlComplementar, _token);
            return entrega;
        }
        public async Task<int> PostEntregaAsync(Entrega p)
        {
            return await _request.PostReturnIntTokenAsync(apiUrlBase, p, _token);
        }
        public async Task<int> PutEntregaAsync(Entrega p)
        {
            var result = await _request.PutAsync(apiUrlBase, p, _token);
            return result;
        }
        public async Task<int> DeleteEntregaAsync(int entregaId)
        {
            string urlComplementar = string.Format("/{0}", entregaId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }

    }
}
