using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP214E.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace TP214E.Data.Tests
{
    [TestClass()]
    public class AlimentTests
    {
        [TestMethod()]
        public void Creer_Aliment_Sans_Nom()
        {
            Aliment aliment = new Aliment();

            aliment.Quantite = 2;
            aliment.UniteMesure = Enumeration.UniteMesure.kilogramme;
            aliment.CoutVente = (decimal)25.25;

            Assert.ThrowsException<ArgumentException>(() => aliment.Nom = "");
        }

        [TestMethod()]
        public void Creer_Aliment_Avec_Quantite_Negative()
        {
            Aliment aliment = new Aliment();
            aliment.Nom = "Pommes";
            aliment.UniteMesure = Enumeration.UniteMesure.kilogramme;
            aliment.CoutVente = (decimal)25.25;

            Assert.ThrowsException<ArgumentException>(() => aliment.Quantite = -2);
        }

        [TestMethod()]
        public void Creer_Aliment_Avec_Cout_Negatif()
        {
            Aliment aliment = new Aliment();
            
            aliment.Nom = "Pommes";
            aliment.Quantite = 2;
            aliment.UniteMesure = Enumeration.UniteMesure.kilogramme;

            Assert.ThrowsException<ArgumentException>(() => aliment.CoutVente = (decimal)-25.25);
        }

        [TestMethod]
        public void Creer_Aliment_Avec_Bons_Parametres()
        {
            Aliment aliment = new Aliment("Pommes", 2, Enumeration.UniteMesure.kilogramme, (decimal) 25.25 );

            string nom = "Pommes";
            int quantite = 2;
            Enumeration.UniteMesure uniteMesure= Enumeration.UniteMesure.kilogramme;
            decimal coutVente = (decimal)25.25;

            Assert.AreEqual(nom, aliment.Nom);
            Assert.AreEqual(quantite, aliment.Quantite);
            Assert.AreEqual(uniteMesure, aliment.UniteMesure);
            Assert.AreEqual(coutVente, aliment.CoutVente);
        }

        [TestMethod]
        public void Creer_Aliment_Sans_Parametres()
        {
            Aliment aliment = new Aliment();
            
            Assert.IsNotNull(aliment);
        }

        [TestMethod]
        public void TestMethodeToString()
        {
            Aliment aliment = new Aliment("Pommes", 2, Enumeration.UniteMesure.kilogramme, (decimal)25.25);

            Assert.AreEqual("Pommes - 25,25 $ - 2 kilogramme", aliment.ToString());
        }
    }
}