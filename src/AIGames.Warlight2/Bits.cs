﻿using System;

namespace AIGames.Warlight2
{
	/// <summary>Represents a class to manipulate and investigate bits.</summary>
	public static class Bits
	{
		/// <summary>A mask for flagging specified bits.</summary>
		/// <remarks>
		/// var bits = 0x0001 | Bits.Flag[1]; => bits: 0x0011
		/// </remarks>
		public static readonly UInt64[] Flag = new UInt64[]
		#region Flags
		{
			0x0000000000000001,
			0x0000000000000002,
			0x0000000000000004,
			0x0000000000000008,
			0x0000000000000010,
			0x0000000000000020,
			0x0000000000000040,
			0x0000000000000080,
			0x0000000000000100,
			0x0000000000000200,
			0x0000000000000400,
			0x0000000000000800,
			0x0000000000001000,
			0x0000000000002000,
			0x0000000000004000,
			0x0000000000008000,
			0x0000000000010000,
			0x0000000000020000,
			0x0000000000040000,
			0x0000000000080000,
			0x0000000000100000,
			0x0000000000200000,
			0x0000000000400000,
			0x0000000000800000,
			0x0000000001000000,
			0x0000000002000000,
			0x0000000004000000,
			0x0000000008000000,
			0x0000000010000000,
			0x0000000020000000,
			0x0000000040000000,
			0x0000000080000000,

			0x0000000100000000,
			0x0000000200000000,
			0x0000000400000000,
			0x0000000800000000,
			0x0000001000000000,
			0x0000002000000000,
			0x0000004000000000,
			0x0000008000000000,
			0x0000010000000000,
			0x0000020000000000,
			0x0000040000000000,
			0x0000080000000000,
			0x0000100000000000,
			0x0000200000000000,
			0x0000400000000000,
			0x0000800000000000,
			0x0001000000000000,
			0x0002000000000000,
			0x0004000000000000,
			0x0008000000000000,
			0x0010000000000000,
			0x0020000000000000,
			0x0040000000000000,
			0x0080000000000000,
			0x0100000000000000,
			0x0200000000000000,
			0x0400000000000000,
			0x0800000000000000,
			0x1000000000000000,
			0x2000000000000000,
			0x4000000000000000,
			0x8000000000000000,
		};
		#endregion

		/// <summary>A mask for unflagging specified bits.</summary>
		/// <remarks>
		/// var bits = bx0011 &amp; Bits.UnFlag[1]; => bits: bx0001
		/// </remarks>
		public static readonly UInt64[] Unflag = new UInt64[]
		#region Unflags
		{
			0xfffffffffffffffe,
			0xfffffffffffffffd,
			0xfffffffffffffffb,
			0xfffffffffffffff7,
			0xffffffffffffffef,
			0xffffffffffffffdf,
			0xffffffffffffffbf,
			0xffffffffffffff7f,
			0xfffffffffffffeff,
			0xfffffffffffffdff,
			0xfffffffffffffbff,
			0xfffffffffffff7ff,
			0xffffffffffffefff,
			0xffffffffffffdfff,
			0xffffffffffffbfff,
			0xffffffffffff7fff,
			0xfffffffffffeffff,
			0xfffffffffffdffff,
			0xfffffffffffbffff,
			0xfffffffffff7ffff,
			0xffffffffffefffff,
			0xffffffffffdfffff,
			0xffffffffffbfffff,
			0xffffffffff7fffff,
			0xfffffffffeffffff,
			0xfffffffffdffffff,
			0xfffffffffbffffff,
			0xfffffffff7ffffff,
			0xffffffffefffffff,
			0xffffffffdfffffff,
			0xffffffffbfffffff,
			0xffffffff7fffffff,

			0xfffffffeffffffff,
			0xfffffffdffffffff,
			0xfffffffbffffffff,
			0xfffffff7ffffffff,
			0xffffffefffffffff,
			0xffffffdfffffffff,
			0xffffffbfffffffff,
			0xffffff7fffffffff,
			0xfffffeffffffffff,
			0xfffffdffffffffff,
			0xfffffbffffffffff,
			0xfffff7ffffffffff,
			0xffffefffffffffff,
			0xffffdfffffffffff,
			0xffffbfffffffffff,
			0xffff7fffffffffff,
			0xfffeffffffffffff,
			0xfffdffffffffffff,
			0xfffbffffffffffff,
			0xfff7ffffffffffff,
			0xffefffffffffffff,
			0xffdfffffffffffff,
			0xffbfffffffffffff,
			0xff7fffffffffffff,
			0xfeffffffffffffff,
			0xfdffffffffffffff,
			0xfbffffffffffffff,
			0xf7ffffffffffffff,
			0xefffffffffffffff,
			0xdfffffffffffffff,
			0xbfffffffffffffff,
			0x7fffffffffffffff,
		};
		#endregion

