using System;
using System.Runtime.InteropServices;

namespace Lykke.Bil2.Cardano
{
    public static partial class Native
    {
        /// <summary>
        /// Construct extended private key from the given bytes.
        /// The memory must be freed with <see cref="cardano_xprv_delete"/>.
        /// </summary>
        /// <param name="bytes">Extended private key data.</param>
        /// <param name="xprv">Extended private key.</param>
        /// <returns></returns>
        [DllImport("cardano_c")]
        public static extern cardano_result cardano_xprv_from_bytes(byte[] bytes, ref IntPtr xprv);

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_xprv_from_bytes"/>.
        /// </summary>
        /// <param name="xprv">Pointer to the object in unmanaged memory.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_xprv_delete(IntPtr xprv);

        /// <summary>
        /// Get the associated public key by private key.
        /// The memory must be freed with <see cref="cardano_xpub_delete"/>.
        /// </summary>
        /// <param name="xprv">Extended private key.</param>
        /// <returns>Public key.</returns>
        [DllImport("cardano_c")]
        public static extern IntPtr cardano_xprv_to_xpub(IntPtr xprv);

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_xprv_to_xpub"/>.
        /// </summary>
        /// <param name="xpub">Pointer to the object in unmanaged memory.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_xpub_delete(IntPtr xpub);
    }
}