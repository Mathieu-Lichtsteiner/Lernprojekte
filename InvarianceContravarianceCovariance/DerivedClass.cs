using System;

namespace InvarianceContravarianceCovariance {
	internal class DerivedClass : BaseClass<OtherClass> {
		protected override void TestMethod1( OtherClass item ) => throw new NotImplementedException();
		protected override void TestMethod2( OtherClass item ) => throw new NotImplementedException();
		protected override void TestMethod3( OtherClass item ) => throw new NotImplementedException();
	}
}
