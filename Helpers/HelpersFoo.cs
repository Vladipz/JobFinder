namespace JobFinder.Helpers
{
	public static class HelpersFoo
	{
		public static string GetStatusClass(string status)
		{
			if (status == "Moderation" || status == "Rejected")
			{
				return "red"; // червоний колір для moderated
			}
			else if (status == "Approved" )
			{
				return "green"; // зелений колір для approved
			}

			else
			{
				return ""; // або повертайте інший клас або пустий рядок в залежності від вашого сценарію
			}
		}
	}
}
