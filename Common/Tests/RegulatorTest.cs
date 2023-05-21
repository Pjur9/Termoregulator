using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
namespace Common.Tests
{
    [TestFixture]
    public class RegulatorTest
    {
        Mock<IRegulator> m_regulator;

        [SetUp]
        public void SetUp()
        {
            m_regulator = new Mock<IRegulator>();
        }


        [Test]
        [TestCase("12", "30", "18", "30", "21", "20")] 
       public void ProvjeraPromjeneRadaRezimaDobriParametriNocni(string psati, string pmin, string ksati, string kmin, string sati, string minute)
        {
            string rezim= m_regulator.Object.promjeni_rezim_rada(psati, pmin, ksati, kmin, sati, minute);
            Assert.AreEqual("nocni", rezim);
        }

        [Test]
        [TestCase("12", "30", "18", "30", "14", "20")]
        public void ProvjeraPromjeneRadaRezimaDobriParametriDnevni(string psati, string pmin, string ksati, string kmin, string sati, string minute)
        {
            string rezim = m_regulator.Object.promjeni_rezim_rada(psati, pmin, ksati, kmin, sati, minute);
            Assert.AreEqual("dnevni", rezim);
          
        }


        [Test]
        [TestCase("25", "60", "18", "30", "21", "20")]
        [TestCase("-12", "-30", "-18", "-30", "-21", "-20")]
        [TestCase("12", "30", "72", "30", "21", "20")]
        [TestCase("12", "30", "-3", "62", "21", "69")]
        public void ProvjeraPromjeneRadaRezimaLosiParametri(string psati, string pmin, string ksati, string kmin, string sati, string minute)
        {
            Assert.Throws<ArgumentException>(
              () =>
              {
                  m_regulator.Object.promjeni_rezim_rada( psati,  pmin,  ksati,  kmin,  sati,  minute);
              });
            
        }

        [Test]
        [TestCase(-25)]
        [TestCase(125)]
        public void TacnostProvjereTemperature(double avg)
        {
            Assert.Throws<ArgumentException>(
              () =>
              {
                  m_regulator.Object.poredi_temp(avg);
              });

        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestStartuj(bool rezim)
        {
            Assert.Throws<ArgumentException>(
              () =>
              {
                  m_regulator.Object.startuj(rezim);
              });
        }

        [TearDown]
        public void TearDown()
        {
            m_regulator = null;

        }

    }
}
