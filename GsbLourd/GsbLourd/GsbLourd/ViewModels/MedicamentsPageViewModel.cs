using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Net.Http;

namespace GsbLourd.ViewModels
{
    public class MedicamentsPageViewModel : ViewModelBase
    {
        private readonly HttpClient _client;
        private readonly INavigationService _navigationService;
        public MedicamentsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _client = new HttpClient();
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Numero = 1;
            RecupMedicament();

            base.OnNavigatedTo(parameters);
        }

        public int Numero
        {
            get { return _numero; }
            set { SetProperty(ref _numero, value); }
        }
        private int _numero;

        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
        }
        private string _code;

        public string Nom
        {
            get { return _nom; }
            set { SetProperty(ref _nom, value); }
        }
        private string _nom;

        public string Famille
        {
            get { return _famille; }
            set { SetProperty(ref _famille, value); }
        }
        private string _famille;

        public string Prix
        {
            get { return _prix; }
            set { SetProperty(ref _prix, value); }
        }
        private string _prix;

        public string ContreIndication
        {
            get { return _contreIndication; }
            set { SetProperty(ref _contreIndication, value); }
        }
        private string _contreIndication;

        public string Effet
        {
            get { return _effet; }
            set { SetProperty(ref _effet, value); }
        }
        private string _effet;

        public string Composition
        {
            get { return _composition; }
            set { SetProperty(ref _composition, value); }
        }
        private string _composition;



        public async void RecupMedicament()
        {
            Uri uri = new Uri("https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/GetMedicaments.php?MED_NUM=" + Numero.ToString());
            Console.WriteLine("{0} URI", uri);

            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var answer = await response.Content.ReadAsStringAsync();

                var Answer = JObject.Parse(answer);

                Code = (string)Answer["MED_DEPOTLEGAL"];
                Nom = (string)Answer["MED_NOMCOMMERCIAL"];
                Famille = (string)Answer["FAM_LIBELLE"];
                Prix = (string)Answer["MED_PRIXECHANTILLON"];
                ContreIndication = (string)Answer["MED_CONTREINDIC"];
                Effet = (string)Answer["MED_EFFETS"];
                Composition = (string)Answer["MED_COMPOSITION"];
            }
        }

        //SUIVANT
        public DelegateCommand Suivant
        {
            get { return _suivant ?? (_suivant = new DelegateCommand(ExecuteSuivant, CanExecuteSuivant)); }
        }
        private DelegateCommand _suivant;

        public async void ExecuteSuivant()
        {
            Numero++;
            RecupMedicament();
        }

        public virtual bool CanExecuteSuivant()
        {
            return true;
        }

        //PRECENDET
        public DelegateCommand Precedent
        {
            get { return _precedent ?? (_precedent = new DelegateCommand(ExecutePrecedent, CanExecutePrecedent)); }
        }
        private DelegateCommand _precedent;

        public async void ExecutePrecedent()
        {
            Numero--;
            RecupMedicament();
        }

        public virtual bool CanExecutePrecedent()
        {
            return true;
        }
    }
}