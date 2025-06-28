[Files]
Source: isxdl.dll; DestDir: {tmp}; Flags: dontcopy

[Icons]

[Setup]
AppName=NET Framework 2.0
DefaultDirName={pf}\CPWizard
DefaultGroupName=CPWizard
ShowLanguageDialog=no
AppCopyright=
AppVerName=CPWizard .NET 2.0 Install
SetupIconFile=
OutputBaseFilename=net
AllowUNCPath=false
MinVersion=4.1.2222,5.0.2195
AllowCancelDuringInstall=false
UsePreviousTasks=false
RestartIfNeededByRun=false
InternalCompressLevel=max
SolidCompression=true
Compression=lzma
WizardSmallImageFile=WizardSmallImage.bmp
WizardImageFile=WizardImage.bmp
WizardImageStretch=false
WizardImageBackColor=clWhite
ExtraDiskSpaceRequired=20000
DisableReadyPage=false
CreateAppDir=false
DisableProgramGroupPage=true
UsePreviousGroup=false
DisableFinishedPage=true
DisableStartupPrompt=true
DisableReadyMemo=false
OutputDir=.

[Run]
Filename: {tmp}\net.exe; WorkingDir: {tmp}; Flags: nowait skipifdoesntexist

[Code]
function isxdl_Download(hWnd: Integer; URL, Filename: PChar): Integer;
external 'isxdl_Download@files:isxdl.dll stdcall';

procedure isxdl_AddFile(URL, Filename: PChar);
external 'isxdl_AddFile@files:isxdl.dll stdcall';

procedure isxdl_AddFileSize(URL, Filename: PChar; Size: Cardinal);
external 'isxdl_AddFileSize@files:isxdl.dll stdcall';

function isxdl_DownloadFiles(hWnd: Integer): Integer;
external 'isxdl_DownloadFiles@files:isxdl.dll stdcall';

procedure isxdl_ClearFiles;
external 'isxdl_ClearFiles@files:isxdl.dll stdcall';

function isxdl_IsConnected: Integer;
external 'isxdl_IsConnected@files:isxdl.dll stdcall';

function isxdl_SetOption(Option, Value: PChar): Integer;
external 'isxdl_SetOption@files:isxdl.dll stdcall';

function isxdl_GetFileName(URL: PChar): PChar;
external 'isxdl_GetFileName@files:isxdl.dll stdcall';

const neturl = 'http://www.tomspeirs.com/dotnetfx.exe';

function NextButtonClick(CurPage: Integer): Boolean;
var
  tmp: String;
  hWnd: Integer;
begin
  tmp := ExpandConstant('{tmp}\net.exe');
  hWnd := StrToInt(ExpandConstant('{wizardhwnd}'));
  isxdl_SetOption('title', 'Downloading dotnetfx.exe');
  if isxdl_Download(hWnd, neturl, tmp) <> 0 then
  begin
    Result := true;
  end;
end;

[CustomMessages]
dx=ewewweew
[Messages]
WelcomeLabel2=In order to install CPWizard, you must have [name] or above installed on your computer. CPWizard setup has detected that it is not present. To download and install [name] please continue. Once complete please re-run CPWizard setup. Note: [name] may require you to reboot your computer.
WelcomeLabel1=CPWizard requires that [name] is installed on your computer. Once installed you will need to re-run CPWizard setup.
