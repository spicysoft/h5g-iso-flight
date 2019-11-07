using Unity.Entities;
using Unity.Mathematics;

namespace IsoFlight
{
	public struct PlayerInfo : IComponentData
	{
		public bool Initialized;
		public float3 Wpos;
	}
}
