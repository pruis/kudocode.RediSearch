namespace KudoCode.Test.Unit
{
	public interface IUnitTestBase
	{
		void Run(string scenarioTitle, string given, string when, string then);
	}
}