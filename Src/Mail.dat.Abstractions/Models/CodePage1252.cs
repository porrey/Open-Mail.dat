namespace System.Text
{
	public static class CodePage1252
	{
		public static Encoding Get()
		{
			Encoding returnValue = null;

			//
			// Application forces Codepage 1252.
			//
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			returnValue= Encoding.GetEncoding(1252);

			if (returnValue.CodePage != 1252)
			{
				throw new Exception("Failed to select code page 1252.");
			}

			return returnValue;
		}
	}
}
