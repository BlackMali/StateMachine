Set-ExecutionPolicy Bypass -Scope Process

$folders = Get-ChildItem -recurse -directory | Where-Object {$_.Name -match "obj" -or $_.Name -match "bin"}

foreach($folder in $folders)
{
    Write-Host $folder.FullName
    Remove-Item -Recurse -Force $folder.FullName
}