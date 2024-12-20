using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class TrackUrlDecryptor : IYouTubeMediaTrackUrlDecryptor
	{
		public string CipherDecryptionAlgorythm { get; }

		public TrackUrlDecryptor(string cipherDecryptionAlgorythm)
		{
			CipherDecryptionAlgorythm = cipherDecryptionAlgorythm;
		}

		public (bool, bool) Decrypt(YouTubeMediaTrackUrl mediaTrackUrl)
		{
			if (mediaTrackUrl.SplitCipher() && mediaTrackUrl.QueryCipher.ContainsKey("s"))
			{
				bool cipherDecripted = DecryptCipherSignature(mediaTrackUrl.QueryCipher["s"], out string decryptedCipher);
				if (cipherDecripted)
				{
					mediaTrackUrl.MergeCipherUrl(decryptedCipher);
					return (cipherDecripted, false);
				}
			}

			return (false, false);
		}

		public bool DecryptCipherSignature(string encryptedCipherSignature, out string decryptionResult)
		{
			decryptionResult = Utils.DecryptCipherSignature(encryptedCipherSignature, CipherDecryptionAlgorythm);
			return !string.IsNullOrEmpty(decryptionResult) && !string.IsNullOrWhiteSpace(decryptionResult);
		}

		public bool DecryptNParam(string encryptedNParam, out string decryptionResult)
		{
			decryptionResult = null;
			return false;
		}
	}
}
