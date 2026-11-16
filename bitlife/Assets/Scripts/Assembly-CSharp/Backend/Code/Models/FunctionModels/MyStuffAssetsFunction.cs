namespace Backend.Code.Models.FunctionModels
{
	public class MyStuffAssetsFunction : MyStuffFunction
	{
		public override void ExecuteWithData(string data)
		{
		}

		public override bool CanExecute(string data)
		{
			return false;
		}

		public string GetErrorMessage(string itemName)
		{
			return null;
		}
	}
}
