using System;
using System.Runtime.InteropServices;

namespace Lykke.Bil2.Cardano
{
    public static partial class Native
    {
        /// <summary>
        /// Create builder for a transaction.
        /// The memory must be freed with <see cref="cardano_transaction_builder_delete"/>.
        /// </summary>
        /// <returns>Transaction builder.</returns>
        [DllImport("cardano_c")]
        public static extern IntPtr cardano_transaction_builder_new();

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_transaction_builder_new"/>.
        /// </summary>
        /// <param name="address">Pointer to the object in unmanaged memory.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_builder_delete(IntPtr tb);

        /// <summary>
        /// Create object used for addressing a specific output of a transaction built from a TxId (hash of the tx) and the offset in the outputs of this transaction.
        /// The memory must be freed with <see cref="cardano_transaction_output_ptr_delete"/>.
        /// </summary>
        /// <param name="txid">Tx hash.</param>
        /// <param name="index">Tx output index.</param>
        /// <returns>Transaction output pointer.</returns>
        [DllImport("cardano_c")]
        public static extern IntPtr cardano_transaction_output_ptr_new(byte[] txid, uint index);

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_transaction_output_ptr_new"/>.
        /// </summary>
        /// <param name="txo">Pointer to the object in unmanaged memory.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_output_ptr_delete(IntPtr txo);

        /// <summary>
        /// Add input to the transaction.
        /// </summary>
        /// <param name="tb">The builder for the transaction.</param>
        /// <param name="txo">Output pointer created with <see cref="cardano_transaction_output_ptr_new"/></param>
        /// <param name="value">The expected amount to spend, in "lovelace"</param>
        [DllImport("cardano_c")]
        public static extern cardano_transaction_error cardano_transaction_builder_add_input(IntPtr tb, IntPtr txo, ulong value);

        /// <summary>
        /// Create output for a transaction.
        /// The memory must be freed with <see cref="cardano_transaction_output_delete"/>.
        /// </summary>
        /// <param name="address">Destination address.</param>
        /// <param name="amount">Amount in "lovelace".</param>
        /// <returns>Transaction output.</returns>
        [DllImport("cardano_c")]
        public static extern IntPtr cardano_transaction_output_new(IntPtr address, ulong amount);

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_transaction_output_new"/>.
        /// </summary>
        /// <param name="txo">Pointer to the object in unmanaged memory.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_output_delete(IntPtr output);

        /// <summary>
        /// Add output to transaction.
        /// </summary>
        /// <param name="tb">The builder for the transaction.</param>
        /// <param name="txo">Output created with <see cref="cardano_transaction_output_new"/></param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_builder_add_output(IntPtr tb, IntPtr output);

        /// <summary>
        /// Get a transaction object.
        /// The memory must be freed with <see cref="cardano_transaction_delete"/>.
        /// </summary>
        /// <param name="tb">The builder for the transaction.</param>
        /// <param name="tx">Transaction.</param>
        /// <returns></returns>
        [DllImport("cardano_c")]
        public static extern cardano_transaction_error cardano_transaction_builder_finalize(IntPtr tb, ref IntPtr tx);

        /// <summary>
        /// Calculates transaction identifier.
        /// </summary>
        /// <param name="tx">Transaction.</param>
        /// <param name="txid">Byte[32] array to fill with transaction identifier data.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_id(IntPtr tx, byte[] txid);

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_transaction_builder_finalize"/>.
        /// </summary>
        /// <param name="tx">Pointer to the object in unmanaged memory.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_delete(IntPtr tx);

        /// <summary>
        /// Take a transaction and create a working area for adding witnesses.
        /// The memory must be freed with <see cref="cardano_transaction_finalized_delete"/>.
        /// </summary>
        /// <param name="tx">Pointer to the transaction object in unmanaged memory.</param>
        /// <returns>Output pointer to the finalized transaction object in unmanaged memory.</returns>
        [DllImport("cardano_c")]
        public static extern IntPtr cardano_transaction_finalized_new(IntPtr tx);

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_transaction_finalized_new"/>.
        /// </summary>
        /// <param name="tf">Pointer to the object in unmanaged memory.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_finalized_delete(IntPtr tf);

        /// <summary>
        /// Add a witness associated with the next input.
        /// Witness need to be added in the same order to the inputs, otherwise protocol level mismatch will happen, and the transaction will be rejected.
        /// </summary>
        /// <param name="tf">Finalized transaction.</param>
        /// <param name="xprv">Private key.</param>
        /// <param name="protocol_magic">Network protocol magic.</param>
        /// <param name="txid">Transaction identifier.</param>
        /// <returns></returns>
        [DllImport("cardano_c")]
        public static extern cardano_transaction_error cardano_transaction_finalized_add_witness(IntPtr tf, IntPtr xprv, cardano_protocol_magic protocol_magic, byte[] txid);

        /// <summary>
        /// Returns finalized transaction with the vector of witnesses.
        /// The memory must be freed with <see cref="cardano_transaction_signed_delete"/>.
        /// </summary>
        /// <param name="tf">A finalized transaction with witnesses.</param>
        /// <param name="txaux">Signed transaction.</param>
        /// <returns></returns>
        [DllImport("cardano_c")]
        public static extern cardano_transaction_error cardano_transaction_finalized_output(IntPtr tf, ref IntPtr txaux);

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_transaction_finalized_output"/>.
        /// </summary>
        /// <param name="tx">Pointer to the object in unmanaged memory.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_signed_delete(IntPtr txaux);

        /// <summary>
        /// Serializes signed transaction to byte array.
        /// </summary>
        /// <param name="txaux">Signed transaction.</param>
        /// <param name="bytes">Pointer to target result byte array.</param>
        /// <param name="size">Result array length.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_signed_bytes(IntPtr txaux, ref IntPtr bytes, ref uint size);

        /// <summary>
        /// Free the memory of an object allocated with <see cref="cardano_transaction_signed_to_bytes"/>.
        /// </summary>
        /// <param name="bytes">Pointer to the object in unmanaged memory.</param>
        /// <param name="size">Array length.</param>
        [DllImport("cardano_c")]
        public static extern void cardano_transaction_signed_bytes_delete(IntPtr bytes, uint size);
    }
}