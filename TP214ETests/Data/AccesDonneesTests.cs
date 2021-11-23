using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using MongoDB.Driver;
using Autofac.Extras.Moq;
using MongoDB.Bson;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class AccesDonneesTests
    {
        [TestMethod]
        public void Tester_Connexion_Client_Valide()
        {
            using (var mock = AutoMock.GetLoose())
            {
                Mock<MongoClient> mockClient = new Mock<MongoClient>();

                mock.Mock<IAccesDonnees>()
                    .Setup(x => x.OuvrirConnexion())
                    .Returns(mockClient.Object);

                var connection = mock.Create<IAccesDonnees>();
                var test = connection.OuvrirConnexion();
                var attendu = mockClient;

                Assert.IsTrue(test != null);
                Assert.AreEqual(attendu.Object, test);
            }
        }

        [TestMethod]
        public void Tester_Connexion_Base_Donnees_Valide()
        {
            using (var mock = AutoMock.GetLoose())
            {
                Mock<IMongoDatabase> mockBD = new Mock<IMongoDatabase>();

                mock.Mock<IAccesDonnees>()
                    .Setup(x => x.ConnecterBaseDonnees())
                    .Returns(mockBD.Object);

                var connection = mock.Create<IAccesDonnees>();
                var test = connection.ConnecterBaseDonnees();
                var attendu = mockBD;

                Assert.IsTrue(test != null);
                Assert.AreEqual(attendu.Object, test);
            }
        }

        [TestMethod()]
        public void Test_recuperer_toutes_les_commandes()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccesDonnees>()
                    .Setup(x => x.ObtenirCollectionCommandes())
                    .Returns(ObtenirMockCommandes());

                var classeAccesDonnees = mock.Create<IAccesDonnees>();
                var attendu = ObtenirMockCommandes();

                var reel = classeAccesDonnees.ObtenirCollectionCommandes();

                Assert.IsTrue(reel != null);
                Assert.AreEqual(attendu.Count, reel.Count);

                for (int i = 0; i < attendu.Count; i++)
                {
                    Assert.AreEqual(attendu[i].NoCommande, reel[i].NoCommande);
                    Assert.AreEqual(attendu[i].ListeArticleCommande[i].Article.NomRecette, reel[i].ListeArticleCommande[i].Article.NomRecette);
                }
            }
        }

        [TestMethod()]
        public void Test_creer_commande_retour_vrai_si_commande_valide()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var commande = ObtenirMockCommandes()[0];
                mock.Mock<IAccesDonnees>()
                    .Setup(x => x.CreerCommande(commande))
                    .Returns(true);

                var classeAccesDonnees = mock.Create<IAccesDonnees>();

                var reel = classeAccesDonnees.CreerCommande(commande);

                Assert.AreEqual(true, reel);

                mock.Mock<IAccesDonnees>()
                    .Verify(x => x.CreerCommande(commande), Times.Exactly(1));
            }
        }

        [TestMethod()]
        public void Test_recuperer_toutes_les_Recettes()
        {
            using (var mock = AutoMock.GetLoose())
            {
                List<Recette> recettesMock = new List<Recette>
                {
                    ObtenirMockCommandes()[0].ListeArticleCommande[0].Article,
                    ObtenirMockCommandes()[0].ListeArticleCommande[1].Article,
                };

                mock.Mock<IAccesDonnees>()
                    .Setup(x => x.ObtenirCollectionRecettes())
                    .Returns(recettesMock);

                var classeAccesDonnees = mock.Create<IAccesDonnees>();
                var reel = classeAccesDonnees.ObtenirCollectionRecettes();

                Assert.IsTrue(reel != null);
                Assert.AreEqual(recettesMock.Count, reel.Count);

                for (int i = 0; i < recettesMock.Count; i++)
                {
                    Assert.AreEqual(recettesMock[i].NomRecette, reel[i].NomRecette);
                    Assert.AreEqual(recettesMock[i].Vendant, reel[i].Vendant);
                }
            }
        }

        [TestMethod()]
        public void Test_recuperer_tout_les_aliments()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccesDonnees>()
                    .Setup(x => x.ObtenirCollectionAliments())
                    .Returns(ObtenirListeAlimentMock());

                var classeAccesDonnees = mock.Create<IAccesDonnees>();
                var attendu = ObtenirListeAlimentMock();
                var reel = classeAccesDonnees.ObtenirCollectionAliments();

                Assert.IsTrue(reel != null);
                Assert.AreEqual(attendu.Count, reel.Count);

                for (int i = 0; i < attendu.Count; i++)
                {
                    Assert.AreEqual(attendu[i].Nom, reel[i].Nom);
                    Assert.AreEqual(attendu[i].Quantite, reel[i].Quantite);
                }
            }
        }

        [TestMethod()]
        public void Test_creer_aliment_retour_vrai_si_aliment_valide()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var alimentMock = ObtenirListeAlimentMock()[0];
                mock.Mock<IAccesDonnees>()
                    .Setup(x => x.CreerAliment(alimentMock))
                    .Returns(true);

                var classeAccesDonnees = mock.Create<IAccesDonnees>();
                var reel = classeAccesDonnees.CreerAliment(alimentMock);

                Assert.AreEqual(true, reel);

                mock.Mock<IAccesDonnees>()
                    .Verify(x => x.CreerAliment(alimentMock), Times.Exactly(1));
            }
        }

        [TestMethod()]
        public void Test_suprimer_aliment_retour_vrai_si_aliment_valide()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var alimentMock = ObtenirListeAlimentMock()[0];
                mock.Mock<IAccesDonnees>()
                    .Setup(x => x.SupprimerAliment(alimentMock));

                var classeAccesDonnees = mock.Create<IAccesDonnees>();

                classeAccesDonnees.SupprimerAliment(alimentMock);

                mock.Mock<IAccesDonnees>()
                    .Verify(x => x.SupprimerAliment(alimentMock), Times.Exactly(1));
            }
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
                    CoutTotalCommande = 85,
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
                    CoutTotalCommande = 42,
                    DateCommande = DateTime.Today
                }
            };
            return commandesMock;
        }

        private List<Aliment> ObtenirListeAlimentMock()
        {
            List<Aliment> alimentMock = new List<Aliment>
                {
                    new Aliment
                    {
                        Nom = "Tomates",
                        Quantite = 20,
                        UniteMesure = Enumeration.UniteMesure.unite,
                        CoutVente = 1
                    },
                    new Aliment
                    {
                        Nom = "Bacon",
                        Quantite = 100,
                        UniteMesure = Enumeration.UniteMesure.kilogramme,
                        CoutVente = (decimal) 0.15
                    },
                    new Aliment
                    {
                        Nom = "Laitue",
                        Quantite = 200,
                        UniteMesure = Enumeration.UniteMesure.kilogramme,
                        CoutVente = (decimal) 0.05
                    }
                };
            return alimentMock;
        }
    }
}
