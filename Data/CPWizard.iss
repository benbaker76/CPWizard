; -- CPWizard.iss --

#define AppName "CPWizard"

[Setup]
AppName=CPWizard
AppVerName=CPWizard {#AppVersion}
AppPublisher=Ben Baker
DefaultDirName={autopf}\CPWizard
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
Source: "CPWizard.runtimeconfig.json"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "CPWizard.chm"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "net.exe";  DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "*.dll"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "HiToText.exe"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "HiToText.xml"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "README.md"; DestDir: "{app}"; Flags: isreadme; CopyMode: alwaysoverwrite

Source: "Media\*"; DestDir: "{localappdata}\{#AppName}\Media";   Flags: recursesubdirs createallsubdirs
Source: "Data\*"; DestDir: "{localappdata}\{#AppName}\Data"; Flags: recursesubdirs createallsubdirs
Source: "Layout\*"; DestDir: "{localappdata}\{#AppName}\Layout"; Flags: recursesubdirs createallsubdirs

[UninstallDelete]
Type: files; Name: "{app}\CPWizard.exe"
Type: files; Name: "{app}\CPWizard.runtimeconfig.json"
Type: files; Name: "{app}\CPWizard.ini"
Type: files; Name: "{app}\CPWizard.chm"
Type: files; Name: "{app}\net.exe"
Type: files; Name: "{app}\*.dll"
Type: files; Name: "{app}\README.md"
Type: files; Name: "{app}\CPWizard.log"
Type: filesandordirs; Name: "{localappdata}\{#AppName}\Media\*"
Type: filesandordirs; Name: "{localappdata}\{#AppName}\Data\*"
Type: filesandordirs; Name: "{localappdata}\{#AppName}\Layout\*"

[Registry]
;Root: HKCU; Subkey: "Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"; ValueType: string; ValueName: "{app}\CPWizard.exe"; ValueData: RUNASADMIN; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run\CPWizard"; Flags: uninsdeletekey noerror
Root: HKLM; Subkey: "Software\GPL Ghostscript\8.54"; ValueType: string; ValueName: GS_DLL; ValueData: "{autopf}\gs\gs\bin\gsdll32.dll";
Root: HKLM; Subkey: "Software\GPL Ghostscript\8.54"; ValueType: string; ValueName: GS_LIB; ValueData: "{autopf}\gs\gs\lib;{autopf}\gs\fonts;{autopf}\gs\gs\Resource";

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
