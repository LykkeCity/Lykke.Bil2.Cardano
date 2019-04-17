namespace Lykke.Bil2.Cardano
{
    public static partial class Native
    {
        public enum cardano_bip39_error
        {
            BIP39_SUCCESS = 0,
            BIP39_INVALID_MNEMONIC = 1,
            BIP39_INVALID_CHECKSUM = 2,
            BIP39_INVALID_WORD_COUNT = 3
        }

        public enum cardano_protocol_magic
        {
            mainnet = 764824073,
            testnet = 1097911063
        }

        public enum cardano_result
        {
            CARDANO_RESULT_SUCCESS = 0,
            CARDANO_RESULT_ERROR = 1
        }

        public enum cardano_transaction_error
        {
            CARDANO_TRANSACTION_SUCCESS = 0,
            CARDANO_TRANSACTION_NO_OUTPUT = 1,
            CARDANO_TRANSACTION_NO_INPUT = 2,
            CARDANO_TRANSACTION_SIGNATURE_MISMATCH = 3,
            CARDANO_TRANSACTION_OVER_LIMIT = 4,
            CARDANO_TRANSACTION_SIGNATURES_EXCEEDED = 5,
            CARDANO_TRANSACTION_COIN_OUT_OF_BOUNDS = 6
        }
    }
}