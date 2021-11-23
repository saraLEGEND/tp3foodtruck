using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace TP214E.Data
{
    public class Recette : IRecette
    {
        private ObjectId id;

        private string nomRecette;

        private decimal vendant;

        private List<(double, Aliment)> listeIngredients;

        public Recette()
        {
        }

        public Recette(string pNomrecette, decimal pVendant)
        {
            NomRecette = pNomrecette;
            Vendant = pVendant;
            ListeIngredients = new List<(double, Aliment)>();
        }

        public ObjectId Id
        {
            get { return id; }
            set { id = value; }
        }

        public string NomRecette
        {
            get { return nomRecette; }
            set
            {
                string nomRecu = value.Trim();
                if (nomRecu.Length > 0)
                {
                    nomRecette = nomRecu;
                }
                else
                {
                    throw new ArgumentException("Le nom de la recette est vide.");
                }
            }
        }

        public List<(double, Aliment)> ListeIngredients
        {
            get { return listeIngredients; }
            set
            {
                if (value != null)
                {
                    foreach ((double quantite, Aliment aliment) in value)
                    {
                        if (quantite <= 0)
                        {
                            throw new ArgumentException("La quantite des doit etre supérieure à zéro");
                        }
                    }
                }
                

                listeIngredients = value;
            }
        }

        public decimal Vendant
        {
            get { return vendant; }
            set
            {
                if (value >= 0)
                {
                    vendant = value;
                }
                else
                {
                    throw new ArgumentException("Le vendant de la recette ne doit pas être un nobre négatif.");
                }
            }
        }

        public override string ToString()
        {
            return String.Format("{0:c} - {1}", Vendant, NomRecette);
        }
    }
}
