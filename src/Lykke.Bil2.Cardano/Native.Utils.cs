using System;
using System.Security.Cryptography;

namespace Lykke.Bil2.Cardano
{
    public static partial class Native
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

            try
            {
                if (cardano_xprv_from_bytes(xprv, ref xprv_ptr) != cardano_result.CARDANO_RESULT_SUCCESS)
                {
                    throw new InvalidOperationException("Invalid XPRV data");
                }

                return cardano_address_from_xprv(xprv_ptr, protocolMagic);
            }
            finally
            {
                // unmanaged memory cleanup

                if (xprv_ptr != default(IntPtr))
                    cardano_xprv_delete(xprv_ptr);
            }
        }

        /// <summary>
        /// Constructs address from given XPRV.
        /// </summary>
        /// <param name="xprv">XPRV.</param>
        /// <param name="protocolMagic">Network specifier.</param>
        /// <returns>Base58 encoded address.</returns>
        public static string cardano_address_from_xprv(IntPtr xprv, cardano_protocol_magic protocolMagic)
        {
            var xpub = default(IntPtr); // XPUB (eXtended PUBlic key)
            var addr = default(IntPtr); // address

            try
            {
                xpub = cardano_xprv_to_xpub(xprv);

                addr = cardano_address_new_from_pubkey(xpub, protocolMagic);

                return cardano_address_export_base58(addr);
            }
            finally
            {
                // unmanaged memory cleanup

                if (xpub != default(IntPtr))
                    cardano_xpub_delete(xpub);

                if (addr != default(IntPtr))
                    cardano_address_delete(addr);
            }
        }
    }
}