using System;
using System.Globalization;
using System.Threading;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

static class Program
{
	static readonly Dictionary<string, Xbox360Button> ButtonMap = new()
	{
		{ "a", Xbox360Button.A },
		{ "b", Xbox360Button.B },
		{ "x", Xbox360Button.X },
		{ "y", Xbox360Button.Y },
		{ "start", Xbox360Button.Start },
		{ "back", Xbox360Button.Back },
		{ "guide", Xbox360Button.Guide },
		{ "lb", Xbox360Button.LeftShoulder },
		{ "rb", Xbox360Button.RightShoulder },
		{ "ls", Xbox360Button.LeftThumb },
		{ "rs", Xbox360Button.RightThumb },
		{ "up", Xbox360Button.Up },
		{ "down", Xbox360Button.Down },
		{ "left", Xbox360Button.Left },
		{ "right", Xbox360Button.Right }
	};

	static void Main(string[] args)
	{
#if DEBUG
		//: log args
		File.AppendAllText("log.txt",Environment.NewLine + "args:" + string.Join(" ", args));
#endif
		try
		{
			if (args.Length == 0)
			{
				File.AppendAllText("log.txt", Environment.NewLine + $"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} Error: no arguments");
				return;
			}
			//: default values
			int duration = 100;
			int pause = 0;

			//: create client
			using var client = new ViGEmClient();
			var controller = client.CreateXbox360Controller();
			controller.Connect();

			//: Get duration arg
			var durationIndex = args.IndexOfAny("-d", "--duration", "-t", "--time");
			if (durationIndex >= 0 && durationIndex + 1 < args.Length) 
				int.TryParse(args[durationIndex + 1], out duration);

			//: Get pause arg
			var pauseIndex = args.IndexOfAny("-p", "--pause");
			if (pauseIndex >= 0 && pauseIndex + 1 < args.Length) 
				int.TryParse(args[pauseIndex + 1], out pause);

			//: Simultaniusly press all buttons in args, then release them
			Thread.Sleep(pause);
			foreach (var buttonName in args)
			{
				if (!ButtonMap.TryGetValue(buttonName, out Xbox360Button button)) continue; //: button not found
				controller.SetButtonState(button, true); //: Pressing a button
			}
			foreach (var buttonName in args)
			{
				if (!ButtonMap.TryGetValue(buttonName, out Xbox360Button button)) continue; //: button not found
				controller.SetButtonState(button, false); //: Releasing a button
			}
			Thread.Sleep(duration);

			controller.Disconnect();
		}
		catch (Exception e)
		{
			File.AppendAllText("log.txt", Environment.NewLine + $"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} Error: {e.Message}");
		}
	}

	static int IndexOfAny(this string[] array, params string[] values)
	{
		for (int i = 0; i < array.Length; i++) {
			if (Array.IndexOf(values, array[i]) >= 0)
				return i;
		}
		return -1;
	}
}