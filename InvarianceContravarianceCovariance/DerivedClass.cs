using System;

namespace InvarianceContravarianceCovariance {
	internal class DerivedClass<T> : BaseClass<OtherClass>, IMethodWithType<T> {
		public void TestMethod4( T item ) => throw new NotImplementedException();
		public override void TestMethod4( OtherClass item ) => throw new NotImplementedException();
		protected override void TestMethod1<TM>( TM item ) => throw new NotImplementedException();
		protected override void TestMethod2( object item ) => throw new NotImplementedException();
		protected override void TestMethod3( OtherClass item ) => throw new NotImplementedException();
	}
}
