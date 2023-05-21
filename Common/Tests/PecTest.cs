using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
namespace Common.Tests
{
    [TestFixture]
    public class PecTest
    {
        
        Mock<IPec> m_pec;

        [SetUp]
        public void SetUp()
        {
            m_pec = new Mock<IPec>();
        }
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IspravnoUkljucivanjePeci(bool rezim)
        {
            Assert.Throws<ArgumentException>(
             () =>
             {
                 m_pec.Object.UkljuciPec(rezim);
             });
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IspravanRadPeci(bool rezim)
        {
            Assert.Throws<ArgumentException>(
            () =>
            {
                m_pec.Object.Rad(rezim);
            });
        }

        [TearDown]
        public void TearDown()
        {
            m_pec = null;

        }

        }

    }