		public const ushort Mask01 = 0x0001;
		public const ushort Mask02 = 0x0003;
		public const ushort Mask03 = 0x0007;
		public const ushort Mask04 = 0x000f;
		public const ushort Mask08 = 0x00ff;
		public const ushort Mask09 = 0x01ff;
		public const ushort Mask10 = 0x03ff;
		public const ushort Mask12 = 0x0fff;
		public const ushort Mask13 = 0x1fff;
		public const ushort Mask14 = 0x3fff;
		public const ushort Mask15 = 0x7fff;
		public const uint Mask16 = 0x0000ffff;
		public const uint Mask18 = 0x0003ffff;
		public const uint Mask19 = 0x0007ffff;
		public const uint Mask20 = 0x000fffff;
		public const uint Mask21 = 0x001fffff;
		public const uint Mask22 = 0x003fffff;
		public const uint Mask23 = 0x007fffff;
		public const uint Mask24 = 0x00ffffff;
		public const uint Mask27 = 0x07ffffff;
		public const uint Mask31 = 0x7fffffff;
		public const uint Mask32 = 0xffffffff;
	
		/// <summary>Counts the number of (1) bits.</summary>
		public static int Count(byte bits) { return BytesCount[bits]; }

		/// <summary>Counts the number of (1) bits.</summary>
		public static int Count(uint bits)
		{
			bits = bits - ((bits >> 1) & 0x55555555);
			bits = (bits & 0x33333333) + ((bits >> 2) & 0x33333333);
			return (int)(((bits + (bits >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
		}

		/// <summary>Counts the number of (1) bits.</summary>
		public static int Count(int bits)
		{
			bits = bits - ((bits >> 1) & 0x55555555);
			bits = (bits & 0x33333333) + ((bits >> 2) & 0x33333333);
			return (((bits + (bits >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
		}

		/// <summary>Counts the number of (1) bits.</summary>
		/// <remarks>
		/// See http://stackoverflow.com/questions/109023
		/// </remarks>
		public static int Count(long bits)
		{
			bits = bits - ((bits >> 1) & 0x5555555555555555);
			bits = (bits & 0x3333333333333333) + ((bits >> 2) & 0x3333333333333333);
			return (int)(unchecked(((bits + (bits >> 4)) & 0xF0F0F0F0F0F0F0F) * 0x101010101010101) >> 56);
		}

		/// <summary>Counts the number of (1) bits.</summary>
		public static int Count(ulong bits)
		{
			bits = bits - ((bits >> 1) & 0x5555555555555555UL);
			bits = (bits & 0x3333333333333333UL) + ((bits >> 2) & 0x3333333333333333UL);
			return (int)(unchecked(((bits + (bits >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
		}

		/// <summary>lookup for Count(byte).</summary>
		private static readonly byte[] BytesCount = { 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 4, 5, 5, 6, 5, 6, 6, 7, 5, 6, 6, 7, 6, 7, 7, 8 };
	}
}
