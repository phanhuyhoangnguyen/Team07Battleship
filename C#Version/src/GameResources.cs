
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// Loading all of the resources needed and set it to variables to be used on the game.
/// </summary>
/// <remarks>
/// Data is loaded from determined directory inside the game directory before the game start.
/// </remarks>

public static class GameResources
{

	private static void LoadFonts()
	{
		NewFont("ArialLarge", "arial.ttf", 80);
		NewFont("Courier", "cour.ttf", 14);
		NewFont("CourierSmall", "cour.ttf", 8);
		NewFont("Menu", "ffaccess.ttf", 8);
	}

	private static void LoadImages()
	{
		//Backgrounds
		NewImage("Menu", "main_page.jpg");
		NewImage("Discovery", "discover.jpg");
		NewImage("Deploy", "deploy.jpg");

		//Deployment
		NewImage("LeftRightButton", "deploy_dir_button_horiz.png");
		NewImage("UpDownButton", "deploy_dir_button_vert.png");
		NewImage("SelectedShip", "deploy_button_hl.png");
		NewImage("PlayButton", "deploy_play_button.png");
		NewImage("RandomButton", "deploy_randomize_button.png");

		//Ships
		int i = 0;
		for (i = 1; i <= 5; i++) {
			NewImage("ShipLR" + i, "ship_deploy_horiz_" + i + ".png");
			NewImage("ShipUD" + i, "ship_deploy_vert_" + i + ".png");
		}

		//Explosions
		NewImage("Explosion", "explosion.png");
		NewImage("Splash", "splash.png");

	}

	private static void LoadSounds()
	{
		NewSound("Error", "error.wav");
		NewSound("Hit", "hit.wav");
		NewSound("Sink", "sink.wav");
		NewSound("Siren", "siren.wav");
		NewSound("Miss", "watershot.wav");
		NewSound("Winner", "winner.wav");
		NewSound("Lose", "lose.wav");
	}

	private static void LoadMusic()
	{
		NewMusic("Background", "horrordrone.mp3");
	}

	/// <summary>
	/// Gets a Font Loaded in the Resources
	/// </summary>
	/// <param name="font">Name of Font</param>
	/// <returns>The Font Loaded with this Name</returns>

	public static Font GameFont(string font)
	{
		return _Fonts [font];
	}

	/// <summary>
	/// Gets an Image loaded in the Resources
	/// </summary>
	/// <param name="image">Name of image</param>
	/// <returns>The image loaded with this name</returns>

	public static Bitmap GameImage(string image)
	{
		return _Images [image];
	}

	/// <summary>
	/// Gets an sound loaded in the Resources
	/// </summary>
	/// <param name="sound">Name of sound</param>
	/// <returns>The sound with this name</returns>

	public static SoundEffect GameSound(string sound)
	{
		return _Sounds [sound];
	}

	/// <summary>
	/// Gets the music loaded in the Resources
	/// </summary>
	/// <param name="music">Name of music</param>
	/// <returns>The music with this name</returns>

	public static Music GameMusic(string music)
	{
		return _Music [music];
	}

	private static Dictionary<string, Bitmap> _Images = new Dictionary<string, Bitmap>();
	private static Dictionary<string, Font> _Fonts = new Dictionary<string, Font>();
	private static Dictionary<string, SoundEffect> _Sounds = new Dictionary<string, SoundEffect>();

	private static Dictionary<string, Music> _Music = new Dictionary<string, Music>();
	private static Bitmap _Background;
	private static Bitmap _Animation;
	private static Bitmap _LoaderFull;
	private static Bitmap _LoaderEmpty;
	private static Font _LoadingFont;

	private static SoundEffect _StartSound;
	/// <summary>
	/// The Resources Class stores all of the Games Media Resources, such as Images, Fonts
	/// Sounds, Music.
	/// </summary>

