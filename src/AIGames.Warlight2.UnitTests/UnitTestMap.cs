using AIGames.Warlight2.Cartography;

namespace AIGames.Warlight2.UnitTests
{
	public class UnitTestMap : Map
	{
		public UnitTestMap() { }

		/// <summary>Inits the default map.</summary>
		public static Map InitSmall()
		{
			UnitTestMap map = new UnitTestMap();
			//map.ApplySettings(new Settings());
			SuperRegion northAmerica = new SuperRegion(1, 4, map);
			SuperRegion southAmerica = new SuperRegion(2, 2, map);

			Region region1 = new Region(1, northAmerica);
			Region region2 = new Region(2, northAmerica);
			Region region3 = new Region(3, northAmerica);
			Region region4 = new Region(4, northAmerica);
			Region region5 = new Region(5, northAmerica);
			Region region6 = new Region(6, northAmerica);
			Region region7 = new Region(7, northAmerica);
			Region region8 = new Region(8, northAmerica);
			Region region9 = new Region(9, northAmerica);
			Region region10 = new Region(10, southAmerica);
			Region region11 = new Region(11, southAmerica);
			
			region1.AddNeighbor(region2);
			region1.AddNeighbor(region4);
			
			region2.AddNeighbor(region4);
			region2.AddNeighbor(region3);
			region2.AddNeighbor(region5);
			
			region3.AddNeighbor(region5);
			region3.AddNeighbor(region6);
			
			region4.AddNeighbor(region5);
			region4.AddNeighbor(region7);
			
			region5.AddNeighbor(region6);
			region5.AddNeighbor(region7);
			region5.AddNeighbor(region8);
			
			region6.AddNeighbor(region8);
			
			region7.AddNeighbor(region8);
			region7.AddNeighbor(region9);
			
			region8.AddNeighbor(region9);
			region9.AddNeighbor(region10);
			region10.AddNeighbor(region11);
			
			map.Add(region1); map.Add(region2); map.Add(region3);
			map.Add(region4); map.Add(region5); map.Add(region6);
			map.Add(region7); map.Add(region8); map.Add(region9);
			map.Add(region10); map.Add(region11);
			map.Add(northAmerica);
			map.Add(southAmerica);

			map.SetPickableStartRegions(new int[] { 1, 2, 3, 4, 7, 8, 9, 11 });
			map.Finish();

			return map;
		}

