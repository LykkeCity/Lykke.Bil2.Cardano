using System;
using System.Runtime.InteropServices;

namespace Lykke.Bil2.Cardano
{
    public static partial class Native
    {
        /// <summary>
        /// Returns 0 if address is valid, 1 if specified string is not base58, 2 if specified string is not valid Cardano address.
        /// </summary>
        /// <param name="address_base58">Address in base58.</param>
        /// <returns></returns>
        [DllImport("cardano_c")]
        public static extern int cardano_address_is_valid(string address_base58);

        /// <summary>
        /// Derieves address from public key.
        /// The memory must be freed with <see cref="cardano_address_delete"/>.
        /// </summary>
        /// <param name="xpub">Public key.</param>
        /// <param name="protocol_magic">Network specific modifier.</param>
        /// <returns>Address.</returns>
        [DllImport("cardano_c")]
        public static extern IntPtr cardano_address_new_from_pubkey(IntPtr xpub, cardano_protocol_magic protocol_magic);

        /// <summary>
        /// Encodes address bytes to base58 string.
        /// </summary>
        /// <param name="address">Address.</param>
        /// <returns>Address base58 representation.</returns>
        [DllImport("cardano_c")]
        public static extern string cardano_address_export_base58(IntPtr address);

        /// <summary>
        /// Creates address object from base58 string.
        /// The memory must be freed with <see cref="cardano_address_delete"/>.
        /// </summary>
        /// <param name="address">Address string.</param>
        /// <returns>Address.</returns>
        [DllImport("cardano_c")]
        public static extern IntPtr cardano_address_import_base58(string address);

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_address_new_from_pubkey"/> or <see cref="cardano_address_import_base58"/>.
        /// </summary>
        /// <param name="address">Pointer to the object in unmanaged memory.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_address_delete(IntPtr address);
    }
}