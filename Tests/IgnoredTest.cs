using NUnit.Framework;

// This file and test seem to be needed by Xamarian Studio in order to run the Kekeri tests. Without this, none of the
// Kekeri tests are invoked, which is annoying. It's possible another test runner would solve the problem...

namespace Tests
{
	[TestFixture()]
	public class IgnoredTest
	{
		[Test()]
		public void TestCase()
		{
		}
	}
}