	public static void LoadResources()
	{
		int width = 0;
		int height = 0;

		width = 800;
		height = 600;

		SwinGame.ChangeScreenSize(800, 600);

		ShowLoadingScreen();

		ShowMessage("Loading fonts...", 0);
		LoadFonts();
		SwinGame.Delay(100);

		ShowMessage("Loading images...", 1);
		LoadImages();
		SwinGame.Delay(100);

		ShowMessage("Loading sounds...", 2);
		LoadSounds();
		SwinGame.Delay(100);

		ShowMessage("Loading music...", 3);
		LoadMusic();
		SwinGame.Delay(100);

		SwinGame.Delay(100);
		ShowMessage("Game loaded...", 5);
		SwinGame.Delay(100);
		EndLoadingScreen(width, height);
	}
  
    /// <summary>
	/// To show the loading screen animation when loading the resources.
	/// </summary>

	private static void ShowLoadingScreen()
	{
		_Background = SwinGame.LoadBitmap(SwinGame.PathToResource("SplashBack.png", ResourceKind.BitmapResource));
		SwinGame.DrawBitmap(_Background, 0, 0);
		SwinGame.RefreshScreen();
		SwinGame.ProcessEvents();

		_Animation = SwinGame.LoadBitmap(SwinGame.PathToResource("SwinGameAni.jpg", ResourceKind.BitmapResource));
		_LoadingFont = SwinGame.LoadFont(SwinGame.PathToResource("arial.ttf", ResourceKind.FontResource), 12);
		_StartSound = Audio.LoadSoundEffect(SwinGame.PathToResource("SwinGameStart.ogg", ResourceKind.SoundResource));

		_LoaderFull = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_full.png", ResourceKind.BitmapResource));
		_LoaderEmpty = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_empty.png", ResourceKind.BitmapResource));

		PlaySwinGameIntro();
	}
  
    /// <summary>
	/// To show the intro animation before the game start.
	/// </summary>

	private static void PlaySwinGameIntro()
	{
		const int ANI_X = 143;
		const int ANI_Y = 134;
		const int ANI_W = 546;
		const int ANI_H = 327;
		const int ANI_V_CELL_COUNT = 6;
		const int ANI_CELL_COUNT = 11;

		Audio.PlaySoundEffect(_StartSound);
		SwinGame.Delay(200);

		int i = 0;
		for (i = 0; i <= ANI_CELL_COUNT - 1; i++) {
			SwinGame.DrawBitmap(_Background, 0, 0);
			SwinGame.DrawBitmapPart(_Animation, (i / ANI_V_CELL_COUNT) * ANI_W, (i % ANI_V_CELL_COUNT) * ANI_H, ANI_W, ANI_H, ANI_X, ANI_Y);
			SwinGame.Delay(20);
			SwinGame.RefreshScreen();
			SwinGame.ProcessEvents();
		}

		SwinGame.Delay(1500);

	}

    /// <summary>
	/// To show the messages.
	/// </summary>
	/// <param name="message">the message text</param>
	/// <param name="number">the message word number</param>
  
	private static void ShowMessage(string message, int number)
	{
		const int TX = 310;
		const int TY = 493;
		const int TW = 200;
		const int TH = 25;
		const int STEPS = 5;
		const int BG_X = 279;
		const int BG_Y = 453;

		int fullW = 0;

		fullW = 260 * number / STEPS;
		SwinGame.DrawBitmap(_LoaderEmpty, BG_X, BG_Y);
		SwinGame.DrawBitmapPart(_LoaderFull, 0, 0, fullW, 66, BG_X, BG_Y);

		SwinGame.DrawTextLines(message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, TX, TY, TW, TH);

		SwinGame.RefreshScreen();
		SwinGame.ProcessEvents();
	}

    /// <summary>
	/// To show the game ending loading screen and set the screen size.
	/// </summary>
	/// <param name="width">the new screen width size</param>
	/// <param name="height">the new screen height size</param>
  
	private static void EndLoadingScreen(int width, int height)
	{
		SwinGame.ProcessEvents();
		SwinGame.Delay(500);
		SwinGame.ClearScreen();
		SwinGame.RefreshScreen();
		SwinGame.FreeFont(_LoadingFont);
		SwinGame.FreeBitmap(_Background);
		SwinGame.FreeBitmap(_Animation);
		SwinGame.FreeBitmap(_LoaderEmpty);
		SwinGame.FreeBitmap(_LoaderFull);
        //Audio.FreeSoundEffect(_StartSound);
		SwinGame.ChangeScreenSize(width, height);
	}

