using System;
using Xunit;
using static Lykke.Bil2.Cardano.Native;

namespace Lykke.Bil2.Cardano.Tests
{
    public class NativeTests
    {
        [Theory]
        [InlineData(cardano_protocol_magic.mainnet)]
        [InlineData(cardano_protocol_magic.testnet)]
        public void ShouldGenerateValidCardanoAddress(cardano_protocol_magic protocolMagic)
        {
            // Arrange

            // Act

            var pair = cardano_address_new(protocolMagic);

            // Assert

            Assert.Equal(0, cardano_address_is_valid(pair.address));
        }
    }
}