		/// <summary>Inits the default map.</summary>
		public static Map Init()
		{
			UnitTestMap map = new UnitTestMap();
			SuperRegion northAmerica = new SuperRegion(1, 5, map);
			SuperRegion southAmerica = new SuperRegion(2, 2, map);
			SuperRegion europe = new SuperRegion(3, 5, map);
			SuperRegion afrika = new SuperRegion(4, 3, map);
			SuperRegion azia = new SuperRegion(5, 7, map);
			SuperRegion australia = new SuperRegion(6, 2, map);

			Region region1 = new Region(1, northAmerica);
			Region region2 = new Region(2, northAmerica);
			Region region3 = new Region(3, northAmerica);
			Region region4 = new Region(4, northAmerica);
			Region region5 = new Region(5, northAmerica);
			Region region6 = new Region(6, northAmerica);
			Region region7 = new Region(7, northAmerica);
			Region region8 = new Region(8, northAmerica);
			Region region9 = new Region(9, northAmerica);
			Region region10 = new Region(10, southAmerica);
			Region region11 = new Region(11, southAmerica);
			Region region12 = new Region(12, southAmerica);
			Region region13 = new Region(13, southAmerica);
			Region region14 = new Region(14, europe);
			Region region15 = new Region(15, europe);
			Region region16 = new Region(16, europe);
			Region region17 = new Region(17, europe);
			Region region18 = new Region(18, europe);
			Region region19 = new Region(19, europe);
			Region region20 = new Region(20, europe);
			Region region21 = new Region(21, afrika);
			Region region22 = new Region(22, afrika);
			Region region23 = new Region(23, afrika);
			Region region24 = new Region(24, afrika);
			Region region25 = new Region(25, afrika);
			Region region26 = new Region(26, afrika);
			Region region27 = new Region(27, azia);
			Region region28 = new Region(28, azia);
			Region region29 = new Region(29, azia);
			Region region30 = new Region(30, azia);
			Region region31 = new Region(31, azia);
			Region region32 = new Region(32, azia);
			Region region33 = new Region(33, azia);
			Region region34 = new Region(34, azia);
			Region region35 = new Region(35, azia);
			Region region36 = new Region(36, azia);
			Region region37 = new Region(37, azia);
			Region region38 = new Region(38, azia);
			Region region39 = new Region(39, australia);
			Region region40 = new Region(40, australia);
			Region region41 = new Region(41, australia);
			Region region42 = new Region(42, australia);

			region1.AddNeighbor(region2);
			region1.AddNeighbor(region4);
			region1.AddNeighbor(region30);
			region2.AddNeighbor(region4);
			region2.AddNeighbor(region3);
			region2.AddNeighbor(region5);
			region3.AddNeighbor(region5);
			region3.AddNeighbor(region6);
			region3.AddNeighbor(region14);
			region4.AddNeighbor(region5);
			region4.AddNeighbor(region7);
			region5.AddNeighbor(region6);
			region5.AddNeighbor(region7);
			region5.AddNeighbor(region8);
			region6.AddNeighbor(region8);
			region7.AddNeighbor(region8);
			region7.AddNeighbor(region9);
			region8.AddNeighbor(region9);
			region9.AddNeighbor(region10);
			region10.AddNeighbor(region11);
			region10.AddNeighbor(region12);
			region11.AddNeighbor(region12);
			region11.AddNeighbor(region13);
			region12.AddNeighbor(region13);
			region12.AddNeighbor(region21);
			region14.AddNeighbor(region15);
			region14.AddNeighbor(region16);
			region15.AddNeighbor(region16);
			region15.AddNeighbor(region18);
			region15.AddNeighbor(region19);
			region16.AddNeighbor(region17);
			region17.AddNeighbor(region19);
			region17.AddNeighbor(region20);
			region17.AddNeighbor(region27);
			region17.AddNeighbor(region32);
			region17.AddNeighbor(region36);
			region18.AddNeighbor(region19);
			region18.AddNeighbor(region20);
			region18.AddNeighbor(region21);
			region19.AddNeighbor(region20);
			region20.AddNeighbor(region21);
			region20.AddNeighbor(region22);
			region20.AddNeighbor(region36);
			region21.AddNeighbor(region22);
			region21.AddNeighbor(region23);
			region21.AddNeighbor(region24);
			region22.AddNeighbor(region23);
			region22.AddNeighbor(region36);
			region23.AddNeighbor(region24);
			region23.AddNeighbor(region25);
			region23.AddNeighbor(region26);
			region23.AddNeighbor(region36);
			region24.AddNeighbor(region25);
			region25.AddNeighbor(region26);
			region27.AddNeighbor(region28);
			region27.AddNeighbor(region32);
			region27.AddNeighbor(region33);
			region28.AddNeighbor(region29);
			region28.AddNeighbor(region31);
			region28.AddNeighbor(region33);
			region28.AddNeighbor(region34);
			region29.AddNeighbor(region30);
			region29.AddNeighbor(region31);
			region30.AddNeighbor(region31);
			region30.AddNeighbor(region34);
			region30.AddNeighbor(region35);
			region31.AddNeighbor(region34);
			region32.AddNeighbor(region33);
			region32.AddNeighbor(region36);
			region32.AddNeighbor(region37);
			region33.AddNeighbor(region34);
			region33.AddNeighbor(region37);
			region33.AddNeighbor(region38);
			region34.AddNeighbor(region35);
			region36.AddNeighbor(region37);
			region37.AddNeighbor(region38);
			region38.AddNeighbor(region39);
			region39.AddNeighbor(region40);
			region39.AddNeighbor(region41);
			region40.AddNeighbor(region41);
			region40.AddNeighbor(region42);
			region41.AddNeighbor(region42);

			map.Add(region1); map.Add(region2); map.Add(region3);
			map.Add(region4); map.Add(region5); map.Add(region6);
			map.Add(region7); map.Add(region8); map.Add(region9);
			map.Add(region10); map.Add(region11); map.Add(region12);
			map.Add(region13); map.Add(region14); map.Add(region15);
			map.Add(region16); map.Add(region17); map.Add(region18);
			map.Add(region19); map.Add(region20); map.Add(region21);
			map.Add(region22); map.Add(region23); map.Add(region24);
			map.Add(region25); map.Add(region26); map.Add(region27);
			map.Add(region28); map.Add(region29); map.Add(region30);
			map.Add(region31); map.Add(region32); map.Add(region33);
			map.Add(region34); map.Add(region35); map.Add(region36);
			map.Add(region37); map.Add(region38); map.Add(region39);
			map.Add(region40); map.Add(region41); map.Add(region42);
			map.Add(northAmerica);
			map.Add(southAmerica);
			map.Add(europe);
			map.Add(afrika);
			map.Add(azia);
			map.Add(australia);
			map.Finish();

			return map;
		}

	}
}
