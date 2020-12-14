using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Formulario
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {

        HttpClient client = new HttpClient();

        public MainWindow()
        {

            InitializeComponent();

            client.BaseAddress = new Uri("https://localhost:44338/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.Loaded += MainWindow_Loaded;

        }

        async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("/forms/");
                response.EnsureSuccessStatusCode(); // Lança um código de erro
                var result = await response.Content.ReadAsAsync<IEnumerable<capturar>>();
                listall.ItemsSource = result;
            }
            catch
            {
                //throw;
            }
        }

        private async void btnGetEstudante_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("/Index/" + txtID.Text);
                response.EnsureSuccessStatusCode(); //lança um código de erro
                var result = await response.Content.ReadAsAsync<capturar>();
                estudanteDetalhesPanel.Visibility = Visibility.Visible;
                estudanteDetalhesPanel.DataContext = result;
            }
            catch (Exception)
            {
                MessageBox.Show("Estudante não localizado");
            }
        }

        private async void btnNovoEstudante_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var detalhe = new capturar()
                {

                    nome = txtnome.Text,
                    id = int.Parse(txtid.Text),
                    sobrenome = txtsobrenome.Text
                };
                var response = await client.PostAsJsonAsync("/forms/Create/", detalhe);
                response.EnsureSuccessStatusCode(); //lança um código de erro
                MessageBox.Show("Incluído com sucesso", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                listall.ItemsSource = await GetAllcapturar();
                listall.ScrollIntoView(listall.ItemContainerGenerator.Items[listall.Items.Count - 1]);
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi incluído. (Verifique se o ID não esta duplicado)");
            }
        }

        private async void btnAtualiza_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var detalhe = new capturar()
                {
                    nome = txtnome.Text,
                    id = int.Parse(txtid.Text),
                    sobrenome = txtsobrenome.Text
                };
                var response = await client.PutAsJsonAsync("forms/Edit/", detalhe);
                response.EnsureSuccessStatusCode(); //lança um código de erro
                MessageBox.Show("Atualizado com sucesso", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                listall.ItemsSource = await GetAllcapturar();
                listall.ScrollIntoView(listall.ItemContainerGenerator.Items[listall.Items.Count - 1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDeletEstudante_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("/forms/Delete/" + txtID.Text);
                response.EnsureSuccessStatusCode(); //lança um código de erro
                MessageBox.Show("Deletado com sucesso");
                listall.ItemsSource = await GetAllcapturar();
                listall.ScrollIntoView(listall.ItemContainerGenerator.Items[listall.Items.Count - 1]);
            }
            catch (Exception)
            {
                MessageBox.Show("Deletado com sucesso");
            }
        }

        public async Task<IEnumerable<capturar>> GetAllcapturar()
        {
            HttpResponseMessage response = await client.GetAsync("/forms/");
            response.EnsureSuccessStatusCode(); //lança um código de erro
            var cap = await response.Content.ReadAsAsync<IEnumerable<capturar>>();
            return cap;
        }
    }
}

