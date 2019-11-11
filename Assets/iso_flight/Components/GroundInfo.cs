using Unity.Entities;
using Unity.Mathematics;

namespace IsoFlight
{
	public struct GroundInfo : IComponentData
	{
		public bool IsActive;
		public bool Initialized;
		public float Timer;
		public float Vx;
		public float Vy;
		//public float3 Wpos;
	}
}
