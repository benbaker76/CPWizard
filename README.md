# CPWizard

## Description

CPWizard is a control panel editor, control panel viewer (controls.dat), game info (listinfo.xml/Catver.ini/nplayers.ini/HallOfFame.xml), game history (history.dat), mame info (mameinfo.dat), special moves (command.dat) and hiscore (story.dat) viewer, manual viewer (pdf), mame artwork viewer (cabinet, control panel, flyers, marquees, snaps, titles, character select, artwork previews and pcb's), emulator artwork viewer (manuals, nfo, operation cards, snaps, titles, carts and boxes) and IRC client will full color and smilie support. It can export CP's to MAME's bezel artwork format to view your controls while inside MAME.

CPWizard is designed to run in the Icon Tray. When you minimize CPWizard it will go to the Icon Tray. You can run MAME and press the pause key (default `p`) and the CPWizard will show it's Menu system.

NOTE: When using MAME 0.112u1 or less the default key to show the Menu is `L`.

You can make CPWizard Run on Startup by selecting Edit > Options > General tab and put a tick next to `Run on Startup`.

With the File > Export Batch feature you can export CP's for your entire MAME collection as bezel's to place in MAME's artwork folder. When you play a game in MAME you can go to the Video Options menu and select Show CP or Hide CP. You can also apply the bezel_0121.zip patch so pausing MAME will show your CP. You don't need CPWizard running to use this feature. You can also export your CP's as png, and export Game Info cards for viewing in your own FE.

CPWizard can run in standalone or resident mode. If you send CPWizard command line args while it is running in the Icon Tray it will process them and remain open (resident mode). If you send it command line args without CPWizard running in the Icon Tray it will process them then exit (standalone mode). If you run CPWizard in standalone mode it is recommended you Enable the Dynamic Data Loading option under Edit > Options > General.

## Features

- Batch export CP or Game Info as Images or bezels for viewing in MAME or FE
- Keyboard or Joystick controlled menu system
- Support for MAME, PC games & other emulators
- Database support for GoodName named ROMs (Category, Developer, NumPlayers, Date and Description info)
- Advanced MAME control mapping (supports ini, ctrlr and cfg custom control entries)
- Detailed MAME game information (including version added, number of players, game status, CPU/audio chips, display info and MAWS Hall of Fame ratings).
- History.dat, MAMEInfo.dat, Command.dat and Story.dat (MAME scores) support
- Auto detects when data files or MAME is updated and auto updates data upon launching
- MAME artwork display (including cabinet, control panel, flyers, marquees, snaps, titles, character select, artwork previews and pcb's)
- Emulator artwork display (including manuals, operation cards, snaps, titles, carts and boxes)
- Manual viewer (pdf support with GhostScript installed)
- IRC Chat will full color and smilie support
- Multiple layout and control support
- Lots of arcade controls, controllers and logo graphics included
- Default arcade control panel layouts (including HotRod, X-Arcade, Mahjong and Lightgun)
- Fully featured control panel editor
- Support for colors.ini (Buttons colors to match real CP's)
- Alpha faded controls that are not used
- Multiple monitor support
- Screen rotation support (90, 180 or 270)
- Label grouping support
- Global volume control

## Command Line Options

`USAGE: CPWizard.exe <options>`

Most Command Line Options are not necessary to run CPWizard in resident mode (running in the icon tray). Most things are done automatically. The only option necessary is -game <name> for emulators other than MAME to tell it which game your running. The other options are for launching CPWizard in standalone mode (Run then exit) or in resident mode (icon tray) to show the CP before launching a game or to run in a single mode (-mode option) such as IRC.

- `-minimized`

Run Minimized

- `-emu [mame|<name>]`

Set mame or <name>. This option is only required when launching CPWizard as standalone or before launching an emu. If you are running CPWizard in resident mode the emulator is detected automatically using it's exe name or it's Window/Class names. To add Emulators go to Edit > Options > Emulators.

- `-game <name>`

Sets the game name. For MAME the game name is detected automatically and you don't need to send this option. For other emulators this option is required.

- `-mode [mainmenu|layout|gameinfo|gamehistory|mameinfo|gamecontrols|hiscore|artwork|manual|irc]`

Sets the display mode if you only want to show specific information.

- `-layout <name>`

Load layout. This is only required to override an existing layout for an Emulator. Layouts are assigned to Emulators in Edit > Options > MAME/Emulators. This is not usually required as you can use the "Layout Override" for showing layouts for specific games.

- `-show`

Show display

- `-timeout <milliseconds>`

Show display for <milliseconds>

## Command Line Examples

- `CPWizard.exe -emu mame -game 1942 -timeout 5000`

Show the MAME layout for 1942 then exit after 5 seconds

- `CPWizard.exe -emu "Project 64" -game "Mario Kart 64" -timeout 5000`

Show the layout for Mario Kart 64 using the Project 64 profile for 5 seconds then exit

- `CPWizard.exe -mode irc -show`

Show the IRC client

## Recommended System Requirements

- 1 Ghz Processor
- 512 MB of RAM
- Windows XP / Vista
- .NET Framework 2.0

## Artwork

You can get artwork like PCB's, Artwork Preview, Character Select etc.

- http://www.progettoemma.net/dany69/

## Data Files

- CatVer.ini - http://www.progettoemma.net/history/catlist.php
- Command.Dat - http://home.comcast.net/~plotor/command.html
- Controls.Dat - http://fe.donkeyfly.com/controls/controls.php
- History.Dat - http://www.arcade-history.com/
- MAMEInfo.Dat - http://www.mameworld.net/mameinfo/
- NPlayers.ini - http://nplayers.arcadebelgium.be/
- Story.Dat - http://www.arcadehits.net/mamescore/home.php?show=files

## Screenshots

![](/images/cpw01.png)

![](/images/cpw02.png)

![](/images/cpw03.png)

![](/images/cpw04.png)

![](/images/cpw05.png)

![](/images/cpw06.png)

![](/images/cpw07.png)

![](/images/cpw08.png)

![](/images/cpwnew01.png)

![](/images/cpwnew02.png)

![](/images/cpwnew03.png)

![](/images/cpwnew04.png)

![](/images/cpwnew05.png)

## Version History

- 11-2-18 - v2.65 - Fixed input mapping of games without Controls.dat
- 2-2-18 - v2.64 - MAME Input changes
- 30-5-17 - v2.63 - Added MAME Filter options
- 30-5-17 - v2.62 - Bug fixes
- 27-5-17 - v2.61 - Added menu options (stigzler)
- 16-2-17 - v2.60 - Added MAME filtering
- 9-1-17 - v2.51 - Fixed event draw accumulation bug (thanks Stigzler!)
- 2-1-17 - v2.50 - Fixed -show command line option. Moved from MDX to SlimDX
- 23-9-16 - v2.42 - Added Show Layout options
- 9-9-15 - v2.41 - Fix MAME xml parsing
- 03-12-13 - v2.40 - Fix MAME xml parsing
- 25-03-13 - v2.39 - Data files update
- 28-09-11 - v2.38 - Fix for player 2 display and .c source file's in ctrlr files
- 15-07-11 - v2.37 - Parent display for MAMEInfo, StoryDat and others
- 31-03-11 - v2.36 - Export a single layout
- 01-07-10 - v2.35 - Fixed wrong detection of invalid xml files
- 29-03-10 - v2.34 - Fixed sub display not changing for different games
- 27-03-10 - v2.33 - Fixed alternating controls display
- 13-02-10 - v2.32 - Added FlipX and FlipY options
- 26-10-09 - v2.31 - Added suppport for Layout Sub display within a layout
- 19-10-09 - v2.30 - Added Support for Multi-Layouts, Sub Screen Display, Auto Show and Advanced Layouts
- 19-06-09 - v2.21 - Fixed bug in analog to digital mapping system
- 22-05-09 - v2.20 - Bug fixes to input control mapping system
- 15-05-09 - v2.19 - Added new pause method and options. Added -exit and -rotate command line options.
- 09-05-09 - v2.18 - Auto rotation option added to display options
- 14-04-09 - v2.17 - Joystick/Key repeat improved. No wrap around for text display.
- 09-04-09 - v2.16 - Joystick hotswap support and joystick repeat added
- 08-04-09 - v2.15 - Fixed preview list bug for older versions of MAME (tested on MAME 0.104)
- 04-02-09 - v2.14 - Fixed bugs introduced with new IPC system
- 30-01-09 - v2.13 - Changed IPC to use WM_COPYDATA
- 29-01-09 - v2.12 - Bug fixes. Improved Show/Hide.
- 28-01-09 - v2.11 - Added scrollbars to the text viewer, menus now loop around
- 27-01-09 - v2.1 - Analog inputs now supported. MAME ROM name now lower case. Added Show Retry. Improved Show/Hide
- 03-01-09 - v2.04 - Added support for multiple Label Links with the same input code
- 03-01-09 - v2.03 - Fixed joystick and mouse codes
- 01-01-09 - v2.02 - Added HiToText Support (Thanks to Fyrecrypts, NOP & HiToText Team)
- 01-01-09 - v2.01 - Removed the selection bar from screens that don't need it. Should be alot easier to scroll through text using a joystick.
- 01-01-09 - v2.0 - Supports Command.Dat Longhand format, scroll text by holding joy down, page skips fullscreen, added color coded text for command.dat, new MAME look
- 04-12-08 - v1.96 - Loading screens now turn off TopMost property. Added option "Show Loading Screens"
- 10-11-08 - v1.95 - Fixed exit issue with MAME.dll
- 07-11-08 - v1.94 - Fixed threading issue with MAME.dll
- 03-11-08 - v1.93 - Improved joystick handling
- 10-10-08 - v1.92 - Fixed listview bug
- 01-10-08 - v1.91 - Placed input codes into their own files (in Data\InputCodes). Fixed broken Dynamic Data Loading
- 20-09-08 - v1.90 - Now supports dual monitor games and resolution change. Added support for source, cloneof, romof & parent to MAME's layout override. Fixed Export Bezel option not saving
- 16-09-08 - v1.89 - Added object coordinates to the status bar
- 11-09-08 - v1.88 - Bezel options are now saved
- 07-09-08 - v1.87 - Fixed bug introduced from previous release
- 01-09-08 - v1.86 - If Command.Dat info is not available for the game it will try the parent ROM
- 01-09-08 - v1.85 - Secondary controls for alternating games now won't light
- 18-07-08 - v1.84 - Added dialog to enter keycode or joystick input when creating labels
- 18-07-08 - v1.83 - Fixed export bezel progress bar, added vertical for 90 degree rotation
- 14-07-08 - v1.82 - Added support for multiple resolutions and skip clones in export bezels.
- 03-07-08 - v1.80 - Added support for vertical bezels in export bezels
- 01-07-08 - v1.79 - Fixed MAME.dll memory leak
- 01-06-08 - v1.78 - Fixed input with U360's, fixed minimizing when starting from GameEx, added support for increment/decrement input mappings.
- 25-05-08 - v1.77 - Fixed Command.Dat bug
- 12-05-08 - v1.76 - Added verbose logging, exit key added, default keycodes use default, X placeholder not rendered, custom text can be added to player codes.
- 14-04-08 - v1.74 - Added window to hide minimizing MAME. Updated data files.
- 13-04-08 - v1.73 - Added support for patch that will unpause MAME when exiting CPWizard. Includes the bezel patch. Fixed Alternating not working in Layout Maps.
- 12-04-08 - v1.72 - Viewer will now work without minimizing. Enhanced Command.Dat viewer. Added MiniInfo to Layout display.
- 10-04-08 - v1.71 - Fixed major bug from last release. Added Exit To Menu option in Data options. Improved preview window. Preview window remembers last game. Added buttons to change order of Layout Maps.
- 08-04-08 - v1.70 - Improved input mapping, enhanced preview window, changed keywords to Layout Maps and added Constants, NumPlayers and Alternating options, fixed moving objects with cursor keys, -exitmenu option added, Player Codes added for alpha fading joysticks and start and coin buttons.
- 30-03-08 - v1.68 - Added support for multiple labels with the same input codes.
- 30-03-08 - v1.67 - Fixed bug using Dynamic Data Loading in standalone mode. Added zoom for PDF viewer.
- 12-03-08 - v1.66 - Fixed bug using layout overrides (Tempest)
- 28-02-08 - v1.65 - Added drop down list for Key Words
- 22-02-08 - v1.64 - Fixed bug when exporting bezels (Kenpachi)
- 19-02-08 - v1.63 - Added help. Big thanks to Matt McLemore (Tempest)
- 18-02-08 - v1.62 - Changed Label Link in Display Options to Label Arrow to avoid confusion with Label Links
- 05-02-08 - v1.61 - Fixed info disappearing when not using the MAME Output system
- 04-02-08 - v1.6 - Fixed mainmenu command line option not exiting back to menu
- 01-02-08 - v1.59 - Added Color values for Label Links and Label Spots
- 01-02-08 - v1.58 - Added Alpha Fade enable and Alpha Fade value
- 14-01-08 - v1.57 - Fixed shutting down PC showing Prompt To Exit message (Lakersfan)
- 14-01-08 - v1.56 - Multiple object select/move/edit/cut/copy/paste. You can select multiple objects using the mouse to draw a selection or hold shift while you select objects. You can now edit object properties of multiple objects at once.
- 12-01-08 - v1.55 - Added prompt to exit, can now edit most options, major graphics enhancement by Nologic (thanks mate!)
- 06-01-08 - v1.53 - Updated graphics
- 06-01-08 - v1.52 - Fixed Control Info offset bug
- 05-01-08 - v1.51 - Fixed bug if you don't select MAME exe at startup (Nologic)
- 05-01-08 - v1.5 - Big update! Improved mapping accuracy (alot). Prompt to save added.
- 30-12-07 - v1.41 - Fixed bug when using MAME 0104 (Dustin Mustangs). Auto update bug fix
- 29-12-07 - v1.40 - Fixed label copy bug. Added "Lightning" layout
- 23-12-07 - v1.37 - Added "Layout Size" to Export Batch
- 20-12-07 - v1.36 - Updated data files to MAME 0122
- 19-12-07 - v1.34 - Fixed text align bug
- 17-12-07 - v1.3 - Full PC Game support. Fixed InterComm.dll problems some people were having
- 14-12-07 - v1.2 - Added command line options, Show CP Only & Back Key will Exit Menu option. You can also disable any menu options.
- 11-12-07 - v1.1 - Added Keyboard layout, Label Link arrow, Show Delay and CP Editor shortcuts
- 08-12-07 - v1.09 - Added ability to include a standard bezel in the Export Batch bezel feature
- 02-12-07 - v1.08 - Added Exporting for Bezels
- 01-12-07 - v1.07 - Improved label mapping and grouping.
- 30-11-07 - v1.06 - Added Preview, Export Batch. Added Color Image and Keycode mappings to Options. Added Page Up/Down input.
- 28-11-07 - v1.05 - Bug fixes
- 25-11-07 - v1.04 - Joystick handling bug fix
- 24-11-07 - v1.03 - Fixed joystick detection bug, GS path bug, added DirectInput dll to setup
- 23-11-07 - v1.02 - Improved MAME input mapping
- 24-09-07 - v1.01 - Added dynamic data loading option. Memory consumption is greatly lowered with a minor compromise of data load times.
- 14-09-07 - v1.00 - Added Joystick input / multiple input
- 09-09-07 - v0.99 - Added NFO Viewer. Improved input handling
- 06-09-07 - v0.98 - Bug fixes (of course). Added support for emulator artwork & pdf's.
- 03-09-07 - v0.97 - Bug fixes. Can now use full keyboard in IRC. Name change.
- 02-09-07 - v0.96 - Bug fixes. Added colors.ini support
- 28-08-07 - v0.95 - Bug fixes. Added error logging. Add support for Artwork Previews and Character Select screen artwork
- 27-08-07 - v0.94 - Bug fixes
- 26-08-07 - v0.93 - First private beta release
- 25-08-07 - v0.92 - Added Emulator maps
- 24-08-07 - v0.91 - Added multiple keycode support, label groups, goodname database support, label files

## Thanks

- Thanks to stigzler for adding many extra cool features.
- Thanks to Howard Casto, SirPoonga, Frostillicus, u_rebelscum and thanks to RandyT for giving me permission to use his CP graphics, and to Jonathan Firth (XPAdder) for permission to use his controller graphics and also thanks to Aaron Giles and the MAME team.
- Thanks to Fyrecrypts, NOP & redhorse for HiToText
- Thanks to TheShanMan for Atari button & Marak for Sanwa JLW joystick graphics
- Thanks to Matt McLemore (Tempest) for the documentation.
- Also thanks to sbaby for testing
