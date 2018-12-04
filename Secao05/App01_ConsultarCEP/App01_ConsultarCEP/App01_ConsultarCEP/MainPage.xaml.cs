using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnBotao.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = txtCEP.Text.Trim();
            if (IsValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscaEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        lblResultado.Text = string.Format("Endereço: {0}\nBairro: {1}\nCidade: {2}\nEstado: {3}  ", end.Logradouro, end.Bairro, end.Localidade, end.Uf);
                    }
                    else
                    {
                        DisplayAlert("ERRO","O endereço não foi encontrado para o CEP informado: " + cep,"OK");
                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }

            }
        }

        private bool IsValidCEP(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP Inválido!\nO CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }

            int novoCEP = 0;

            if (!int.TryParse(cep, out novoCEP))
            {
                DisplayAlert("Erro", "CEP Inválido!\nO CEP deve ser composto apenas por números.", "OK");
                valido = false;
            }

            return valido;
        }

    }
}
