namespace InvarianceContravarianceCovariance {
	internal abstract class BaseClass<TC> {
		protected abstract void TestMethod1<TM>( TM item );
		protected abstract void TestMethod2( object item );
		protected abstract void TestMethod3( TC item );
	}
}
