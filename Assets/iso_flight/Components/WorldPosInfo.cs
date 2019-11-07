using Unity.Entities;
using Unity.Mathematics;

namespace IsoFlight
{
	public struct WorldPosInfo : IComponentData
	{
		public float3 Wpos;
	}
}
