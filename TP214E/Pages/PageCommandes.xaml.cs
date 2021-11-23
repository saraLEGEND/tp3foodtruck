using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TP214E.Data;


namespace TP214E.Pages
{
    /// <summary>
    /// Logique d'interaction pour PageCommandes.xaml
    /// </summary>
    public partial class PageCommandes : Page
    {
        private Commande commande;
        private DispatcherTimer dispatcherTimer;

        public PageCommandes()
        {
            InitializeComponent();
            AfficherPlatsAEcran();
            InitialiserMinuteurConfirmationCommande();
            LblTotalCommande.Content = String.Format("{0:c}", 0);
        }

        private void BtnHistorique_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/PageHistoriqueCommandes.xaml", UriKind.Relative));

        }
        private void BtnRetourAccueil_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/PageAccueil.xaml", UriKind.Relative));
        }

        private void AfficherPlatsAEcran()
        {
            foreach (Recette plat in PageAccueil.listeRecettes)
            {
                LstPlats.Items.Add(plat);
            }
        }

        private void BtnEffacer_Click(object sender, RoutedEventArgs e)
        {
            LstCommande.Items.Clear();
            CalculerTotalCommandeEnCours();
        }

        private void BtnAjouterPlat_Click(object sender, RoutedEventArgs e)
        {
            int index = LstPlats.SelectedIndex;
            if (index != -1)
            {
                Recette recetteSelectionnee = (Recette)LstPlats.Items[index];
                
                if (ValiderPlatUnique(recetteSelectionnee))
                {
                    AjouterPlatACommande(recetteSelectionnee);
                }
            }
        }

        public bool ValiderPlatUnique(Recette pRecetteSelectionnee)
        {
            bool estUnique = true;

            foreach (ArticleCommande element in LstCommande.Items)
            {
                if (pRecetteSelectionnee == element.Article)
                {
                    const string message = "Le plat existe déjà dans la commande. Augmenter plutôt la quantité.";
                    const string caption = "Plat en double";
                    var result = MessageBox.Show(message, caption, MessageBoxButton.OK);
                    estUnique = false;
                }
            }
            return estUnique;
        }

        public void AjouterPlatACommande(Recette pRecetteSelectionnee)
        {
            ArticleCommande article = new ArticleCommande(1, pRecetteSelectionnee);
            LstCommande.Items.Add(article);
            CalculerTotalCommandeEnCours();
        }

        private void BtnAugmenterQte_Click(object sender, RoutedEventArgs e)
        {
            int index = LstCommande.SelectedIndex;
            if (index != -1)
            {
                ((ArticleCommande)LstCommande.Items[index]).QuantiteArticle++;
                LstCommande.Items.Refresh();
                CalculerTotalCommandeEnCours();
            }
        }

        private void BtnDiminuerQte_Click(object sender, RoutedEventArgs e)
        {
            int index = LstCommande.SelectedIndex;
            if (index != -1)
            {
                if (((ArticleCommande)LstCommande.Items[index]).QuantiteArticle == 1)
                {
                    LstCommande.Items.RemoveAt(index);
                }
                else
                {
                    ((ArticleCommande)LstCommande.Items[index]).QuantiteArticle--;
                }

                LstCommande.Items.Refresh();
                CalculerTotalCommandeEnCours();
            }
        }

        private void BtnCommander_Click(object sender, RoutedEventArgs e)
        {
            commande = new Commande(ObtenirNoCommande());
            commande.DateCommande = DateTime.Now;
    
            if (LstCommande.Items.Count > 0 )
            {
                foreach (ArticleCommande article in LstCommande.Items)
                {
                    commande.ListeArticleCommande.Add(article);
                }

                commande.CalculerVendantCommande();
                PageAccueil.listeCommandes.Add(commande);
                PageAccueil.dal.CreerCommande(commande);
                ReinitialiserApresCommande();
            }
            else
            {
                MessageBox.Show("La commande doit contenir des plats pour être enregistrer.",
                    "Attention",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        public int ObtenirNoCommande()
        {
            int noCommande;
            
            if (PageAccueil.listeCommandes.Count == 0)
            {
                noCommande = 1000;
            } else
            {
                noCommande = PageAccueil.listeCommandes.Max(t => t.NoCommande+1);
            }
            return noCommande;
        }

        public void CalculerTotalCommandeEnCours()
        {
            decimal total = 0;
            foreach (ArticleCommande article in LstCommande.Items)
            {
                total += article.CalculerVendantArticle();
            }
            LblTotalCommande.Content = String.Format("{0:c}", total);
        }

        private void InitialiserMinuteurConfirmationCommande()
        {
            LblConfirmCommande.Visibility = Visibility.Collapsed;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LblConfirmCommande.Visibility = Visibility.Collapsed;
            dispatcherTimer.IsEnabled = false;
        }

        private void ReinitialiserApresCommande()
        {
            LblConfirmCommande.Visibility = Visibility.Visible;
            dispatcherTimer.Start();
            LstCommande.Items.Clear();
            CalculerTotalCommandeEnCours();
        }
    }
}
