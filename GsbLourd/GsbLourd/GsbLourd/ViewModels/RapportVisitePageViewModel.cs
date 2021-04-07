using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GsbLourd.ViewModels
{
    public class RapportVisitePageViewModel : ViewModelBase
    {
        private readonly HttpClient _client;
        private readonly INavigationService _navigationService;
        public RapportVisitePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _client = new HttpClient();
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
        }

        public bool IsFirst
        {
            get { return _isFirst; }
            set { SetProperty(ref _isFirst, value); }
        }
        private bool _isFirst;

        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _id;

        public string Nom
        {
            get { return _nom; }
            set { SetProperty(ref _nom, value); }
        }
        private string _nom;

        public string RapportId
        {
            get { return _rapportId; }
            set { SetProperty(ref _rapportId, value); }
        }
        private string _rapportId;

        public string Motif
        {
            get { return _motif; }
            set { SetProperty(ref _motif, value); }
        }
        private string _motif;

        public string Bilan
        {
            get { return _bilan; }
            set { SetProperty(ref _bilan, value); }
        }
        private string _bilan;

        public string PraticienId
        {
            get { return _praticienId; }
            set { SetProperty(ref _praticienId, value); }
        }
        private string _praticienId;

        public string Praticien
        {
            get { return _praticien; }
            set { SetProperty(ref _praticien, value); }
        }
        private string _praticien;

        public string RapportDate
        {
            get { return _rapportDate; }
            set { SetProperty(ref _rapportDate, value); }
        }
        private string _rapportDate;

        public int Numero
        {
            get { return _numero; }
            set { SetProperty(ref _numero, value); }
        }
        private int _numero;

        public string VisiteurId
        {
            get { return _visiteurId; }
            set { SetProperty(ref _visiteurId, value); }
        }
        private string _visiteurId;

        public String Answer
        {
            get { return _answer; }
            set { SetProperty(ref _answer, value); }
        }
        private String _answer;

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Id = (string)parameters["id"];
            Nom = (string)parameters["nom"];

            Numero = 0;
            IsFirst = false;

            await GetRapportVisiteCommand();

            base.OnNavigatedTo(parameters);

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
            IsFirst = true;
            GetRapportVisiteCommand();
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
            if(Numero != 0)
            {
                Numero--;
            }
            else
            {
                IsFirst = false;
            }
            
            GetRapportVisiteCommand();
        }

        public virtual bool CanExecutePrecedent()
        {
            return true;
        }

        public async Task GetRapportVisiteCommand()
        {
            try
            {
                Uri uri = new Uri("https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/RapportDeVisite/GetRapportVisite.php?VIS_MATRICULE=" + Id + "&NUMERO=" + Numero);
                Console.WriteLine("{0} URI", uri);

                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Test 1");
                    var answer = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("{0} Test 2", answer);

                    Console.WriteLine("Test 3");
                    dynamic dynJson = JsonConvert.DeserializeObject(answer);

                    Console.WriteLine("{0} Test 3", dynJson);



                    Console.WriteLine("{0} JSON", dynJson[0].PRA_NOM);

                    Console.WriteLine("{0} NOM", dynJson[0].PRA_NOM);


                    VisiteurId = dynJson[0].VIS_MATRICULE;
                    RapportId = dynJson[0].RAP_NUM;
                    RapportDate = dynJson[0].RAP_DATE;
                    Motif = dynJson[0].RAP_MOTIF;
                    Bilan = dynJson[0].RAP_BILAN;
                    PraticienId = dynJson[0].PRA_NUM;
                    Praticien = dynJson[0].PRA_NOM + " " + dynJson[0].PRA_PRENOM;




                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur, la recherche est vide");
                Numero--;
            }
        }




        //NAVIGATION
        public DelegateCommand NavigateCreerRapportCommand
        {
            get { return _navigateCreerRapportCommand ?? (_navigateCreerRapportCommand = new DelegateCommand(ExecuteNavigateCreerRapportCommand, CanExecuteNavigateCreerRapportCommand)); }
        }
        private DelegateCommand _navigateCreerRapportCommand;

        public async void ExecuteNavigateCreerRapportCommand()
        {
            NavigationParameters navigationParameters = new NavigationParameters();

            navigationParameters.Add("id", Id);
            navigationParameters.Add("nom", Nom);

            await _navigationService.NavigateAsync("CreationRapportVisitePage", navigationParameters);
        }
        public virtual bool CanExecuteNavigateCreerRapportCommand()
        {
            return true;
        }
    }
}