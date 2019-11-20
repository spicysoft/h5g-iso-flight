using Unity.Entities;
using Unity.Mathematics;

namespace IsoFlight
{
	public struct BlockGenerateInfo : IComponentData
	{
		public bool Initialized;
		public float Timer;
		public float Interval;
	}
}
