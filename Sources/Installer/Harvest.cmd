"c:\Program Files (x86)\WiX Toolset v3.10\bin\heat.exe" dir ..\Platform\BrainSimulator\bin\Release\modules\ -sfrag -sreg -ag -out HarvestedModules.wxs -t HeatFilter.xslt -dr INSTALLFOLDER -cg HarvestedModules -var wix.ModulesSourceDirectory