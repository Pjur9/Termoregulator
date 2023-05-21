using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
namespace Common.Tests
{
    [TestFixture]
    public class UredjajTest
    {
        Mock<Uredjaj> m_device;

        [SetUp]
        public void SetUp()
        {
            m_device = new Mock<Uredjaj>();
        }
        [Test]
        [TestCase(1, 20)]
        public void UredjajKonstruktor(int id, double t)
        {
            Uredjaj u = new Uredjaj(id, t);
            Assert.AreEqual(id, u.Id);
            Assert.AreEqual(t, u.TemperaturaProstorije);
        }

        [Test]
        [TestCase(-1, -22)]
        [TestCase(-1, 20)]
        [TestCase(-1, 200)]
        [TestCase(1, 200)]
        [TestCase(1, -30)]
        public void UredjajKonstruktorLosiParametri(int id, double t)
        {
            
                Assert.Throws<ArgumentNullException>(() =>
                {
                   Uredjaj r = new Uredjaj(id, t);
                }
                 );
          
        }

        [TearDown]
        public void TearDown()
        {
            m_device = null;

        }
        
    }
}

