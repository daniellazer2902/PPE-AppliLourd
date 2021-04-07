using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GsbLourd.ViewModels
{
    public class CreationRapportVisitePageViewModel : ViewModelBase
    {
        private readonly HttpClient _client;
        private readonly INavigationService _navigationService;
        public CreationRapportVisitePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _client = new HttpClient();
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
        }

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

        public string VisiteurId
        {
            get { return _visiteurId; }
            set { SetProperty(ref _visiteurId, value); }
        }
        private string _visiteurId;

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Id = (string)parameters["id"];
            Nom = (string)parameters["nom"];

            base.OnNavigatedTo(parameters);

        }

        public DelegateCommand CreationRapportVisite
        {
            get { return _creationRapportVisite ?? (_creationRapportVisite = new DelegateCommand(ExecuteCreationRapportVisite, CanExecuteCreationRapportVisite)); }
        }
        private DelegateCommand _creationRapportVisite;

        public async void ExecuteCreationRapportVisite()
        {
            Uri uri = new Uri("https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/RapportDeVisite/InsertRapportDeVisite.php?RAP_MOTIF=" + Motif + "&VIS_MATRICULE=" + Id + "&PRA_NUM=" + PraticienId + "&RAP_DATE=" + RapportDate + "&RAP_BILAN=" + Bilan);

            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                NavigationParameters navigationParameters = new NavigationParameters();

                navigationParameters.Add("id", Id);
                navigationParameters.Add("nom", Nom);

                await _navigationService.NavigateAsync("RapportVisitePage", navigationParameters);

            }
        }
        public virtual bool CanExecuteCreationRapportVisite()
        {
            return true;
        }
    }
}