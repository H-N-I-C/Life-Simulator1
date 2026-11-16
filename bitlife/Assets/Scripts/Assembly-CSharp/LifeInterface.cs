using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Game;

public class LifeInterface
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimHouse, float> _003C_003E9__15_0;

		public static Func<SimHouse, bool> _003C_003E9__15_2;

		public static Func<SimHouse, bool> _003C_003E9__15_3;

		public static Func<SimJewel, bool> _003C_003E9__15_1;

		public static Func<SimModelGift, string> _003C_003E9__15_4;

		public static Func<SimCasinoCollectible, string> _003C_003E9__15_5;

		public static Func<SimRacingCollectible, string> _003C_003E9__15_6;

		public static Comparison<ListItem> _003C_003E9__16_0;

		public static Action _003C_003E9__17_0;

		public static Func<DataHustlerScamType, string> _003C_003E9__67_0;

		public static Func<DataHustlerScamType, bool> _003C_003E9__67_1;

		public static Func<DataHustlerScamType, bool> _003C_003E9__67_2;

		public static Func<DataHustlerScamType, bool> _003C_003E9__67_3;

		public static Action _003C_003E9__69_0;

		public static Func<SimStreetPerson, string> _003C_003E9__71_0;

		public static Func<SimStreetPerson, int> _003C_003E9__71_1;

		public static Func<SimStreetPerson, string> _003C_003E9__71_2;

		public static Func<SimStreetPerson, bool> _003C_003E9__71_3;

		public static Func<SimStreetPerson, bool> _003C_003E9__71_4;

		public static Func<SimStreetPerson, bool> _003C_003E9__71_5;

		public static Func<KeyValuePair<int, SimBusiness>, double> _003C_003E9__73_0;

		public static Func<SimProduct, double> _003C_003E9__75_1;

		public static Func<SimBusiness, double> _003C_003E9__75_0;

		public static Func<SimProduct, double> _003C_003E9__76_0;

		public static Func<SimEmployee, float> _003C_003E9__76_1;

		public static Func<SimEmployee, string> _003C_003E9__78_0;

		public static Func<SimAnnualReport, bool> _003C_003E9__80_0;

		public static Func<SimAnnualReport, bool> _003C_003E9__80_1;

		public static Func<SimAnnualReport, string> _003C_003E9__81_0;

		public static Func<SimAnnualReport, float> _003C_003E9__81_2;

		public static Func<KeyValuePair<int, SimPortfolioAnnualReport>, bool> _003C_003E9__93_0;

		public static Func<SimCrypto, double> _003C_003E9__94_0;

		public static Func<SimFund, float> _003C_003E9__95_0;

		public static Func<SimStock, bool> _003C_003E9__96_0;

		public static Func<SimStock, bool> _003C_003E9__96_1;

		public static Func<SimStock, bool> _003C_003E9__96_2;

		public static Func<SimBond, bool> _003C_003E9__97_0;

		public static Func<SimBond, string> _003C_003E9__97_1;

		public static Func<SimBond, double> _003C_003E9__97_2;

		public static Func<SimBond, bool> _003C_003E9__97_3;

		public static Func<SimBond, string> _003C_003E9__97_4;

		public static Func<SimBond, double> _003C_003E9__97_5;

		public static Func<SimBond, bool> _003C_003E9__97_6;

		public static Func<SimBond, string> _003C_003E9__97_7;

		public static Func<SimBond, double> _003C_003E9__97_8;

		public static Func<SimStock, bool> _003C_003E9__98_0;

		public static Func<SimStock, string> _003C_003E9__98_1;

		public static Func<SimInvestment, double> _003C_003E9__99_0;

		public static Func<SimAstronaut, float> _003C_003E9__148_0;

		public static Func<char, bool> _003C_003E9__148_1;

		public static Func<SimProbe, bool> _003C_003E9__149_0;

		public static Func<SimProbe, bool> _003C_003E9__149_1;

		public static Func<SimProbe, bool> _003C_003E9__149_2;

		public static Func<SimAstronaut, int> _003C_003E9__159_0;

		public static Func<SimAstronaut, string> _003C_003E9__159_1;

		public static Func<SimModelGift, string> _003C_003E9__176_0;

		public static Func<SimModelGift, bool> _003C_003E9__176_1;

		public static Func<SimModelGift, bool> _003C_003E9__176_2;

		public static Func<SimAuctionItem, string> _003C_003E9__177_1;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_1;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_2;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_3;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_4;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_5;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_6;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_7;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_8;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_9;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_10;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_11;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_12;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_13;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_14;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_15;

		public static Func<SimAuctionItem, bool> _003C_003E9__178_16;

		public static Func<SimCommunard, bool> _003C_003E9__183_0;

		public static Func<SimCommunard, bool> _003C_003E9__183_1;

		public static Func<SimCommunard, bool> _003C_003E9__184_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__189_0;

		public static Func<SimAuctionItem, string> _003C_003E9__189_1;

		public static Func<SimAuctionItem, bool> _003C_003E9__189_2;

		public static Func<SimAuctionItem, string> _003C_003E9__189_3;

		public static Func<SimAuctionItem, bool> _003C_003E9__189_4;

		public static Func<SimAuctionItem, string> _003C_003E9__189_5;

		public static Func<SimModelGift, string> _003C_003E9__189_6;

		public static Func<SimAuctionItem, bool> _003C_003E9__189_7;

		public static Func<SimAuctionItem, string> _003C_003E9__189_8;

		public static Func<SimAuctionItem, bool> _003C_003E9__189_9;

		public static Func<SimAuctionItem, string> _003C_003E9__189_10;

		public static Func<SimAuctionItem, bool> _003C_003E9__189_11;

		public static Func<SimAuctionItem, string> _003C_003E9__189_12;

		public static Func<SimJewel, string> _003C_003E9__189_13;

		public static Func<SimAuctionItem, bool> _003C_003E9__189_14;

		public static Func<SimAuctionItem, string> _003C_003E9__189_15;

		public static Func<SimAuctionItem, bool> _003C_003E9__189_16;

		public static Func<SimAuctionItem, string> _003C_003E9__189_17;

		public static Func<SimAuctionItem, bool> _003C_003E9__189_18;

		public static Func<SimAuctionItem, string> _003C_003E9__189_19;

		public static Func<SimModelGig, bool> _003C_003E9__194_0;

		public static Func<SimModelGig, bool> _003C_003E9__194_1;

		public static Func<SimModel, bool> _003C_003E9__197_0;

		public static Func<SimSpyAgencySecurityFeature, string> _003C_003E9__203_0;

		public static Func<SimSecretAgentGadget, string> _003C_003E9__204_1;

		public static Func<SimSecretAgentGadget, string> _003C_003E9__204_3;

		public static Func<SimSecretAgent, int> _003C_003E9__206_0;

		public static Func<SimSecretAgent, int> _003C_003E9__206_1;

		public static Func<SimSecretAgent, int> _003C_003E9__206_2;

		public static Func<SimSecretAgent, int> _003C_003E9__206_3;

		public static Func<SimSecretAgent, int> _003C_003E9__206_4;

		public static Func<SimSecretAgent, int> _003C_003E9__206_5;

		public static Func<SimSecretAgent, int> _003C_003E9__206_6;

		public static Func<SimSecretAgent, int> _003C_003E9__206_7;

		public static Func<SimSecretAgentMission, int> _003C_003E9__207_0;

		public static Func<SimSecretAgentMission, bool> _003C_003E9__208_0;

		public static Func<SimSecretAgentMission, bool> _003C_003E9__208_1;

		public static Func<SimSecretAgent, int> _003C_003E9__209_0;

		public static Func<SimZoo, double> _003C_003E9__216_0;

		public static Func<SimZooHabitat, string> _003C_003E9__217_0;

		public static Func<SimZooHabitat, string> _003C_003E9__218_0;

		public static Func<SimZooAnimal, string> _003C_003E9__218_1;

		public static Func<SimZooAnimal, string> _003C_003E9__218_2;

		public static Func<SimZooAnimal, string> _003C_003E9__219_0;

		public static Func<SimZooAnimal, string> _003C_003E9__220_0;

		public static Func<SimZooAnimal, string> _003C_003E9__220_1;

		public static Func<DataZooFundraiser, float> _003C_003E9__223_0;

		public static Func<DataZooAnimalGroomMethods, int> _003C_003E9__223_1;

		public static Func<SimZooAnimal, int> _003C_003E9__226_0;

		public static Func<SimZooRequestedAnimal, string> _003C_003E9__226_1;

		public static Func<SimDealerCartel, float> _003C_003E9__227_0;

		public static Func<SimDealerCartelMember, DEALER_RANK> _003C_003E9__229_0;

		public static Func<SimDealerDrug, string> _003C_003E9__230_0;

		public static Func<SimDealerDrugCut, float> _003C_003E9__231_0;

		public static Func<SimDealerDrug, string> _003C_003E9__232_0;

		public static Func<SimDealerDrugCut, float> _003C_003E9__232_1;

		public static Func<SimDealerStreetPerson, bool> _003C_003E9__233_0;

		public static Func<SimDealerStreetPerson, float> _003C_003E9__233_1;

		public static Func<SimDealerStreetPerson, bool> _003C_003E9__233_2;

		public static Func<SimDealerStreetPerson, float> _003C_003E9__233_3;

		public static Func<SimDealerSupplyRun, float> _003C_003E9__236_0;

		public static Comparison<ListItem> _003C_003E9__240_0;

		public static Func<SimCasinoAmenity, string> _003C_003E9__242_0;

		public static Func<SimCasinoEntertainmentAct, float> _003C_003E9__243_0;

		public static Func<SimCasinoGambler, CASINO_GAMBLING_STYLE> _003C_003E9__246_0;

		public static Func<SimCasinoMafiaMember, double> _003C_003E9__246_1;

		public static Func<SimCasinoCelebrity, float> _003C_003E9__247_0;

		public static Func<SimCasinoGambler, string> _003C_003E9__252_0;

		public static Func<SimCasinoCollectible, string> _003C_003E9__253_0;

		public static Func<SimLuxuryIsland, double> _003C_003E9__258_0;

		public static Func<SimAircraft, float> _003C_003E9__259_0;

		public static Func<SimWatercraft, double> _003C_003E9__260_0;

		public static Func<SimCar, float> _003C_003E9__261_0;

		public static Func<SimJewel, float> _003C_003E9__262_0;

		public static Func<SimLuxuryIsland, float> _003C_003E9__264_0;

		public static Func<SimAircraft, bool> _003C_003E9__271_0;

		public static Func<SimCar, bool> _003C_003E9__271_1;

		public static Func<SimWatercraft, bool> _003C_003E9__271_2;

		public static Func<SimJewel, string> _003C_003E9__272_0;

		public static Func<SimAircraft, bool> _003C_003E9__273_0;

		public static Func<SimAircraft, string> _003C_003E9__273_1;

		public static Func<SimCar, bool> _003C_003E9__273_2;

		public static Func<SimCar, string> _003C_003E9__273_3;

		public static Func<SimWatercraft, bool> _003C_003E9__273_4;

		public static Func<SimWatercraft, string> _003C_003E9__273_5;

		public static Func<SimAuctionItem, string> _003C_003E9__279_1;

		public static Func<SimAircraft, bool> _003C_003E9__280_0;

		public static Func<SimAircraft, string> _003C_003E9__280_1;

		public static Func<SimCar, bool> _003C_003E9__280_2;

		public static Func<SimCar, string> _003C_003E9__280_3;

		public static Func<SimRecreationalVehicle, string> _003C_003E9__280_4;

		public static Func<SimWatercraft, bool> _003C_003E9__280_5;

		public static Func<SimWatercraft, string> _003C_003E9__280_6;

		public static Func<SimHeirloom, string> _003C_003E9__281_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_1;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_2;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_3;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_4;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_5;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_6;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_7;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_8;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_9;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_10;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_11;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_12;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_13;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_14;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_15;

		public static Func<SimAuctionItem, bool> _003C_003E9__282_16;

		public static Func<SimJewel, string> _003C_003E9__283_0;

		public static Func<SimModelGift, string> _003C_003E9__286_0;

		public static Func<SimModelGift, bool> _003C_003E9__286_1;

		public static Func<SimModelGift, bool> _003C_003E9__286_2;

		public static Func<SimCasinoCollectible, string> _003C_003E9__287_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_1;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_2;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_3;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_4;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_5;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_6;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_7;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_8;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_9;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_10;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_11;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_12;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_13;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_14;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_15;

		public static Func<SimAuctionItem, bool> _003C_003E9__288_16;

		public static Func<SimRecreationalVehicle, bool> _003C_003E9__302_1;

		public static Func<SimOutdoorHandbookEntry, string> _003C_003E9__304_1;

		public static Func<SimOutdoorEntryDiscovery, string> _003C_003E9__304_2;

		public static Func<SimOutdoorFishingTreasure, string> _003C_003E9__305_0;

		public static Func<SimOutdoorFishingTreasure, string> _003C_003E9__307_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__311_0;

		public static Func<SimCar, float> _003C_003E9__312_0;

		public static Func<SimRacingTrack, int> _003C_003E9__315_0;

		public static Func<SimRacingTrack, int> _003C_003E9__315_1;

		public static Func<SimRacingTrack, int> _003C_003E9__317_1;

		public static Func<SimRacingTrack, int> _003C_003E9__317_3;

		public static Func<SimRacingTrack, int> _003C_003E9__318_1;

		public static Func<SimRaceCarMod, bool> _003C_003E9__321_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__322_0;

		public static Func<SimAuctionItem, string> _003C_003E9__322_1;

		public static Func<SimRacingCollectible, string> _003C_003E9__329_0;

		public static Func<SimRacingCollectible, string> _003C_003E9__331_0;

		public static Func<SimVampireCouncilMember, int> _003C_003E9__347_0;

		internal float _003CGetT2List_003Eb__15_0(SimHouse x)
		{
			return 0f;
		}

		internal bool _003CGetT2List_003Eb__15_2(SimHouse x)
		{
			return false;
		}

		internal bool _003CGetT2List_003Eb__15_3(SimHouse x)
		{
			return false;
		}

		internal bool _003CGetT2List_003Eb__15_1(SimJewel x)
		{
			return false;
		}

		internal string _003CGetT2List_003Eb__15_4(SimModelGift t)
		{
			return null;
		}

		internal string _003CGetT2List_003Eb__15_5(SimCasinoCollectible t)
		{
			return null;
		}

		internal string _003CGetT2List_003Eb__15_6(SimRacingCollectible t)
		{
			return null;
		}

		internal int _003CGetT3List_003Eb__16_0(ListItem x, ListItem y)
		{
			return 0;
		}

		internal void _003CGetT4List_003Eb__17_0()
		{
		}

		internal string _003CAvailableScamList_003Eb__67_0(DataHustlerScamType x)
		{
			return null;
		}

		internal bool _003CAvailableScamList_003Eb__67_1(DataHustlerScamType x)
		{
			return false;
		}

		internal bool _003CAvailableScamList_003Eb__67_2(DataHustlerScamType x)
		{
			return false;
		}

		internal bool _003CAvailableScamList_003Eb__67_3(DataHustlerScamType x)
		{
			return false;
		}

		internal void _003CGetCrimeList_003Eb__69_0()
		{
		}

		internal string _003CAvailableStreetPersonArray_003Eb__71_0(SimStreetPerson x)
		{
			return null;
		}

		internal int _003CAvailableStreetPersonArray_003Eb__71_1(SimStreetPerson x)
		{
			return 0;
		}

		internal string _003CAvailableStreetPersonArray_003Eb__71_2(SimStreetPerson x)
		{
			return null;
		}

		internal bool _003CAvailableStreetPersonArray_003Eb__71_3(SimStreetPerson x)
		{
			return false;
		}

		internal bool _003CAvailableStreetPersonArray_003Eb__71_4(SimStreetPerson x)
		{
			return false;
		}

		internal bool _003CAvailableStreetPersonArray_003Eb__71_5(SimStreetPerson x)
		{
			return false;
		}

		internal double _003CBusinessIndustryList_003Eb__73_0(KeyValuePair<int, SimBusiness> x)
		{
			return 0.0;
		}

		internal double _003CCompaniesForSaleList_003Eb__75_1(SimProduct x)
		{
			return 0.0;
		}

		internal double _003CCompaniesForSaleList_003Eb__75_0(SimBusiness x)
		{
			return 0.0;
		}

		internal double _003CBusinessItemArray_003Eb__76_0(SimProduct x)
		{
			return 0.0;
		}

		internal float _003CBusinessItemArray_003Eb__76_1(SimEmployee x)
		{
			return 0f;
		}

		internal string _003CBusinessEmployeeList_003Eb__78_0(SimEmployee x)
		{
			return null;
		}

		internal bool _003CAnnualReportList_003Eb__80_0(SimAnnualReport x)
		{
			return false;
		}

		internal bool _003CAnnualReportList_003Eb__80_1(SimAnnualReport x)
		{
			return false;
		}

		internal string _003CPreviousCEOList_003Eb__81_0(SimAnnualReport x)
		{
			return null;
		}

		internal float _003CPreviousCEOList_003Eb__81_2(SimAnnualReport x)
		{
			return 0f;
		}

		internal bool _003CInvestmentsItemArray_003Eb__93_0(KeyValuePair<int, SimPortfolioAnnualReport> x)
		{
			return false;
		}

		internal double _003CCryptocurrencyItemArray_003Eb__94_0(SimCrypto x)
		{
			return 0.0;
		}

		internal float _003CFundsMarketItemArray_003Eb__95_0(SimFund x)
		{
			return 0f;
		}

		internal bool _003CPennyStockMarketItemArray_003Eb__96_0(SimStock x)
		{
			return false;
		}

		internal bool _003CPennyStockMarketItemArray_003Eb__96_1(SimStock x)
		{
			return false;
		}

		internal bool _003CPennyStockMarketItemArray_003Eb__96_2(SimStock x)
		{
			return false;
		}

		internal bool _003CBondMarketItemArray_003Eb__97_0(SimBond x)
		{
			return false;
		}

		internal string _003CBondMarketItemArray_003Eb__97_1(SimBond x)
		{
			return null;
		}

		internal double _003CBondMarketItemArray_003Eb__97_2(SimBond x)
		{
			return 0.0;
		}

		internal bool _003CBondMarketItemArray_003Eb__97_3(SimBond x)
		{
			return false;
		}

		internal string _003CBondMarketItemArray_003Eb__97_4(SimBond x)
		{
			return null;
		}

		internal double _003CBondMarketItemArray_003Eb__97_5(SimBond x)
		{
			return 0.0;
		}

		internal bool _003CBondMarketItemArray_003Eb__97_6(SimBond x)
		{
			return false;
		}

		internal string _003CBondMarketItemArray_003Eb__97_7(SimBond x)
		{
			return null;
		}

		internal double _003CBondMarketItemArray_003Eb__97_8(SimBond x)
		{
			return 0.0;
		}

		internal bool _003CStockMarketItemArray_003Eb__98_0(SimStock x)
		{
			return false;
		}

		internal string _003CStockMarketItemArray_003Eb__98_1(SimStock x)
		{
			return null;
		}

		internal double _003CPortfolioItemArray_003Eb__99_0(SimInvestment x)
		{
			return 0.0;
		}

		internal float _003CGetAstronautListItems_003Eb__148_0(SimAstronaut x)
		{
			return 0f;
		}

		internal bool _003CGetAstronautListItems_003Eb__148_1(char t)
		{
			return false;
		}

		internal bool _003CProbeMenuItems_003Eb__149_0(SimProbe t)
		{
			return false;
		}

		internal bool _003CProbeMenuItems_003Eb__149_1(SimProbe t)
		{
			return false;
		}

		internal bool _003CProbeMenuItems_003Eb__149_2(SimProbe t)
		{
			return false;
		}

		internal int _003CAvailableSpaceTrainingClassMatesItemArray_003Eb__159_0(SimAstronaut x)
		{
			return 0;
		}

		internal string _003CAvailableSpaceTrainingClassMatesItemArray_003Eb__159_1(SimAstronaut x)
		{
			return null;
		}

		internal string _003CItemArrayForCloset_003Eb__176_0(SimModelGift t)
		{
			return null;
		}

		internal bool _003CItemArrayForCloset_003Eb__176_1(SimModelGift x)
		{
			return false;
		}

		internal bool _003CItemArrayForCloset_003Eb__176_2(SimModelGift x)
		{
			return false;
		}

		internal string _003CBelongingsItemArrayForSimAuction_003Eb__177_1(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_0(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_1(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_2(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_3(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_4(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_5(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_6(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_7(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_8(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_9(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_10(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_11(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_12(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_13(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_14(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_15(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CBelongingsItemArrayForSimIAPTypeAuction_003Eb__178_16(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetCommunardsList_003Eb__183_0(SimCommunard x)
		{
			return false;
		}

		internal bool _003CGetCommunardsList_003Eb__183_1(SimCommunard x)
		{
			return false;
		}

		internal bool _003CGetCommunardListForCommunard_003Eb__184_0(SimCommunard x)
		{
			return false;
		}

		internal bool _003CGetTotemsList_003Eb__189_0(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTotemsList_003Eb__189_1(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTotemsList_003Eb__189_2(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTotemsList_003Eb__189_3(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTotemsList_003Eb__189_4(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTotemsList_003Eb__189_5(SimAuctionItem x)
		{
			return null;
		}

		internal string _003CGetTotemsList_003Eb__189_6(SimModelGift t)
		{
			return null;
		}

		internal bool _003CGetTotemsList_003Eb__189_7(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTotemsList_003Eb__189_8(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTotemsList_003Eb__189_9(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTotemsList_003Eb__189_10(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTotemsList_003Eb__189_11(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTotemsList_003Eb__189_12(SimAuctionItem x)
		{
			return null;
		}

		internal string _003CGetTotemsList_003Eb__189_13(SimJewel t)
		{
			return null;
		}

		internal bool _003CGetTotemsList_003Eb__189_14(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTotemsList_003Eb__189_15(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTotemsList_003Eb__189_16(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTotemsList_003Eb__189_17(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTotemsList_003Eb__189_18(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTotemsList_003Eb__189_19(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetEliteGigsList_003Eb__194_0(SimModelGig t)
		{
			return false;
		}

		internal bool _003CGetEliteGigsList_003Eb__194_1(SimModelGig t)
		{
			return false;
		}

		internal bool _003CGetColleaguesList_003Eb__197_0(SimModel t)
		{
			return false;
		}

		internal string _003CGetSecurityFeaturesList_003Eb__203_0(SimSpyAgencySecurityFeature t)
		{
			return null;
		}

		internal string _003CGetGadgetsList_003Eb__204_1(SimSecretAgentGadget t)
		{
			return null;
		}

		internal string _003CGetGadgetsList_003Eb__204_3(SimSecretAgentGadget t)
		{
			return null;
		}

		internal int _003CGetSpyAgencyAgentsList_003Eb__206_0(SimSecretAgent x)
		{
			return 0;
		}

		internal int _003CGetSpyAgencyAgentsList_003Eb__206_1(SimSecretAgent x)
		{
			return 0;
		}

		internal int _003CGetSpyAgencyAgentsList_003Eb__206_2(SimSecretAgent x)
		{
			return 0;
		}

		internal int _003CGetSpyAgencyAgentsList_003Eb__206_3(SimSecretAgent x)
		{
			return 0;
		}

		internal int _003CGetSpyAgencyAgentsList_003Eb__206_4(SimSecretAgent x)
		{
			return 0;
		}

		internal int _003CGetSpyAgencyAgentsList_003Eb__206_5(SimSecretAgent x)
		{
			return 0;
		}

		internal int _003CGetSpyAgencyAgentsList_003Eb__206_6(SimSecretAgent x)
		{
			return 0;
		}

		internal int _003CGetSpyAgencyAgentsList_003Eb__206_7(SimSecretAgent x)
		{
			return 0;
		}

		internal int _003CGetSpyAgencyMissionsList_003Eb__207_0(SimSecretAgentMission t)
		{
			return 0;
		}

		internal bool _003CGetSecretAgentPastMissions_003Eb__208_0(SimSecretAgentMission t)
		{
			return false;
		}

		internal bool _003CGetSecretAgentPastMissions_003Eb__208_1(SimSecretAgentMission t)
		{
			return false;
		}

		internal int _003CGetRivalAgencyDatabaseList_003Eb__209_0(SimSecretAgent t)
		{
			return 0;
		}

		internal double _003CGetAvailableZoosList_003Eb__216_0(SimZoo t)
		{
			return 0.0;
		}

		internal string _003CGetZooHabitatsList_003Eb__217_0(SimZooHabitat t)
		{
			return null;
		}

		internal string _003CGetZooAnimalsList_003Eb__218_0(SimZooHabitat t)
		{
			return null;
		}

		internal string _003CGetZooAnimalsList_003Eb__218_1(SimZooAnimal t)
		{
			return null;
		}

		internal string _003CGetZooAnimalsList_003Eb__218_2(SimZooAnimal n)
		{
			return null;
		}

		internal string _003CGetZooAnimalsListForBreedInHabitat_003Eb__219_0(SimZooAnimal t)
		{
			return null;
		}

		internal string _003CGetZooSpecificHabitatList_003Eb__220_0(SimZooAnimal t)
		{
			return null;
		}

		internal string _003CGetZooSpecificHabitatList_003Eb__220_1(SimZooAnimal n)
		{
			return null;
		}

		internal float _003CGetZooEmployeeRelationshipList_003Eb__223_0(DataZooFundraiser t)
		{
			return 0f;
		}

		internal int _003CGetZooEmployeeRelationshipList_003Eb__223_1(DataZooAnimalGroomMethods t)
		{
			return 0;
		}

		internal int _003CGetZooTradingPostList_003Eb__226_0(SimZooAnimal t)
		{
			return 0;
		}

		internal string _003CGetZooTradingPostList_003Eb__226_1(SimZooRequestedAnimal t)
		{
			return null;
		}

		internal float _003CGetDealerGatewayList_003Eb__227_0(SimDealerCartel t)
		{
			return 0f;
		}

		internal DEALER_RANK _003CGetDealerCartelMembersList_003Eb__229_0(SimDealerCartelMember t)
		{
			return DEALER_RANK.NONE;
		}

		internal string _003CGetDealerCartelDrugsList_003Eb__230_0(SimDealerDrug t)
		{
			return null;
		}

		internal float _003CGetDealerCartelListForDrug_003Eb__231_0(SimDealerDrugCut x)
		{
			return 0f;
		}

		internal string _003CGetDealerCartelManufactureList_003Eb__232_0(SimDealerDrug t)
		{
			return null;
		}

		internal float _003CGetDealerCartelManufactureList_003Eb__232_1(SimDealerDrugCut x)
		{
			return 0f;
		}

		internal bool _003CGetDealerCartelListForStreet_003Eb__233_0(SimDealerStreetPerson t)
		{
			return false;
		}

		internal float _003CGetDealerCartelListForStreet_003Eb__233_1(SimDealerStreetPerson x)
		{
			return 0f;
		}

		internal bool _003CGetDealerCartelListForStreet_003Eb__233_2(SimDealerStreetPerson t)
		{
			return false;
		}

		internal float _003CGetDealerCartelListForStreet_003Eb__233_3(SimDealerStreetPerson x)
		{
			return 0f;
		}

		internal float _003CGetDealerCartelSupplyRuns_003Eb__236_0(SimDealerSupplyRun t)
		{
			return 0f;
		}

		internal int _003CGetCasinoTypesList_003Eb__240_0(ListItem x, ListItem y)
		{
			return 0;
		}

		internal string _003CGetCasinoAmenitiesList_003Eb__242_0(SimCasinoAmenity x)
		{
			return null;
		}

		internal float _003CGetCasinoEntertainmentList_003Eb__243_0(SimCasinoEntertainmentAct t)
		{
			return 0f;
		}

		internal CASINO_GAMBLING_STYLE _003CGetCasinoGamblersList_003Eb__246_0(SimCasinoGambler t)
		{
			return CASINO_GAMBLING_STYLE.NONE;
		}

		internal double _003CGetCasinoGamblersList_003Eb__246_1(SimCasinoMafiaMember t)
		{
			return 0.0;
		}

		internal float _003CGetCasinoCelebritiesList_003Eb__247_0(SimCasinoCelebrity t)
		{
			return 0f;
		}

		internal string _003CGetCasinoTournamentGamblersList_003Eb__252_0(SimCasinoGambler t)
		{
			return null;
		}

		internal string _003CItemArrayForLuckyCharms_003Eb__253_0(SimCasinoCollectible t)
		{
			return null;
		}

		internal double _003CGetLuxuryIslandRealtorList_003Eb__258_0(SimLuxuryIsland t)
		{
			return 0.0;
		}

		internal float _003CGetLuxuryAircraftDealershipList_003Eb__259_0(SimAircraft t)
		{
			return 0f;
		}

		internal double _003CGetLuxuryWatercraftDealershipList_003Eb__260_0(SimWatercraft t)
		{
			return 0.0;
		}

		internal float _003CGetLuxuryCarDealershipList_003Eb__261_0(SimCar t)
		{
			return 0f;
		}

		internal float _003CGetLuxuryUpscaleJewelersList_003Eb__262_0(SimJewel t)
		{
			return 0f;
		}

		internal float _003CGetLuxuryOwnedIslandsList_003Eb__264_0(SimLuxuryIsland t)
		{
			return 0f;
		}

		internal bool _003CBelongingItemArrayListForLuxuryVehicles_003Eb__271_0(SimAircraft x)
		{
			return false;
		}

		internal bool _003CBelongingItemArrayListForLuxuryVehicles_003Eb__271_1(SimCar x)
		{
			return false;
		}

		internal bool _003CBelongingItemArrayListForLuxuryVehicles_003Eb__271_2(SimWatercraft x)
		{
			return false;
		}

		internal string _003CGetBelongingsLuxuryJewelItemsToAddDisplay_003Eb__272_0(SimJewel t)
		{
			return null;
		}

		internal bool _003CGetBelongingsLuxuryVehiclesToAddDisplay_003Eb__273_0(SimAircraft x)
		{
			return false;
		}

		internal string _003CGetBelongingsLuxuryVehiclesToAddDisplay_003Eb__273_1(SimAircraft t)
		{
			return null;
		}

		internal bool _003CGetBelongingsLuxuryVehiclesToAddDisplay_003Eb__273_2(SimCar x)
		{
			return false;
		}

		internal string _003CGetBelongingsLuxuryVehiclesToAddDisplay_003Eb__273_3(SimCar t)
		{
			return null;
		}

		internal bool _003CGetBelongingsLuxuryVehiclesToAddDisplay_003Eb__273_4(SimWatercraft x)
		{
			return false;
		}

		internal string _003CGetBelongingsLuxuryVehiclesToAddDisplay_003Eb__273_5(SimWatercraft t)
		{
			return null;
		}

		internal string _003CGetBelongingsMarketItemsToAddDisplay_003Eb__279_1(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetBelongingsVehiclesToAddDisplay_003Eb__280_0(SimAircraft x)
		{
			return false;
		}

		internal string _003CGetBelongingsVehiclesToAddDisplay_003Eb__280_1(SimAircraft t)
		{
			return null;
		}

		internal bool _003CGetBelongingsVehiclesToAddDisplay_003Eb__280_2(SimCar x)
		{
			return false;
		}

		internal string _003CGetBelongingsVehiclesToAddDisplay_003Eb__280_3(SimCar t)
		{
			return null;
		}

		internal string _003CGetBelongingsVehiclesToAddDisplay_003Eb__280_4(SimRecreationalVehicle t)
		{
			return null;
		}

		internal bool _003CGetBelongingsVehiclesToAddDisplay_003Eb__280_5(SimWatercraft x)
		{
			return false;
		}

		internal string _003CGetBelongingsVehiclesToAddDisplay_003Eb__280_6(SimWatercraft t)
		{
			return null;
		}

		internal string _003CGetBelongingsHeirloomsToAddDisplay_003Eb__281_0(SimHeirloom t)
		{
			return null;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_0(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_1(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_2(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_3(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_4(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_5(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_6(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_7(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_8(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_9(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_10(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_11(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_12(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_13(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_14(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_15(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetBelongingsIAPItemsToAddDisplay_003Eb__282_16(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetBelongingsJewelItemsToAddDisplay_003Eb__283_0(SimJewel t)
		{
			return null;
		}

		internal string _003CGetBelongingsClosetItemsToAddDisplay_003Eb__286_0(SimModelGift t)
		{
			return null;
		}

		internal bool _003CGetBelongingsClosetItemsToAddDisplay_003Eb__286_1(SimModelGift x)
		{
			return false;
		}

		internal bool _003CGetBelongingsClosetItemsToAddDisplay_003Eb__286_2(SimModelGift x)
		{
			return false;
		}

		internal string _003CGetBelongingsLuckyCharmsItemsToAddDisplay_003Eb__287_0(SimCasinoCollectible t)
		{
			return null;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_0(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_1(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_2(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_3(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_4(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_5(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_6(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_7(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_8(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_9(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_10(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_11(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_12(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_13(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_14(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_15(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CAddIapItemsToBelongingsIfNeeded_003Eb__288_16(SimAuctionItem t)
		{
			return false;
		}

		internal bool _003CGetOutdoorLifestyleList_003Eb__302_1(SimRecreationalVehicle x)
		{
			return false;
		}

		internal string _003CGetOutdoorHandbookTypeEntriesList_003Eb__304_1(SimOutdoorHandbookEntry x)
		{
			return null;
		}

		internal string _003CGetOutdoorHandbookTypeEntriesList_003Eb__304_2(SimOutdoorEntryDiscovery x)
		{
			return null;
		}

		internal string _003CItemArrayForOutdoorFishingTreasures_003Eb__305_0(SimOutdoorFishingTreasure t)
		{
			return null;
		}

		internal string _003CGetBelongingsOutdoorFishingTreasuresItemsToAddDisplay_003Eb__307_0(SimOutdoorFishingTreasure t)
		{
			return null;
		}

		internal bool _003CGetRaceCarGarageList_003Eb__311_0(SimAuctionItem t)
		{
			return false;
		}

		internal float _003CGetRaceCarDealershipList_003Eb__312_0(SimCar t)
		{
			return 0f;
		}

		internal int _003CGetRacesListForType_003Eb__315_0(SimRacingTrack x)
		{
			return 0;
		}

		internal int _003CGetRacesListForType_003Eb__315_1(SimRacingTrack t)
		{
			return 0;
		}

		internal int _003CGetRacingTimeTrialsList_003Eb__317_1(SimRacingTrack x)
		{
			return 0;
		}

		internal int _003CGetRacingTimeTrialsList_003Eb__317_3(SimRacingTrack x)
		{
			return 0;
		}

		internal int _003CGetRacingTrackRecordList_003Eb__318_1(SimRacingTrack x)
		{
			return 0;
		}

		internal bool _003CGetGaragedItemArrayForRaceCar_003Eb__321_0(SimRaceCarMod mod)
		{
			return false;
		}

		internal bool _003CGetGarageEmptyBayCarList_003Eb__322_0(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetGarageEmptyBayCarList_003Eb__322_1(SimAuctionItem x)
		{
			return null;
		}

		internal string _003CItemArrayForRacingJackets_003Eb__329_0(SimRacingCollectible t)
		{
			return null;
		}

		internal string _003CGetBelongingsRacingJacketsItemsToAddDisplay_003Eb__331_0(SimRacingCollectible t)
		{
			return null;
		}

		internal int _003CGetVampireCouncilList_003Eb__347_0(SimVampireCouncilMember p)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass100_0
	{
		public SimParent parent;

		internal bool _003CRelationshipItemArrayForParent_003Eb__0(SimCommunard x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass101_0
	{
		public SimSibling sibling;

		internal bool _003CRelationshipItemArrayForSibling_003Eb__0(SimCommunard x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass102_0
	{
		public SimLover lover;

		internal bool _003CRelationshipItemArrayForLoverButOverrideProfileWithListItem_003Eb__0(SimCommunard x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass103_0
	{
		public SimChild child;

		internal bool _003CRelationshipItemArrayForChild_003Eb__0(SimCommunard x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass111_0
	{
		public SimPerson friend;

		internal bool _003CRelationshipItemArrayForFriend_003Eb__0(SimCommunard x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass177_0
	{
		public int auctionItemType;

		internal bool _003CBelongingsItemArrayForSimAuction_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass189_0
	{
		public (MARKET_ITEM_TYPE, MINI_PAYWALL_TYPE, string, string) dataItem;

		internal bool _003CGetTotemsList_003Eb__20(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass204_0
	{
		public int i;

		internal bool _003CGetGadgetsList_003Eb__0(SimSecretAgentGadget t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass204_1
	{
		public int i;

		internal bool _003CGetGadgetsList_003Eb__2(SimSecretAgentGadget t)
		{
			return false;
		}
	}

	[StructLayout((LayoutKind)3)]
	internal struct _003C_003Ec__DisplayClass214_0
	{
		public List<(string, string)> fileList;

		public SimSecretAgentHackingMission mission;

		public BracketParser parser;

		public List<string> listExistingColors;
	}

	private sealed class _003C_003Ec__DisplayClass214_1
	{
		public string fileName;

		public Func<(string, string), bool> _003C_003E9__1;

		internal bool _003CGetHackingMissionDatabaseFilesList_003Eb__1((string, string) t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass279_0
	{
		public MARKET_ITEM_TYPE itemType;

		internal bool _003CGetBelongingsMarketItemsToAddDisplay_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass279_1
	{
		public SimAuctionItem item;

		public Scenario scenario;

		public _003C_003Ec__DisplayClass279_0 CS_0024_003C_003E8__locals1;

		internal void _003CGetBelongingsMarketItemsToAddDisplay_003Eb__2()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass289_0
	{
		public bool isLuxury;

		internal bool _003CHasCars_003Eb__0(SimCar t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass290_0
	{
		public bool isLuxury;

		internal bool _003CHasAircraft_003Eb__0(SimAircraft t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass291_0
	{
		public bool isLuxury;

		internal bool _003CHasWatercraft_003Eb__0(SimWatercraft t)
		{
			return false;
		}
	}

	[StructLayout((LayoutKind)3)]
	internal struct _003C_003Ec__DisplayClass303_0
	{
		public SimOutdoor outdoorLifestyle;
	}

	private sealed class _003C_003Ec__DisplayClass304_0
	{
		public OUTDOOR_HANDBOOK_ENTRY_TYPE type;

		internal bool _003CGetOutdoorHandbookTypeEntriesList_003Eb__0(SimOutdoorHandbookEntry x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass317_0
	{
		public SimRacing racing;

		internal bool _003CGetRacingTimeTrialsList_003Eb__0(SimRacingTrack t)
		{
			return false;
		}

		internal bool _003CGetRacingTimeTrialsList_003Eb__2(SimRacingTrack t)
		{
			return false;
		}
	}

	[StructLayout((LayoutKind)3)]
	internal struct _003C_003Ec__DisplayClass318_0
	{
		public SimRacing racing;

		public ListItem listItem;

		public ArrayList listItems;
	}

	[StructLayout((LayoutKind)3)]
	internal struct _003C_003Ec__DisplayClass319_0
	{
		public ArrayList listItems;

		public SimRacing racing;
	}

	private sealed class _003C_003Ec__DisplayClass323_0
	{
		public bool isLuxury;

		internal bool _003CHasCarsAvailableToBeGaraged_003Eb__0(SimCar t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public string occupationString;

		internal void _003CRefreshUI_003Eb__0(string result)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass81_0
	{
		public string id;

		internal bool _003CPreviousCEOList_003Eb__1(SimAnnualReport x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass84_0
	{
		public SimHouse house;

		internal bool _003CHouseItemArrayForHouse_003Eb__0(SimPropertyLeaseContract x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass95_0
	{
		public SimFund aFund;

		internal bool _003CFundsMarketItemArray_003Eb__1(SimInvestment x)
		{
			return false;
		}
	}

	public int T1ButtonState;

	private readonly LifeInterfaceMusicProducer _musicProducer;

	private readonly LifeInterfaceFightClub _fightClub;

	private const string MovieDirectorContainerId = "movie_director";

	private const string MovieDirectorHubListId = "genesis_movie_director_hub";

	private const int MovieDirectorMinimumAge = 18;

	public ArrayList GetRelationshipItemsForPerson(SimPerson person)
	{
		return null;
	}

	public void RefreshUI()
	{
	}

	public string GetOccupationEmojiString()
	{
		return null;
	}

	public void CheckForBoosts()
	{
	}

	public void UserSelectsProfile()
	{
	}

	public void UserSelectsT1Button()
	{
	}

	public void UserSelectsT2Button()
	{
	}

	public void UserSelectsBigTButton()
	{
	}

	public void UserSelectsT3Button()
	{
	}

	public void UserSelectsT4Button()
	{
	}

	public List GetT1List()
	{
		return null;
	}

	public List GetT2List()
	{
		return null;
	}

	public List GetT3List()
	{
		return null;
	}

	public List GetT4List()
	{
		return null;
	}

	public ArrayList AssetShoppingArray(bool placeholderList = false, bool housingOnly = false, bool carsOnly = false)
	{
		return null;
	}

	public ArrayList HouseShoppingItemArray()
	{
		return null;
	}

	public ArrayList PetShoppingItemArray(bool placeholderList = false)
	{
		return null;
	}

	public List CreateCatBreederListWithPlaceholder(bool usePlaceholder)
	{
		return null;
	}

	public List CreateDogBreederListWithPlaceholder(bool usePlaceholder)
	{
		return null;
	}

	public ArrayList PrisonItemArray()
	{
		return null;
	}

	public ArrayList PrisonJuvenileItemArray()
	{
		return null;
	}

	public ArrayList SchoolItemArray()
	{
		return null;
	}

	public ArrayList JobItemArray()
	{
		return null;
	}

	public ArrayList MilitaryItemArray()
	{
		return null;
	}

	public ArrayList PartTimeJobItemArrayForJob(SimPartTimeJob job)
	{
		return null;
	}

	public ArrayList AvailableOccupationItemArray()
	{
		return null;
	}

	public ArrayList AvailableSpecialCareerItemArray()
	{
		return null;
	}

	public ArrayList AvailableItemPromotionItemArray()
	{
		return null;
	}

	private static int NumberOfUniqueMajorDegrees()
	{
		return 0;
	}

	private static bool AvailableForGoldenDiploma()
	{
		return false;
	}

	private static bool AvailableForGoldenResume()
	{
		return false;
	}

	private static bool AvailableForFightMode()
	{
		return false;
	}

	public ArrayList AvailableSpecialCareerPromotionItemArray()
	{
		return null;
	}

	public static List StreetListItemArray(bool currentlyAHustler)
	{
		return null;
	}

	public ArrayList AvailableEducationItemArray()
	{
		return null;
	}

	public List CreateAvailableJobListWithPlaceholder(bool usePlaceholder)
	{
		return null;
	}

	public List CreateAvailableJobListForGoldenResume(bool isCalledFromRegularJobsListItem = false)
	{
		return null;
	}

	public ArrayList AvailableMilitaryBranchItemArray()
	{
		return null;
	}

	public List CreateAvailablePartTimeJobListWithPlaceholder(bool usePlaceholder)
	{
		return null;
	}

	public ArrayList AvailableFreelanceItemArray()
	{
		return null;
	}

	public ArrayList AvailablePrisonJobItemArray()
	{
		return null;
	}

	public ArrayList AvailablePrisonGangItemArray()
	{
		return null;
	}

	public ArrayList AvailablePrisonYardItemArray()
	{
		return null;
	}

	public ArrayList AvailableClassmateItemArray()
	{
		return null;
	}

	public ArrayList AvailableFacultyItemArray()
	{
		return null;
	}

	public ArrayList AvailableSchoolActivityItemArray()
	{
		return null;
	}

	public ArrayList AvailableSchoolCliqueItemArray()
	{
		return null;
	}

	public ArrayList AvailableSchoolFratItemArray()
	{
		return null;
	}

	public ArrayList AvailableCoworkerItemArray()
	{
		return null;
	}

	public ArrayList AvailableBandmateItemArray()
	{
		return null;
	}

	public ArrayList AvailableMobsterItemArray()
	{
		return null;
	}

	public ArrayList AvailableCostarItemArray()
	{
		return null;
	}

	public ArrayList PoliticianItemArray()
	{
		return null;
	}

	public ArrayList AthleteItemArray()
	{
		return null;
	}

	public ArrayList SportsStatsItemArrayForSport(int sport)
	{
		return null;
	}

	public ArrayList MusicianItemArray()
	{
		return null;
	}

	public ArrayList MusicianDiscographyItemArray()
	{
		return null;
	}

	public ArrayList MafiaItemArray()
	{
		return null;
	}

	public ArrayList ActorItemArray()
	{
		return null;
	}

	public ArrayList ActorRoleArray(bool placeholderList = false)
	{
		return null;
	}

	public ArrayList ActorFilmographyItemArray()
	{
		return null;
	}

	public ArrayList StreetHustlerItemArray()
	{
		return null;
	}

	public ArrayList AvailableHustlesArray()
	{
		return null;
	}

	public ArrayList AvailableScamList(SimStreetPerson aPerson = null)
	{
		return null;
	}

	public ListItem CreateTierScamItem(SimStreetPerson aPerson, DataHustlerScamType scamType, int scamTier)
	{
		return null;
	}

	public ArrayList GetCrimeList()
	{
		return null;
	}

	public ArrayList GetLoveList()
	{
		return null;
	}

	public ArrayList AvailableStreetPersonArray()
	{
		return null;
	}

	public ArrayList BusinessGatewayList()
	{
		return null;
	}

	public ArrayList BusinessIndustryList()
	{
		return null;
	}

	public ArrayList BusinessCareerItemArray()
	{
		return null;
	}

	public ArrayList CompaniesForSaleList(bool placeholderList = false)
	{
		return null;
	}

	public ArrayList BusinessItemArray()
	{
		return null;
	}

	public ArrayList ProductItemArray(SimSpecialJobBusiness job, SimProduct product)
	{
		return null;
	}

	public ArrayList BusinessEmployeeList(SimSpecialJobBusiness specialJob)
	{
		return null;
	}

	public ArrayList EmployeeRelationshipList(SimEmployee anEmployee)
	{
		return null;
	}

	public ArrayList AnnualReportList(SimSpecialJobBusiness specialJob)
	{
		return null;
	}

	public ArrayList PreviousCEOList(SimSpecialJobBusiness specialJob, IEnumerable<SimAnnualReport> olderReports)
	{
		return null;
	}

	public ArrayList HistoricalCEOReportList(SimSpecialJobBusiness specialJob, IEnumerable<SimAnnualReport> reports)
	{
		return null;
	}

	public ArrayList CarItemArrayForCar(SimCar car)
	{
		return null;
	}

	public ArrayList HouseItemArrayForHouse(SimHouse house)
	{
		return null;
	}

	public ArrayList AvailableGhostItemArrayForHouse(SimHouse house)
	{
		return null;
	}

	public ArrayList HeirloomItemArrayForHeirloom(SimHeirloom heirloom)
	{
		return null;
	}

	public ArrayList AircraftItemArrayForAircraft(SimAircraft aircraft)
	{
		return null;
	}

	public ArrayList WatercraftItemArrayForWatercraft(SimWatercraft watercraft)
	{
		return null;
	}

	public ArrayList RecreationalVehicleItemArrayForRecreationalVehicle(SimRecreationalVehicle recreationalVehicle)
	{
		return null;
	}

	public ArrayList JewelItemArrayForJewel(SimJewel jewel)
	{
		return null;
	}

	public ArrayList ClosetItemArrayForModelGift(SimModelGift gift)
	{
		return null;
	}

	public ArrayList InstrumentItemArrayForInstrument(SimMusicInstrumentPhysical instrument)
	{
		return null;
	}

	public ArrayList InvestmentsItemArray()
	{
		return null;
	}

	public ArrayList CryptocurrencyItemArray()
	{
		return null;
	}

	public ArrayList FundsMarketItemArray()
	{
		return null;
	}

	public ArrayList PennyStockMarketItemArray()
	{
		return null;
	}

	public ArrayList BondMarketItemArray()
	{
		return null;
	}

	public ArrayList StockMarketItemArray()
	{
		return null;
	}

	public ArrayList PortfolioItemArray()
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForParent(SimParent parent)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForSibling(SimSibling sibling)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForLoverButOverrideProfileWithListItem(SimLover lover, ListItem overrideProfileItem)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForChild(SimChild child)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForGrandchild(SimChild child)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForNieceNephew(SimChild child)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForPet(SimPet pet)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForExLover(SimLover exLover)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForClassmate(SimClassmate classmate)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForTeacher(SimTeacher teacher)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForCoworker(SimCoworker coworker)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForFriend(SimPerson friend)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForBandmate(SimBandmate bandmate)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForMobster(SimMobster mobster)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForAgent(SimAgent agent)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForCostar(SimCostar costar)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForStreetPerson(SimStreetPerson streetPerson)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForFinancialAdvisor(SimFinancialAdvisor advisor)
	{
		return null;
	}

	public ArrayList RelationshipItemArrayForTenant(SimTenant tenant)
	{
		return null;
	}

	public string RelationshipStringForDeathYearsAgo(int yearsAgo)
	{
		return null;
	}

	public ArrayList SocialMediaItemArray()
	{
		return null;
	}

	public ArrayList SocialMediaFacebookItemArray()
	{
		return null;
	}

	public ArrayList SocialMediaInstagramItemArray()
	{
		return null;
	}

	public ArrayList SocialMediaTikTokItemArray()
	{
		return null;
	}

	public ArrayList SocialMediaTwitchItemArray()
	{
		return null;
	}

	private ArrayList SocialMediaOnlyFansItemArray()
	{
		return null;
	}

	public ArrayList SocialMediaTwitterItemArray()
	{
		return null;
	}

	public ArrayList SocialMediaYouTubeItemArray()
	{
		return null;
	}

	public ArrayList FamilyPlotItemArray()
	{
		return null;
	}

	public ArrayList AccessoriesItemArray()
	{
		return null;
	}

	public ArrayList AdoptionCandidateItemArray()
	{
		return null;
	}

	public ArrayList SurrogateItemArray()
	{
		return null;
	}

	public string EmojiStringForPoliticalApproval()
	{
		return null;
	}

	public bool ShowMusicTheme()
	{
		return false;
	}

	public float GetMusicPopularity()
	{
		return 0f;
	}

	public int GetMusicRole()
	{
		return 0;
	}

	public bool ShowMafiaTheme()
	{
		return false;
	}

	public bool ShowCultTheme()
	{
		return false;
	}

	public float GetActorReputation()
	{
		return 0f;
	}

	public bool ShowActorTheme()
	{
		return false;
	}

	public bool ShowProSportsTheme()
	{
		return false;
	}

	public float GetProSportsGreatness()
	{
		return 0f;
	}

	public int GetProSportsSport()
	{
		return 0;
	}

	public bool ShowBusinessTheme()
	{
		return false;
	}

	public bool ShowPoliticalTheme()
	{
		return false;
	}

	public float GetPoliticalApproval()
	{
		return 0f;
	}

	private ArrayList GetAstronautGatewayListItems()
	{
		return null;
	}

	private ArrayList GetAstronautStartListOptions(SimSpecialJobAstronaut specialJob)
	{
		return null;
	}

	public ArrayList GetAstronautListItems()
	{
		return null;
	}

	private static ArrayList ProbeMenuItems(SimSpecialJobAstronaut specialJob)
	{
		return null;
	}

	private static ArrayList DiscoveriesMenuItems(SimSpecialJobAstronaut specialJob)
	{
		return null;
	}

	private static (ArrayList, bool) DiscoveriesMenuItemsProfileOnly(List<SimSpecialJobAstronaut> specialJobList)
	{
		return default;
	}

	private static ArrayList PublicationsMenuItems(SimSpecialJobAstronaut special)
	{
		return null;
	}

	private static ArrayList CelestialBodiesList(SimSpecialJobAstronaut specialJob, bool showPublish)
	{
		return null;
	}

	private static ArrayList AlienTransmissionMenuItems(SimSpecialJobAstronaut specialJob, bool showPublish)
	{
		return null;
	}

	private static ArrayList ArtifactsItemMenuItems(SimSpecialJobAstronaut special, SimArtifact artifact)
	{
		return null;
	}

	private static ArrayList ArtifactsMenuItemsProfiles(SimSpecialJobAstronaut specialJob, int artifactOrigin)
	{
		return null;
	}

	private static ArrayList ArtifactsMenuItems(SimSpecialJobAstronaut specialJob, int artifactOrigin)
	{
		return null;
	}

	private static ArrayList MissionsMenuItems(SimSpecialJobAstronaut specialJob)
	{
		return null;
	}

	private ArrayList AvailableSpaceTrainingClassMatesItemArray(SimSpecialJobAstronaut job, bool fullAstronaut)
	{
		return null;
	}

	private ArrayList RelationshipItemArrayForAstronautClassmate(SimAstronaut classmate)
	{
		return null;
	}

	private ArrayList RelationshipItemsAstronautClassMate(SimAstronaut classmate)
	{
		return null;
	}

	public bool ShowAstronautTheme()
	{
		return false;
	}

	public ArrayList BelongingsMenuItemsArray(bool placeholderList = false)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForAntiquesInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForIAPItemsInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForArtInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForGemstoneInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForAutoInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForFashionInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForHistoricalRelicsInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForTaxidermiesInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForWeaponsInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForSportsMemorabiliaInteraction(SimAuctionItem item)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForHeirlooms()
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForJewels()
	{
		return null;
	}

	private ArrayList ItemArrayForCloset()
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForSimAuction(int auctionItemType)
	{
		return null;
	}

	private ArrayList BelongingsItemArrayForSimIAPTypeAuction()
	{
		return null;
	}

	private ArrayList BelongingItemArrayListVehicles()
	{
		return null;
	}

	private ArrayList BlackMarketItemArray()
	{
		return null;
	}

	public List GetPrisonCommuneList()
	{
		return null;
	}

	public List GetOwnedCommuneList()
	{
		return null;
	}

	private List GetCommunardsList()
	{
		return null;
	}

	private List GetCommunardListForCommunard(SimCommunard communard)
	{
		return null;
	}

	private List GetCeremoniesList()
	{
		return null;
	}

	private List GetStructuresList(SimCult cult)
	{
		return null;
	}

	private List GetPlotList()
	{
		return null;
	}

	private List GetAvailablePlotsList()
	{
		return null;
	}

	public ArrayList GetTotemsList(bool useImpacts)
	{
		return null;
	}

	public ArrayList ModelGatewayList()
	{
		return null;
	}

	public List GetPortfolioList()
	{
		return null;
	}

	public List GetCastingCallsList()
	{
		return null;
	}

	private List GetLocalGigsList()
	{
		return null;
	}

	public List GetEliteGigsList()
	{
		return null;
	}

	public List GetModelJobList()
	{
		return null;
	}

	public List GetClientsList()
	{
		return null;
	}

	public List GetColleaguesList()
	{
		return null;
	}

	public List GetBookingAgentRelationshipListForAgent(SimBookingAgent agent)
	{
		return null;
	}

	public List GetModelRelationshipListForModel(SimModel model)
	{
		return null;
	}

	public List GetSpyAgencyList()
	{
		return null;
	}

	private List GetAvailableFrontsList()
	{
		return null;
	}

	private List GetAvailableRelocationFrontsList()
	{
		return null;
	}

	public List GetSecurityFeaturesList()
	{
		return null;
	}

	private List GetGadgetsList()
	{
		return null;
	}

	public List GetGadgetListForGadget(SimSecretAgentGadget gadget)
	{
		return null;
	}

	private List GetSpyAgencyAgentsList()
	{
		return null;
	}

	private List GetSpyAgencyMissionsList()
	{
		return null;
	}

	public List GetSecretAgentPastMissions()
	{
		return null;
	}

	public List GetRivalAgencyDatabaseList(SimRivalSpyAgency rivalAgency)
	{
		return null;
	}

	private List GetRivalAgencysAgentList(SimRivalSpyAgency rivalAgency)
	{
		return null;
	}

	private List GetRivalAgencysMissionsList(SimRivalSpyAgency rivalAgency)
	{
		return null;
	}

	private List GetSecretAgentListForAgent(SimSecretAgent agent)
	{
		return null;
	}

	public List GetHackingMissionDatabaseDriveList(SimSecretAgentHackingMission mission)
	{
		return null;
	}

	private List GetHackingMissionDatabaseFilesList(SimSecretAgentHackingMission mission, string drive, List<string> listExistingColors)
	{
		return null;
	}

	public ArrayList GetZooList(bool placeholderList = false)
	{
		return null;
	}

	public ArrayList GetAvailableZoosList()
	{
		return null;
	}

	private List GetZooHabitatsList()
	{
		return null;
	}

	private List GetZooAnimalsList()
	{
		return null;
	}

	private List GetZooAnimalsListForBreedInHabitat(string breed, List<SimZooAnimal> group)
	{
		return null;
	}

	private List GetZooSpecificHabitatList(SimZooHabitat habitat)
	{
		return null;
	}

	private List GetZooAttractionsList()
	{
		return null;
	}

	private List GetZooEmployeesList()
	{
		return null;
	}

	private List GetZooEmployeeRelationshipList(SimZooEmployee employee)
	{
		return null;
	}

	private List GetZooGiftShopList()
	{
		return null;
	}

	private List GetZooSpecificAnimalList(SimZooAnimal animal)
	{
		return null;
	}

	public List GetZooTradingPostList()
	{
		return null;
	}

	public ArrayList GetDealerGatewayList(bool placeholderList = false)
	{
		return null;
	}

	public ArrayList GetDealerJobList()
	{
		return null;
	}

	public ArrayList GetDealerCartelMembersList()
	{
		return null;
	}

	public ArrayList GetDealerCartelDrugsList()
	{
		return null;
	}

	public ArrayList GetDealerCartelListForDrug(SimDealerDrug drug)
	{
		return null;
	}

	public ArrayList GetDealerCartelManufactureList()
	{
		return null;
	}

	public ArrayList GetDealerCartelListForStreet(SimDealerStreet street)
	{
		return null;
	}

	public ArrayList GetDealerCartelMemberRelationshipList(SimDealerCartelMember member)
	{
		return null;
	}

	public ArrayList GetDealerStreetPersonRelationshipList(SimDealerStreetPerson person)
	{
		return null;
	}

	public ArrayList GetDealerCartelSupplyRuns()
	{
		return null;
	}

	public bool ShowDealerTheme()
	{
		return false;
	}

	public List GetAuctionHousesList()
	{
		return null;
	}

	private List GetCasinoGamesList()
	{
		return null;
	}

	public List GetCasinoTypesList()
	{
		return null;
	}

	public List GetCasinoList()
	{
		return null;
	}

	private List GetCasinoAmenitiesList()
	{
		return null;
	}

	private List GetCasinoEntertainmentList()
	{
		return null;
	}

	public List GetCasinoGameRoomsList()
	{
		return null;
	}

	public List GetCasinoGameRoomList(SimCasinoGameRoom gameRoom)
	{
		return null;
	}

	private List GetCasinoGamblersList()
	{
		return null;
	}

	private List GetCasinoCelebritiesList()
	{
		return null;
	}

	private List GetCasinoGamblerRelationshipList(SimCasinoGambler gambler, CASINO_AUDIENCE_TYPE audience)
	{
		return null;
	}

	private List GetCasinoCelebrityRelationshipList(SimCasinoCelebrity celebrity)
	{
		return null;
	}

	public List GetCasinoEntertainmentActList(SimCasinoEntertainmentAct act)
	{
		return null;
	}

	private List GetHeroCasinoGamesList()
	{
		return null;
	}

	public List GetCasinoTournamentGamblersList(SimCasinoTournament tournament)
	{
		return null;
	}

	private ArrayList ItemArrayForLuckyCharms()
	{
		return null;
	}

	public ArrayList LuckyCharmsItemArrayForCasinoCollectible(SimCasinoCollectible collectible)
	{
		return null;
	}

	private List GetLuxuryList()
	{
		return null;
	}

	private List GetLuxurySpaList()
	{
		return null;
	}

	private List GetLuxurySecretSocietiesList()
	{
		return null;
	}

	private List GetLuxuryIslandRealtorList()
	{
		return null;
	}

	private List GetLuxuryAircraftDealershipList()
	{
		return null;
	}

	private List GetLuxuryWatercraftDealershipList()
	{
		return null;
	}

	private List GetLuxuryCarDealershipList()
	{
		return null;
	}

	private List GetLuxuryUpscaleJewelersList()
	{
		return null;
	}

	public List GetLuxuryAssetsList()
	{
		return null;
	}

	public List GetLuxuryOwnedIslandsList()
	{
		return null;
	}

	public List GetLuxuryIslandListForIsland(SimLuxuryIsland island)
	{
		return null;
	}

	public List GetLuxuryOwnedAircraftList()
	{
		return null;
	}

	public List GetLuxuryOwnedAutomobilesList()
	{
		return null;
	}

	public List GetLuxuryOwnedWatercraftList()
	{
		return null;
	}

	public List GetLuxuryOwnedJewelryList()
	{
		return null;
	}

	private ArrayList BelongingsItemArrayListForLuxuryJewels()
	{
		return null;
	}

	private ArrayList BelongingItemArrayListForLuxuryVehicles()
	{
		return null;
	}

	private ArrayList GetBelongingsLuxuryJewelItemsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsLuxuryVehiclesToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	public ArrayList GetBlackMarketMuseumList(bool placeholderList = false)
	{
		return null;
	}

	public ArrayList GetBlackMarketMuseumWingList(SimMuseum museum, int floor, int wing)
	{
		return null;
	}

	private ArrayList GetMuseumDisplayItemInteraction(SimDisplayItem displayItem, int floor, int wing, int idx)
	{
		return null;
	}

	private ArrayList GetMuseumDisplayPetInteractionList(SimDisplayItem displayItem, int floor, int wing, int idx)
	{
		return null;
	}

	private ArrayList GetMuseumAddDisplayItemList(SimMuseumWing wing, SimDisplayItem replaceItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsMarketItemsToAddDisplay(SimMuseumWing wing, MARKET_ITEM_TYPE itemType, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsVehiclesToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsHeirloomsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsIAPItemsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsJewelItemsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsPetItemsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsArtifactItemsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsClosetItemsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private ArrayList GetBelongingsLuckyCharmsItemsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	private void AddIapItemsToBelongingsIfNeeded()
	{
	}

	private bool HasCars(Life life, bool isLuxury)
	{
		return false;
	}

	private bool HasAircraft(Life life, bool isLuxury)
	{
		return false;
	}

	private bool HasWatercraft(Life life, bool isLuxury)
	{
		return false;
	}

	private static ListItem CreateSleepoverListItem(SimPerson person)
	{
		return null;
	}

	private static bool CanDoSleepOverWithPerson(SimPerson person)
	{
		return false;
	}

	private ListItem GetMovieDirectorItemFromT4(ArrayList listItems)
	{
		return null;
	}

	private List BuildMovieDirectorHub()
	{
		return null;
	}

	private static bool CanBuildMovieDirectorHub(GameManager gameManager)
	{
		return false;
	}

	public ListItem GetOutdoorLifestyleFishingTreasures()
	{
		return null;
	}

	private ListItem GetOutdoorLifestyleListItem()
	{
		return null;
	}

	private List GetOutdoorLifestyleList()
	{
		return null;
	}

	public List GetWildlifeHandbookList()
	{
		return null;
	}

	public static List GetOutdoorHandbookTypeEntriesList(List<SimOutdoorHandbookEntry> entryList, OUTDOOR_HANDBOOK_ENTRY_TYPE type)
	{
		return null;
	}

	private ArrayList ItemArrayForOutdoorFishingTreasures()
	{
		return null;
	}

	private ArrayList OutdoorFishingTreasuresItemArrayForOutdoorFishingTreasure(SimOutdoorFishingTreasure fishingTreasure)
	{
		return null;
	}

	public ArrayList GetBelongingsOutdoorFishingTreasuresItemsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	public List GetFishingList()
	{
		return null;
	}

	private ArrayList GetMountedFishList()
	{
		return null;
	}

	public ArrayList MountedFishItemArrayForFish(SimOutdoorMountedFish fish)
	{
		return null;
	}

	public List GetRaceCarGarageList(bool includeRacing = true)
	{
		return null;
	}

	public List GetRaceCarDealershipList()
	{
		return null;
	}

	public List GetRacingHubList(bool excludeGarageItem = false)
	{
		return null;
	}

	private List GetRacingRacesList()
	{
		return null;
	}

	private List GetRacesListForType(string title, RACING_TRACK_CATEGORY category)
	{
		return null;
	}

	private List GetRacingDriversList()
	{
		return null;
	}

	private List GetRacingTimeTrialsList()
	{
		return null;
	}

	private List GetRacingTrackRecordList()
	{
		return null;
	}

	private List GetRacingTrophyCaseList()
	{
		return null;
	}

	private List GetRacingDriverInteractionList(SimRaceCarDriver driver)
	{
		return null;
	}

	private List GetGaragedItemArrayForRaceCar(SimRaceCar raceCar)
	{
		return null;
	}

	private List GetGarageEmptyBayCarList()
	{
		return null;
	}

	private bool HasCarsAvailableToBeGaraged(Life life, bool isLuxury)
	{
		return false;
	}

	private static ListItem GetRacingGarageItem()
	{
		return null;
	}

	private static ListItem GetRacingItemFromT4()
	{
		return null;
	}

	private static ListItem GetRacingDealerShoppingItem()
	{
		return null;
	}

	private static ListItem GetRacingGarageForCarListItem(SimCar car)
	{
		return null;
	}

	private static ListItem GetBelongingsInteractionItem(SimAuctionItem item)
	{
		return null;
	}

	public ArrayList ItemArrayForRacingJackets()
	{
		return null;
	}

	public ArrayList RacingJacketsItemArrayForRacingCollectible(SimRacingCollectible collectible)
	{
		return null;
	}

	public ArrayList GetBelongingsRacingJacketsItemsToAddDisplay(SimMuseumWing wing, SimDisplayItem simDisplayItem, int slotIndex)
	{
		return null;
	}

	public ListItem GetSoundCloudAvailableListItem()
	{
		return null;
	}

	public List GetSoundCloudSubList()
	{
		return null;
	}

	public List GetSoundCloudFollowersList()
	{
		return null;
	}

	public List GetSoundCloudTrackList()
	{
		return null;
	}

	public bool CanShareValentinesHero()
	{
		return false;
	}

	public ListItem GetValentineShareListItem()
	{
		return null;
	}

	public CustomTheme GetThemeForPerson(SimPerson person)
	{
		return null;
	}

	public bool CanCelebrateAnniversary()
	{
		return false;
	}

	public ListItem GetAnniversaryActivity()
	{
		return null;
	}

	private void AddVampireModeListItem(ArrayList listItems)
	{
	}

	private List GetProgenyVampiresList()
	{
		return null;
	}

	private ArrayList GetVampireRelationshipForMaker(SimPerson maker)
	{
		return null;
	}

	private ArrayList GetVampireRelationshipListForMakerWithNoRelationship(SimPerson maker)
	{
		return null;
	}

	private ArrayList GetVampireModeList()
	{
		return null;
	}

	private static ArrayList GetVampireRelationshipListForFamiliar(SimVampireFamiliar familiar)
	{
		return null;
	}

	public List GetVampireCouncilList()
	{
		return null;
	}

	public List GetVampireCouncilMembersList(List<SimVampireCouncilMember> councilMembersList)
	{
		return null;
	}

	public List GetVampireRelationshipListForCouncilMember(SimVampireCouncilMember member)
	{
		return null;
	}

	private static void AddVampireConversionRequestViaPerson(SimPerson person, ArrayList list)
	{
	}

	private static void AddConvertHumanNPCToVampire(SimPerson person, ArrayList list)
	{
	}

	private static void AddVampireDrinkBloodOfPerson(SimPerson person, ArrayList list)
	{
	}

	private static void AddVampireHypnotizePerson(SimPerson person, ArrayList list, string descriptionString = null)
	{
	}

	private static void AddVampireMakerSacrifice(SimPerson person, ArrayList list)
	{
	}

	public static void AddVampireHuntWithPerson(SimPerson person, ArrayList list)
	{
	}

	private static bool IsVampireEntryEnabled()
	{
		return false;
	}

	private static ListItem GetVampireEntryListItem()
	{
		return null;
	}

	private static ListItem GetVampirePurchaseInfoListItem()
	{
		return null;
	}

	internal static (string, string) _003CGetHackingMissionDatabaseFilesList_003Eg__GenerateUniqueFileAndColor_007C214_0([Optional][DefaultParameterValue(false)] bool useMissionFileType, ref _003C_003Ec__DisplayClass214_0 P_1)
	{
		return default;
	}

	internal static bool _003CGetOutdoorLifestyleList_003Eg__HasOffroadVehicle_007C302_0()
	{
		return false;
	}

	internal static string _003CGetWildlifeHandbookList_003Eg__GetCountString_007C303_0(OUTDOOR_HANDBOOK_ENTRY_TYPE type, ref _003C_003Ec__DisplayClass303_0 P_1)
	{
		return null;
	}

	internal static void _003CGetRacingTrackRecordList_003Eg__AddTracksToList_007C318_0(RACING_TRACK_CATEGORY category, ref _003C_003Ec__DisplayClass318_0 P_1)
	{
	}

	internal static void _003CGetRacingTrophyCaseList_003Eg__CreateListItems_007C319_0(List<SimRacingCup> trophyCaseList, string dividerString, ref _003C_003Ec__DisplayClass319_0 P_2)
	{
	}
}
