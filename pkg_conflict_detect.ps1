$dir = "C:\Users\csont\source\repos\ORM_Core"
$nugets = Get-ChildItem -Path $dir -Directory | ?{ $_.PSIsContainer } | ForEach-Object { $_.Name }
$nCount = $nugets.Count

Write-Host "Found $nCount nuggets in '$dir' direcotry"

$nugetObjects = $nugets | %{ [pscustomobject]@{ Name = $_ -replace '\.([0-9]).*([0-9])$'; Version = $_ -replace '^([A-Za-z]).*([A-Za-z])\.' }} | Group-Object "Name"

$conflicts = $nugetObjects | Where-Object {$_.Count -gt 1}
if ($conflicts.Count -gt 0) {
    Write-Host "Found Nuget multiuple versions"
    $conflicts
}
else {
    Write-Host "Jey - Not found any Nuget version conflicts"
}