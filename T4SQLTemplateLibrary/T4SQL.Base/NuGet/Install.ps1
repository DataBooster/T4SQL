param($installPath, $toolsPath, $package, $project)

Get-ChildItem Registry::HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\ |
	Where-Object {$_.PSChildName -match "^\d{2,}\.\d+$"} |
	Get-ItemProperty -Name "UserItemTemplatesLocation" |
	ForEach-Object {
		Copy-Item ([System.IO.Path]::Combine($installPath, "Visual Studio\Templates\ItemTemplates\*")) $_.UserItemTemplatesLocation -Recurse
	}
    