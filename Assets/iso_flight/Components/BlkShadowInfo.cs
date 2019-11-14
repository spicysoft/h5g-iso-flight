using Unity.Entities;

namespace IsoFlight
{
	public struct BlkShadowInfo : IComponentData
	{
		public bool IsActive;
		public bool Initialized;
		public int SerialId;
	}
}
