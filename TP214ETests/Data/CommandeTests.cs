using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using Moq;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class CommandeTests
    {
        [TestMethod()]
        public void Test_constructeur_avec_parametre()
        {
            int noCommande = 1000;

            Commande commande = new Commande(noCommande);

            Assert.AreEqual(noCommande, commande.NoCommande);
            Assert.IsNotNull(commande.ListeArticleCommande);
        }

        [TestMethod()]
        public void Test_constructeur_sans_parametre()
        {
            Commande commande = new Commande();
            Assert.IsNotNull(commande.ListeArticleCommande);
        }

        [TestMethod()]
        public void Test_execption_sur_constructeur_si_no_commande_plus_petit_que_1000()
        {
            int noCommande = 999;

            Commande commande;

            Assert.ThrowsException<ArgumentException>(() => commande = new Commande(noCommande));
        }

        [TestMethod()]
        public void Test_execption_si_cout_total_commande_est_negatif()
        {
            var commande = new Commande();

            Assert.ThrowsException<ArgumentException>(() => commande.CoutTotalCommande = -1000);
        }

        [TestMethod()]
        public void Test_execption_si_date_commande_est_dans_le_future()
        {
            var commande = new Commande();
            DateTime demain = DateTime.Today.AddDays(1);

            Assert.ThrowsException<ArgumentException>(() => commande.DateCommande = demain);
        }

        [TestMethod()]
        public void Test_methode_Calculer_Vendant_Commande()
        {
            Commande commande = ObtenirMockCommandes()[0];

            Assert.AreEqual(85, commande.CalculerVendantCommande());
        }

        [TestMethod()]
        public void Test_methode_to_string()
        {
            DateTime aujourdhui = DateTime.Today;

            Commande commande = ObtenirMockCommandes()[0];

            commande.CalculerVendantCommande();

            Assert.AreEqual(aujourdhui + " - 1000 = 85,00 $", commande.ToString());
        }

        private List<Commande> ObtenirMockCommandes()
        {
            List<Commande> commandesMock = new List<Commande>
            {
                new Commande
                {
                    NoCommande = 1000,
                    ListeArticleCommande = new List<ArticleCommande>
                    {
                        new ArticleCommande
                        {
                            Article = new Recette
                            {
                                NomRecette = "Burger BLT",
                                Vendant = 20
                            },
                            QuantiteArticle = 2,
                            CoutArticle = 40
                        },
                        new ArticleCommande
                        {
                            Article = new Recette
                            {
                                NomRecette = "Burger Inter",
                                Vendant = 15
                            },
                            QuantiteArticle = 3,
                            CoutArticle = 45
                        },
                    },
                    DateCommande = DateTime.Today
                },
                new Commande
                {
                    NoCommande = 1001,
                    ListeArticleCommande = new List<ArticleCommande>
                    {
                        new ArticleCommande
                        {
                            Article = new Recette
                            {
                                NomRecette = "Burger BLT",
                                Vendant = 20
                            },
                            QuantiteArticle = 1,
                            CoutArticle = 20
                        },
                        new ArticleCommande
                        {
                            Article = new Recette
                            {
                                NomRecette = "Poutine",
                                Vendant = 11
                            },
                            QuantiteArticle = 2,
                            CoutArticle = 22
                        },
                    },
                    DateCommande = DateTime.Today
                }
            };
            return commandesMock;
        }
    }
}

