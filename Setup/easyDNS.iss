; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "EasyDNS"
#define MyAppVersion "1.0"
#define MyAppPublisher "theDiary Software"
#define MyAppExeName "EasyDNS.exe"
#define MyAppServiceExeName "EasyDNSService.exe"
#define ServiceName "EasyDNSListener"
#define ServiceDisplayName "EasyDNS Listener"
[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{01C5C1FE-62E8-4FEA-B523-90026BDD7467}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputBaseFilename="EasyDNS Setup"
SetupIconFile=..\Resources\easyDNS.ico
UninstallDisplayIcon=..\Resources\easyDNS.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "EasyDNS.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "EasyDNSService.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[run]
Filename: {sys}\sc.exe; Parameters: "create {#ServiceName} start= auto binPath= ""{app}\{#MyAppServiceExeName}"" DisplayName= ""{#ServiceDisplayName}""" ; StatusMsg: "Installing EasyDNS Service"; Flags: runhidden
Filename: {sys}\net.exe; Parameters: "start {#ServiceName}";  StatusMsg: "Starting EasyDNS Service"; Flags: runhidden
[UninstallRun]
Filename: {sys}\sc.exe; Parameters: "stop EasyDNSListener" ; Flags: runhidden
Filename: {sys}\sc.exe; Parameters: "delete EasyDNSListener" ; Flags: runhidden