using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Dfe.FE.Interventions.Application.FeProviders;
using Dfe.FE.Interventions.Domain.FeProviders;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Dfe.FE.Interventions.Application.UnitTests.FeProvidersTests.FeProviderManagerTests
{
    public class WhenUpsertingAProvider
    {
        private Mock<IFeProviderRepository> _feProviderRepositoryMock;
        private Mock<ILogger<FeProviderManager>> _loggerMock;
        private FeProviderManager _manager;

        [SetUp]
        public void Arrange()
        {
            _feProviderRepositoryMock = new Mock<IFeProviderRepository>();

            _loggerMock = new Mock<ILogger<FeProviderManager>>();

            _manager = new FeProviderManager(
                _feProviderRepositoryMock.Object,
                _loggerMock.Object);
        }

        [Test]
        public async Task ThenItShouldUpsertProviderInRepository()
        {
            var cancellationToken = new CancellationToken();
            var provider = new FeProvider
            {
                Ukprn = 12345678,
            };

            await _manager.UpsertProvider(provider, cancellationToken);

            _feProviderRepositoryMock.Verify(repo => repo.UpsertProviderAsync(provider, cancellationToken),
                Times.Once);
        }

        [TestCase(1234567)]
        [TestCase(123456789)]
        public void AndUkprnIsNot8DigitsThenItShouldThrowAnInvalidRequestException(int ukprn)
        {
            var provider = new FeProvider
            {
                Ukprn = ukprn,
            };

            var actual = Assert.ThrowsAsync<InvalidRequestException>(async () =>
                await _manager.UpsertProvider(provider, CancellationToken.None));
            Assert.AreEqual("UKPRN must be an 8 digit number", actual.Message);
        }
    }
}