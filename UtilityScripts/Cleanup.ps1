Clear-Host

Write-Output("Repo Cleanup Script by Colby Prince")
Write-Output("This script still in development")
Write-Output("")

:Tilde_Files
Write-Output("Removing Tilde Files...")
$FileNo = 0
$Files = [System.Collections.ArrayList]::new()
Get-ChildItem -Recurse | ForEach-Object{
    if($_.Name.contains("~")){
        $FileNo ++; $Files.Add($_.FullName); Remove-Item $_.FullName
    }
}
Write-Output("Removed " + $FileNo.ToString() + " Tilde Files")
Write-Output("Files Removed:")
Write-output($Files.ToArray())

Write-Output("")

Pause