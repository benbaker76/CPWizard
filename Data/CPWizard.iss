; -- CPWizard.iss --

#define MyAppVersion ""

[Setup]
AppName=CPWizard
AppVerName=CPWizard {#MyAppVersion}
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
Source: "*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs
Source: "README.md"; DestDir: "{app}"; Flags: isreadme; CopyMode: alwaysoverwrite

[UninstallDelete]
Type: filesandordirs; Name: "{app}\*"

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
