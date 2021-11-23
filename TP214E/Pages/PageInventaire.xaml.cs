using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TP214E.Data;
using TP214E.Enumeration;

namespace TP214E
{
    /// <summary>
    /// Logique d'interaction pour Inventaire.xaml
    /// </summary>
    public partial class PageInventaire : Page
    {
        private bool estPourAjouter = false;
        private bool estPourModifier = false;
        private bool estPourSupprimer = false;

        public PageInventaire(AccesDonnees dal)
        {
            InitializeComponent();
            AfficherAlimentsAEcran();
            ActiverChampsFormulaire(false);
        }

        private void AfficherAlimentsAEcran()
        {
            foreach (Aliment aliment in PageAccueil.listeAliments)
            {
                LstAliments.Items.Add(aliment);
            }
        }

        private void ActiverChampsFormulaire(bool pActiver)
        {
            TxTNom.IsEnabled = pActiver;
            TxtQuantite.IsEnabled = pActiver;
            TxtCoutVente.IsEnabled = pActiver;
            OptGramme.IsEnabled = pActiver;
            OptKilogramme.IsEnabled = pActiver;
            OptMillilitre.IsEnabled = pActiver;
            OptLitre.IsEnabled = pActiver;
            OptUnite.IsEnabled = pActiver;
        }

        private void BtnRetourAccueil_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/PageAccueil.xaml", UriKind.Relative));
        }

        private void LstAliments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = LstAliments.SelectedIndex;
            if (index != -1)
            {
                Aliment aliment = (Aliment)LstAliments.Items[index];
                TxTNom.Text = aliment.Nom;
                TxtQuantite.Text = aliment.Quantite.ToString();
                TxtCoutVente.Text = aliment.CoutVente.ToString();
                switch (aliment.UniteMesure)
                {
                    case UniteMesure.gramme:
                        OptGramme.IsChecked = true;
                        break;
                    case UniteMesure.kilogramme:
                        OptKilogramme.IsChecked = true;
                        break;
                    case UniteMesure.millilitre:
                        OptMillilitre.IsChecked = true;
                        break;
                    case UniteMesure.litre:
                        OptLitre.IsChecked = true;
                        break;
                    case UniteMesure.unite:
                        OptUnite.IsChecked = true;
                        break;
                }
            }
        }

        private void ViderInformationsAlimentAEcran()
        {
            TxTNom.Text = "";
            TxtQuantite.Text = "";
            TxtCoutVente.Text = "";
            OptGramme.IsChecked = false;
            OptKilogramme.IsChecked = false;
            OptMillilitre.IsChecked = false;
            OptLitre.IsChecked = false;
            OptUnite.IsChecked = false;
            LblTitreActionChoisiPourAliment.Content = "";
            LblTitreActionChoisiPourAliment.Background = new SolidColorBrush(Colors.Black);
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AcualiserAffichageSelonActionChoisi("AJOUTER UN ALIMENT");
            estPourAjouter = true;
            estPourModifier = false;
            estPourSupprimer = false;
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            AcualiserAffichageSelonActionChoisi("MODIFIER UN ALIMENT");
            estPourAjouter = false;
            estPourModifier = true;
            estPourSupprimer = false;
        }


        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            AcualiserAffichageSelonActionChoisi("SUPPRIMER UN ALIMENT");
            estPourAjouter = false;
            estPourModifier = false;
            estPourSupprimer = true;
        }

        private void AcualiserAffichageSelonActionChoisi(string pTitreActionChoisi)
        {
            ViderInformationsAlimentAEcran();
            ActiverChampsFormulaire(true);
            LblTitreActionChoisiPourAliment.Content = pTitreActionChoisi;
            LblTitreActionChoisiPourAliment.Background = new SolidColorBrush(Colors.GreenYellow);
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            ViderInformationsAlimentAEcran();
            ActiverChampsFormulaire(false);
            LblTitreActionChoisiPourAliment.Content = "";
            LblTitreActionChoisiPourAliment.Background = new SolidColorBrush(Colors.Black);
            LstAliments.SelectedIndex = -1;
            estPourAjouter = false;
            estPourModifier = false;
            estPourSupprimer = false;
        }

        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (TxTNom.Text != "" && TxtCoutVente.Text != "" && TxtQuantite.Text != ""
                && OptGramme.IsChecked.Value || OptKilogramme.IsChecked.Value ||
                OptMillilitre.IsChecked.Value || OptLitre.IsChecked.Value || OptUnite.IsChecked.Value)
            {
                if (estPourAjouter)
                {
                    AjouterOuModifierAliment();
                    LstAliments.IsEnabled = false;
                }

                if (estPourModifier)
                {
                    AjouterOuModifierAliment();
                }

                if (estPourSupprimer)
                {
                    SupprimerAliment();
                }

                ViderInformationsAlimentAEcran();
                ActiverChampsFormulaire(false);
                estPourAjouter = false;
                estPourModifier = false;
                estPourSupprimer = false;
                LstAliments.IsEnabled = true;
                LstAliments.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Les champs ne doivent pas être vide.",
                    "Attention",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void AjouterOuModifierAliment()
        {
            int index = LstAliments.SelectedIndex;
            Aliment aliment = new Aliment();
            aliment = DefinirValeursAliment(aliment);
            
            if (index != -1)
            {
                ((Aliment)LstAliments.Items[index]).Nom = aliment.Nom;
                ((Aliment)LstAliments.Items[index]).Quantite = aliment.Quantite;
                ((Aliment)LstAliments.Items[index]).CoutVente = aliment.CoutVente;
                ((Aliment)LstAliments.Items[index]).UniteMesure = aliment.UniteMesure;
                LstAliments.Items.Refresh();
                aliment.Id = ((Aliment)LstAliments.Items[index]).Id;
                PageAccueil.dal.MettreAJourAliment(aliment);
            }
            else
            {
                LstAliments.Items.Add(aliment);
                PageAccueil.dal.CreerAliment(aliment);
            }
        }

        private Aliment DefinirValeursAliment(Aliment pAliment)
        {
            try
            {
                pAliment.Nom = TxTNom.Text.Trim();
                pAliment.Quantite = double.Parse(TxtQuantite.Text.Trim());
                pAliment.CoutVente = decimal.Parse(TxtCoutVente.Text.Trim());
                
                if (OptGramme.IsChecked.Value)
                {
                    pAliment.UniteMesure = UniteMesure.gramme;
                }
                else if (OptKilogramme.IsChecked.Value)
                {
                    pAliment.UniteMesure = UniteMesure.kilogramme;
                }
                else if (OptMillilitre.IsChecked.Value)
                {
                    pAliment.UniteMesure = UniteMesure.millilitre;
                }
                else if (OptLitre.IsChecked.Value)
                {
                    pAliment.UniteMesure = UniteMesure.litre;
                }
                else
                {
                    pAliment.UniteMesure = UniteMesure.unite;
                }
            }
            catch (ArgumentException msgException)
            {
                MessageBox.Show(msgException.Message,
                    "Attention",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("La valeur entrée n'est pas valide.",
                    "Attention",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("La valeur entrée n'a pu être traitée.",
                    "Attention",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            return pAliment;
        }

        private void SupprimerAliment()
        {
            Aliment aliment = new Aliment();
            int index = LstAliments.SelectedIndex;
            if (index != -1)
            {
                aliment = (Aliment)LstAliments.SelectedItem;
                LstAliments.Items.RemoveAt(index);
                PageAccueil.dal.SupprimerAliment(aliment);
            }
            else
            {
                MessageBox.Show("Veuillez choisir un aliment",
                    "Attention",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
