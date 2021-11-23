using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class ArticleCommandeTests
    {
        [TestMethod()]
        public void Creer_ArticleCommande_Avec_Bons_Parametres()
        {
            Recette poutine = new Recette();
            ArticleCommande articleCommande = new ArticleCommande(2, poutine);
            articleCommande.CoutArticle = (decimal)37.89;

            int quantiteArticle = 2;
            Recette article = poutine;
            decimal coutArticle = (decimal)37.89;

            Assert.AreEqual(quantiteArticle, articleCommande.QuantiteArticle);
            Assert.AreEqual(article, articleCommande.Article);
            Assert.AreEqual(coutArticle, articleCommande.CoutArticle);
        }

        [TestMethod]
        public void Creer_ArticleCommande_Sans_Parametres()
        {
            ArticleCommande articleCommande = new ArticleCommande();

            Assert.IsNotNull(articleCommande);
        }

        [TestMethod]
        public void Creer_ArticleCommande_Avec_Quantite_Negatif()
        {
            Recette poutine = new Recette();
            ArticleCommande articleCommande = new ArticleCommande(2, poutine);
            articleCommande.CoutArticle = (decimal)37.89;

            Recette article = poutine;

            Assert.ThrowsException<ArgumentException>(() => articleCommande.QuantiteArticle = -2);
        }

        [TestMethod]
        public void Calculer_Cout_vendant()
        {
            Recette poutine = new Recette("poutine", (decimal)12.65);
            ArticleCommande articleCommande = new ArticleCommande(2, poutine);

            Assert.AreEqual((decimal)25.30, articleCommande.CalculerVendantArticle());
        }

        [TestMethod]
        public void TesterMethodeString()
        {
            Recette poutine = new Recette("poutine", (decimal)12.65);

            ArticleCommande articleCommande = new ArticleCommande(2, poutine);
            articleCommande.CoutArticle = (decimal)12.65;

            Assert.AreEqual("2 - poutine = 12,65 $", articleCommande.ToString());
        }
    }
}



