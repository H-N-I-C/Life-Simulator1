namespace Backend.Code.Models.FunctionModels
{
	public class MyStuffFunction
	{
		public virtual void Execute()
		{
		}

		public virtual void ExecuteWithData(string data)
		{
		}

		public virtual bool CanExecute(string data)
		{
			return false;
		}
	}
}
