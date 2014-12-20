using AIGames.Warlight2.Cartography;
using AIGames.Warlight2.Instructions;
using System;
using System.IO;

namespace AIGames.Warlight2.Platform
{
	public abstract class Runner
	{
		protected Runner() : this(Console.In, Console.Out) { }
		protected Runner(TextReader reader, TextWriter writer)
		{
			this.Reader = reader;
			this.Writer = new InstructionWriter(writer);
			this.Map = new Map();
		}

		public InstructionWriter Writer { get; protected set; }
		public TextReader Reader { get; protected set; }
		public int Round { get; protected set; }

		public Map Map { get; protected set; }

		public abstract void SettingsBotName(SettingsBotNameInstruction instruction);
		public abstract void SettingsStartingArmies(SettingsStartingArmiesInstruction instruction);

		public abstract SelectPickStartingRegionsInstruction PickStartingRegions(GetPickStartingRegionsInstruction instruction);
		public abstract Instruction PlaceArmies(GoPlaceArmiesInstruction instruction);
		public abstract Instruction AttackTransfer(GoAttackTransferInstruction instruction);

		public abstract void UpdateMap(UpdateMapInstruction instruction);
		public abstract void OpponentMoves(OpponentMovesInstruction instruction);

		public void Run()
		{
			string line;
			while ((line = this.Reader.ReadLine()) != null)
			{
				var tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				// if empty line, skip.
				if (tokens.Length == 0) { continue; }
				line = line.Trim();

#if !DEBUG
				try
				{
#endif
					switch (tokens[0])
					{
						case "opponent_moves": OpponentMoves(OpponentMovesInstruction.Detokenize(tokens)); break;
						case "pick_starting_regions": ParsePickStartingRegions(line, tokens); break;
						case "settings": ParseSettings(line, tokens); break;
						case "update_map": UpdateMap(UpdateMapInstruction.Detokenize(tokens)); break;
						case "go": ParseGo(line, tokens); break;
						case "setup_map": ParseSetupMap(line, tokens); break;
						// skip these tokens.
						case "Round":
						case "null":
							break;
						default: ParseByTokenOne(line, tokens); break;
					}
#if !DEBUG
				}
				catch (Exception x)
				{
					Console.Error.WriteLine(line);
					Console.Error.WriteLine(x);
				}
#endif
			}
		}

		private void ParseSetupMap(string line, string[] tokens)
		{
			switch (tokens[1])
			{
				case "super_regions": 
					this.Map.Setup(SetupSuperRegionsInstruction.Detokenize(tokens)); break;

				case "regions":
					this.Map.Setup(SetupRegionsInstruction.Detokenize(tokens)); break;

				case "neighbors":
					this.Map.Setup(SetupNeighborInstruction.Detokenize(tokens)); break;
			}
		}

		private static void ParseByTokenOne(String line, string[] tokens)
		{
			if (line == "No moves") { return; }

			// we can have that output while reading a file.
			if (SelectPickStartingRegionsInstruction.Pattern.IsMatch(line)) { return; }

			switch (tokens[1])
			{
				case "attack/transfer":
				case "place_armies": break;
				default:
					throw new NotSupportedException(line);
			}
		}

		private void ParseGo(String line, string[] tokens)
		{
			switch (tokens[1])
			{
				case "place_armies":
					this.Round++;
					var placeArmies = GoPlaceArmiesInstruction.Detokenize(tokens);
					this.Writer.Add(PlaceArmies(placeArmies));
					break;
				case "attack/transfer":
					var attackTransfer = GoAttackTransferInstruction.Detokenize(tokens);
					this.Writer.Add(AttackTransfer(attackTransfer));
					break;
				default:
					throw new NotSupportedException(line);
			}
		}
	   
		private void ParsePickStartingRegions(String line, string[] tokens)
		{
			var instruction = GetPickStartingRegionsInstruction.Detokenize(tokens);
			SelectPickStartingRegionsInstruction answer = PickStartingRegions(instruction);
			this.Writer.Add(answer);
		}

		private void ParseSettings(String line, string[] tokens)
		{
			if (SettingsBotNameInstruction.Pattern.IsMatch(line))
			{
				var settingsNameInstruction = SettingsBotNameInstruction.Parse(line);
				SettingsBotName(settingsNameInstruction);
			}
			else
			{
				switch (tokens[1])
				{
					case "starting_armies":
						var settingsStartingArmiesInstruction = SettingsStartingArmiesInstruction.Detokenize(tokens);
						SettingsStartingArmies(settingsStartingArmiesInstruction);
						break;
					default:
						throw new NotSupportedException(line);
				}
			}
		}
	}
}
