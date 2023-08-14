; -- CPWizard.iss --

[Setup]
AppName=CPWizard
AppVerName=CPWizard 2.69
AppPublisher=Headsoft
DefaultDirName={sd}\CPWizard
DefaultGroupName=CPWizard
UninstallDisplayIcon={app}\CPWizard.exe
Compression=lzma
SolidCompression=yes
OutputDir=Setup
OutputBaseFilename=CPWizardSetup
WizardImageFile=WizardImage.bmp
WizardSmallImageFile=WizardSmallImage.bmp

[Files]
Source: "CPWizard.exe"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "CPWizard.chm"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "net.exe";  DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "MAME32.dll"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "MAME64.dll"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "HiToText.exe"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "HiToText.xml"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "ICSharpCode.SharpZipLib.dll"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "SlimDX.dll"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "..\..\ReadMe.md"; DestDir: "{app}"; Flags: isreadme; CopyMode: alwaysoverwrite
Source: "Media\*"; DestDir: "{app}\Media";   Flags: recursesubdirs createallsubdirs
Source: "Data\*"; DestDir: "{app}\Data"; Flags: recursesubdirs createallsubdirs
Source: "Layout\*"; DestDir: "{app}\Layout"; Flags: recursesubdirs createallsubdirs

[UninstallDelete]
Type: files; Name: "{app}\CPWizard.exe"
Type: files; Name: "{app}\CPWizard.ini"
Type: files; Name: "{app}\CPWizard.chm"
Type: files; Name: "{app}\net.exe"
Type: files; Name: "{app}\MAME32.dll"
Type: files; Name: "{app}\MAME64.dll"
Type: files; Name: "{app}\ICSharpCode.SharpZipLib.dll"
Type: files; Name: "{app}\SlimDX.dll"
Type: files; Name: "{app}\ReadMe.txt"
Type: files; Name: "{app}\CPWizard.log"
Type: filesandordirs; Name: "{app}\Media\*"
Type: filesandordirs; Name: "{app}\Data\*"
Type: filesandordirs; Name: "{app}\Layout\*"

[Registry]
;Root: HKCU; Subkey: "Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"; ValueType: string; ValueName: "{app}\CPWizard.exe"; ValueData: RUNASADMIN; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run\CPWizard"; Flags: uninsdeletekey noerror
Root: HKLM; Subkey: "Software\GPL Ghostscript\8.54"; ValueType: string; ValueName: GS_DLL; ValueData: "{pf}\gs\gs\bin\gsdll32.dll";
Root: HKLM; Subkey: "Software\GPL Ghostscript\8.54"; ValueType: string; ValueName: GS_LIB; ValueData: "{pf}\gs\gs\lib;{pf}\gs\fonts;{pf}\gs\gs\Resource";

[Code]
function InitializeSetup(): Boolean;
var
  NetFrameWorkInstalled : Boolean;
  ErrorCode: Integer;
Begin
  NetFrameWorkInstalled := RegKeyExists(HKLM,'SOFTWARE\Microsoft\.NETFramework\policy\v2.0');

  if NetFrameWorkInstalled = false then
  begin
    ExtractTemporaryFile('net.exe');
    ShellExec('open', ExpandConstant( '{tmp}\net.exe'),'','',SW_SHOWNORMAL, ewNoWait, ErrorCode);
  End;
  Result := true;
end;

[Icons]
Name: "{group}\CPWizard"; Filename: "{app}\CPWizard.exe"
Name: "{group}\Uninstall CPWizard"; Filename: "{app}\unins000.exe"
