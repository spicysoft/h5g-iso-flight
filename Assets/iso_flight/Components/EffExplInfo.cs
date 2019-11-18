using Unity.Entities;

namespace IsoFlight
{
	public struct EffExplInfo : IComponentData
	{
		public bool Initialized;
		public float Timer;
	}
}
