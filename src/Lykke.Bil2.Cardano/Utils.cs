using System;
using System.Security.Cryptography;
using static Lykke.Bil2.Cardano.Native;

namespace Lykke.Bil2.Cardano
{
    public static class Utils
    {
        /// <summary>
        /// Generates new private key and corresponding address for given network.
        /// </summary>
        /// <param name="protocolMagic">Network specifier.</param>
        /// <returns>Pair of base58 encoded address and its private key.</returns>
        public static (string address, byte[] xprv) cardano_address_new(cardano_protocol_magic protocolMagic)
        {
            var seed = new byte[64];
            var xprv = new byte[96]; // eXtended PRiVate key

            // generate seed (we don't need BIP39 so just use cryptographic random generator)
            new RNGCryptoServiceProvider().GetBytes(seed);

            // make up the key data
            Array.Copy(SHA512.Create().ComputeHash(seed, 0, 32), xprv, 64);
            Array.Copy(seed, 32, xprv, 64, 32);

            // some Cardano magic
            xprv[0] &= 248;
            xprv[31] &= 63;
            xprv[31] |= 64;
            xprv[31] &= 0b1101_1111;

            return
            (
                address: cardano_address_from_xprv(xprv, protocolMagic),
                xprv: xprv
            );
        }

        /// <summary>
        /// Constructs address from given XPRV data.
        /// </summary>
        /// <param name="xprv">XPRV data.</param>
        /// <param name="protocolMagic">Network specifier.</param>
        /// <returns>Base58 encoded address.</returns>
        public static string cardano_address_from_xprv(byte[] xprv, cardano_protocol_magic protocolMagic)
        {
            var xprv_ptr = default(IntPtr); // XPRV
            var xpub_ptr = default(IntPtr); // XPUB (eXtended PUBlic key)
            var addr_ptr = default(IntPtr); // address

            try
            {
                if (cardano_xprv_from_bytes(xprv, ref xprv_ptr) != cardano_result.CARDANO_RESULT_SUCCESS)
                {
                    throw new InvalidOperationException("Invalid XPRV data");
                }

                xpub_ptr = cardano_xprv_to_xpub(xprv_ptr);
                
                addr_ptr = cardano_address_new_from_pubkey(xpub_ptr, protocolMagic);

                return cardano_address_export_base58(addr_ptr);
            }
            finally
            {
                // unmanaged memory cleanup

                if (xprv_ptr != default(IntPtr))
                    cardano_xprv_delete(xprv_ptr);

                if (xpub_ptr != default(IntPtr))
                    cardano_xpub_delete(xpub_ptr);

                if (addr_ptr != default(IntPtr))
                    cardano_address_delete(addr_ptr);
            }
        }
    }
}