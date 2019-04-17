using System;
using Xunit;

namespace Lykke.Bil2.Cardano.Tests
{
    public class NativeTests
    {
        [Theory]
        [InlineData(Native.cardano_protocol_magic.mainnet)]
        [InlineData(Native.cardano_protocol_magic.testnet)]
        public void ShouldGenerateValidCardanoAddress(Native.cardano_protocol_magic protocolMagic)
        {
            // Arrange

            // Act

            var pair = Utils.cardano_address_new(protocolMagic);

            // Assert

            Assert.Equal(0, Native.cardano_address_is_valid(pair.address));
        }
    }
}
