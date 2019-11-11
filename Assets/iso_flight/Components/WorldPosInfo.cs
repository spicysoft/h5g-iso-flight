using Unity.Entities;
using Unity.Mathematics;

namespace IsoFlight
{
	public struct WorldPosInfo : IComponentData
	{
		public bool DontCalcOrder;
		public float3 Wpos;
	}
}
