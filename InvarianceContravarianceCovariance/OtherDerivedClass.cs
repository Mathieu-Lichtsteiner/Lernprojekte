using System;

namespace InvarianceContravarianceCovariance
{
	internal class OtherDerivedClass : BaseClass<OtherClass>, IMethodWithType<OtherClass> {
		public override void TestMethod4(OtherClass item)
		{
			throw new NotImplementedException();
		}

		protected override void TestMethod1<TM>(TM item)
		{
			throw new NotImplementedException();
		}

		protected override void TestMethod2(object item)
		{
			throw new NotImplementedException();
		}

		protected override void TestMethod3(OtherClass item)
		{
			throw new NotImplementedException();
		}
	}
}