using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class UnitTests
    {
        public UnitTests()
        {
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FailingTest_FailsAtFailing_ByFailing()
        {
            Assert.IsTrue(false);
        }
    }
}