    /// <summary>
	/// Load and add a new font to the game.
	/// </summary>
	/// <param name="fontName">new name for the font on the game</param>
	/// <param name="filename">the font's file name</param>
	/// <param name="size">the size of the font</param>
  
	private static void NewFont(string fontName, string filename, int size)
	{
		_Fonts.Add(fontName, SwinGame.LoadFont(SwinGame.PathToResource(filename, ResourceKind.FontResource), size));
	}

    /// <summary>
	/// Load and add a new bitmap image to the game.
	/// </summary>
	/// <param name="imageName">new name for the image on the game</param>
	/// <param name="fileName">the image's file name</param>
  
	private static void NewImage(string imageName, string filename)
	{
		_Images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(filename, ResourceKind.BitmapResource)));
	}

    /// <summary>
	/// Load and add a new transparent image to the game.
	/// </summary>
	/// <param name="imageName">new name for the image on the game</param>
	/// <param name="fileName">the image's file name</param>
	/// <param name="transColor">the image's transparent color</param>
  
	private static void NewTransparentColorImage(string imageName, string fileName, Color transColor)
	{
		_Images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(fileName, ResourceKind.BitmapResource), true, transColor));
	}

    /// <summary>
	/// Load and add a new transparent image to the game.
	/// </summary>
	/// <param name="imageName">new name for the image on the game</param>
	/// <param name="fileName">the image's file name</param>
	/// <param name="transColor">the image's transparent color</param>
  
	private static void NewTransparentColourImage(string imageName, string fileName, Color transColor)
	{
		NewTransparentColorImage(imageName, fileName, transColor);
	}

    /// <summary>
	/// Load and add a new sound to the game.
	/// </summary>
	/// <param name="soundName">new name for the sound on the game</param>
	/// <param name="fileName">the sound's file name</param>
  
	private static void NewSound(string soundName, string filename)
	{
		_Sounds.Add(soundName, Audio.LoadSoundEffect(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
	}

    /// <summary>
	/// Load and add a new music to the game.
	/// </summary>
	/// <param name="musicName">new name for the music on the game</param>
	/// <param name="fileName">the music's file name</param>
  
	private static void NewMusic(string musicName, string filename)
	{
		_Music.Add(musicName, Audio.LoadMusic(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
	}
  
    /// <summary>
	/// Load and add default fonts.
	/// </summary>
  
	private static void FreeFonts()
	{
		Font obj2 = default(Font);
		foreach (Font obj in _Fonts.Values) {
			if (obj == obj2)
				SwinGame.FreeFont(obj);
		}
	}
  
    /// <summary>
	/// Load and add default images.
	/// </summary>
  
	private static void FreeImages()
	{
		Bitmap obj2 = default(Bitmap);
		foreach (Bitmap obj in _Images.Values) {
			if (obj == obj2)
				SwinGame.FreeBitmap(obj);
		}
	}
  
    /// <summary>
	/// Load and add default sounds.
	/// </summary>
  
	private static void FreeSounds()
	{
		SoundEffect obj2 = default(SoundEffect);
		foreach (SoundEffect obj in _Sounds.Values) {
			if (obj == obj2)
				Audio.FreeSoundEffect(obj);
		}
	}
  
    /// <summary>
	/// Load and add default musics.
	/// </summary>
  
	private static void FreeMusic()
	{
		Music obj2 = default(Music);
		foreach (Music obj in _Music.Values) {
			if (obj == obj2)
				Audio.FreeMusic(obj);
		}
	}
  
    /// <summary>
	/// Run the modules for the default resources.
	/// </summary>
  
	public static void FreeResources()
	{
		FreeFonts();
		FreeImages();
		FreeMusic();
		FreeSounds();
		SwinGame.ProcessEvents();
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
