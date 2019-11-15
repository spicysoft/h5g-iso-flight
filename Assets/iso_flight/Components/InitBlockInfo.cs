using Unity.Entities;
using Unity.Mathematics;

namespace IsoFlight
{
	public struct InitBlockInfo : IComponentData
	{
		public bool Initialized;
		public int BlockCnt;
		public int GeneretedNum;
	}
}
